namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ChallangeWorkflowStepEditViewModel
    {
        public System.Guid Id { get; set; }

        public System.Guid WorkflowId { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        public System.Guid? PreviousId { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableSteps { get; set; }
    }
}
