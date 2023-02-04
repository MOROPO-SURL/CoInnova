namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaWorkflow : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public IdeaWorkflow()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("StartStepId")]
        public IdeaWorkflowStep StartStep { get; set; }
        public System.Guid? StartStepId { get; set; }
    }
}
