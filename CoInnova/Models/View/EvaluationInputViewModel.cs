using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class EvaluationInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.Evaluation, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Description")]
        public System.Guid? IdeaId { get; set; }

        [Display(Name = "Valoration")]
        [Range(1, 10)]
        public int Valoration { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Argument")]
        public string Argument { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableIdeaSelectListItems { get; set; }

        public Persistence.Entities.Evaluation Export()
        {
            var entity = new Persistence.Entities.Evaluation();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Evaluation entity)
        {
            Id = entity.Id;
            IdeaId = entity.IdeaId;
            Valoration = entity.Valoration;
            Argument = entity.Argument;
        }

        public void Mergue(Persistence.Entities.Evaluation entity)
        {
            entity.Valoration = Valoration;
            entity.Argument = Argument;
        }
    }
}
