using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Models.View.Backend;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Innovation
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("innovation")]
    public class ChallangesController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Challange, System.Guid, ChallangeInputBackendViewModel, ChallangeDisplayViewModel>
    {
        private readonly IEntityRepository<ChallangeWorkflow, System.Guid> challangeWorkflowRepository;
        private readonly IEntityRepository<Document, System.Guid> documentRepository;
        private readonly IEntityRepository<Collaborator, System.Guid> collaboratorRepository;
        private readonly IEntityRepository<ChallangeWorkflowIteration, System.Guid> challangeWorkflowIterationRepository;
        private readonly IEntityRepository<ChallangeWorkflowStep, System.Guid> challangeWorkflowStepRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<InnovationAgent, System.Guid> innovationAgentRepository;
        private readonly Managers.RelativeDocumentFileManager documentManager;
        private readonly IImageFileManagerFacade imageFileManager;

        public ChallangesController(IImageFileManagerFacade imageFileManager, Managers.RelativeDocumentFileManager documentManager, IEntityRepository<InnovationAgent, System.Guid> innovationAgentRepository, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<ChallangeWorkflowStep, System.Guid> challangeWorkflowStepRepository, IEntityRepository<ChallangeWorkflowIteration, System.Guid> challangeWorkflowIterationRepository, IEntityRepository<Collaborator, System.Guid> collaboratorRepository, IEntityRepository<Document, System.Guid> documentRepository, IEntityRepository<ChallangeWorkflow, System.Guid> challangeWorkflowRepository, IEntityRepository<Challange, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ChallangesController> localizer, ILogger<ChallangesController> logger) : base(repository, localizer, logger)
        {
            this.challangeWorkflowRepository = challangeWorkflowRepository;
            this.documentRepository = documentRepository;
            this.collaboratorRepository = collaboratorRepository;
            this.challangeWorkflowIterationRepository = challangeWorkflowIterationRepository;
            this.challangeWorkflowStepRepository = challangeWorkflowStepRepository;
            this.imageRepository = imageRepository;
            this.innovationAgentRepository = innovationAgentRepository;
            this.documentManager = documentManager;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Permission.ChallangeViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Permission.ChallangeCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.ChallangeCreator)]
        public override IActionResult Create(ChallangeInputBackendViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        public override IActionResult Edit(ChallangeInputBackendViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Constants.Permission.ChallangeRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.ChallangeRemover)]
        public override IActionResult Delete(ChallangeDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Challange entity, ChallangeInputBackendViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.InnovationAgentId = modelInput.InnovationAgentId;
            entity.InnovationAgent = innovationAgentRepository.Get(modelInput.InnovationAgentId.Value);

            if (modelInput.WorkflowId != null)
            {
                entity.WorkflowId = modelInput.WorkflowId;
                entity.Workflow = challangeWorkflowRepository.Get(modelInput.WorkflowId.Value);
            }
        }

        protected override void PreEdit(Challange entity, ChallangeInputBackendViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.InnovationAgentId != modelInput.InnovationAgentId)
            {
                entity.InnovationAgentId = modelInput.InnovationAgentId;
                entity.InnovationAgent = innovationAgentRepository.Get(modelInput.InnovationAgentId.Value);
            }

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
                    entity.Workflow = challangeWorkflowRepository.Get(modelInput.WorkflowId.Value);
                }
            }
        }

        protected override void ConfigureCreate(ChallangeInputBackendViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ChallangeInputBackendViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigureEdit(Challange entity, ChallangeInputBackendViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Challange entity, ChallangeInputBackendViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            Populate(modelInput);
        }

        private void Populate(ChallangeInputBackendViewModel model)
        {
            model.AvailableWorkflowSelectListItems = challangeWorkflowRepository.GetAll().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name
            }).ToArray();
            model.AvailableInnovationAgentSelectListItems = innovationAgentRepository.GetAll().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Identity.Name
            }).ToArray();
        }

        #region Documents

        [HttpGet]
        [Route("edit/{key}/documents")]
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
        public ActionResult EditDocuments(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ChallangeDocumentsViewModel
            {
                Challange = new ChallangeDisplayViewModel(),
                Documents = entity.DocumentBridges.Select(item =>
                {
                    var result = new Models.DocumentDisplayCommonModel();

                    result.Import(item.Stern);

                    return result;
                }).ToArray()
            };

            model.Challange.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/documents/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
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

                            entity.DocumentBridges.Add(new ChallangeDocumentBridge
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
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
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
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
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
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
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

                var model = new ChallangeCollaboratorsViewModel
                {
                    Challange = new ChallangeDisplayViewModel(),
                    Collaborators = includedCollaborators.Select(item =>
                    {
                        var iModel = new CollaboratorDisplayViewModel();

                        iModel.Import(item);

                        return iModel;
                    }).ToArray(),
                    AvailableCollaborators = availableCollaborators.Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = string.IsNullOrEmpty(item.User.Identity.LastName) ? item.User.Identity.Name : string.Format("{0} {1}", item.User.Identity.Name, item.User.Identity.LastName) }).ToArray()
                };

                model.Challange.Import(entity);

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
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
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
                        entity.CollaboratorBridges.Add(new ChallangeCollaboratorBridge { ProwId = entity.Id, SternId = collaboratorId.Value });

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
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
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
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
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

                var model = new ChallangeCollaboratorsViewModel
                {
                    Challange = new ChallangeDisplayViewModel(),
                    Collaborators = includedCollaborators.Select(item =>
                    {
                        var iModel = new CollaboratorDisplayViewModel();

                        iModel.Import(item);

                        return iModel;
                    }).ToArray(),
                    AvailableCollaborators = availableCollaborators.Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = item.OrganizationIdentity.Name }).ToArray()
                };

                model.Challange.Import(entity);

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
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
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
                        entity.CollaboratorBridges.Add(new ChallangeCollaboratorBridge { ProwId = entity.Id, SternId = collaboratorId.Value });

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
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
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

        #region WorkflowIterations

        [HttpGet]
        [Route("edit/{key}/workflow/iterations")]
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
        public ActionResult EditWorkflowIterations(System.Guid key)
        {
            var entity = Repository.Get(key);

            if (entity.WorkflowId ==  null)
            {
                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Warning, "Proceso de Innovación", "No se ha especificado el modelo de gestión a utilizar en el proceso de innovación para este reto. Puede especificarlo en este formulario.", null);

                return RedirectToAction("Edit", new { key = entity.Id });
            }

            var model = new ChallangeWorkflowIterationsViewModel
            {
                Challange = new ChallangeDisplayViewModel(),
                WorkflowIterations = challangeWorkflowIterationRepository.FindBy(ent1ty => ent1ty.ChallangeId == entity.Id && ent1ty.WorkflowStep.WorkflowId == entity.WorkflowId).OrderBy(ent1ty => ent1ty.CreatedAt).ToArray().Select(item =>
                {
                    var result = new ChallangeWorkflowIterationDisplayViewModel();

                    result.Import(item);

                    return result;
                }).ToArray()
            };

            var pendingWorkflowSteps = new System.Collections.Generic.List<ChallangeWorkflowStepDisplayViewModel>();

            var currentWorkflowStep = challangeWorkflowStepRepository.Get(entity.Workflow.StartStepId.Value);

            while (currentWorkflowStep != null)
            {
                if (!model.WorkflowIterations.Any(item => item.WorkflowStep.Id == currentWorkflowStep.Id))
                {
                    if (model.NextChallangeWorkflowStep == null)
                    {
                        model.NextChallangeWorkflowStep = new ChallangeWorkflowStepDisplayViewModel
                        {
                            Id = currentWorkflowStep.Id,
                            Name = currentWorkflowStep.Name,
                            Description = currentWorkflowStep.Description
                        };
                    }
                    else
                    {
                        pendingWorkflowSteps.Add(new ChallangeWorkflowStepDisplayViewModel
                        {
                            Id = currentWorkflowStep.Id,
                            Name = currentWorkflowStep.Name,
                            Description = currentWorkflowStep.Description
                        });
                    }
                }

                currentWorkflowStep = currentWorkflowStep.NextId == null ? null : challangeWorkflowStepRepository.Get(currentWorkflowStep.NextId.Value);
            }

            model.PendingWorkflowSteps = pendingWorkflowSteps.ToArray();

            model.Challange.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/workflow/iterations/next")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
        public IActionResult StartNextIteration(System.Guid key, string summary)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    try
                    {
                        var workflowIterations = challangeWorkflowIterationRepository.FindBy(ent1ty => ent1ty.ChallangeId == entity.Id).OrderBy(ent1ty => ent1ty.CreatedAt).ToArray();

                        var targetWorkflowStep = challangeWorkflowStepRepository.Get(entity.Workflow.StartStepId.Value);

                        while (targetWorkflowStep != null)
                        {
                            if (!workflowIterations.Any(item => item.WorkflowStep.Id == targetWorkflowStep.Id))
                            {
                                break;
                            }

                            targetWorkflowStep = targetWorkflowStep.NextId == null ? null : challangeWorkflowStepRepository.Get(targetWorkflowStep.NextId.Value);
                        }

                        challangeWorkflowIterationRepository.Add(new ChallangeWorkflowIteration
                        {
                            Challange = entity,
                            ChallangeId = entity.Id,
                            Summary = summary,
                            WorkflowStepId = targetWorkflowStep.Id,
                            WorkflowStep = targetWorkflowStep
                        }, User.Identity.Name);

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("EditWorkflowIterations", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("EditWorkflowIterations", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/workflow/iterations/{id}/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
        public IActionResult EditIteration(System.Guid key, System.Guid id, string summary)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = challangeWorkflowIterationRepository.Get(id);

                    if (entity.ChallangeId.Value != key)
                    {
                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                        return RedirectToAction("EditWorkflowIterations", new { key });
                    }

                    try
                    {
                        entity.Summary = summary;

                        challangeWorkflowIterationRepository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetEditionSuccessMessage());

                        return RedirectToAction("EditWorkflowIterations", new { key });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }

                    SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                    return RedirectToAction("EditWorkflowIterations", new { key });
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);
                }
            }

            SetDefaultNotification(notificationType: Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        #region Documents

        [HttpPost]
        [Route("edit/{key}/workflow/iterations/{iterationId}/documents/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
        public IActionResult AddWorkflowIterationDocument(System.Guid key, System.Guid iterationId, DocumentInputViewModel model)
        {
            try
            {
                var entity = challangeWorkflowIterationRepository.Get(iterationId);

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

                            entity.DocumentBridges.Add(new ChallangeWorkflowIterationDocumentBridge
                            {
                                ProwId = entity.Id,
                                SternId = document.Id
                            });

                            documentManager.SaveFileContent(document.Id, memoryStream.ToArray(), document.ContentType);

                            challangeWorkflowIterationRepository.Update(entity, User.Identity.Name);
                        }

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("EditWorkflowIterations", new { key });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("EditWorkflowIterations", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/workflow/iterations/{iterationId}/documents/remove")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
        public IActionResult RemoveWorkflowIterationDocument(System.Guid key, System.Guid iterationId, System.Guid documentId)
        {
            try
            {
                var entity = challangeWorkflowIterationRepository.Get(iterationId);

                var bridge = entity.DocumentBridges.First(b => b.SternId == documentId);

                entity.DocumentBridges.Remove(bridge);

                challangeWorkflowIterationRepository.Update(entity, User.Identity.Name);

                try
                {
                    documentManager.DeleteFile(bridge.Stern);
                }
                catch { }

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("EditWorkflowIterations", new { key });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("EditWorkflowIterations", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/workflow/iterations/{iterationId}/documents/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
        public IActionResult EditWorkflowIterationDocument(System.Guid key,System.Guid iterationId, DocumentInputViewModel modelInput)
        {
            ModelState.ClearValidationState("File");
            ModelState.MarkFieldValid("File");

            if (ModelState.IsValid)
            {
                try
                {
                    var entity = challangeWorkflowIterationRepository.Get(iterationId);

                    try
                    {
                        var bridge = entity.DocumentBridges.First(b => b.SternId == modelInput.Id);

                        bridge.Stern.Name = modelInput.Name;
                        bridge.Stern.Description = modelInput.Description;

                        challangeWorkflowIterationRepository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetEditionSuccessMessage());

                        return RedirectToAction("EditWorkflowIterations", new { key });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }

                    SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                    return RedirectToAction("EditWorkflowIterations", new { key });
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

        #region Images

        [HttpPost]
        [Route("edit/{key}/workflow/iterations/{iterationId}/images/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.ChallangeEditor)]
        public IActionResult AddWorkflowIterationImage(System.Guid key, System.Guid iterationId)
        {
            try
            {
                var imageFile = Request.Form.Files[0];

                var entity = challangeWorkflowIterationRepository.Get(iterationId);

                // TODO Validate imageFile

                if (ModelState.IsValid)
                {
                    try
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            imageFile.CopyTo(memoryStream);

                            var image = new Image { ContentType = imageFile.ContentType, Length = imageFile.Length, About = "For Challange Workflow Iteration " + entity.Id };

                            imageRepository.Add(image, User.Identity.Name, false);

                            imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), imageFile.ContentType);

                            entity.ImageBridges.Add(new ChallangeWorkflowIterationImageBridge
                            {
                                ProwId = entity.Id,
                                SternId = image.Id
                            });
                        }

                        challangeWorkflowIterationRepository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("EditWorkflowIterations", new { key });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("EditWorkflowIterations", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/workflow/iterations/{iterationId}/images/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult DeleteWorkflowIterationImage(System.Guid key, System.Guid iterationId, System.Guid imageId)
        {
            try
            {
                var entity = challangeWorkflowIterationRepository.Get(iterationId);

                var bridge = entity.ImageBridges.First(b => b.SternId == imageId);

                entity.ImageBridges.Remove(bridge);

                challangeWorkflowIterationRepository.Update(entity, User.Identity.Name);

                imageRepository.Delete(bridge.Stern, User.Identity.Name);

                imageFileManager.DeleteImageFile(bridge.Stern);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("EditWorkflowIterations", new { key });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("EditWorkflowIterations", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #endregion
    }
}
