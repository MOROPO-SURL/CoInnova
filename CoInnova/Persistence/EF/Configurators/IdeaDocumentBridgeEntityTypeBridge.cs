namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class IdeaDocumentBridgeEntityTypeBridge : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.IdeaDocumentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.IdeaDocumentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.IdeaId, bridge.DocumentId });

            entityTypeBuilder.HasOne(bridge => bridge.Idea).WithMany(entity => entity.DocumentBridges).HasForeignKey(bridge => bridge.IdeaId);
            entityTypeBuilder.HasOne(bridge => bridge.Document).WithMany().HasForeignKey(bridge => bridge.DocumentId);
        }
    }
}
