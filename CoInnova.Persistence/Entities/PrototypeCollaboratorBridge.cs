namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class PrototypeCollaboratorBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Prototype, System.Guid, Collaborator, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Prototype Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Collaborator Stern { get; set; }
    }
}
