namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeCollaboratorBridge
    {
        public System.Guid CollaboratorId { get; set; }

        public System.Guid ChallangeId { get; set; }

        public Collaborator Collaborator { get; set; }

        public Challange Challange { get; set; }
    }
}
