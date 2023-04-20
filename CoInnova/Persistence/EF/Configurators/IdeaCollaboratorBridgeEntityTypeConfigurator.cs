namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class IdeaCollaboratorBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<Entities.IdeaCollaboratorBridge, Entities.Idea, System.Guid, Entities.Collaborator, System.Guid>
    {
        public IdeaCollaboratorBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.CollaboratorBridges;
            sternNavigationExpression = entity => entity.IdeaBridges;
        }
    }
}
