namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class EvaluationCollaboratorBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Evaluation, System.Guid, Collaborator, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Evaluation Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Collaborator Stern { get; set; }
    }
}
