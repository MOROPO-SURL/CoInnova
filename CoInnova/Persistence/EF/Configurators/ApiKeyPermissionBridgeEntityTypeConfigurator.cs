namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ApiKeyPermissionBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.ApiKeyPermissionBridge, Entities.ApiKey, System.Guid, Framework.Guama.Core.Security.Common.Persistence.Entities.Permission, int>
    {
        public ApiKeyPermissionBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.PermissionBridges;
        }
    }
}
