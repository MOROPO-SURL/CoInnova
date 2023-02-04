namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class EvaluationCollaboratorBridgeEntityTypeConfigurator : SAPRESSC.Core.Persistence.EF.Configurators.AbstractEntityTypeConfigurator<Entities.EvaluationCollaboratorBridge>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.EvaluationCollaboratorBridge> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasKey(bridge => new { bridge.EvaluationId, bridge.CollaboratorId });

            entityTypeBuilder.HasOne(bridge => bridge.Collaborator).WithMany().HasForeignKey(bridge => bridge.CollaboratorId);
            entityTypeBuilder.HasOne(bridge => bridge.Evaluation).WithMany(entity => entity.CollaboratorBridges).HasForeignKey(bridge => bridge.EvaluationId);
        }
    }
}
