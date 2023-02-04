using Microsoft.AspNetCore.Mvc;
using MOROPO.SAPRESSC.Core.Web.Security.Managers.Facade;

namespace MOROPO.UIC.CoInnova.Components
{
    public class DefaultSidebarViewComponent : ViewComponent
    {
        private readonly IUserProfileManagerFacade userProfileManager;

        public DefaultSidebarViewComponent(IUserProfileManagerFacade userProfileManager)
        {
            this.userProfileManager = userProfileManager;
        }

        public IViewComponentResult Invoke()
        {
            return View((Models.View.UserProfileViewModel)userProfileManager.GetUserProfile(User.Identity.Name));
        }
    }
}
