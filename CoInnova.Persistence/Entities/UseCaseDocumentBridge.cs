namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class UseCaseDocumentBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<UseCase, System.Guid, Document, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public UseCase Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Document Stern { get; set; }
    }
}
