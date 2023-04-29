namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class InnovationAgentEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<Entities.InnovationAgent>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.InnovationAgent> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(entity => entity.Identity).WithOne().HasForeignKey<Entities.InnovationAgent>(entity => entity.IdentityId);
        }
    }
}
