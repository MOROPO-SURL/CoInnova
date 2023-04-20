namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class PrototypeImageBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.PrototypeImageBridge, Entities.Prototype, System.Guid, Framework.Guama.Media.Persistence.Entities.Image, System.Guid>
    {
        public PrototypeImageBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.ImageBridges;
        }
    }
}
