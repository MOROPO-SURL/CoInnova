namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ChallangeDocumentBridgeEntityTypeBridge : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ChallangeDocumentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ChallangeDocumentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.ChallangeId, bridge.DocumentId });

            entityTypeBuilder.HasOne(bridge => bridge.Challange).WithMany(entity => entity.DocumentBridges).HasForeignKey(bridge => bridge.ChallangeId);
            entityTypeBuilder.HasOne(bridge => bridge.Document).WithMany().HasForeignKey(bridge => bridge.DocumentId);
        }
    }
}
