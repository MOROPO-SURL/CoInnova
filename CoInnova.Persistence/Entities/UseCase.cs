namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class UseCase : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public UseCase()
        {
            Id = System.Guid.NewGuid();
            ImageBridges = new System.Collections.Generic.List<UseCaseImageBridge>();
            DocumentBridges = new System.Collections.Generic.List<UseCaseDocumentBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("PrototypeId")]
        public Prototype Prototype { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? PrototypeId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("BenefitedId")]
        public Collaborator Benefited { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? BenefitedId { get; set; }

        public string About { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("FeatureImageId")]
        public Framework.Guama.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid? FeatureImageId { get; set; }

        public System.Collections.Generic.List<UseCaseImageBridge> ImageBridges { get; set; }

        public System.Collections.Generic.List<UseCaseDocumentBridge> DocumentBridges { get; set; }
    }
}
