namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class Evaluation : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Evaluation()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
            CollaboratorBridges = new System.Collections.Generic.List<EvaluationCollaboratorBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdeaId")]
        public Idea Idea { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? IdeaId { get; set; }

        public int Valoration { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Argument { get; set; }

        public System.Collections.Generic.List<EvaluationCollaboratorBridge> CollaboratorBridges { get; set; } 
    }
}
