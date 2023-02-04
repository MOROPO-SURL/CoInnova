namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class EvaluationCollaboratorBridge
    {
        public System.Guid EvaluationId { get; set; }

        public System.Guid CollaboratorId { get; set; }

        public Evaluation Evaluation { get; set; }

        public Collaborator Collaborator { get; set; }
    }
}
