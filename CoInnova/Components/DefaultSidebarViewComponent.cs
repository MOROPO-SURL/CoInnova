using Microsoft.AspNetCore.Mvc;

namespace MOROPO.UIC.CoInnova.Components
{
    public class DefaultSidebarViewComponent : ViewComponent
    {
        private readonly Framework.Guama.Core.Presentation.Web.Security.Managers.Facade.IUserProfileManagerFacade userProfileManager;

        public DefaultSidebarViewComponent(Framework.Guama.Core.Presentation.Web.Security.Managers.Facade.IUserProfileManagerFacade userProfileManager)
        {
            this.userProfileManager = userProfileManager;
        }

        public IViewComponentResult Invoke()
        {
            return View((Models.View.UserProfileViewModel)userProfileManager.GetUserProfile(User.Identity.Name));
        }
    }
}
