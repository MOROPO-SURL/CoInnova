using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Components
{
    public class AutenticatedUserOptionListItemsViewComponent : ViewComponent
    {
        public readonly IEntityRepository<InnovationAgent, System.Guid> innovationAgentRepository;

        public AutenticatedUserOptionListItemsViewComponent(IEntityRepository<InnovationAgent, System.Guid> innovationAgentRepository)
        {
            this.innovationAgentRepository = innovationAgentRepository;
        }

        public IViewComponentResult Invoke()
        {
            var agents = innovationAgentRepository.GetAll().ToArray();

            foreach (var agent in agents)
            {
                return View(agent.UserBridges.Any(item => item.SternId == User.Identity.Name));
            }

            return View(false);
        }
    }
}
