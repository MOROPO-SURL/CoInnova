using Microsoft.EntityFrameworkCore;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class UseCaseRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.UseCase, System.Guid>
    {
        public UseCaseRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<UseCase> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Benefited).ThenInclude(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.Benefited).ThenInclude(entity => entity.User);
        }
    }
}
