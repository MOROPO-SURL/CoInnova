using Microsoft.AspNetCore.Mvc;
using MOROPO.SAPRESSC.Core.Persistence.EF.Entities;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.UIC.CoInnova.Models.View;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Security
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("security")]
    public class TracesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementFilteredController<Trace, long, TraceInputViewModel, TraceDisplayViewModel, Models.View.Filter.TracesFilterViewModel>
    {
        public TracesController(IEntityRepository<Trace, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<TracesController> localizer, Microsoft.Extensions.Logging.ILogger<TracesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Permission.TraceViewer)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Constants.Permission.TraceViewer)]
        public override IActionResult Filter(Models.View.Filter.TracesFilterViewModel filterModel)
        {
            return base.Filter(filterModel);
        }

        protected override string GetFilterSessionID()
        {
            return "traces-management-filter";
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(TraceInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Edit(long key)
        {
            return NotFound();
        }

        public override IActionResult Edit(TraceInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Delete(long key)
        {
            return NotFound();
        }

        public override IActionResult Delete(TraceDisplayViewModel displayModel)
        {
            return NotFound();
        }
    }
}
