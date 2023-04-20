using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Presentation.Models;
using MOROPO.UIC.CoInnova.Models.Api;
using MOROPO.UIC.CoInnova.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Controllers.API
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Swashbuckle.AspNetCore.Annotations.SwaggerTag("Prototype's endpoints.")]
    public class PrototypesController : Framework.Guama.Core.Presentation.Web.API.Controllers.AbstractEntityApiConsultController<Prototype, System.Guid, PrototypeDisplayApiModel>
    {
        public PrototypesController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<Prototype, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<PrototypesController> localizer, Microsoft.Extensions.Logging.ILogger<PrototypesController> logger) : base(repository, localizer, logger)
        {
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Prototype's list", "Endpoint to obtain the full list of Prototypes.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PrototypeDisplayApiModel[]))]
        public override ActionResult<PrototypeDisplayApiModel[]> Get()
        {
            return base.Get();
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get a Prototype", "Endpoint to obtain a Prototypes by it's id.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PrototypeDisplayApiModel))]
        public override ActionResult<PrototypeDisplayApiModel> Get(System.Guid key)
        {
            return base.Get(key);
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Prototype's pagination", "Endpoint to obtain the paginated list of Prototypes.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<PrototypeDisplayApiModel>))]
        public override ActionResult<PaginationModel<PrototypeDisplayApiModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }
    }
}
