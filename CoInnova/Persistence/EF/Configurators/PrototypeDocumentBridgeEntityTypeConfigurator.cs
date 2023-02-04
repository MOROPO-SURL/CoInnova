namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class PrototypeDocumentBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.PrototypeDocumentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.PrototypeDocumentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.PrototypeId, bridge.DocumentId });

            entityTypeBuilder.HasOne(bridge => bridge.Prototype).WithMany(entity => entity.DocumentBridges).HasForeignKey(bridge => bridge.PrototypeId);
            entityTypeBuilder.HasOne(bridge => bridge.Document).WithMany().HasForeignKey(bridge => bridge.DocumentId);
        }
    }
}
