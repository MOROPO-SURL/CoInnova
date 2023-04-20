namespace MOROPO.UIC.CoInnova.Models.View
{
    public class CollaboratorDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Collaborator, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel User { get; set; }

        public OrganizationDisplayViewModel OrganizationIdentity { get; set; }

        public void Import(Persistence.Entities.Collaborator entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            User = new Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel();

            User.Import(entity.User);

            if (entity.OrganizationIdentityId != null)
            {
                OrganizationIdentity = new OrganizationDisplayViewModel();

                OrganizationIdentity.Import(entity.OrganizationIdentity);
            }
        }
    }
}
