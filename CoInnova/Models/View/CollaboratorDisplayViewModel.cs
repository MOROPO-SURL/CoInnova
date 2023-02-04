namespace MOROPO.UIC.CoInnova.Models.View
{
    public class CollaboratorDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Collaborator, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public SAPRESSC.Middleware.Security.Identity.Web.Backend.Models.View.UserDisplayViewModel User { get; set; }

        public OrganizationDisplayViewModel OrganizationIdentity { get; set; }

        public void Import(Persistence.Entities.Collaborator entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            User = new SAPRESSC.Middleware.Security.Identity.Web.Backend.Models.View.UserDisplayViewModel();

            User.Import(entity.User);

            if (entity.OrganizationIdentityId != null)
            {
                OrganizationIdentity = new OrganizationDisplayViewModel();

                OrganizationIdentity.Import(entity.OrganizationIdentity);
            }
        }
    }
}
