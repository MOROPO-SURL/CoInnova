namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaWorkflowIterationDocumentBridge
    {
        public System.Guid IterationId { get; set; }

        public System.Guid DocumentId { get; set; }

        public IdeaWorkflowIteration Iteration { get; set; }

        public Document Document { get; set; }
    }
}
