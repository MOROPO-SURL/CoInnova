namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class PrototypeDocumentBridge
    {
        public System.Guid PrototypeId { get; set; }

        public System.Guid DocumentId { get; set; }

        public Prototype Prototype { get; set; }

        public Document Document { get; set; }
    }
}
