using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class EvaluationRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Evaluation, System.Guid>
    {
        public EvaluationRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Entities.Evaluation> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Idea)
                .Include(entity => entity.CollaboratorBridges).ThenInclude(bridge => bridge.Stern).ThenInclude(entity => entity.User).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }

        public override System.Func<Entities.Evaluation, object> GetPagingOrderSelector()
        {
            return entity => entity.CreatedAt;
        }

        public override bool IsPagingOrderDescending()
        {
            return true;
        }
    }
}
