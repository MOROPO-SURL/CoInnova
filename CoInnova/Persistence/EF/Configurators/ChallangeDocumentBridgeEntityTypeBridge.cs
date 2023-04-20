namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ChallangeDocumentBridgeEntityTypeBridge : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.ChallangeDocumentBridge, Entities.Challange, System.Guid, Entities.Document, System.Guid>
    {
        public ChallangeDocumentBridgeEntityTypeBridge()
        {
            prowNavigationExpression = entity => entity.DocumentBridges;
        }
    }
}
