namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class PrototypingLevelRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.PrototypingLevel, System.Guid>
    {
        public PrototypingLevelRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<Entities.PrototypingLevel, object> GetPagingOrderSelector()
        {
            return entity => entity.Numerator;
        }
    }
}
