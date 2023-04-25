using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class IdeaWorkflowIterationRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.IdeaWorkflowIteration, System.Guid>
    {
        public IdeaWorkflowIterationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.IdeaWorkflowIteration> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.WorkflowStep)
                .Include(entity => entity.DocumentBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.ImageBridges).ThenInclude(bridge => bridge.Stern);
        }
    }
}
