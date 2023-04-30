namespace MOROPO.UIC.CoInnova.Models.View
{
    public class InnovationAgentModeratorsViewModel
    {
        public InnovationAgentDisplayViewModel Agent { get; set; }

        public Framework.Guama.Security.Personal.Presentation.Web.Backend.Models.View.UserDisplayViewModel[] Moderators { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableUserSeletListItems { get; set; }
    }
}
