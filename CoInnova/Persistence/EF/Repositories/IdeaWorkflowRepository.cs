namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class IdeaWorkflowRepository : SAPRESSC.Core.Persistence.EF.Repositories.AbstractEntityRepository<Entities.IdeaWorkflow, System.Guid>
    {
        public IdeaWorkflowRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
