using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View.Backend
{
    public class PrototypeInputBackendViewModel : PrototypeInputViewModel
    {
        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "InnovationAgent")]
        public System.Guid? InnovationAgentId { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableInnovationAgents { get; set; }

        public override void Import(Prototype entity)
        {
            base.Import(entity);

            InnovationAgentId = entity.InnovationAgentId;
        }
    }
}
