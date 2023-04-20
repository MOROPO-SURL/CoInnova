using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Components
{
    public class PersonCollaboratorCreateFormViewComponent : ViewComponent
    {
        private readonly IEntityRepository<User, string> userRepository;
        private readonly IEntityRepository<Collaborator, System.Guid> collaboratorRepository;

        public PersonCollaboratorCreateFormViewComponent(IEntityRepository<Collaborator, System.Guid> collaboratorRepository, IEntityRepository<User, string> userRepository)
        {
            this.userRepository = userRepository;
            this.collaboratorRepository = collaboratorRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = new Models.View.PersonCollaboratorInputViewModel();

            PopulateAvailableUsers(model);

            return View(model);
        }

        private void PopulateAvailableUsers(Models.View.PersonCollaboratorInputViewModel model)
        {
            model.AvailableUsers = userRepository.GetAll().ToArray().Except(collaboratorRepository.FindBy(entity => entity.OrganizationIdentityId == null).Select(item => item.User).ToArray()).ToArray().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id,
                Text = string.IsNullOrEmpty(item.Identity.LastName) ? string.Format("{0} ({1})", item.Identity.Name.Trim(), item.Id) : string.Format("{0} {1} ({2})", item.Identity.Name.Trim(), item.Identity.LastName.Trim(), item.Id)
            }).ToArray();
        }
    }
}
