namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ChallangeWorkflowIterationDocumentBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ChallangeWorkflowIterationDocumentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ChallangeWorkflowIterationDocumentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.IterationId, bridge.DocumentId });

            entityTypeBuilder.HasOne(bridge => bridge.Iteration).WithMany(entity => entity.DocumentBridges).HasForeignKey(bridge => bridge.IterationId);
            entityTypeBuilder.HasOne(bridge => bridge.Document).WithMany().HasForeignKey(bridge => bridge.DocumentId);
        }
    }
}
