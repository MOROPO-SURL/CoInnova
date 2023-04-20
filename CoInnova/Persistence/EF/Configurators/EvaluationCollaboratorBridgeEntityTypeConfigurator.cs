namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class EvaluationCollaboratorBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.EvaluationCollaboratorBridge, Entities.Evaluation, System.Guid, Entities.Collaborator, System.Guid>
    {
        public EvaluationCollaboratorBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.CollaboratorBridges;
        }
    }
}
