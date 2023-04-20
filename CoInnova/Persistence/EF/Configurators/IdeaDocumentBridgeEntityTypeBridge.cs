namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class IdeaDocumentBridgeEntityTypeBridge : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.IdeaDocumentBridge, Entities.Idea, System.Guid, Entities.Document, System.Guid>
    {
        public IdeaDocumentBridgeEntityTypeBridge()
        {
            prowNavigationExpression = entity => entity.DocumentBridges;
        }
    }
}
