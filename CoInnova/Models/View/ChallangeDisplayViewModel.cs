using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ChallangeDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Challange, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public ChallangeWorkflowDisplayViewModel Workflow { get; set; }

        [Display(Name = "InnovationAgent")]
        public InnovationAgentDisplayViewModel InnovationAgent { get; set; }

        public void Import(Persistence.Entities.Challange entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            CreatedAt = entity.CreatedAt;
            IsPublic = entity.IsPublic;
            InnovationAgent = new InnovationAgentDisplayViewModel();

            InnovationAgent.Import(entity.InnovationAgent);

            if (entity.WorkflowId != null)
            {
                Workflow = new ChallangeWorkflowDisplayViewModel();

                Workflow.Import(entity.Workflow);
            }
        }
    }
}
