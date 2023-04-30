using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Identity.Persistence.Entities;
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
    public class AgentsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<InnovationAgent, System.Guid, InnovationAgentInputViewModel, InnovationAgentDisplayViewModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<OrganizationIdentity, System.Guid> identityRepository;
        private readonly IEntityRepository<PhoneContactItem, System.Guid> phoneContactItemRepository;
        private readonly IEntityRepository<EmailContactItem, System.Guid> emailContactItemRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public AgentsController(IImageFileManagerFacade imageFileManager, IEntityRepository<EmailContactItem, System.Guid> emailContactItemRepository, IEntityRepository<PhoneContactItem, System.Guid> phoneContactItemRepository, IEntityRepository<OrganizationIdentity, System.Guid> identityRepository, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<InnovationAgent, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<AgentsController> localizer, ILogger<AgentsController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.identityRepository = identityRepository;
            this.phoneContactItemRepository = phoneContactItemRepository;
            this.emailContactItemRepository = emailContactItemRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Permission.InnovationAgentViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Permission.InnovationAgentCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.InnovationAgentCreator)]
        public override IActionResult Create(InnovationAgentInputViewModel model)
        {
            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    ModelState.ClearValidationState("ImageFile");

                    model.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(model);

                    updateModelTask.Wait();
                }
            }

            return base.Create(model);
        }

        [RequirePermissions(Constants.Permission.InnovationAgentEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Permission.InnovationAgentEditor)]
        public override IActionResult Edit(InnovationAgentInputViewModel model)
        {
            ModelState.ClearValidationState("ImageFile");
            ModelState.MarkFieldValid("ImageFile");

            if (Request.Form.Files.Count > 0)
            {
                var formFile = Request.Form.Files[0];

                if (formFile.Length > 0)
                {
                    model.ImageFile = formFile;

                    var updateModelTask = TryUpdateModelAsync(model);

                    updateModelTask.Wait();
                }
            }

            return base.Edit(model);
        }

        [RequirePermissions(Constants.Permission.InnovationAgentRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.InnovationAgentRemover)]
        public override IActionResult Delete(InnovationAgentDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(InnovationAgent entity, InnovationAgentInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "InnovationAgent " + entity.Id };

                imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void PreEdit(InnovationAgent entity, InnovationAgentInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.Identity.Image != null)
                {
                    imageFileManager.DeleteImageFile(entity.Identity.Image);

                    imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.Identity.Image != null)
            {
                imageFileManager.DeleteImageFile(entity.Identity.Image);

                imageRepository.Delete(entity.Identity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "InnovationAgent " + entity.Id };

                imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.Identity.Image = image;
            }
        }

        protected override void PostDelete(InnovationAgent entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.Identity, User.Identity.Name);

            if (entity.Identity.Image == null) return;

            imageRepository.Delete(entity.Identity.Image, User.Identity.Name);

            imageFileManager.DeleteImageFile(entity.Identity.Image);
        }

        #region Contacts

        [Route("edit/{key}/contacts")]
        [RequirePermissions(Constants.Permission.InnovationAgentEditor)]
        public IActionResult EditContacts(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var resultModel = new InnovationAgentContactsViewModel
                {
                    Agent = new InnovationAgentDisplayViewModel(),
                    PhoneContacts = entity.Identity.PhoneContactItemBridges.Select(bridge => new PhoneContactDisplayViewModel
                    {
                        Id = bridge.SternId,
                        Label = bridge.Stern.Label,
                        Number = bridge.Stern.PhoneNumber
                    }).ToList(),
                    EmailContacts = entity.Identity.EmailContactItemBridges.Select(bridge => new EmailContactDisplayViewModel
                    {
                        Id = bridge.SternId,
                        Label = bridge.Stern.Label,
                        Address = bridge.Stern.EmailAddress
                    }).ToList()
                };

                resultModel.Agent.Import(entity);

                return View(resultModel);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        #region Phone

        [Route("edit/{key}/contacts/phone/add")]
        [RequirePermissions(Constants.Permission.InnovationAgentEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditContactsAddPhone(System.Guid key, PhoneContactInputViewModel modelInput)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    var item = phoneContactItemRepository.Add(modelInput.Export(), User.Identity.Name, false);

                    entity.Identity.PhoneContactItemBridges.Add(new OrganizationPhoneContactItemBridge { ProwId = entity.IdentityId.Value, SternId = item.Id });

                    Repository.Update(entity, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());
                }
                else
                {
                    var messages = new System.Collections.Generic.List<string>();

                    foreach (var item in ModelState)
                    {
                        if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                        foreach (var error in item.Value.Errors)
                        {
                            messages.Add(error.ErrorMessage);
                        }
                    }

                    SetDefaultNotification(NotificationType.Danger, "Agregar Número Telefónico", messages.ToArray());
                }

                return RedirectToAction("EditContacts", new { key });
            }
            catch
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }
        }

        [Route("edit/{key}/contacts/phone/delete")]
        [RequirePermissions(Constants.Permission.InnovationAgentEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditContactsPhoneDelete(System.Guid key, System.Guid itemId)
        {
            try
            {
                var entity = Repository.Get(key);

                try
                {
                    var bridge = entity.Identity.PhoneContactItemBridges.First(b => b.SternId == itemId);

                    entity.Identity.PhoneContactItemBridges.Remove(bridge);

                    phoneContactItemRepository.Delete(bridge.Stern, User.Identity.Name, false);

                    Repository.Update(entity, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    logger.LogError(e.InnerException, e.Message);

                    SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);
                }

                return RedirectToAction("EditContacts", new { key = entity.Id });
            }
            catch
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }
        }

        #endregion

        #region Email

        [Route("edit/{key}/contacts/email/add")]
        [RequirePermissions(Constants.Permission.InnovationAgentEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditContactsAddEmail(System.Guid key, EmailContactInputViewModel modelInput)
        {
            try
            {
                var entity = Repository.Get(key);

                if (ModelState.IsValid)
                {
                    var item = emailContactItemRepository.Add(modelInput.Export(), User.Identity.Name, false);

                    entity.Identity.EmailContactItemBridges.Add(new OrganizationEmailContactItemBridge { ProwId = entity.IdentityId.Value, SternId = item.Id });

                    Repository.Update(entity, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());
                }
                else
                {
                    var messages = new System.Collections.Generic.List<string>();

                    foreach (var item in ModelState)
                    {
                        if (ModelState.GetFieldValidationState(item.Key) != Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid) continue;

                        foreach (var error in item.Value.Errors)
                        {
                            messages.Add(error.ErrorMessage);
                        }
                    }

                    SetDefaultNotification(NotificationType.Danger, "Agregar Correo Electrónico", messages.ToArray());
                }

                return RedirectToAction("EditContacts", new { key });
            }
            catch
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }
        }

        [Route("edit/{key}/contacts/email/delete")]
        [RequirePermissions(Constants.Permission.InnovationAgentEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditContactsEmailDelete(System.Guid key, System.Guid itemId)
        {
            try
            {
                var entity = Repository.Get(key);

                try
                {
                    var bridge = entity.Identity.EmailContactItemBridges.First(b => b.SternId == itemId);

                    entity.Identity.EmailContactItemBridges.Remove(bridge);

                    emailContactItemRepository.Delete(bridge.Stern, User.Identity.Name, false);

                    Repository.Update(entity, User.Identity.Name);

                    SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
                {
                    logger.LogError(e.InnerException, e.Message);

                    SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());
                }
                catch (System.Exception e)
                {
                    logger.LogError(e.InnerException, e.Message);
                }

                return RedirectToAction("EditContacts", new { key = entity.Id });
            }
            catch
            {
                SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

                return GetDefaultActionResult();
            }
        }

        #endregion

        #endregion
    }
}
