using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.UIC.CoInnova.Models.View;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Auxiliars
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("auxiliars")]
    public class SpheresController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Persistence.Entities.Sphere, System.Guid, SphereInputViewModel, SphereDisplayViewModel>
    {
        public SpheresController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<Persistence.Entities.Sphere, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<SpheresController> localizer, Microsoft.Extensions.Logging.ILogger<SpheresController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Permission.SphereViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Permission.SphereCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.SphereCreator)]
        public override IActionResult Create(SphereInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Constants.Permission.SphereEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Permission.SphereEditor)]
        public override IActionResult Edit(SphereInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Constants.Permission.SphereRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.SphereRemover)]
        public override IActionResult Delete(SphereDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
