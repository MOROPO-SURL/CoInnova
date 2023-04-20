namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class IdeaWorkflowStepRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.IdeaWorkflowStep, System.Guid>
    {
        public IdeaWorkflowStepRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
