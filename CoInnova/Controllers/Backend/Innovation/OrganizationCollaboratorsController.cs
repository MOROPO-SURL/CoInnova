using Microsoft.AspNetCore.Mvc;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Managers.Facade;
using MOROPO.SAPRESSC.Middleware.Security.Identity.Persistence.Entities;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Innovation
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("innovation")]
    public class OrganizationCollaboratorsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Collaborator, System.Guid, OrganizationCollaboratorInputViewModel, CollaboratorDisplayViewModel>
    {
        private readonly IEntityRepository<Image, System.Guid> imageRepository;
        private readonly IEntityRepository<User, string> userRepository;
        private readonly IEntityRepository<OrganizationIdentity, System.Guid> identityRepository;
        private readonly IImageFileManagerFacade imageFileManager;

        public OrganizationCollaboratorsController(IImageFileManagerFacade imageFileManager, IEntityRepository<OrganizationIdentity, System.Guid> identityRepository, IEntityRepository<User, string> userRepository, IEntityRepository<Image, System.Guid> imageRepository, IEntityRepository<Collaborator, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<OrganizationCollaboratorsController> localizer, Microsoft.Extensions.Logging.ILogger<OrganizationCollaboratorsController> logger) : base(repository, localizer, logger)
        {
            this.imageRepository = imageRepository;
            this.userRepository = userRepository;
            this.identityRepository = identityRepository;
            this.imageFileManager = imageFileManager;
        }

        [RequirePermissions(Constants.Permission.CollaboratorViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        protected override System.Linq.Expressions.Expression<System.Func<Collaborator, bool>> GetGlobalSelector()
        {
            return entity => entity.OrganizationIdentityId != null;
        }

        [RequirePermissions(Constants.Permission.CollaboratorCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.CollaboratorCreator)]
        public override IActionResult Create(OrganizationCollaboratorInputViewModel model)
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

        [RequirePermissions(Constants.Permission.CollaboratorEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Permission.CollaboratorEditor)]
        public override IActionResult Edit(OrganizationCollaboratorInputViewModel model)
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

        [RequirePermissions(Constants.Permission.CollaboratorRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.CollaboratorRemover)]
        public override IActionResult Delete(CollaboratorDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Collaborator entity, OrganizationCollaboratorInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.User = userRepository.Get(modelInput.UserId);
            entity.UserId = modelInput.UserId;

            if (modelInput.ImageFile == null) return;

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Collaborator Organization " + entity.Id };

                imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PreEdit(Collaborator entity, OrganizationCollaboratorInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (modelInput.UserId != entity.UserId)
            {
                entity.User = userRepository.Get(modelInput.UserId);
                entity.UserId = modelInput.UserId;
            }

            if (modelInput.ImageFile == null)
            {
                if (modelInput.ResetImage && entity.OrganizationIdentity.Image != null)
                {
                    imageFileManager.DeleteImageFile(entity.OrganizationIdentity.Image);

                    imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);
                }

                return;
            }

            if (entity.OrganizationIdentity.Image != null)
            {
                imageFileManager.DeleteImageFile(entity.OrganizationIdentity.Image);

                imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);
            }

            using (var memoryStream = new System.IO.MemoryStream())
            {
                modelInput.ImageFile.CopyTo(memoryStream);

                var image = new Image { ContentType = modelInput.ImageFile.ContentType, Length = modelInput.ImageFile.Length, About = "Collaborator Organization " + entity.Id };

                imageFileManager.SaveImageFileContent(image.Id, memoryStream.ToArray(), modelInput.ImageFile.ContentType);

                entity.OrganizationIdentity.Image = image;
            }
        }

        protected override void PostDelete(Collaborator entity)
        {
            base.PostDelete(entity);

            identityRepository.Delete(entity.OrganizationIdentity, User.Identity.Name);

            if (entity.OrganizationIdentity.Image == null) return;

            imageRepository.Delete(entity.OrganizationIdentity.Image, User.Identity.Name);

            imageFileManager.DeleteImageFile(entity.OrganizationIdentity.Image);
        }

        protected override void ConfigureCreate(OrganizationCollaboratorInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateAvailableUsers(modelInput);
        }

        protected override void ConfigureEdit(Collaborator entity, OrganizationCollaboratorInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateAvailableUsers(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(OrganizationCollaboratorInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateAvailableUsers(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Collaborator entity, OrganizationCollaboratorInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            PopulateAvailableUsers(modelInput);
        }

        private void PopulateAvailableUsers(OrganizationCollaboratorInputViewModel model)
        {
            model.AvailableUsers = userRepository.GetAll().ToArray().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id,
                Text = string.IsNullOrEmpty(item.Identity.LastName) ? string.Format("{0} ({1})", item.Identity.Name.Trim(), item.Id) : string.Format("{0} {1} ({2})", item.Identity.Name.Trim(), item.Identity.LastName.Trim(), item.Id)
            }).ToArray();
        }
    }
}
