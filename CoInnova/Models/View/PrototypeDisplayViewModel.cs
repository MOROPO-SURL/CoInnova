using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class PrototypeDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Prototype, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.Guid? FeatureImageId { get; set; }

        public string FeatureImageExtension { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "CreatedAt")]
        public System.DateTime CreatedAt { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "IsPublic")]
        public bool IsPublic { get; set; }

        [Display(Name = "PrototypingLevel")]
        public PrototypingLevelDisplayViewModel Level { get; set; }

        [Display(Name = "InnovationAgent")]
        public CollaboratorDisplayViewModel InnovationAgent { get; set; }

        [Display(Name = "Summary")]
        public string Summary { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Idea")]
        public IdeaDisplayViewModel Idea { get; set; }

        public void Import(Persistence.Entities.Prototype entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Summary = entity.Summary;
            Description = entity.Description;
            IsPublic = entity.IsPublic;
            FeatureImageId = entity.FeatureImageId;
            FeatureImageExtension = entity.FeatureImage?.ContentType.ContentTypeToExtension();
            Level = new PrototypingLevelDisplayViewModel();
            InnovationAgent = new CollaboratorDisplayViewModel();

            Level.Import(entity.Level);
            InnovationAgent.Import(entity.InnovationAgent);

            if (entity.Idea != null)
            {
                Idea = new IdeaDisplayViewModel();

                Idea.Import(entity.Idea);
            }
        }
    }
}
