namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class Prototype : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public Prototype()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
            ImageBridges = new System.Collections.Generic.List<PrototypeImageBridge>();
            DocumentBridges = new System.Collections.Generic.List<PrototypeDocumentBridge>();
            SphereBridges = new System.Collections.Generic.List<PrototypeSphereBridge>();
            CollaboratorBridges = new System.Collections.Generic.List<PrototypeCollaboratorBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }

        public bool IsPublic { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("LevelId")]
        public PrototypingLevel Level { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? LevelId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("InnovationAgentId")]
        public Collaborator InnovationAgent { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? InnovationAgentId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Summary { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdeaId")]
        public Idea Idea { get; set; }
        public System.Guid? IdeaId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FeatureImageId")]
        public Framework.Guama.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid? FeatureImageId { get; set; }

        public System.Collections.Generic.List<PrototypeImageBridge> ImageBridges { get; set; }

        public System.Collections.Generic.List<PrototypeDocumentBridge> DocumentBridges { get; set; }

        public System.Collections.Generic.List<PrototypeSphereBridge> SphereBridges { get; set; }

        public System.Collections.Generic.List<PrototypeCollaboratorBridge> CollaboratorBridges { get; set; }
    }
}
