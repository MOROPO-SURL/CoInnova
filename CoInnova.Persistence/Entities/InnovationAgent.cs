namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class InnovationAgent : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public InnovationAgent()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
        }

        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public Framework.Guama.Identity.Persistence.Entities.OrganizationIdentity Identity { get; set; }
        public System.Guid? IdentityId { get; set; }

        public System.Collections.Generic.List<InnovationAgentUserBridge> UserBridges { get; set; }
    }
}