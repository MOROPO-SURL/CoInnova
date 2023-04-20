namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeCollaboratorBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Challange, System.Guid, Collaborator, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Challange Prow { get; set; }
        
        public System.Guid SternId { get; set; }
        public Collaborator Stern { get; set; }
    }
}
