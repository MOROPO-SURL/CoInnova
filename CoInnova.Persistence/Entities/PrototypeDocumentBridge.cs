namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class PrototypeDocumentBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Prototype, System.Guid, Document, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Prototype Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Document Stern { get; set; }
    }
}
