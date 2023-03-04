using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.SAPRESSC.Core.Common.Models.View;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Innovation
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("innovation")]
    public class PrototypesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Prototype, System.Guid, PrototypeInputViewModel, PrototypeDisplayViewModel>
    {
        private readonly IEntityRepository<Idea, System.Guid> ideaRepository;
        private readonly IEntityRepository<PrototypingLevel, System.Guid> levelRepository;
        private readonly IEntityRepository<Collaborator, System.Guid> collaboratorRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly Managers.RelativeDocumentFileManager documentManager;
        private readonly IImageFileManagerFacade imageFileManager;

        public PrototypesController(IImageFileManagerFacade imageFileManager, Managers.RelativeDocumentFileManager documentManager, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Collaborator, System.Guid> collaboratorRepository, IEntityRepository<PrototypingLevel, System.Guid> levelRepository, IEntityRepository<Idea, System.Guid> ideaRepository, IEntityRepository<Prototype, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PrototypesController> localizer, Microsoft.Extensions.Logging.ILogger<PrototypesController> logger) : base(repository, localizer, logger)
        {
            this.ideaRepository = ideaRepository;
            this.levelRepository = levelRepository;
            this.collaboratorRepository = collaboratorRepository;
            this.imageRepository = imageRepository;
            this.documentManager = documentManager;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Permission.PrototypeViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Permission.PrototypeCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.PrototypeCreator)]
        public override IActionResult Create(PrototypeInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        public override IActionResult Edit(PrototypeInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Constants.Permission.PrototypeRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.PrototypeRemover)]
        public override IActionResult Delete(PrototypeDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Prototype entity, PrototypeInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.LevelId = modelInput.LevelId;
            entity.Level = levelRepository.Get(modelInput.LevelId.Value);

            entity.InnovationAgentId = modelInput.InnovationAgentId;
            entity.InnovationAgent = collaboratorRepository.Get(modelInput.InnovationAgentId.Value);

            if (modelInput.IdeaId != null)
            {
                entity.IdeaId = modelInput.IdeaId;
                entity.Idea = ideaRepository.Get(modelInput.IdeaId.Value);
            }
        }

        protected override void PreEdit(Prototype entity, PrototypeInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.LevelId != modelInput.LevelId)
            {
                entity.LevelId = modelInput.LevelId;
                entity.Level = levelRepository.Get(modelInput.LevelId.Value);
            }

            if (entity.InnovationAgentId != modelInput.InnovationAgentId)
            {
                entity.InnovationAgentId = modelInput.InnovationAgentId;
                entity.InnovationAgent = collaboratorRepository.Get(modelInput.InnovationAgentId.Value);
            }

            if (entity.IdeaId != modelInput.IdeaId)
            {
                if (modelInput.IdeaId == null)
                {
                    entity.IdeaId = null;
                    entity.Idea = null;
                }
                else
                {
                    entity.IdeaId = modelInput.IdeaId;
                    entity.Idea = ideaRepository.Get(modelInput.IdeaId.Value);
                }
            }
        }

        protected override IActionResult GetSuccessCreateActionResult(System.Guid key)
        {
            return base.GetSuccessEditActionResult(key);
        }

        protected override void ConfigureCreate(PrototypeInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(PrototypeInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigureEdit(Prototype entity, PrototypeInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Prototype entity, PrototypeInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            Populate(modelInput);
        }

        private void Populate(PrototypeInputViewModel model)
        {
            model.AvailableIdeas = ideaRepository.GetAll().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Title
            }).ToArray();
            model.AvailablePrototypingLevels = levelRepository.GetAll().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id.ToString(),
                Text = string.Format("L{0} {1}", item.Numerator, item.Name)
            }).ToArray();
            model.AvailableInnovationAgents = collaboratorRepository.FindBy(entity => entity.OrganizationIdentityId != null).Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.OrganizationIdentity.Name
            }).ToArray();
        }

        #region Documents

        [HttpGet]
        [Route("edit/{key}/documents")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public ActionResult EditDocuments(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new PrototypeDocuementsViewModel
            {
                Prototype = new PrototypeDisplayViewModel(),
                Documents = entity.DocumentBridges.Select(item =>
                {
                    var result = new Models.DocumentDisplayCommonModel();

                    result.Import(item.Document);

                    return result;
                }).ToArray()
            };

            model.Prototype.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/documents/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
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

                            entity.DocumentBridges.Add(new PrototypeDocumentBridge
                            {
                                PrototypeId = entity.Id,
                                Document = document
                            });

                            documentManager.SaveFileContent(document.Id, memoryStream.ToArray(), document.ContentType);

                            Repository.Update(entity, User.Identity.Name);
                        }

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editdocuments", new { key = entity.Id });
                    }
                    catch (SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editdocuments", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/documents/remove")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult RemoveDocument(System.Guid key, System.Guid documentId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.DocumentBridges.First(b => b.DocumentId == documentId);

                entity.DocumentBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                try
                {
                    documentManager.DeleteFile(bridge.Document);
                }
                catch { }

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editdocuments", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editdocuments", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/documents/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
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
                        var bridge = entity.DocumentBridges.First(b => b.DocumentId == modelInput.Id);

                        bridge.Document.Name = modelInput.Name;
                        bridge.Document.Description = modelInput.Description;

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                        return RedirectToAction("editdocuments", new { key = entity.Id });
                    }
                    catch (SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }

                    SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                    return RedirectToAction("editdocuments", new { key = entity.Id });
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);
                }
            }

            SetDefaultNotification(notificationType: NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        #endregion

        #region Images

        [HttpGet]
        [Route("edit/{key}/images")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public ActionResult EditImages(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new PrototypeImagesViewModel
            {
                Prototype = new PrototypeDisplayViewModel(),
                FeatureImage = entity.FeatureImageId == null ? null : new Models.ImageDisplayCommonModel(),
                Images = entity.ImageBridges.Select(bridge =>
                {
                    var result = new Models.ImageDisplayCommonModel();

                    result.Import(bridge.Image);

                    return result;
                }).ToArray()
            };

            model.Prototype.Import(entity);

            model.FeatureImage?.Import(entity.FeatureImage);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/images/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult AddImage(System.Guid key)
        {
            try
            {
                var imageFile = Request.Form.Files[0];

                var entity = Repository.Get(key);

                // TODO Validate imageFile

                if (ModelState.IsValid)
                {
                    try
                    {
                        using (var memoryStream = new System.IO.MemoryStream())
                        {
                            imageFile.CopyTo(memoryStream);

                            var image = new Image { ContentType = imageFile.ContentType, Length = imageFile.Length, About = "For Prototype " + entity.Id };

                            imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), imageFile.ContentType);

                            entity.ImageBridges.Add(new PrototypeImageBridge
                            {
                                PrototypeId = entity.Id,
                                Image = image
                            });
                        }

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editimages", new { key = entity.Id });
                    }
                    catch (SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editimages", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/images/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult DeleteImage(System.Guid key, System.Guid imageId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.ImageBridges.First(b => b.ImageId == imageId);

                entity.ImageBridges.Remove(bridge);

                if (entity.FeatureImageId == imageId)
                {
                    entity.FeatureImage = null;
                    entity.FeatureImageId = null;
                }

                Repository.Update(entity, User.Identity.Name);

                imageRepository.Delete(bridge.Image, User.Identity.Name);

                imageFileManager.DeleteImageFile(bridge.Image);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editimages", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editimages", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #region Collaborators

        [Route("edit/{key}/collaborators")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult EditCollaborators(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var availableCollaborators = collaboratorRepository.GetAll().ToList();

                var prototypeCollaborators = new System.Collections.Generic.List<Collaborator>();

                foreach (var bridge in entity.CollaboratorBridges)
                {
                    foreach (var item in availableCollaborators)
                    {
                        if (item.Id != bridge.CollaboratorId) continue;

                        prototypeCollaborators.Add(item);

                        availableCollaborators.Remove(item);
                    }
                }

                var model = new PrototypeCollaboratorsViewModel
                {
                    Prototype = new PrototypeDisplayViewModel(),
                    Collaborators = prototypeCollaborators.Select(item =>
                    {
                        var iModel = new CollaboratorDisplayViewModel();

                        iModel.Import(item);

                        return iModel;
                    }).ToArray(),
                    AvailableCollaborators = availableCollaborators.Select(item =>
                    {
                        if (item.OrganizationIdentityId == null)
                        {
                            return new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = string.Format("{0} {1}", item.User.Identity.Name, item.User.Identity.LastName) };
                        }

                        return new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = item.OrganizationIdentity.Name };
                    }).ToArray()
                };

                model.Prototype.Import(entity);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        //[Route("edit/members/add")]
        //[RequirePermissions(Permission.CreationGroupEditor)]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult AddMember(CreationGroupMembersInputViewModel modelInput)
        //{
        //    CreationGroup entity = null;

        //    try
        //    {
        //        entity = Repository.Get(modelInput.CreationGroupId);
        //    }
        //    catch { }

        //    if (entity == null)
        //    {
        //        SetDefaultNotification(MOROPO.SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

        //        return GetDefaultActionResult();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        entity.ArtistBridges.Add(new CreationGroupArtistBridge { CreationGroupId = entity.Id, ArtistId = modelInput.ArtistId });

        //        Repository.Update(entity, User.Identity.Name);

        //        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());
        //    }
        //    else
        //    {
        //        var messages = new List<string>();

        //        foreach (var item in ModelState)
        //        {
        //            if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

        //            foreach (var error in item.Value.Errors)
        //            {
        //                messages.Add(error.ErrorMessage);
        //            }
        //        }

        //        SetDefaultNotification(NotificationType.Danger, messages.ToArray());
        //    }

        //    return RedirectToAction("members", new { key = entity.Id });
        //}

        #endregion
    }
}
