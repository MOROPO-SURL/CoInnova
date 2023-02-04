using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Auxiliars
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("auxiliars")]
    public class ChallangeWorkflowsController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<ChallangeWorkflow, System.Guid, ChallangeWorkflowInputViewModel, ChallangeWorkflowDisplayViewModel>
    {
        private readonly IEntityRepository<ChallangeWorkflowStep, System.Guid> stepRepository;

        public ChallangeWorkflowsController(IEntityRepository<ChallangeWorkflowStep, System.Guid> stepRepository, IEntityRepository<ChallangeWorkflow, System.Guid> repository, IStringLocalizer<ChallangeWorkflowsController> localizer, ILogger<ChallangeWorkflowsController> logger) : base(repository, localizer, logger)
        {
            this.stepRepository = stepRepository;
        }

        [RequirePermissions(Constants.Permission.ChallangeWorkflowViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Permission.ChallangeWorkflowCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.ChallangeWorkflowCreator)]
        public override IActionResult Create(ChallangeWorkflowInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Constants.Permission.ChallangeWorkflowEditor)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Permission.ChallangeWorkflowEditor)]
        public override IActionResult Edit(ChallangeWorkflowInputViewModel modelInput)
        {
            ModelState.MarkFieldSkipped("StartStepName");

            return base.Edit(modelInput);
        }

        [RequirePermissions(Constants.Permission.ChallangeWorkflowRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.ChallangeWorkflowRemover)]
        public override IActionResult Delete(ChallangeWorkflowDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PostCreate(ChallangeWorkflow entity, ChallangeWorkflowInputViewModel model)
        {
            base.PostCreate(entity, model);

            var starStep = stepRepository.Add(new ChallangeWorkflowStep
            {
                Name = model.StartStepName,
                Description = model.StartStepDescription,
                WorkflowId = entity.Id
            }, User.Identity.Name);

            entity.StartStepId = starStep.Id;
            entity.StartStep = starStep;

            Repository.Update(entity, User.Identity.Name);
        }

        #region Steps

        [HttpGet]
        [Route("edit/{key}/steps")]
        [RequirePermissions(Constants.Permission.ChallangeWorkflowEditor)]
        public ActionResult Steps(System.Guid key)
        {
            var entity = Repository.Get(key);

            var model = new ChallangeWorkflowStepsDisplayViewModel
            {
                Workflow = new ChallangeWorkflowDisplayViewModel()
            };

            model.Workflow.Import(entity);

            var stepId = entity.StartStepId;

            while (stepId != null)
            {
                var step = stepRepository.Get(stepId.Value);

                model.Steps.Add(new ChallangeWorkflowStepDisplayViewModel
                {
                    Id = step.Id,
                    Name = step.Name,
                    Description = step.Description
                });

                stepId = step.NextId;
            };

            return View(model);
        }

        [HttpPost]
        [Route("edit/steps/create")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.ChallangeWorkflowEditor)]
        public IActionResult CreateStep(ChallangeWorkflowStepCreateViewModel model)
        {
            try
            {
                var entity = Repository.Get(model.WorkflowId);

                if (ModelState.IsValid)
                {
                    var lastStep = stepRepository.FindOneBy(item => item.WorkflowId == entity.Id && item.NextId == null);

                    try
                    {
                        var newStep = stepRepository.Add(new ChallangeWorkflowStep
                        {
                            Name = model.Name,
                            Description = model.Description,
                            WorkflowId = entity.Id
                        }, User.Identity.Name);

                        lastStep.NextId = newStep.Id;

                        stepRepository.Update(lastStep, User.Identity.Name);

                        SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("steps", new { key = entity.Id });
                    }
                    catch (SAPRESSC.Core.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("steps", new { key = entity.Id });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(SAPRESSC.Core.Common.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        #endregion
    }
}
