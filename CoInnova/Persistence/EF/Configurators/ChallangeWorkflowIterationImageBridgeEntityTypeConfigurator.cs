namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ChallangeWorkflowIterationImageBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.ChallangeWorkflowIterationImageBridge, Entities.ChallangeWorkflowIteration, System.Guid, Framework.Guama.Media.Persistence.Entities.Image, System.Guid>
    {
        public ChallangeWorkflowIterationImageBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.ImageBridges;
        }
    }
}
