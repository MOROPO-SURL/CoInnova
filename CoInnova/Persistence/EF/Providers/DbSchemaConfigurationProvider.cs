namespace MOROPO.UIC.CoInnova.Persistence.EF.Providers
{
    public class DbSchemaConfigurationProvider : Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public System.Collections.Generic.ICollection<Framework.Guama.Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return new System.Collections.Generic.List<Framework.Guama.Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator>
                {
                    new Configurators.ChallangeWorkflowIterationImageBridgeEntityTypeConfigurator(),
                    new Configurators.ChallangeCollaboratorBridgeEntityTypeConfigurator(),
                    new Configurators.IdeaCollaboratorBridgeEntityTypeConfigurator(),
                    new Configurators.IdeaWorkflowIterationImageBridgeEntityTypeConfigurator(),
                    new Configurators.ChallangeWorkflowIterationDocumentBridgeEntityTypeConfigurator(),
                    new Configurators.IdeaWorkflowIterationDocumentBridgeEntityTypeConfigurator(),
                    new Configurators.ChallangeDocumentBridgeEntityTypeBridge(),
                    new Configurators.IdeaDocumentBridgeEntityTypeBridge(),
                    new Configurators.EvaluationCollaboratorBridgeEntityTypeConfigurator(),
                    new Configurators.PrototypeDocumentBridgeEntityTypeConfigurator(),
                    new Configurators.PrototypeImageBridgeEntityTypeConfigurator(),
                    new Configurators.PrototypingLevelEntityTypeConfigurator(),
                    new Configurators.ApiKeyPermissionBridgeEntityTypeConfigurator(),
                    new Configurators.CollaboratorEntityTypeConfigurator(),
                    new Configurators.PrototypeSphereBridgeEntityTypeConfigurator(),
                    new Configurators.UseCaseDocumentBridgeEntityTypeConfigurator(),
                    new Configurators.UseCaseImageBridgeEntityTypeConfigurator(),
                    new Configurators.PrototypeCollaboratorBridgeEntityTypeConfigurator(),
                    new Configurators.InnovationAgentEntityTypeConfigurator(),
                    new Configurators.InnovationAgentUserBridgeEntityTypeConfigurator()
                };
            }
        }

        public string DefaultPersistenceSchema => null;
    }
}
