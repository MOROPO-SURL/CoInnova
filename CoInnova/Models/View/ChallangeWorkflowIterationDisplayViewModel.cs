using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ChallangeWorkflowIterationDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.ChallangeWorkflowIteration, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; private set; }

        public string Summary { get; set; }

        public ChallangeWorkflowStepDisplayViewModel WorkflowStep { get; set; }

        public ImageDisplayCommonModel[] Images { get; set; }

        public DocumentDisplayCommonModel[] Documents { get; set; }

        public void Import(Persistence.Entities.ChallangeWorkflowIteration entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            Summary = entity.Summary;
            Images = entity.ImageBridges.Select(bridge => new ImageDisplayCommonModel
            {
                Id = bridge.SternId,
                Extension = bridge.Stern.ContentType.ContentTypeToExtension(),
                Width = bridge.Stern.Width,
                Height = bridge.Stern.Height
            }).ToArray();
            Documents = entity.DocumentBridges.Select(bridge => new DocumentDisplayCommonModel
            {
                Id = bridge.SternId,
                ContentType = bridge.Stern.ContentType,
                Length = bridge.Stern.Length,
                Description = bridge.Stern.Description,
                Name = bridge.Stern.Name
            }).ToArray();
            WorkflowStep = new ChallangeWorkflowStepDisplayViewModel
            {
                Id = entity.WorkflowStepId.Value,
                Name = entity.WorkflowStep.Name,
                Description = entity.WorkflowStep.Description
            };
        }
    }
}
