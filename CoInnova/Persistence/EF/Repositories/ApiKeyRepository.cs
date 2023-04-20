using Microsoft.EntityFrameworkCore;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class ApiKeyRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.ApiKey, System.Guid>
    {
        public ApiKeyRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.ApiKey, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }

        protected override System.Linq.IQueryable<Entities.ApiKey> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.PermissionBridges).ThenInclude(bridge => bridge.Stern);
        }
    }
}
