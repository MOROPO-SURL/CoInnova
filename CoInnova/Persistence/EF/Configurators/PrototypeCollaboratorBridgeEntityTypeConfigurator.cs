namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class PrototypeCollaboratorBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.PrototypeCollaboratorBridge, Entities.Prototype, System.Guid, Entities.Collaborator, System.Guid>
    {
        public PrototypeCollaboratorBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.CollaboratorBridges;
        }
    }
}
