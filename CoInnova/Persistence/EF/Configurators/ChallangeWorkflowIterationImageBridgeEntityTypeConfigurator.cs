namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ChallangeWorkflowIterationImageBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ChallangeWorkflowIterationImageBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ChallangeWorkflowIterationImageBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.IterationId, bridge.ImageId });

            entityTypeBuilder.HasOne(bridge => bridge.Iteration).WithMany(entity => entity.ImageBridges).HasForeignKey(bridge => bridge.IterationId);
            entityTypeBuilder.HasOne(bridge => bridge.Image).WithMany().HasForeignKey(bridge => bridge.ImageId);
        }
    }
}
