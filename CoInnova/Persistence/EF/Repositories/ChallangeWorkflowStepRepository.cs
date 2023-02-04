namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class ChallangeWorkflowStepRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.ChallangeWorkflowStep, System.Guid>
    {
        public ChallangeWorkflowStepRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}