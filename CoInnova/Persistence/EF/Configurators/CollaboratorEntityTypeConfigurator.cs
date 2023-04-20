namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class CollaboratorEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<Entities.Collaborator>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Collaborator> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(entity => entity.OrganizationIdentity).WithOne().HasForeignKey<Entities.Collaborator>(entity => entity.OrganizationIdentityId);
        }
    }
}
