using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class UseCaseInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<UseCase, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string About { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Benefited")]
        public System.Guid? BenefitedId { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableCollaborators { get; set; }

        public PrototypeDisplayViewModel Prototype { get; set; }

        public UseCase Export()
        {
            var entity = new UseCase();

            Mergue(entity);

            return entity;
        }

        public void Import(UseCase entity)
        {
            Id = entity.Id;
            About = entity.About;
            BenefitedId = entity.BenefitedId.Value;
        }

        public void Mergue(UseCase entity)
        {
            entity.About = About;
        }
    }
}
