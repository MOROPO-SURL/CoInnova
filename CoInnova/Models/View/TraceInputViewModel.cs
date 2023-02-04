using MOROPO.SAPRESSC.Core.Persistence.EF.Entities;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class TraceInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Trace, long>
    {
        public long Id { get; set; }

        public Trace Export()
        {
            throw new System.NotImplementedException();
        }

        public void Import(Trace entity)
        {
            throw new System.NotImplementedException();
        }

        public void Mergue(Trace entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
