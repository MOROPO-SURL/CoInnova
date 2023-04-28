using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Innovation
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("innovation")]
    public class EvaluationsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Evaluation, System.Guid, EvaluationInputViewModel, EvaluationDisplayViewModel>
    {
        private readonly IEntityRepository<Idea, System.Guid> ideaRepository;
        private readonly IEntityRepository<Collaborator, System.Guid> collaboratorRepository;

        public EvaluationsController(IEntityRepository<Collaborator, System.Guid> collaboratorRepository, IEntityRepository<Idea, System.Guid> ideaRepository, IEntityRepository<Evaluation, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<EvaluationsController> localizer, ILogger<EvaluationsController> logger) : base(repository, localizer, logger)
        {
            this.ideaRepository = ideaRepository;
            this.collaboratorRepository = collaboratorRepository;
        }

        [RequirePermissions(Constants.Permission.EvaluationViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Permission.EvaluationCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.EvaluationCreator)]
        public override IActionResult Create(EvaluationInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        public override IActionResult Edit(EvaluationInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Constants.Permission.EvaluationRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.EvaluationRemover)]
        public override IActionResult Delete(EvaluationDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Evaluation entity, EvaluationInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.IdeaId != null)
            {
                entity.IdeaId = modelInput.IdeaId;
                entity.Idea = ideaRepository.Get(modelInput.IdeaId.Value);
            }
        }

        protected override void PreEdit(Evaluation entity, EvaluationInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.IdeaId != modelInput.IdeaId)
            {
                if (modelInput.IdeaId == null)
                {
                    entity.IdeaId = null;
                    entity.Idea = null;
                }
                else
                {
                    entity.IdeaId = modelInput.IdeaId;
                    entity.Idea = ideaRepository.Get(modelInput.IdeaId.Value);
                }
            }
        }

        protected override void ConfigureCreate(EvaluationInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(EvaluationInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigureEdit(Evaluation entity, EvaluationInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Evaluation entity, EvaluationInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            Populate(modelInput);
        }

        private void Populate(EvaluationInputViewModel model)
        {
            model.AvailableIdeaSelectListItems = ideaRepository.GetAll().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Title
            }).ToArray();
        }

        #region Person Collaborators

        [Route("edit/{key}/collaborators")]
        [RequirePermissions(Constants.Permission.EvaluationEditor)]
        public IActionResult EditCollaborators(System.Guid key)
        {
            try
            {
                var entity = Repository.Get(key);

                var availableCollaborators = collaboratorRepository.FindBy(ent1ty => ent1ty.OrganizationIdentityId == null).ToList();

                var includedCollaborators = new System.Collections.Generic.List<Collaborator>();

                foreach (var bridge in entity.CollaboratorBridges)
                {
                    if (bridge.Stern.OrganizationIdentityId != null) continue;

                    var collaborator = availableCollaborators.FirstOrDefault(item => item.Id == bridge.SternId);

                    includedCollaborators.Add(collaborator);

                    availableCollaborators.Remove(collaborator);
                }

                var model = new EvaluationCollaboratorsViewModel
                {
                    Evaluation = new EvaluationDisplayViewModel(),
                    Collaborators = includedCollaborators.Select(item =>
                    {
                        var iModel = new CollaboratorDisplayViewModel();

                        iModel.Import(item);

                        return iModel;
                    }).ToArray(),
                    AvailableCollaborators = availableCollaborators.Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem { Value = item.Id.ToString(), Text = string.IsNullOrEmpty(item.User.Identity.LastName) ? item.User.Identity.Name : string.Format("{0} {1}", item.User.Identity.Name, item.User.Identity.LastName) }).ToArray()
                };

                model.Evaluation.Import(entity);

                return View(model);
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [Route("edit/{key}/collaborators/add")]
        [RequirePermissions(Constants.Permission.EvaluationEditor)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCollaborator(System.Guid key, System.Guid? collaboratorId)
        {
            try
            {
                var entity = Repository.Get(key);

                if (collaboratorId != null)
                {
                    try
                    {
                        entity.CollaboratorBridges.Add(new EvaluationCollaboratorBridge { ProwId = entity.Id, SternId = collaboratorId.Value });

                        Repository.Update(entity, User.Identity.Name);

                        SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetCreationSuccessMessage());

                        return RedirectToAction("EditCollaborators", new { key });
                    }
                    catch (Framework.Guama.Core.Presentation.Web.Frontend.InvalidModelException e)
                    {
                        ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
                    }
                }

                ModelState.AddModelError("collaboratorId", string.Format(localizer["RequiredValidationErrorMessage"], localizer["Collaborator"]));

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetInvalidMessage());

                return RedirectToAction("EditCollaborators", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetDefaultActionResult();
        }

        [HttpPost]
        [Route("edit/{key}/collaborators/delete")]
        [ValidateAntiForgeryToken]
        [RequirePermissions(Constants.Permission.EvaluationEditor)]
        public IActionResult DeleteCollaborator(System.Guid key, System.Guid collaboratorId)
        {
            try
            {
                var entity = Repository.Get(key);

                var bridge = entity.CollaboratorBridges.First(b => b.SternId == collaboratorId);

                entity.CollaboratorBridges.Remove(bridge);

                Repository.Update(entity, User.Identity.Name);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Success, GetDeletionSuccessMessage());

                return RedirectToAction("EditCollaborators", new { key = entity.Id });
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                logger.LogError(e.InnerException, e.Message);

                SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

                return RedirectToAction("EditCollaborators", new { key });
            }
            catch (System.Exception e)
            {
                logger.LogError(e.InnerException, e.Message);
            }

            SetDefaultNotification(Framework.Guama.Core.Presentation.Models.View.NotificationType.Danger, GetNotFoundMessage());

            return GetSuccessDeleteActionResult();
        }

        #endregion
    }
}
