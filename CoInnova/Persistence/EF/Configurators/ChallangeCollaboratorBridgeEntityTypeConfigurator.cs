namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ChallangeCollaboratorBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.ChallangeCollaboratorBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.ChallangeCollaboratorBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.CollaboratorId, bridge.ChallangeId });

            entityTypeBuilder.HasOne(bridge => bridge.Collaborator).WithMany(entity => entity.ChallangeBridges).HasForeignKey(bridge => bridge.CollaboratorId);
            entityTypeBuilder.HasOne(bridge => bridge.Challange).WithMany(entity => entity.CollaboratorBridges).HasForeignKey(bridge => bridge.ChallangeId);
        }
    }
}
