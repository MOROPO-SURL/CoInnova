namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class IdeaWorkflowIterationDocumentBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.IdeaWorkflowIterationDocumentBridge, Entities.IdeaWorkflowIteration, System.Guid, Entities.Document, System.Guid>
    {
        public IdeaWorkflowIterationDocumentBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.DocumentBridges;
        }
    }
}
