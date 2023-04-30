using Microsoft.EntityFrameworkCore;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class InnovationAgentRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.InnovationAgent, System.Guid>
    {
        public InnovationAgentRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<InnovationAgent> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Identity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.Identity).ThenInclude(entity => entity.PhoneContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.Identity).ThenInclude(entity => entity.EmailContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.Identity).ThenInclude(entity => entity.SocialContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.Identity).ThenInclude(entity => entity.PostalContactItemBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.UserBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}
