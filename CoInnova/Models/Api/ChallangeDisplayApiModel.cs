using System.Linq;

namespace MOROPO.UIC.CoInnova.Models.Api
{
    public class ChallangeDisplayApiModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Challange, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public System.Guid? WorkflowId { get; set; }

        public ImageDisplayCommonModel FeatureImage { get; set; }

        public System.Collections.Generic.List<DocumentDisplayCommonModel> Documents { get; set; }

        public void Import(Persistence.Entities.Challange entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            IsPublic = entity.IsPublic;
            CreatedAt = entity.CreatedAt;
            WorkflowId = entity.WorkflowId;
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
