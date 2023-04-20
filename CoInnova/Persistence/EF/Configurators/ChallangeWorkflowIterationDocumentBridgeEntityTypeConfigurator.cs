namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ChallangeWorkflowIterationDocumentBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.ChallangeWorkflowIterationDocumentBridge, Entities.ChallangeWorkflowIteration, System.Guid, Entities.Document, System.Guid>
    {
        public ChallangeWorkflowIterationDocumentBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.DocumentBridges;
        }
    }
}
