namespace MOROPO.UIC.CoInnova.Models.View.Filter
{
    public class TracesFilterViewModel : SAPRESSC.Core.Persistence.Models.Filter.IEntityFilterModel<SAPRESSC.Core.Persistence.EF.Entities.Trace, long>
    {
        public string Author { get; set; }

        public System.Linq.Expressions.Expression<System.Func<SAPRESSC.Core.Persistence.EF.Entities.Trace, bool>> GetSelector()
        {
            if (Author == null || Author == string.Empty) return null;

            return user => user.Author.Contains(Author);
        }
    }
}
