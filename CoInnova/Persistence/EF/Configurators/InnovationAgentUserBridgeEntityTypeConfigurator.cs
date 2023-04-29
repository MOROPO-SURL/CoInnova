using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.UIC.CoInnova.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Persistence.EF.Configurators
{
    public class InnovationAgentUserBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<InnovationAgentUserBridge, InnovationAgent, System.Guid, User, string>
    {
        public InnovationAgentUserBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.UserBridges;
        }
    }
}
