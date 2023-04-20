using MOROPO.Framework.Guama.Media.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class PrototypeImageBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Prototype, System.Guid, Image, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Prototype Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Image Stern { get; set; }
    }
}
