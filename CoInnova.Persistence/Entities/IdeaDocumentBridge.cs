namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaDocumentBridge
    {
        public System.Guid IdeaId { get; set; }

        public System.Guid DocumentId { get; set; }

        public Idea Idea { get; set; }

        public Document Document { get; set; }
    }
}
