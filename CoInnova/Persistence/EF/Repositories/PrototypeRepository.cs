using Microsoft.EntityFrameworkCore;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class PrototypeRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Prototype, System.Guid>
    {
        public PrototypeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Prototype> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Level)
                .Include(entity => entity.Idea)
                .Include(entity => entity.InnovationAgent).ThenInclude(entity => entity.User).ThenInclude(entity => entity.Identity)
                .Include(entity => entity.InnovationAgent).ThenInclude(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.DocumentBridges).ThenInclude(bridge => bridge.Document)
                .Include(entity => entity.ImageBridges).ThenInclude(bridge => bridge.Image)
                .Include(entity => entity.CollaboratorBridges);
        }
    }
}
