using Microsoft.EntityFrameworkCore;
using MOROPO.SAPRESSC.Core.Security.Common.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Identity.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Media.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Region.Persistence.Entities;
using MOROPO.SAPRESSC.Middleware.Security.Identity.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Persistence.EF
{
    public class ApplicationDbContext :
        SAPRESSC.Core.Persistence.EF.Context.AbstractTraceableDbContext,
        SAPRESSC.Core.Security.Common.Persistence.EF.Contract.IModulePersistenceDbContext,
        SAPRESSC.Middleware.Security.Identity.Persistence.EF.Contract.IModulePersistenceDbContext,
        SAPRESSC.Middleware.Identity.Persistence.EF.Context.IIdentityMiddlewarePersistenceDbContext
    {
        public ApplicationDbContext(System.Collections.Generic.IEnumerable<SAPRESSC.Core.Persistence.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade> dbSchemaConfigurationProviders, DbContextOptions options) : base(dbSchemaConfigurationProviders, options)
        {
        }

        //#if DEBUG
        //        public override bool TraceChanges => false;
        //#endif

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermissionBridge> RolePermissionBridges { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoleBridge> UserRoleBridges { get; set; }

        public DbSet<Thumbnail> Thumbnails { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Video> Videos { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Locality> Localities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<CountryRegion> CountryRegions { get; set; }
        public DbSet<StateRegion> StateRegions { get; set; }
        public DbSet<MunicipalityRegion> MunicipalityRegions { get; set; }
        public DbSet<LocalityRegion> LocalityRegions { get; set; }

        public DbSet<PersonIdentity> PersonIdentities { get; set; }
        public DbSet<OrganizationPostalContactItemBridge> OrganizationPostalContactItemBridges { get; set; }
        public DbSet<OrganizationPhoneContactItemBridge> OrganizationPhoneContactItemBridges { get; set; }
        public DbSet<OrganizationEmailContactItemBridge> OrganizationEmailContactItemBridges { get; set; }
        public DbSet<PersonWebSiteContactItemBridge> PersonWebSiteContactItemBridges { get; set; }
        public DbSet<PersonSocialContactItemBridge> PersonSocialContactItemBridges { get; set; }
        public DbSet<PersonPostalContactItemBridge> PersonPostalContactItemBridges { get; set; }
        public DbSet<PersonPhoneContactItemBridge> PersonPhoneContactItemBridges { get; set; }
        public DbSet<PersonEmailContactItemBridge> PersonEmailContactItemBridges { get; set; }
        public DbSet<PostalContactItem> PostalContactItems { get; set; }
        public DbSet<WebSiteContactItem> WebSiteContactItems { get; set; }
        public DbSet<SocialContactItem> SocialContactItems { get; set; }
        public DbSet<PhoneContactItem> PhoneContactItems { get; set; }
        public DbSet<EmailContactItem> EmailContactItems { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<OrganizationIdentity> OrganizationIdentities { get; set; }
        public DbSet<OrganizationSocialContactItemBridge> OrganizationSocialContactItemBridges { get; set; }
        public DbSet<OrganizationWebSiteContactItemBridge> OrganizationWebSiteContactItemBridges { get; set; }

        public DbSet<Entities.Collaborator> Collaborators { get; set; }
        public DbSet<Entities.Document> Documents { get; set; }
        public DbSet<Entities.Challange> Challanges { get; set; }
        public DbSet<Entities.ChallangeCollaboratorBridge> ChallangeCollaboratorBridges { get; set; }
        public DbSet<Entities.ChallangeWorkflow> ChallangeWorkflows { get; set; }
        public DbSet<Entities.ChallangeWorkflowStep> ChallangeWorkflowSteps { get; set; }
        public DbSet<Entities.ChallangeWorkflowIteration> ChallangeWorkflowIterations { get; set; }
        public DbSet<Entities.ChallangeWorkflowIterationDocumentBridge> ChallangeWorkflowIterationDocumentBridges { get; set; }
        public DbSet<Entities.ChallangeWorkflowIterationImageBridge> ChallangeWorkflowIterationsImageBridges { get; set; }
        public DbSet<Entities.Idea> Ideas { get; set; }
        public DbSet<Entities.IdeaCollaboratorBridge> IdeaCollaboratorBridges { get; set; }
        public DbSet<Entities.IdeaWorkflowStep> IdeaWorkflowSteps { get; set; }
        public DbSet<Entities.IdeaWorkflowIteration> IdeaWorkflowIterations { get; set; }
        public DbSet<Entities.IdeaWorkflowIterationDocumentBridge> IdeaWorkflowIterationDocumentBridges { get; set; }
        public DbSet<Entities.IdeaWorkflowIterationImageBridge> IdeaWorkflowIterationImageBridges { get; set; }
        public DbSet<Entities.ChallangeDocumentBridge> ChallangeDocumentBridges { get; set; }
        public DbSet<Entities.IdeaDocumentBridge> IdeaDocumentBridges { get; set; }
        public DbSet<Entities.Evaluation> Evaluations { get; set; }
        public DbSet<Entities.EvaluationCollaboratorBridge> EvaluationCollaboratorBridges { get; set; }
        public DbSet<Entities.ChallangeComment> ChallangeComments { get; set; }
        public DbSet<Entities.IdeaComment> IdeaComments { get; set; }
        public DbSet<Entities.Prototype> Prototypes { get; set; }
        public DbSet<Entities.PrototypeDocumentBridge> PrototypeDocumentBridges { get; set; }
        public DbSet<Entities.PrototypeImageBridge> PrototypeImageBridges { get; set; }
        public DbSet<Entities.PrototypingLevel> PrototypingLevels { get; set; }
        public DbSet<Entities.ApiKey> ApiKeys { get; set; }
    }
}
