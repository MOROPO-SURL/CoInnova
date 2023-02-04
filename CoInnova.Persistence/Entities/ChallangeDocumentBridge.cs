namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeDocumentBridge
    {
        public System.Guid ChallangeId { get; set; }

        public System.Guid DocumentId { get; set; }

        public Challange Challange { get; set; }

        public Document Document { get; set; }
    }
}
