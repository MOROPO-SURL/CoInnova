namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class UseCaseDocumentBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.UseCaseDocumentBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.UseCaseDocumentBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.UseCaseId, bridge.DocumentId });

            entityTypeBuilder.HasOne(bridge => bridge.UseCase).WithMany(entity => entity.DocumentBridges).HasForeignKey(bridge => bridge.UseCaseId);
            entityTypeBuilder.HasOne(bridge => bridge.Document).WithMany().HasForeignKey(bridge => bridge.DocumentId);
        }
    }
}
