using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Managers.Facade;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Innovation
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("innovation")]
    public class PrototypesController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Prototype, System.Guid, PrototypeInputViewModel, PrototypeDisplayViewModel>
    {
        private readonly IEntityRepository<Idea, System.Guid> ideaRepository;
        private readonly IEntityRepository<PrototypingLevel, System.Guid> levelRepository;
        private readonly IEntityRepository<Collaborator, System.Guid> collaboratorRepository;
        private readonly IEntityRepository<Sphere, System.Guid> sphereRepository;
        private readonly IEntityRepository<UseCase, System.Guid> useCaseRepository;
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<Document, System.Guid> documentRepository;
        private readonly Managers.RelativeDocumentFileManager documentManager;
        private readonly IImageFileManagerFacade imageFileManager;

        public PrototypesController(IImageFileManagerFacade imageFileManager, Managers.RelativeDocumentFileManager documentManager, IEntityRepository<Document, System.Guid> documentRepository, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<UseCase, System.Guid> useCaseRepository, IEntityRepository<Sphere, System.Guid> sphereRepository, IEntityRepository<Collaborator, System.Guid> collaboratorRepository, IEntityRepository<PrototypingLevel, System.Guid> levelRepository, IEntityRepository<Idea, System.Guid> ideaRepository, IEntityRepository<Prototype, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PrototypesController> localizer, Microsoft.Extensions.Logging.ILogger<PrototypesController> logger) : base(repository, localizer, logger)
        {
            this.ideaRepository = ideaRepository;
            this.levelRepository = levelRepository;
            this.collaboratorRepository = collaboratorRepository;
            this.sphereRepository = sphereRepository;
            this.useCaseRepository = useCaseRepository;
            this.imageRepository = imageRepository;
            this.documentRepository = documentRepository;
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

            var model = new PrototypeDocumentsViewModel
            {
                Prototype = new PrototypeDisplayViewModel(),
                Documents = entity.DocumentBridges.Select(item =>
                {
                    var result = new Models.DocumentDisplayCommonModel();

                    result.Import(item.Stern);

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

                            documentRepository.Add(document, User.Identity.Name, false);

                            entity.DocumentBridges.Add(new PrototypeDocumentBridge
                            {
                                ProwId = entity.Id,
                                SternId = document.Id
                            });

                            documentManager.SaveFileContent(document.Id, memoryStream.ToArray(), document.ContentType);

                            Repository.Update(entity, User.Identity.Name);
                        }

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editdocuments", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
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

                var bridge = entity.DocumentBridges.First(b => b.SternId == documentId);

                entity.DocumentBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                try
                {
                    documentManager.DeleteFile(bridge.Stern);
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
                        var bridge = entity.DocumentBridges.First(b => b.SternId == modelInput.Id);

                        bridge.Stern.Name = modelInput.Name;
                        bridge.Stern.Description = modelInput.Description;

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                        return RedirectToAction("editdocuments", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
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

                    result.Import(bridge.Stern);

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

                            imageRepository.Add(image, User.Identity.Name, false);

                            imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), imageFile.ContentType);

                            entity.ImageBridges.Add(new PrototypeImageBridge
                            {
                                ProwId = entity.Id,
                                SternId = image.Id
                            });
                        }

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editimages", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
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

                var bridge = entity.ImageBridges.First(b => b.SternId == imageId);

                entity.ImageBridges.Remove(bridge);

                if (entity.FeatureImageId == imageId)
                {
                    entity.FeatureImage = null;
                    entity.FeatureImageId = null;
                }

                Repository.Update(entity, User.Identity.Name);

                imageRepository.Delete(bridge.Stern, User.Identity.Name);

                imageFileManager.DeleteImageFile(bridge.Stern);

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

        [HttpPost]
        [Route("edit/{key}/images/featured")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult FeaturedImage(System.Guid key, System.Guid imageId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.ImageBridges.First(b => b.SternId == imageId);

                entity.FeatureImageId = bridge.SternId;

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

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

        #region Executors

        [Route("edit/{key}/executors")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult EditExecutors(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var availableCollaborators = collaboratorRepository.FindBy(ent1ty => ent1ty.OrganizationIdentityId != null).ToList();

                var prototypeExecutors = new System.Collections.Generic.List<Collaborator>();

                foreach (var bridge in entity.CollaboratorBridges)
                {
                    var executor = availableCollaborators.First(item => item.Id == bridge.SternId);

                    prototypeExecutors.Add(executor);

                    availableCollaborators.Remove(executor);
                }

                var model = new PrototypeCollaboratorsViewModel
                {
                    Prototype = new PrototypeDisplayViewModel(),
                    Executors = prototypeExecutors.Select(item =>
                    {
                        var iModel = new CollaboratorDisplayViewModel();

                        iModel.Import(item);

                        return iModel;
                    }).ToArray(),
                    AvailableCollaborators = availableCollaborators.Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = item.OrganizationIdentity.Name }).ToArray()
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

        [Route("edit/{key}/executors/add")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddExecutor(System.Guid key, System.Guid? executorId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (executorId != null)
                {
                    try
                    {
                        entity.CollaboratorBridges.Add(new PrototypeCollaboratorBridge { ProwId = entity.Id, SternId = executorId.Value });

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editexecutors", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                ModelState.AddModelError("executorId", string.Format(localizer["RequiredValidationErrorMessage"], localizer["Executor"]));

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editexecutors", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/executors/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult DeleteExecutor(System.Guid key, System.Guid executorId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.CollaboratorBridges.First(b => b.SternId == executorId);

                entity.CollaboratorBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editexecutors", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editexecutors", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #region Spheres

        [Route("edit/{key}/spheres")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult EditSpheres(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var availableSpheres = sphereRepository.GetAll().ToList();

                var prototypeSpheres = new System.Collections.Generic.List<Sphere>();

                foreach (var bridge in entity.SphereBridges)
                {
                    var sphere = availableSpheres.First(item => item.Id == bridge.SternId);

                    prototypeSpheres.Add(sphere);

                    availableSpheres.Remove(sphere);
                }

                var model = new PrototypeSpheresViewModel
                {
                    Prototype = new PrototypeDisplayViewModel(),
                    Spheres = prototypeSpheres.Select(item =>
                    {
                        var iModel = new SphereDisplayViewModel();

                        iModel.Import(item);

                        return iModel;
                    }).ToArray(),
                    AvailableSpheres = availableSpheres.Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToArray()
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

        [Route("edit/{key}/spheres/add")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddSphere(System.Guid key, System.Guid? sphereId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (sphereId != null)
                {
                    try
                    {
                        entity.SphereBridges.Add(new PrototypeSphereBridge { ProwId = entity.Id, SternId = sphereId.Value });

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editspheres", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                ModelState.AddModelError("sphereId", string.Format(localizer["RequiredValidationErrorMessage"], localizer["Sphere"]));

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editspheres", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/spheres/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult DeleteSphere(System.Guid key, System.Guid sphereId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.SphereBridges.First(b => b.SternId == sphereId);

                entity.SphereBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editspheres", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editspheres", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #region UseCases

        private void PopulateUseCaseInputViewModel(UseCaseInputViewModel model)
        {
            model.AvailableCollaborators = collaboratorRepository.FindBy(ent1ty => ent1ty.OrganizationIdentityId != null).ToArray().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = item.OrganizationIdentity.Name }).ToArray();
        }

        [Route("edit/{key}/usecases")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult EditUseCases(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var model = new PrototypeUseCasesViewModel
                {
                    Prototype = new PrototypeDisplayViewModel(),
                    UseCases = useCaseRepository.FindBy(item => item.PrototypeId == entity.Id).ToArray().Select(item =>
                    {
                        var result = new UseCaseDisplayViewModel();

                        result.Import(item);

                        return result;
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

        [HttpGet]
        [Route("edit/{key}/usecases/add")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult AddUseCase(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);
                
                var model = new UseCaseInputViewModel
                {
                    Prototype = new PrototypeDisplayViewModel()
                };

                model.Prototype.Import(entity);

                PopulateUseCaseInputViewModel(model);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/{key}/usecases/add")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddUseCase(System.Guid key, UseCaseInputViewModel model)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    try
                    {
                        var ent1ty = model.Export();

                        ent1ty.PrototypeId = entity.Id;
                        ent1ty.Benefited = collaboratorRepository.Get(model.BenefitedId.Value);
                        ent1ty.BenefitedId = model.BenefitedId.Value;

                        useCaseRepository.Add(ent1ty, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editusecases", new { key = entity.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                model.Prototype = new PrototypeDisplayViewModel();

                model.Prototype.Import(entity);

                PopulateUseCaseInputViewModel(model);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpGet]
        [Route("edit/{key}/usecases/modify/{id}")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult ModifyUseCase(System.Guid key, System.Guid id)
        {
            try
            {
                var entity = useCaseRepository.Get(id);

                var model = new UseCaseInputViewModel
                {
                    Prototype = new PrototypeDisplayViewModel()
                };

                model.Import(entity);
                model.Prototype.Import(Repository.Get(entity.PrototypeId.Value));

                PopulateUseCaseInputViewModel(model);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return RedirectToAction("editusecases", new { key });
        }

        [HttpPost]
        [Route("edit/{key}/usecases/modify")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        [ValidateAntiForgeryToken]
        public IActionResult ModifyUseCase(System.Guid key, UseCaseInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = useCaseRepository.Get(model.Id);

                    model.Mergue(entity);

                    if (model.BenefitedId != entity.BenefitedId)
                    {
                        entity.Benefited = collaboratorRepository.Get(model.BenefitedId.Value);
                        entity.BenefitedId = model.BenefitedId.Value;
                    }

                    useCaseRepository.Update(entity, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                    return RedirectToAction("editusecases", new { key = entity.PrototypeId.Value });
                }
                catch (System.Exception e)
                {
                    if (!(e.InnerException is Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException))
                    {
                        logger.LogError(e.InnerException, e.Message);

                        SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                        return RedirectToAction("editusecases", new { key });
                    }
                }
            }

            SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

            model.Prototype = new PrototypeDisplayViewModel();

            model.Prototype.Import(Repository.Get(key));

            PopulateUseCaseInputViewModel(model);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/usecases/delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteUseCase(System.Guid key, System.Guid id)
        {
            try
            {
                var entity = useCaseRepository.Get(id);

                useCaseRepository.Delete(entity, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editusecases", new { key });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editusecases", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return RedirectToAction("editusecases", new { key });
        }

        #region UseCase Documents

        [HttpGet]
        [Route("edit/{key}/usecases/{id}/documents")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public ActionResult EditUseCaseDocuments(System.Guid key, System.Guid id)
        {
            var target = useCaseRepository.Get(id);

            if (target.PrototypeId != key)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return RedirectToAction("editusecases", new { key });
            }

            var entity = Repository.Get(key);

            var model = new UseCaseDocumentsViewModel
            {
                Prototype = new PrototypeDisplayViewModel(),
                UseCase = new UseCaseDisplayViewModel(),
                Documents = target.DocumentBridges.Select(item =>
                {
                    var result = new Models.DocumentDisplayCommonModel();

                    result.Import(item.Stern);

                    return result;
                }).ToArray()
            };

            model.UseCase.Import(target);
            model.Prototype.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/usecases/{id}/documents/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult AddUseCaseDocument(System.Guid key, System.Guid id, DocumentInputViewModel model)
        {
            try
            {
                var target = useCaseRepository.Get(id);

                if (key != target.PrototypeId)
                {
                    throw new System.InvalidOperationException();
                }

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

                            target.DocumentBridges.Add(new UseCaseDocumentBridge
                            {
                                ProwId = target.Id,
                                Stern = document
                            });

                            documentManager.SaveFileContent(document.Id, memoryStream.ToArray(), document.ContentType);

                            useCaseRepository.Update(target, User.Identity.Name);
                        }

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editusecasedocuments", new { key = entity.Id, id = target.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editusecases", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/usecases/{id}/documents/remove")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult RemoveUseCaseDocument(System.Guid key, System.Guid id, System.Guid documentId)
        {
            try
            {
                var target = useCaseRepository.Get(id);

                if (key != target.PrototypeId)
                {
                    throw new System.InvalidOperationException();
                }

                var entity = Repository.Get(key);

                var bridge = target.DocumentBridges.First(b => b.SternId == documentId);

                target.DocumentBridges.Remove(bridge);

                useCaseRepository.Update(target, User.Identity.Name);

                try
                {
                    documentManager.DeleteFile(bridge.Stern);
                }
                catch { }

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editusecasedocuments", new { key = entity.Id, id = target.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editusecasedocuments", new { key, id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/usecases/{tid}/documents/edit")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult EditUseCaseDocument(System.Guid key, System.Guid tid, DocumentInputViewModel modelInput)
        {
            ModelState.ClearValidationState("File");
            ModelState.MarkFieldValid("File");

            if (ModelState.IsValid)
            {
                try
                {
                    var target = useCaseRepository.Get(tid);

                    if (key != target.PrototypeId)
                    {
                        throw new System.InvalidOperationException();
                    }

                    var entity = Repository.Get(key);

                    try
                    {
                        var bridge = target.DocumentBridges.First(b => b.SternId == modelInput.Id);

                        bridge.Stern.Name = modelInput.Name;
                        bridge.Stern.Description = modelInput.Description;

                        useCaseRepository.Update(target, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                        return RedirectToAction("editusecasedocuments", new { key = entity.Id, id = target.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }

                    SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                    return RedirectToAction("editusecasedocuments", new { key, id = tid });
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

        #region UseCase Images

        [HttpGet]
        [Route("edit/{key}/usecases/{id}/images")]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public ActionResult EditUseCaseImages(System.Guid key, System.Guid id)
        {
            var target = useCaseRepository.Get(id);

            if (target.PrototypeId != key)
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return RedirectToAction("editusecases", new { key });
            }

            var entity = Repository.Get(key);

            var model = new UseCaseImagesViewModel
            {
                Prototype = new PrototypeDisplayViewModel(),
                UseCase = new UseCaseDisplayViewModel(),
                FeatureImage = entity.FeatureImageId == null ? null : new Models.ImageDisplayCommonModel(),
                Images = target.ImageBridges.Select(bridge =>
                {
                    var result = new Models.ImageDisplayCommonModel();

                    result.Import(bridge.Stern);

                    return result;
                }).ToArray()
            };

            model.Prototype.Import(entity);
            model.UseCase.Import(target);

            model.FeatureImage?.Import(entity.FeatureImage);

            return View(model);
        }

        [HttpPost]
        [Route("edit/{key}/usecases/{id}/images/add")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult AddUseCaseImage(System.Guid key, System.Guid id)
        {
            try
            {
                var imageFile = Request.Form.Files[0];

                var target = useCaseRepository.Get(id);

                if (key != target.PrototypeId)
                {
                    throw new System.InvalidOperationException();
                }

                var entity = Repository.Get(key);

                // TODO Validate imageFile

                if (ModelState.IsValid)
                {
                    try
                    {
                        using (var imageSkData = SkiaSharp.SKData.Create(imageFile.OpenReadStream(), imageFile.Length))
                        {
                            using (var imageSkBitmap = SkiaSharp.SKBitmap.Decode(imageSkData))
                            {
                                var image = new Image 
                                { 
                                    ContentType = imageFile.ContentType, 
                                    Length = imageFile.Length, 
                                    About = "For UseCase " + entity.Id,
                                    Width = imageSkBitmap.Width,
                                    Height = imageSkBitmap.Height
                                };

                                imageFileManager.SaveImageFileContent(image.Id, imageSkData.ToArray(), imageFile.ContentType);

                                target.ImageBridges.Add(new UseCaseImageBridge
                                {
                                    ProwId = target.Id,
                                    Stern = image
                                });
                            }
                        }

                        useCaseRepository.Update(target, User.Identity.Name);

                        SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("editusecaseimages", new { key = entity.Id, id = target.Id });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("editusecaseimages", new { key = entity.Id, id = target.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/usecases/{id}/images/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult DeleteUseCaseImage(System.Guid key, System.Guid id, System.Guid imageId)
        {
            try
            {
                var target = useCaseRepository.Get(id);

                if (key != target.PrototypeId)
                {
                    throw new System.InvalidOperationException();
                }

                var entity = Repository.Get(key);

                var bridge = target.ImageBridges.First(b => b.SternId == imageId);

                target.ImageBridges.Remove(bridge);

                if (target.FeatureImageId == imageId)
                {
                    target.FeatureImage = null;
                    target.FeatureImageId = null;
                }

                useCaseRepository.Update(target, User.Identity.Name);

                imageRepository.Delete(bridge.Stern, User.Identity.Name);

                imageFileManager.DeleteImageFile(bridge.Stern);

                SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("editusecaseimages", new { key, id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("editusecaseimages", new { key, id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/usecases/{id}/images/featured")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.PrototypeEditor)]
        public IActionResult FeaturedUseCaseImage(System.Guid key, System.Guid id, System.Guid imageId)
        {
            try
            {
                var target = useCaseRepository.Get(id);

                if (key != target.PrototypeId)
                {
                    throw new System.InvalidOperationException();
                }

                var entity = Repository.Get(key);

                var bridge = target.ImageBridges.First(b => b.SternId == imageId);

                target.FeatureImageId = bridge.SternId;

                useCaseRepository.Update(target, User.Identity.Name);

                SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

                return RedirectToAction("editusecaseimages", new { key, id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion

        #endregion
    }
}
