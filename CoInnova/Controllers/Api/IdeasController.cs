using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Presentation.Models;
using MOROPO.UIC.CoInnova.Models.Api;
using MOROPO.UIC.CoInnova.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Controllers.API
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Swashbuckle.AspNetCore.Annotations.SwaggerTag("Idea's endpoints.")]
    public class IdeasController : Framework.Guama.Core.Presentation.Web.API.Controllers.AbstractEntityApiConsultController<Idea, System.Guid, IdeaDisplayApiModel>
    {
        public IdeasController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<Idea, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<IdeasController> localizer, Microsoft.Extensions.Logging.ILogger<IdeasController> logger) : base(repository, localizer, logger)
        {
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Idea's list", "Endpoint to obtain the full list of Ideas.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(IdeaDisplayApiModel[]))]
        public override ActionResult<IdeaDisplayApiModel[]> Get()
        {
            return base.Get();
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get a Idea", "Endpoint to obtain a Ideas by it's id.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(IdeaDisplayApiModel))]
        public override ActionResult<IdeaDisplayApiModel> Get(System.Guid key)
        {
            return base.Get(key);
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Idea's pagination", "Endpoint to obtain the paginated list of Ideas.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<IdeaDisplayApiModel>))]
        public override ActionResult<PaginationModel<IdeaDisplayApiModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }
    }
}
