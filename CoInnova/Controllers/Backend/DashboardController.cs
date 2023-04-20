using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;
using MOROPO.UIC.CoInnova.Extensions;
using System.Collections.Generic;

namespace MOROPO.UIC.CoInnova.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.ChangeUserPasswordRequiredAction]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.CatchChangeUserPasswordRequiredException]
    [Route("management/[controller]")]
    [Route("{culture:regex(^[[a-z]]{{2}}(?:-[[A-Z]]{{2}})?$)}/management/[controller]")]
    public class DashboardController : Framework.Guama.Core.Presentation.Web.Common.Controllers.BaseWebController
    {
        private readonly IEnumerable<Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders;
        private readonly IEntityRepository<Trace, long> traceRepository;
        private readonly IEntityRepository<User, string> userRepository;

        private static readonly string[] DAYS_OF_WEEK = new[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

        public DashboardController(IEntityRepository<User, string> userRepository, IEntityRepository<Trace, long> traceRepository, IEnumerable<Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders, Microsoft.Extensions.Localization.IStringLocalizer<DashboardController> localizer, ILogger<DashboardController> logger) : base(localizer, logger)
        {
            this.managementDashboardSectionProviders = managementDashboardSectionProviders;
            this.userRepository = userRepository;
            this.traceRepository = traceRepository;
        }

        public IActionResult Index()
        {
            var startEdgeDate = System.DateTime.Today;
            var passWeekEdgeDate = startEdgeDate.AddDays(-6);
            var traces = traceRepository.GetAll();

            var model = new Models.View.ManagementDashboardViewModel { Sections = managementDashboardSectionProviders.ToManagementDashboardSectionGroups() };

            var tracesByUsers = new Dictionary<string, long>();

            foreach (Trace trace in traces)
            {
                if (!tracesByUsers.ContainsKey(trace.Author))
                {
                    tracesByUsers.Add(trace.Author, 1);
                }
                else
                {
                    tracesByUsers[trace.Author]++;
                }
            }

            model.UsersList.AddRange(tracesByUsers.Keys);
            model.TracesbyUsers.AddRange(tracesByUsers.Values);

            for (System.DateTime currentDate = passWeekEdgeDate; currentDate <= startEdgeDate; currentDate = currentDate.AddDays(1))
            {
                model.WeekStatisticsColumns.Add(DAYS_OF_WEEK[(int)currentDate.DayOfWeek]);

                model.WeekTracesStatisticsValues.Add(traceRepository.Count(entity => entity.Registered.Date == currentDate));
            }

            return View(model);
        }
    }
}
