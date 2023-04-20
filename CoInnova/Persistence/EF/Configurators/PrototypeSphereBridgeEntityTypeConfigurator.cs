namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class PrototypeSphereBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.PrototypeSphereBridge, Entities.Prototype, System.Guid, Entities.Sphere, System.Guid>
    {
        public PrototypeSphereBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.SphereBridges;
        }
    }
}
