namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaWorkflowIteration : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public IdeaWorkflowIteration()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
            ImageBridges = new System.Collections.Generic.List<IdeaWorkflowIterationImageBridge>();
            DocumentBridges = new System.Collections.Generic.List<IdeaWorkflowIterationDocumentBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; private set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Summary { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdeaId")]
        public Idea Idea { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? IdeaId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkflowStepId")]
        public ChallangeWorkflowStep WorkflowStep { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? WorkflowStepId { get; set; }

        public virtual System.Collections.Generic.List<IdeaWorkflowIterationImageBridge> ImageBridges { get; set; }

        public virtual System.Collections.Generic.List<IdeaWorkflowIterationDocumentBridge> DocumentBridges { get; set; }
    }
}