using Microsoft.AspNetCore.Mvc;

namespace MOROPO.UIC.CoInnova.Controllers.Frontend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.ChangeUserPasswordRequiredAction]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.CatchChangeUserPasswordRequiredException]
    public class ModerationController : ControllerBase
    {
        public IActionResult Index()
        {
            return RedirectToAction("index", "dashboard", new { Area = "moderation" });
        }
    }
}
