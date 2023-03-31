using Microsoft.EntityFrameworkCore;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class IdeaRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Idea, System.Guid>
    {
        public IdeaRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
