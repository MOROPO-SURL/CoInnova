using Microsoft.EntityFrameworkCore;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class UseCaseRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.UseCase, System.Guid>
    {
        public UseCaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<Entities.UseCase> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.DocumentBridges).ThenInclude(bridge => bridge.Document)
                .Include(entity => entity.ImageBridges).ThenInclude(bridge => bridge.Image)
                .Include(entity => entity.FeatureImage)
                .Include(entity => entity.Benefited).ThenInclude(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.Benefited).ThenInclude(entity => entity.User);
        }
    }
}
