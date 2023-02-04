namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class PrototypeImageBridge
    {
        public System.Guid PrototypeId { get; set; }

        public System.Guid ImageId { get; set; }

        public Prototype Prototype { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image Image { get; set; }
    }
}
