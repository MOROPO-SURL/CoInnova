namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeWorkflowIterationDocumentBridge
    {
        public System.Guid IterationId { get; set; }

        public System.Guid DocumentId { get; set; }

        public ChallangeWorkflowIteration Iteration { get; set; }

        public Document Document { get; set; }
    }
}
