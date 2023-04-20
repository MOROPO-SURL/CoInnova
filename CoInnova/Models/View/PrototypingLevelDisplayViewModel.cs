using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class PrototypingLevelDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.PrototypingLevel, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Numerator")]
        public int Numerator { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public void Import(Persistence.Entities.PrototypingLevel entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Numerator = entity.Numerator;
        }
    }
}
