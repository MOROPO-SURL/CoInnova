namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeWorkflowIteration : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public ChallangeWorkflowIteration()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
            ImageBridges = new System.Collections.Generic.List<ChallangeWorkflowIterationImageBridge>();
            DocumentBridges = new System.Collections.Generic.List<ChallangeWorkflowIterationDocumentBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; private set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Summary { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ChallangeId")]
        public Challange Challange { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? ChallangeId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkflowStepId")]
        public ChallangeWorkflowStep WorkflowStep { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? WorkflowStepId { get; set; }

        public virtual System.Collections.Generic.List<ChallangeWorkflowIterationImageBridge> ImageBridges { get; set; }

        public virtual System.Collections.Generic.List<ChallangeWorkflowIterationDocumentBridge> DocumentBridges { get; set; }
    }
}
