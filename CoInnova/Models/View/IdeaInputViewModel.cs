using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class IdeaInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Idea, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "IsPublic")]
        public bool IsPublic { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Workflow")]
        public System.Guid? WorkflowId { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableWorkflowSelectListItems { get; set; }

        [Display(Name = "Challange")]
        public System.Guid? ChallangeId { get; set; }
        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableChallangeSelectListItems { get; set; }

        public Idea Export()
        {
            var entity = new Idea();

            Mergue(entity);

            return entity;
        }

        public virtual void Import(Idea entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            IsPublic = entity.IsPublic;
            WorkflowId = entity.WorkflowId;
            ChallangeId = entity.ChallangeId;
        }

        public void Mergue(Idea entity)
        {
            entity.Title = Title;
            entity.Description = Description;
            entity.IsPublic = IsPublic;
        }
    }
}
