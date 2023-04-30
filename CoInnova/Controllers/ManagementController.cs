namespace MOROPO.UIC.CoInnova.Controllers
{
    public class ManagementController : Microsoft.AspNetCore.Mvc.Controller
    {
        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            return Redirect("management/dashboard");
        }
    }
}
