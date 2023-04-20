namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class ChallangeWorkflowStepRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.ChallangeWorkflowStep, System.Guid>
    {
        public ChallangeWorkflowStepRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}