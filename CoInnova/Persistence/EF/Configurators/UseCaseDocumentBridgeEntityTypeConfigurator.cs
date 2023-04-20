namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class UseCaseDocumentBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.UseCaseDocumentBridge, Entities.UseCase, System.Guid, Entities.Document, System.Guid>
    {
        public UseCaseDocumentBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.DocumentBridges;
        }
    }
}
