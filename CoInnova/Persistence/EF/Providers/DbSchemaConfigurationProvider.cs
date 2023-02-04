namespace MOROPO.UIC.CoInnova.Persistence.EF.Providers
{
    public class DbSchemaConfigurationProvider : SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public System.Collections.Generic.ICollection<SAPRESSC.Core.Persistence.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return new System.Collections.Generic.List<SAPRESSC.Core.Persistence.EF.Configurators.IEntityTypeConfigurator>
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
                    new Configurators.CollaboratorEntityTypeConfigurator()
                };
            }
        }

        public string DefaultPersistenceSchema => null;
    }
}
