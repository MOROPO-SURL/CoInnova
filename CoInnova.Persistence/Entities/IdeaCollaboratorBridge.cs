namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaCollaboratorBridge
    {
        public System.Guid CollaboratorId { get; set; }

        public System.Guid IdeaId { get; set; }

        public Collaborator Collaborator { get; set; }

        public Idea Idea { get; set; }
    }
}
