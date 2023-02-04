using System.Linq;

namespace MOROPO.UIC.CoInnova.Models.Api
{
    public class IdeaDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Idea, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public ImageDisplayCommonModel FeatureImage { get; set; }

        public System.Guid? ChallangeId { get; set; }

        public System.Collections.Generic.List<DocumentDisplayCommonModel> Documents { get; set; }

        public void Import(Persistence.Entities.Idea entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            Title = entity.Title;
            Description = entity.Description;
            IsPublic = entity.IsPublic;
            ChallangeId = entity.ChallangeId;
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
