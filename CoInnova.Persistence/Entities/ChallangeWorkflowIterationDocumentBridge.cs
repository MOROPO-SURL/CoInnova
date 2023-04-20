namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeWorkflowIterationDocumentBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<ChallangeWorkflowIteration, System.Guid, Document, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public ChallangeWorkflowIteration Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Document Stern { get; set; }
    }
}
