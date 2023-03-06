using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System;

namespace MOROPO.UIC.CoInnova.Controllers.Frontend
{
    [Area("frontend")]
    public class PrototypesController : SAPRESSC.Core.Web.Frontend.Controllers.AbstractEntityManagementController<Prototype, System.Guid, PrototypeInputViewModel, PrototypeDisplayViewModel>
    {
        private readonly IOptions<PrototypeDisplayViewModel> model;
        public PrototypesController(IOptions<PrototypeDisplayViewModel> model, IEntityRepository<Prototype, Guid> repository, IStringLocalizer<PrototypesController> localizer, ILogger<PrototypesController> logger) : base(repository, localizer, logger)
        {
            this.model = model;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("details")]
        public IActionResult Details(PrototypeDisplayViewModel prototypeDisplay)
        {
            if(prototypeDisplay.Id == System.Guid.Empty)
            {
                prototypeDisplay = model.Value;
            } 
            return View(prototypeDisplay);
        }
    }
}
