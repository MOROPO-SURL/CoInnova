namespace MOROPO.UIC.CoInnova.Models.View.Filter
{
    public class TracesFilterViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Filter.Contract.IEntityFilterModel<Framework.Guama.Core.Persistence.ORM.EF.Entities.Trace, long>
    {
        public string Author { get; set; }

        public System.Linq.Expressions.Expression<System.Func<Framework.Guama.Core.Persistence.ORM.EF.Entities.Trace, bool>> GetSelector()
        {
            if (Author == null || Author == string.Empty) return null;

            return user => user.Author.Contains(Author);
        }
    }
}
