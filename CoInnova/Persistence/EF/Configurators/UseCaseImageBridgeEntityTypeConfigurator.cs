namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class UseCaseImageBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.UseCaseImageBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.UseCaseImageBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.UseCaseId, bridge.ImageId });

            entityTypeBuilder.HasOne(bridge => bridge.UseCase).WithMany(entity => entity.ImageBridges).HasForeignKey(bridge => bridge.UseCaseId);
            entityTypeBuilder.HasOne(bridge => bridge.Image).WithMany().HasForeignKey(bridge => bridge.ImageId);
        }
    }
}
