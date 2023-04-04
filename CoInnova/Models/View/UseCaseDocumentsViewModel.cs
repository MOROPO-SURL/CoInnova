namespace MOROPO.UIC.CoInnova.Models.View
{
    public class UseCaseDocumentsViewModel
    {
        public UseCaseDisplayViewModel UseCase { get; set; }

        public PrototypeDisplayViewModel Prototype { get; set; }

        public DocumentDisplayCommonModel[] Documents { get; set; }
    }
}
