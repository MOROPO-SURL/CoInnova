namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class PrototypeCollaboratorBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.PrototypeCollaboratorBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.PrototypeCollaboratorBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.CollaboratorId, bridge.PrototypeId });

            entityTypeBuilder.HasOne(bridge => bridge.Collaborator).WithMany().HasForeignKey(bridge => bridge.CollaboratorId);
            entityTypeBuilder.HasOne(bridge => bridge.Prototype).WithMany(entity => entity.CollaboratorBridges).HasForeignKey(bridge => bridge.PrototypeId);
        }
    }
}
