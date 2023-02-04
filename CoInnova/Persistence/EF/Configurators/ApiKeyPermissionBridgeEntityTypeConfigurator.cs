namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ApiKeyPermissionBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ApiKeyPermissionBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ApiKeyPermissionBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ApiKeyId, bridge.PermissionId });

            entityTypeBuilder.HasOne(bridge => bridge.ApiKey).WithMany(entity => entity.PermissionBridges).HasForeignKey(bridge => bridge.ApiKeyId);
            entityTypeBuilder.HasOne(bridge => bridge.Permission).WithMany().HasForeignKey(bridge => bridge.PermissionId);
        }
    }
}
