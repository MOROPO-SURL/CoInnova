namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class PrototypeSphereBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Prototype, System.Guid, Sphere, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Prototype Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Sphere Stern { get; set; }
    }
}
