namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class IdeaWorkflowIterationDocumentBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.IdeaWorkflowIterationDocumentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.IdeaWorkflowIterationDocumentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.IterationId, bridge.DocumentId });

            entityTypeBuilder.HasOne(bridge => bridge.Iteration).WithMany(entity => entity.DocumentBridges).HasForeignKey(bridge => bridge.IterationId);
            entityTypeBuilder.HasOne(bridge => bridge.Document).WithMany().HasForeignKey(bridge => bridge.DocumentId);
        }
    }
}
