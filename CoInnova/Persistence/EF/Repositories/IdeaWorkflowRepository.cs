namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class IdeaWorkflowRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.IdeaWorkflow, System.Guid>
    {
        public IdeaWorkflowRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
