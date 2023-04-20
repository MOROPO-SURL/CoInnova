namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeDocumentBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Challange, System.Guid, Document, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Challange Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Document Stern { get; set; }
    }
}
