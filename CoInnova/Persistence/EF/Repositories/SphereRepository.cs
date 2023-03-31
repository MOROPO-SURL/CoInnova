namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class SphereRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Sphere, System.Guid>
    {
        public SphereRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
