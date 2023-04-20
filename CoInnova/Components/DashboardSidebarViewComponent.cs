using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade;
using MOROPO.UIC.CoInnova.Extensions;

namespace MOROPO.UIC.CoInnova.Components
{
    public class DashboardSidebarViewComponent : ViewComponent
    {
        private readonly System.Collections.Generic.IEnumerable<IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders;

        public DashboardSidebarViewComponent(System.Collections.Generic.IEnumerable<IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders)
        {
            this.managementDashboardSectionProviders = managementDashboardSectionProviders;
        }

        public IViewComponentResult Invoke()
        {
            return View(new Models.View.ManagementDashboardViewModel
            {
                Sections = managementDashboardSectionProviders.ToManagementDashboardSectionGroups()
            });
        }
    }
}
