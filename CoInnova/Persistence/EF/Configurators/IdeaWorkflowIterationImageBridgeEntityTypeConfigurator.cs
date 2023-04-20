namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class IdeaWorkflowIterationImageBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.IdeaWorkflowIterationImageBridge, Entities.IdeaWorkflowIteration, System.Guid, Framework.Guama.Media.Persistence.Entities.Image, System.Guid>
    {
        public IdeaWorkflowIterationImageBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.ImageBridges;
        }
    }
}
