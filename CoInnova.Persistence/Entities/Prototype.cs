namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class Prototype : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public Prototype()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
            ImageBridges = new System.Collections.Generic.List<PrototypeImageBridge>();
            DocumentBridges = new System.Collections.Generic.List<PrototypeDocumentBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public bool IsPublic { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("LevelId")]
        public PrototypingLevel Level { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? LevelId { get; set; }

        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("IdeaId")]
        public Idea Idea { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? IdeaId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FeatureImageId")]
        public SAPRESSC.Middleware.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid? FeatureImageId { get; set; }

        public System.Collections.Generic.List<PrototypeImageBridge> ImageBridges { get; set; }

        public System.Collections.Generic.List<PrototypeDocumentBridge> DocumentBridges { get; set; }
    }
}
