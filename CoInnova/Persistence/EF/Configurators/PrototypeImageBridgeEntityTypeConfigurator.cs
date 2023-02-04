namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class PrototypeImageBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.PrototypeImageBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.PrototypeImageBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.PrototypeId, bridge.ImageId });

            entityTypeBuilder.HasOne(bridge => bridge.Prototype).WithMany(entity => entity.ImageBridges).HasForeignKey(bridge => bridge.PrototypeId);
            entityTypeBuilder.HasOne(bridge => bridge.Image).WithMany().HasForeignKey(bridge => bridge.ImageId);
        }
    }
}
