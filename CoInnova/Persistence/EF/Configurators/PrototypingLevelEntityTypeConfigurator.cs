namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class PrototypingLevelEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<Entities.PrototypingLevel>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.PrototypingLevel> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasIndex(entity => entity.Numerator).IsUnique();
        }
    }
}
