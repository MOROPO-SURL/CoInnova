using MOROPO.Framework.Guama.Media.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaWorkflowIterationImageBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<IdeaWorkflowIteration, System.Guid, Image, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public IdeaWorkflowIteration Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Image Stern { get; set; }
    }
}
