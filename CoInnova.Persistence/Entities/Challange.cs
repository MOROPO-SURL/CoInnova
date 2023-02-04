namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class Challange : SAPRESSC.Core.Persistence.Entities.IEntity<System.Guid>
    {
        public Challange()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
            CollaboratorBridges = new System.Collections.Generic.List<ChallangeCollaboratorBridge>();
            DocumentBridges = new System.Collections.Generic.List<ChallangeDocumentBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public System.DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("WorkflowId")]
        public ChallangeWorkflow Workflow { get; set; }
        public System.Guid? WorkflowId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FeatureImageId")]
        public SAPRESSC.Middleware.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid? FeatureImageId { get; set; }

        public virtual System.Collections.Generic.List<ChallangeCollaboratorBridge> CollaboratorBridges { get; set; }

        public virtual System.Collections.Generic.List<ChallangeDocumentBridge> DocumentBridges { get; set; }
    }
}
