using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Frontend.Moderation
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.ChangeUserPasswordRequiredAction]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.CatchChangeUserPasswordRequiredException]
    [Area("moderation")]
    public class DashboardController : Framework.Guama.Core.Presentation.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<InnovationAgent, System.Guid> innovationAgentRepository;

        public DashboardController(IEntityRepository<InnovationAgent, System.Guid> innovationAgentRepository, Microsoft.Extensions.Localization.IStringLocalizer<DashboardController> localizer, ILogger<DashboardController> logger) : base(localizer, logger)
        {
            this.innovationAgentRepository = innovationAgentRepository;
        }

        public IActionResult Index()
        {
            System.Guid key = System.Guid.Empty;

            try
            {
                key = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Guid>(HttpContext.Session.GetString(Constants.Session.TARGET_INNOVATION_AGENT_ID));
            }
            catch
            {
                var agents = innovationAgentRepository.GetAll().ToArray();

                foreach (var agent in agents)
                {
                    if (!agent.UserBridges.Any(item => item.SternId == User.Identity.Name)) continue;

                    HttpContext.Session.SetString(Constants.Session.TARGET_INNOVATION_AGENT_ID, Newtonsoft.Json.JsonConvert.SerializeObject(agent.Id));

                    key = agent.Id;

                    break;
                }
            }

            try
            {
                var targetEntity = innovationAgentRepository.Get(key);

                var model = new Models.View.ModerationDashboardViewModel
                {
                    Target = new Models.View.InnovationAgentDisplayViewModel()
                };

                model.Target.Import(targetEntity);

                var entities = innovationAgentRepository.GetAll().ToList();

                var others = new System.Collections.Generic.List<Models.View.InnovationAgentDisplayViewModel>();

                while (entities.Count > 0)
                {
                    var entity = entities.First();

                    if ((entity.Id != key) && (!entity.UserBridges.Any(item => item.SternId == User.Identity.Name)))
                    {
                        var mod3l = new Models.View.InnovationAgentDisplayViewModel();

                        mod3l.Import(entity);

                        others.Add(mod3l);
                    }

                    entities.Remove(entity);
                }

                model.Others = others.ToArray();

                return View(model);
            }
            catch
            {
                return RedirectToAction("index", "home", new { Area = string.Empty });
            }
        }
    }
}
