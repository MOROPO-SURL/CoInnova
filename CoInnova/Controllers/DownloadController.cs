using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.UIC.CoInnova.Extensions;
using MOROPO.UIC.CoInnova.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class DownloadController : Framework.Guama.Core.Presentation.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<Document, System.Guid> documentRepository;
        private readonly Managers.RelativeDocumentFileManager documentManager;

        public DownloadController(Managers.RelativeDocumentFileManager documentManager, IEntityRepository<Document, System.Guid> documentRepository, Microsoft.Extensions.Localization.IStringLocalizer<DownloadController> localizer, ILogger<DownloadController> logger) : base(localizer, logger)
        {
            this.documentRepository = documentRepository;
            this.documentManager = documentManager;
        }

        public IActionResult Document(System.Guid key)
        {
            var entity = documentRepository.Get(key);

            return new FileContentResult(documentManager.GetFileContent(entity), entity.ContentType)
            {
                FileDownloadName = string.Format("{0}.{1}", entity.Name, entity.ContentType.AsContentTypeToExtension())
            };
        }
    }
}
