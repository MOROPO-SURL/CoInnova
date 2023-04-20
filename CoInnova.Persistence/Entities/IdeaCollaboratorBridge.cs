namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class IdeaCollaboratorBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Idea, System.Guid, Collaborator, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Idea Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Collaborator Stern { get; set; }
    }
}
