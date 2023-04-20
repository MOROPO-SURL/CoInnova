namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class ChallangeWorkflowRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.ChallangeWorkflow, System.Guid>
    {
        public ChallangeWorkflowRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
