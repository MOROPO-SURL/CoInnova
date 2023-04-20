namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class PrototypeDocumentBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.PrototypeDocumentBridge, Entities.Prototype, System.Guid, Entities.Document, System.Guid>
    {
        public PrototypeDocumentBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.DocumentBridges;
        }
    }
}
