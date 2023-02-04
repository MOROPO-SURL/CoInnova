namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class IdeaWorkflowIterationImageBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.IdeaWorkflowIterationImageBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.IdeaWorkflowIterationImageBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.IterationId, bridge.ImageId });

            entityTypeBuilder.HasOne(bridge => bridge.Iteration).WithMany(entity => entity.ImageBridges).HasForeignKey(bridge => bridge.IterationId);
            entityTypeBuilder.HasOne(bridge => bridge.Image).WithMany().HasForeignKey(bridge => bridge.ImageId);
        }
    }
}
