namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaWorkflowIterationImageBridge
    {
        public System.Guid IterationId { get; set; }

        public System.Guid ImageId { get; set; }

        public IdeaWorkflowIteration Iteration { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image Image { get; set; }
    }
}
