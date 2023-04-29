namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class ChallangeCollaboratorBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.ChallangeCollaboratorBridge, Entities.Challange, System.Guid, Entities.Collaborator, System.Guid>
    {
        public ChallangeCollaboratorBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.CollaboratorBridges;
        }
    }
}
