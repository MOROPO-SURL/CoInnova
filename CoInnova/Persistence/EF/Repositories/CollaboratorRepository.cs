using Microsoft.EntityFrameworkCore;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class CollaboratorRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Collaborator, System.Guid>
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
                .Include(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.PhoneContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.EmailContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.SocialContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.PostalContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.PhoneContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.EmailContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.SocialContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.PostalContactItemBridges).ThenInclude(bridge => bridge.Stern);
        }
    }
}
