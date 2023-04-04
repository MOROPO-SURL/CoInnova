using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class UseCaseDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.UseCase, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string About { get; set; }

        public System.Guid? FeatureImageId { get; set; }

        public string FeatureImageExtension { get; set; }

        public bool IsFeatureImageLandscape { get; set; }

        public CollaboratorDisplayViewModel Collaborator { get; set; }

        public void Import(Persistence.Entities.UseCase entity)
        {
            Id = entity.Id;
            About = entity.About;
            FeatureImageId = entity.FeatureImageId;
            FeatureImageExtension = entity.FeatureImage?.ContentType.ContentTypeToExtension();
            IsFeatureImageLandscape = entity.FeatureImage?.Width > entity.FeatureImage?.Height; 
            Collaborator = new CollaboratorDisplayViewModel();

            Collaborator.Import(entity.Benefited);
        }
    }
}
