namespace MOROPO.UIC.CoInnova.Extensions
{
    public static class ManagementDashboardSectionExtensions
    {
        public static Microsoft.AspNetCore.Html.HtmlString ToIconographyHtmlString(this Framework.Guama.Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel target)
        {
            if (target.Controller == "users") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-account\"></span>");

            if (target.Controller == "roles") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-account-group\"></span>");

            if (target.Controller == "traces") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-shoe-print\"></span>");

            if (target.Controller == "apikeys") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-key\"></span>");

            if (target.Controller == "prototypinglevels") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-signal\"></span>");

            if (target.Controller == "personcollaborators") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-handshake\"></span>");

            if (target.Controller == "prototypes") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-developer-board\"></span>");

            if (target.Controller == "challangecomments") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-comment-text-multiple\"></span>");

            if (target.Controller == "ideacomments") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-comment-text-multiple-outline\"></span>");

            if (target.Controller == "evaluations") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-star-circle\"></span>");

            if (target.Controller == "ideas" || target.Controller == "ideaworkflows") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-lightbulb-on\"></span>");

            if (target.Controller == "challanges" || target.Controller == "challangeworkflows") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-podium\"></span>");

            if (target.Controller == "spheres") return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-city-variant\"></span>");

            return new Microsoft.AspNetCore.Html.HtmlString("<span class=\"mdi mdi-toy-brick-outline\"></span>");
        }
    }
}
