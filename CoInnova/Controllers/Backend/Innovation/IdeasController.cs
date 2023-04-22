using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Innovation
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("innovation")]
    public class IdeasController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Idea, System.Guid, IdeaInputViewModel, IdeaDisplayViewModel>
    {
        private readonly IEntityRepository<IdeaWorkflow, System.Guid> ideaWorkflowRepository;
        private readonly IEntityRepository<Challange, System.Guid> challangeRepository;
        private readonly IEntityRepository<Document, System.Guid> documentRepository;
        private readonly IEntityRepository<Collaborator, System.Guid> collaboratorRepository;
        private readonly Managers.RelativeDocumentFileManager documentManager;

        public IdeasController(Managers.RelativeDocumentFileManager documentManager, IEntityRepository<Collaborator, System.Guid> collaboratorRepository, IEntityRepository<Document, System.Guid> documentRepository, IEntityRepository<Challange, System.Guid> challangeRepository, IEntityRepository<IdeaWorkflow, System.Guid> ideaWorkflowRepository, IEntityRepository<Idea, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<IdeasController> localizer, ILogger<IdeasController> logger) : base(repository, localizer, logger)
        {
            this.ideaWorkflowRepository = ideaWorkflowRepository;
            this.challangeRepository = challangeRepository;
            this.documentRepository = documentRepository;
            this.collaboratorRepository = collaboratorRepository;
            this.documentManager = documentManager;
        }

        [RequirePermissions(Constants.Permission.IdeaViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Permission.IdeaCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.IdeaCreator)]
        public override IActionResult Create(IdeaInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        public override IActionResult Edit(IdeaInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Constants.Permission.IdeaRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.IdeaRemover)]
        public override IActionResult Delete(IdeaDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Idea entity, IdeaInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.WorkflowId != null)
            {
                entity.WorkflowId = modelInput.WorkflowId;
                entity.Workflow = ideaWorkflowRepository.Get(modelInput.WorkflowId.Value);
            }

            if (modelInput.ChallangeId != null)
            {
                entity.ChallangeId = modelInput.ChallangeId;
                entity.Challange = challangeRepository.Get(modelInput.ChallangeId.Value);
            }
        }

        protected override void PreEdit(Idea entity, IdeaInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.WorkflowId != modelInput.WorkflowId)
            {
                if (modelInput.WorkflowId == null)
                {
                    entity.WorkflowId = null;
                    entity.Workflow = null;
                }
                else
                {
                    entity.WorkflowId = modelInput.WorkflowId;
                    entity.Workflow = ideaWorkflowRepository.Get(modelInput.WorkflowId.Value);
                }
            }

            if (entity.ChallangeId != modelInput.ChallangeId)
            {
                if (modelInput.ChallangeId == null)
                {
                    entity.ChallangeId = null;
                    entity.Challange = null;
                }
                else
                {
                    entity.ChallangeId = modelInput.ChallangeId;
                    entity.Challange = challangeRepository.Get(modelInput.ChallangeId.Value);
                }
            }
        }

        protected override void ConfigureCreate(IdeaInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(IdeaInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigureEdit(Idea entity, IdeaInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Idea entity, IdeaInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            Populate(modelInput);
        }

        private void Populate(IdeaInputViewModel model)
        {
            model.AvailableWorkflowSelectListItems = ideaWorkflowRepository.GetAll().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name
            }).ToArray();
            model.AvailableChallangeSelectListItems = challangeRepository.GetAll().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Title
            }).ToArray();
        }

        #region Documents

        [HttpGet]
        [Route("edit/{key}/documents")]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        public ActionResult EditDocuments(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new IdeaDocumentsViewModel
            {
                Idea = new IdeaDisplayViewModel(),
                Documents = entity.DocumentBridges.Select(item =>
                {
                    var result = new Models.DocumentDisplayCommonModel();

                    result.Import(item.Stern);

                    return result;
                }).ToArray()
            };

            model.Idea.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/documents/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        public IActionResult AddDocument(System.Guid key, DocumentInputViewModel model)
        {
            try
            {
                var entity = Repository.Get(key);

                ModelState.ClearValidationState("File");

                model.File = Request.Form.Files[0];

                TryValidateModel(model);

                if (ModelState.IsValid)
                {
                    try
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            model.File.CopyTo(memoryStream);

                            var document = new Document
                            {
                                Name = model.Name,
                                Description = model.Description,
                                ContentType = model.File.ContentType,
                                Length = model.File.Length
                            };

                            documentRepository.Add(document, User.Identity.Name, false);

                            entity.DocumentBridges.Add(new IdeaDocumentBridge
                            {
                                ProwId = entity.Id,
                                SternId = document.Id
                            });

                            documentManager.SaveFileContent(document.Id, memoryStream.ToArray(), document.ContentType);

                            Repository.Update(entity, User.Identity.Name);
                        }

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editdocuments", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editdocuments", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/documents/remove")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        public IActionResult RemoveDocument(System.Guid key, System.Guid documentId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.DocumentBridges.First(b => b.SternId == documentId);

                entity.DocumentBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                try
                {
                    documentManager.DeleteFile(bridge.Stern);
                }
                catch { }

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editdocuments", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editdocuments", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/documents/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        public IActionResult EditDocument(System.Guid key, DocumentInputViewModel modelInput)
        {
            ModelState.ClearValidationState("File");
            ModelState.MarkFieldValid("File");

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = Repository.Get(key);

                    try
                    {
                        var bridge = entity.DocumentBridges.First(b => b.SternId == modelInput.Id);

                        bridge.Stern.Name = modelInput.Name;
                        bridge.Stern.Description = modelInput.Description;

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetEditionSuccessMessage());

                        return RedirectToAction("editdocuments", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }

                    SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                    return RedirectToAction("editdocuments", new { key = entity.Id });
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);
                }
            }

            SetDefaultNotification(notificationType: Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        #endregion

        #region Person Collaborators

        [Route("edit/{key}/collaboration/persons")]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        public IActionResult EditPersonCollaborations(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var availableCollaborators = collaboratorRepository.FindBy(ent1ty => ent1ty.OrganizationIdentityId == null).ToList();

                var includedCollaborators = new System.Collections.Generic.List<Collaborator>();

                foreach (var bridge in entity.CollaboratorBridges)
                {
                    if (bridge.Stern.OrganizationIdentityId != null) continue;

                    var collaborator = availableCollaborators.FirstOrDefault(item => item.Id == bridge.SternId);

                    includedCollaborators.Add(collaborator);

                    availableCollaborators.Remove(collaborator);
                }

                var model = new IdeaCollaboratorsViewModel
                {
                    Idea = new IdeaDisplayViewModel(),
                    Collaborators = includedCollaborators.Select(item =>
                    {
                        var iModel = new CollaboratorDisplayViewModel();

                        iModel.Import(item);

                        return iModel;
                    }).ToArray(),
                    AvailableCollaborators = availableCollaborators.Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = string.IsNullOrEmpty(item.User.Identity.LastName) ? item.User.Identity.Name : string.Format("{0} {1}", item.User.Identity.Name, item.User.Identity.LastName) }).ToArray()
                };

                model.Idea.Import(entity);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/{key}/collaboration/persons/add")]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPersonCollaboration(System.Guid key, System.Guid? collaboratorId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (collaboratorId != null)
                {
                    try
                    {
                        entity.CollaboratorBridges.Add(new IdeaCollaboratorBridge { ProwId = entity.Id, SternId = collaboratorId.Value });

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("EditPersonCollaborations", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                ModelState.AddModelError("collaboratorId", string.Format(localizer["RequiredValidationErrorMessage"], localizer["Collaborator"]));

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("EditPersonCollaborations", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/collaboration/persons/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        public IActionResult DeletePersonCollaboration(System.Guid key, System.Guid collaboratorId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.CollaboratorBridges.First(b => b.SternId == collaboratorId);

                entity.CollaboratorBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("EditPersonCollaborations", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("EditPersonCollaborations", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #region Organization Collaborators

        [Route("edit/{key}/collaboration/organizations")]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        public IActionResult EditOrganizationCollaborations(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var availableCollaborators = collaboratorRepository.FindBy(ent1ty => ent1ty.OrganizationIdentityId != null).ToList();

                var includedCollaborators = new System.Collections.Generic.List<Collaborator>();

                foreach (var bridge in entity.CollaboratorBridges)
                {
                    if (bridge.Stern.OrganizationIdentityId == null) continue;

                    var collaborator = availableCollaborators.First(item => item.Id == bridge.SternId);

                    includedCollaborators.Add(collaborator);

                    availableCollaborators.Remove(collaborator);
                }

                var model = new IdeaCollaboratorsViewModel
                {
                    Idea = new IdeaDisplayViewModel(),
                    Collaborators = includedCollaborators.Select(item =>
                    {
                        var iModel = new CollaboratorDisplayViewModel();

                        iModel.Import(item);

                        return iModel;
                    }).ToArray(),
                    AvailableCollaborators = availableCollaborators.Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = item.OrganizationIdentity.Name }).ToArray()
                };

                model.Idea.Import(entity);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/{key}/collaboration/organizations/add")]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrganizationCollaboration(System.Guid key, System.Guid? collaboratorId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (collaboratorId != null)
                {
                    try
                    {
                        entity.CollaboratorBridges.Add(new IdeaCollaboratorBridge { ProwId = entity.Id, SternId = collaboratorId.Value });

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("EditOrganizationCollaborations", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                ModelState.AddModelError("collaboratorId", string.Format(localizer["RequiredValidationErrorMessage"], localizer["Collaborator"]));

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("EditOrganizationCollaborations", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/collaboration/organizations/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.IdeaEditor)]
        public IActionResult DeleteOrganizationCollaboration(System.Guid key, System.Guid collaboratorId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.CollaboratorBridges.First(b => b.SternId == collaboratorId);

                entity.CollaboratorBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("EditOrganizationCollaborations", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("EditOrganizationCollaborations", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion
    }
}
