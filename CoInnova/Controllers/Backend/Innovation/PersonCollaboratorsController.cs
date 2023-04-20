using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Innovation
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("innovation")]
    public class PersonCollaboratorsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Collaborator, System.Guid, PersonCollaboratorInputViewModel, CollaboratorDisplayViewModel>
    {
        private readonly IEntityRepository<User, string> userRepository;

        public PersonCollaboratorsController(IEntityRepository<User, string> userRepository, IEntityRepository<Collaborator, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PersonCollaboratorsController> localizer, Microsoft.Extensions.Logging.ILogger<PersonCollaboratorsController> logger) : base(repository, localizer, logger)
        {
            this.userRepository = userRepository;
        }

        [RequirePermissions(Constants.Permission.CollaboratorViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        protected override System.Linq.Expressions.Expression<System.Func<Collaborator, bool>> GetGlobalSelector()
        {
            return entity => entity.OrganizationIdentityId == null;
        }

        [RequirePermissions(Constants.Permission.CollaboratorCreator)]
        public override IActionResult Create()
        {
            return GetDefaultActionResult();
        }

        [RequirePermissions(Constants.Permission.CollaboratorCreator)]
        public override IActionResult Create(PersonCollaboratorInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        public override IActionResult Edit(System.Guid key)
        {
            return NotFound();
        }

        public override IActionResult Edit(PersonCollaboratorInputViewModel modelInput)
        {
            return NotFound();
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

        protected override void PreCreate(Collaborator entity, PersonCollaboratorInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            entity.User = userRepository.Get(modelInput.UserId);
            entity.UserId = modelInput.UserId;
        }

        protected override void ConfigureCreate(PersonCollaboratorInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateAvailableUsers(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(PersonCollaboratorInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateAvailableUsers(modelInput);
        }

        private void PopulateAvailableUsers(PersonCollaboratorInputViewModel model)
        {
            model.AvailableUsers = userRepository.GetAll().ToArray().Except(Repository.GetAll().Select(item => item.User).ToArray()).ToArray().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id,
                Text = string.IsNullOrEmpty(item.Identity.LastName) ? string.Format("{0} ({1})", item.Identity.Name.Trim(), item.Id) : string.Format("{0} {1} ({2})", item.Identity.Name.Trim(), item.Identity.LastName.Trim(), item.Id)
            }).ToArray();
        }
    }
}
