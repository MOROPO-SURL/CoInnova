namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class PrototypeCollaboratorBridge
    {
        public System.Guid CollaboratorId { get; set; }

        public System.Guid PrototypeId { get; set; }

        public Collaborator Collaborator { get; set; }

        public Prototype Prototype { get; set; }
    }
}
