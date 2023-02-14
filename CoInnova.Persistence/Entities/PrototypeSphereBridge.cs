namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class PrototypeSphereBridge
    {
        public System.Guid PrototypeId { get; set; }

        public System.Guid SphereId { get; set; }

        public Prototype Prototype { get; set; }

        public Sphere Sphere { get; set; }
    }
}
