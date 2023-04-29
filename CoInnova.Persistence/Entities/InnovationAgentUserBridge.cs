using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class InnovationAgentUserBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<InnovationAgent, System.Guid, User, string>
    {
        public System.Guid ProwId { get; set; }
        public InnovationAgent Prow { get; set; }

        public string SternId { get; set; }
        public User Stern { get; set; }
    }
}
