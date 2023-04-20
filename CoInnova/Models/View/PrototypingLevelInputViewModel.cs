using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class PrototypingLevelInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.PrototypingLevel, System.Guid>
    {
        public PrototypingLevelInputViewModel()
        {
            AvailableNumbers = new System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
        }

        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Numerator")]
        public int Numerator { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public System.Collections.Generic.List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> AvailableNumbers { get; set; }

        public void Import(Persistence.Entities.PrototypingLevel entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            Numerator = entity.Numerator;
        }

        public void Mergue(Persistence.Entities.PrototypingLevel entity) 
        {
            entity.Name = Name;
            entity.Numerator = Numerator;
            entity.Description = Description;
        }

        public Persistence.Entities.PrototypingLevel Export()
        {
            var entity = new Persistence.Entities.PrototypingLevel();

            Mergue(entity);

            return entity;
        }
    }
}
