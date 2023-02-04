namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class Idea : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Idea()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
            DocumentBridges = new System.Collections.Generic.List<IdeaDocumentBridge>();
            CollaboratorBridges = new System.Collections.Generic.List<IdeaCollaboratorBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public System.DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FeatureImageId")]
        public SAPRESSC.Middleware.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid? FeatureImageId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkflowId")]
        public IdeaWorkflow Workflow { get; set; }
        public System.Guid? WorkflowId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ChallangeId")]
        public Challange Challange { get; set; }
        public System.Guid? ChallangeId { get; set; }

        public virtual System.Collections.Generic.List<IdeaDocumentBridge> DocumentBridges { get; set; }

        public virtual System.Collections.Generic.List<IdeaCollaboratorBridge> CollaboratorBridges { get; set; }
    }
}
