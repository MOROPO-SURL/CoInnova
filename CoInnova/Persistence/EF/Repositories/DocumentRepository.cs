namespace MOROPO.UIC.CoInnova.Persistence.EF.Repositories
{
    public class DocumentRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.AbstractEntityRepository<Entities.Document, System.Guid>
    {
        public DocumentRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }
    }
}
