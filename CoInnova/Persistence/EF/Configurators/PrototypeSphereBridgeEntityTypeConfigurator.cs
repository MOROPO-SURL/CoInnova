namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class PrototypeSphereBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.PrototypeSphereBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.PrototypeSphereBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.PrototypeId, bridge.SphereId });

            entityTypeBuilder.HasOne(bridge => bridge.Prototype).WithMany(entity => entity.SphereBridges).HasForeignKey(bridge => bridge.PrototypeId);
            entityTypeBuilder.HasOne(bridge => bridge.Sphere).WithMany().HasForeignKey(bridge => bridge.SphereId);
        }
    }
}
