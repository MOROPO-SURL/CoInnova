namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeWorkflowStep : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public ChallangeWorkflowStep()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("NextId")]
        public ChallangeWorkflowStep Next { get; set; }
        public System.Guid? NextId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkflowId")]
        public ChallangeWorkflow Workflow { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? WorkflowId { get; set; }
    }
}
