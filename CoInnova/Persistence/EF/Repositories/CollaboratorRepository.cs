using Microsoft.EntityFrameworkCore;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class CollaboratorRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Collaborator, System.Guid>
    {
        public CollaboratorRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.Collaborator, object> GetPagingOrderSelector()
        {
            return entity => entity.CreatedAt;
        }

        protected override System.Linq.IQueryable<Entities.Collaborator> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.PersonPhoneContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.PersonEmailContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.PersonSocialContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.PersonPostalContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.OrganizationPhoneContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.OrganizationEmailContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.OrganizationSocialContactItemBridges).ThenInclude(bridge => bridge.ContactItem)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.OrganizationPostalContactItemBridges).ThenInclude(bridge => bridge.ContactItem);
        }
    }
}
