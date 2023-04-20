using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Controllers.Frontend
{
    [Area("frontend")]
    public class PrototypesController : Framework.Guama.Core.Presentation.Web.Frontend.Controllers.AbstractEntityManagementController<Prototype, System.Guid, PrototypeInputViewModel, PrototypeDisplayViewModel>
    {
        private readonly IOptions<PrototypeDisplayViewModel> model;
        public PrototypesController(IOptions<PrototypeDisplayViewModel> model, Framework.Guama.Core.Persistence.Repositories.IEntityRepository<Prototype, System.Guid> repository, IStringLocalizer<PrototypesController> localizer, ILogger<PrototypesController> logger) : base(repository, localizer, logger)
        {
            this.model = model;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("details")]
        public IActionResult Details(PrototypeImagesViewModel prototypeImageDisplay)
        {
            if(prototypeImageDisplay.Prototype == null)
            {
                prototypeImageDisplay.Prototype = model.Value;
            }

            return View(prototypeImageDisplay);
        }
    }
}
