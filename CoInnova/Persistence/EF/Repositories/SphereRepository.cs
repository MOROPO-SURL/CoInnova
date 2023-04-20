namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class SphereRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Sphere, System.Guid>
    {
        public SphereRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
