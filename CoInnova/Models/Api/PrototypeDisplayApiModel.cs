using System.Linq;

namespace MOROPO.UIC.CoInnova.Models.Api
{
    public class PrototypeDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Prototype, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public System.Guid PrototypeLevelId { get; set; }

        public string Description { get; set; }

        public System.Guid? IdeaId { get; set; }

        public ImageDisplayCommonModel FeatureImage { get; set; }

        public System.Collections.Generic.List<ImageDisplayCommonModel> Images { get; set; }

        public System.Collections.Generic.List<DocumentDisplayCommonModel> Documents { get; set; }

        public void Import(Persistence.Entities.Prototype entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            Name = entity.Name;
            Description = entity.Description;
            IsPublic = entity.IsPublic;
            PrototypeLevelId = entity.LevelId.Value;
            IdeaId = entity.IdeaId;
            Images = entity.ImageBridges.Select(bridge =>
            {
                var result = new ImageDisplayCommonModel();

                result.Import(bridge.Image);

                return result;
            }).ToList();
            Documents = entity.DocumentBridges.Select(bridge =>
            {
                var result = new DocumentDisplayCommonModel();

                result.Import(bridge.Document);

                return result;
            }).ToList();

            if (entity.FeatureImageId != null)
            {
                FeatureImage = new ImageDisplayCommonModel();

                FeatureImage.Import(entity.FeatureImage);
            }
        }
    }
}
