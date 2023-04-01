namespace MOROPO.UIC.CoInnova.Models.View
{
    public class UseCaseImagesViewModel
    {
        public UseCaseDisplayViewModel UseCase { get; set; }

        public PrototypeDisplayViewModel Prototype { get; set; }

        public ImageDisplayCommonModel FeatureImage { get; set; }

        public ImageDisplayCommonModel[] Images { get; set; }
    }
}
