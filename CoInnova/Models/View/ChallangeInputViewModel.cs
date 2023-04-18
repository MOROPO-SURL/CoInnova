using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ChallangeInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.Challange, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Public?")]
        public bool IsPublic { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Workflow")]
        public System.Guid? WorkflowId { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableWorkflowSelectListItems { get; set; }

        public Challange Export()
        {
            var entity = new Challange();

            Mergue(entity);

            return entity;
        }

        public void Import(Challange entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            IsPublic = entity.IsPublic;
            WorkflowId = entity.WorkflowId;
        }

        public void Mergue(Challange entity)
        {
            entity.Title = Title;
            entity.Description = Description;
            entity.IsPublic = IsPublic;
        }
    }
}
