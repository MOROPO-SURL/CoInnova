namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class UseCaseImageBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.UseCaseImageBridge, Entities.UseCase, System.Guid, Framework.Guama.Media.Persistence.Entities.Image, System.Guid>
    {
        public UseCaseImageBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.ImageBridges;
        }
    }
}
