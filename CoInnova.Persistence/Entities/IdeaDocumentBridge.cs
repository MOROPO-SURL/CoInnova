namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaDocumentBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Idea, System.Guid, Document, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Idea Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Document Stern { get; set; }
    }
}
