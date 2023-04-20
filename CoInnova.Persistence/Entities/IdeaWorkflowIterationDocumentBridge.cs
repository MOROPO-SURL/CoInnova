namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaWorkflowIterationDocumentBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<IdeaWorkflowIteration, System.Guid, Document, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public IdeaWorkflowIteration Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Document Stern { get; set; }
    }
}
