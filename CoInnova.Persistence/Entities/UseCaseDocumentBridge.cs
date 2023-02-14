namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class UseCaseDocumentBridge
    {
        public System.Guid UseCaseId { get; set; }

        public System.Guid DocumentId { get; set; }

        public UseCase UseCase { get; set; }

        public Document Document { get; set; }
    }
}
