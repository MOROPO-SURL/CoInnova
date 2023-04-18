using Microsoft.EntityFrameworkCore;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class ChallangeRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.Challange, System.Guid>
    {
        public ChallangeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Challange> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Workflow)
                .Include(entity => entity.FeatureImage)
                .Include(entity => entity.CollaboratorBridges).ThenInclude(bridge => bridge.Collaborator).ThenInclude(entity => entity.OrganizationIdentity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.DocumentBridges).ThenInclude(bridge => bridge.Document);
        }
    }
}
