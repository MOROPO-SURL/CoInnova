using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Auxiliars
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("auxiliars")]
    public class PrototypingLevelsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<PrototypingLevel, System.Guid, PrototypingLevelInputViewModel, PrototypingLevelDisplayViewModel>
    {
        public PrototypingLevelsController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<PrototypingLevel, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PrototypingLevelsController> localizer, Microsoft.Extensions.Logging.ILogger<PrototypingLevelsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Permission.PrototypingLevelViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Permission.PrototypingLevelCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.PrototypingLevelCreator)]
        public override IActionResult Create(PrototypingLevelInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Constants.Permission.PrototypingLevelEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Permission.PrototypingLevelEditor)]
        public override IActionResult Edit(PrototypingLevelInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Constants.Permission.PrototypingLevelRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.PrototypingLevelRemover)]
        public override IActionResult Delete(PrototypingLevelDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void ConfigureCreate(PrototypingLevelInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            PopulateForAvailableNumbers(modelInput);
        }

        protected override void ConfigureEdit(PrototypingLevel entity, PrototypingLevelInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            PopulateForAvailableNumbers(modelInput, entity);
        }

        protected override void ConfigurePostFailCreateValidation(PrototypingLevelInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            PopulateForAvailableNumbers(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(PrototypingLevel entity, PrototypingLevelInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            PopulateForAvailableNumbers(modelInput, entity);
        }

        private void PopulateForAvailableNumbers(PrototypingLevelInputViewModel model)
        {
            var unavailableNumbers = Repository.GetAll().Select(entity => entity.Numerator).ToArray();

            var wave = 1;

            while (model.AvailableNumbers.Count < 10)
            {
                for (int i = 10 * (wave - 1); i < 10 * wave; i++)
                {
                    var target = i + 1;

                    if (unavailableNumbers.Any(item => item == target)) continue;

                    model.AvailableNumbers.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = target.ToString(),
                        Value = target.ToString()
                    });

                    if (model.AvailableNumbers.Count == 10) break;
                }

                wave++;
            }
        }

        private void PopulateForAvailableNumbers(PrototypingLevelInputViewModel model, PrototypingLevel entity)
        {
            var unavailableNumbers = Repository.FindBy(item => item.Numerator != entity.Numerator).Select(item => item.Numerator).ToArray();

            var wave = 1;

            while (model.AvailableNumbers.Count < 10)
            {
                for (int i = 10 * (wave - 1); i < 10 * wave; i++)
                {
                    var target = i + 1;

                    if (unavailableNumbers.Any(item => item == target)) continue;

                    model.AvailableNumbers.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Text = target.ToString(),
                        Value = target.ToString()
                    });

                    if (model.AvailableNumbers.Count == 10) break;
                }

                wave++;
            }
        }
    }
}
