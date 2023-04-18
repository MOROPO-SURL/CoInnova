using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.SAPRESSC.Core.Persistence.Repositories;
using MOROPO.SAPRESSC.Core.Web.Security.Attributes;
using MOROPO.UIC.CoInnova.Models.View;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Controllers.Backend.Innovation
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(SAPRESSC.Core.Security.Common.Web.Backend.Constants.Permission.ShellManager)]
    [Area("innovation")]
    public class ChallangesController : SAPRESSC.Core.Web.Backend.Controllers.AbstractEntityManagementController<Challange, System.Guid, ChallangeInputViewModel, ChallangeDisplayViewModel>
    {
        private readonly IEntityRepository<ChallangeWorkflow, System.Guid> challangeWorkflowRepository;

        public ChallangesController(IEntityRepository<ChallangeWorkflow, System.Guid> challangeWorkflowRepository, IEntityRepository<Challange, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ChallangesController> localizer, ILogger<ChallangesController> logger) : base(repository, localizer, logger)
        {
            this.challangeWorkflowRepository = challangeWorkflowRepository;
        }

        [RequirePermissions(Constants.Permission.ChallangeViewer)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Permission.ChallangeCreator)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Permission.ChallangeCreator)]
        public override IActionResult Create(ChallangeInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        public override IActionResult Edit(ChallangeInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        [RequirePermissions(Constants.Permission.ChallangeRemover)]
        public override IActionResult Delete(System.Guid key)
        {
            return base.Delete(key);
        }

        [RequirePermissions(Constants.Permission.ChallangeRemover)]
        public override IActionResult Delete(ChallangeDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        protected override void PreCreate(Challange entity, ChallangeInputViewModel modelInput)
        {
            base.PreCreate(entity, modelInput);

            if (modelInput.WorkflowId != null)
            {
                entity.WorkflowId = modelInput.WorkflowId;
                entity.Workflow = challangeWorkflowRepository.Get(modelInput.WorkflowId.Value);
            }
        }

        protected override void PreEdit(Challange entity, ChallangeInputViewModel modelInput)
        {
            base.PreEdit(entity, modelInput);

            if (entity.WorkflowId != modelInput.WorkflowId)
            {
                if (modelInput.WorkflowId == null)
                {
                    entity.WorkflowId = null;
                    entity.Workflow = null;
                }
                else
                {
                    entity.WorkflowId = modelInput.WorkflowId;
                    entity.Workflow = challangeWorkflowRepository.Get(modelInput.WorkflowId.Value);
                }
            }
        }

        protected override void ConfigureCreate(ChallangeInputViewModel modelInput)
        {
            base.ConfigureCreate(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailCreateValidation(ChallangeInputViewModel modelInput)
        {
            base.ConfigurePostFailCreateValidation(modelInput);

            Populate(modelInput);
        }

        protected override void ConfigureEdit(Challange entity, ChallangeInputViewModel modelInput)
        {
            base.ConfigureEdit(entity, modelInput);

            Populate(modelInput);
        }

        protected override void ConfigurePostFailEditValidation(Challange entity, ChallangeInputViewModel modelInput)
        {
            base.ConfigurePostFailEditValidation(entity, modelInput);

            Populate(modelInput);
        }

        private void Populate(ChallangeInputViewModel model)
        {
            model.AvailableWorkflowSelectListItems = challangeWorkflowRepository.GetAll().Select(item => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name
            }).ToArray();
        }

        #region Documents

        //[HttpGet]
        //[Route("edit/{key}/documents")]
        //[RequirePermissions(Constants.Permission.ChallangeEditor)]
        //public ActionResult EditDocuments(System.Guid key)
        //{
        //    var entity = Repository.Get(key);

        //    var model = new ChallangeDocumentsViewModel
        //    {
        //        Challange = new ChallangeDisplayViewModel(),
        //        Documents = entity.DocumentBridges.Select(item =>
        //        {
        //            var result = new Models.DocumentDisplayCommonModel();

        //            result.Import(item.Document);

        //            return result;
        //        }).ToArray()
        //    };

        //    model.Challange.Import(entity);

        //    return View(model);
        //}

        //[HttpPost]
        //[Route("edit/{key}/documents/add")]
        //[ValidateAntiForgeryToken]
        //[RequirePermissions(Constants.Permission.ChallangeEditor)]
        //public IActionResult AddDocument(System.Guid key, DocumentInputViewModel model)
        //{
        //    try
        //    {
        //        var entity = Repository.Get(key);

        //        ModelState.ClearValidationState("File");

        //        model.File = Request.Form.Files[0];

        //        TryValidateModel(model);

        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                using (var memoryStream = new System.IO.MemoryStream())
        //                {
        //                    model.File.CopyTo(memoryStream);

        //                    var document = new Document
        //                    {
        //                        Name = model.Name,
        //                        Description = model.Description,
        //                        ContentType = model.File.ContentType,
        //                        Length = model.File.Length
        //                    };

        //                    entity.DocumentBridges.Add(new ChallangeDocumentBridge
        //                    {
        //                        ChallangeId = entity.Id,
        //                        Document = document
        //                    });

        //                    documentManager.SaveFileContent(document.Id, memoryStream.ToArray(), document.ContentType);

        //                    Repository.Update(entity, User.Identity.Name);
        //                }

        //                SetDefaultNotification(NotificationType.Success, GetCreationSuccessMessage());

        //                return RedirectToAction("editdocuments", new { key = entity.Id });
        //            }
        //            catch (SAPRESSC.Core.Web.Frontend.InvalidModelException e)
        //            {
        //                ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
        //            }
        //        }

        //        SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

        //        return RedirectToAction("editdocuments", new { key = entity.Id });
        //    }
        //    catch (System.Exception e)
        //    {
        //        logger.LogError(e.InnerException, e.Message);
        //    }

        //    SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

        //    return GetDefaultActionResult();
        //}

        //[HttpPost]
        //[Route("edit/{key}/documents/remove")]
        //[ValidateAntiForgeryToken]
        //[RequirePermissions(Constants.Permission.ChallangeEditor)]
        //public IActionResult RemoveDocument(System.Guid key, System.Guid documentId)
        //{
        //    try
        //    {
        //        var entity = Repository.Get(key);

        //        var bridge = entity.DocumentBridges.First(b => b.DocumentId == documentId);

        //        entity.DocumentBridges.Remove(bridge);

        //        Repository.Update(entity, User.Identity.Name);

        //        try
        //        {
        //            documentManager.DeleteFile(bridge.Document);
        //        }
        //        catch { }

        //        SetDefaultNotification(NotificationType.Success, GetDeletionSuccessMessage());

        //        return RedirectToAction("editdocuments", new { key = entity.Id });
        //    }
        //    catch (Microsoft.EntityFrameworkCore.DbUpdateException e)
        //    {
        //        logger.LogError(e.InnerException, e.Message);

        //        SetDefaultNotification(NotificationType.Warning, GetOnDeletionDependecyConstrainErrorMessage());

        //        return RedirectToAction("editdocuments", new { key });
        //    }
        //    catch (System.Exception e)
        //    {
        //        logger.LogError(e.InnerException, e.Message);
        //    }

        //    SetDefaultNotification(NotificationType.Danger, GetNotFoundMessage());

        //    return GetSuccessDeleteActionResult();
        //}

        //[HttpPost]
        //[Route("edit/{key}/documents/edit")]
        //[ValidateAntiForgeryToken]
        //[RequirePermissions(Constants.Permission.ChallangeEditor)]
        //public IActionResult EditDocument(System.Guid key, DocumentInputViewModel modelInput)
        //{
        //    ModelState.ClearValidationState("File");
        //    ModelState.MarkFieldValid("File");

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var entity = Repository.Get(key);

        //            try
        //            {
        //                var bridge = entity.DocumentBridges.First(b => b.DocumentId == modelInput.Id);

        //                bridge.Document.Name = modelInput.Name;
        //                bridge.Document.Description = modelInput.Description;

        //                Repository.Update(entity, User.Identity.Name);

        //                SetDefaultNotification(NotificationType.Success, GetEditionSuccessMessage());

        //                return RedirectToAction("editdocuments", new { key = entity.Id });
        //            }
        //            catch (SAPRESSC.Core.Web.Frontend.InvalidModelException e)
        //            {
        //                ModelState.AddModelError(e.ModelKey, e.ErrorMessage);
        //            }

        //            SetDefaultNotification(NotificationType.Danger, GetInvalidMessage());

        //            return RedirectToAction("editdocuments", new { key = entity.Id });
        //        }
        //        catch (System.Exception e)
        //        {
        //            logger.LogError(e.InnerException, e.Message);
        //        }
        //    }

        //    SetDefaultNotification(notificationType: NotificationType.Danger, GetNotFoundMessage());

        //    return GetDefaultActionResult();
        //}

        #endregion
    }
}
