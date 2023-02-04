namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class IdeaCollaboratorBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.IdeaCollaboratorBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.IdeaCollaboratorBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.CollaboratorId, bridge.IdeaId });

            entityTypeBuilder.HasOne(bridge => bridge.Collaborator).WithMany(entity => entity.IdeaBridges).HasForeignKey(bridge => bridge.CollaboratorId);
            entityTypeBuilder.HasOne(bridge => bridge.Idea).WithMany(entity => entity.CollaboratorBridges).HasForeignKey(bridge => bridge.IdeaId);
        }
    }
}
