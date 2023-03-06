using Microsoft.EntityFrameworkCore;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class PrototypeRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Prototype, System.Guid>
    {
        public PrototypeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.Prototype, object> GetPagingOrderSelector()
        {
            return entity => entity.Name;
        }
    }
}
