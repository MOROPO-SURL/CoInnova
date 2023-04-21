using Microsoft.EntityFrameworkCore;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class IdeaRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Idea, System.Guid>
    {
        public IdeaRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Idea> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Workflow)
                .Include(entity => entity.Challange)
                .Include(entity => entity.FeatureImage)
                .Include(entity => entity.CollaboratorBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.DocumentBridges).ThenInclude(bridge => bridge.Stern);
        }
    }
}
