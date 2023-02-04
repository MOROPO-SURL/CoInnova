namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeWorkflowIterationImageBridge
    {
        public System.Guid IterationId { get; set; }

        public System.Guid ImageId { get; set; }

        public ChallangeWorkflowIteration Iteration { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image Image { get; set; }
    }
}
