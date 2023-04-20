namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaWorkflowStep : Framework.Guama.Core.Persistence.Entities.Contract.INamedEntity<System.Guid>
    {
        public IdeaWorkflowStep()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("NextId")]
        public IdeaWorkflowStep Next { get; set; }
        public System.Guid? NextId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkflowId")]
        public IdeaWorkflow Workflow { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? WorkflowId { get; set; }
    }
}
