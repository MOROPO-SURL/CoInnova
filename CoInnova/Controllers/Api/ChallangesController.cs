using Microsoft.AspNetCore.Mvc;
using MOROPO.SAPRESSC.Core.Common.Models;
using MOROPO.UIC.CoInnova.Models.Api;
using MOROPO.UIC.CoInnova.Persistence.Entities;

namespace MOROPO.UIC.CoInnova.Controllers.Api
{
    [Microsoft.AspNetCore.Authorization.Authorize(AuthenticationSchemes = SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.ApiKeyAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    [Swashbuckle.AspNetCore.Annotations.SwaggerTag("Challange's endpoints.")]
    public class ChallangesController : SAPRESSC.Core.Web.Api.Controllers.AbstractEntityApiConsultController<Challange, System.Guid, ChallangeDisplayApiModel>
    {
        public ChallangesController(SAPRESSC.Core.Persistence.Repositories.IEntityRepository<Challange, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<ChallangesController> localizer, Microsoft.Extensions.Logging.ILogger<ChallangesController> logger) : base(repository, localizer, logger)
        {
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Challange's list", "Endpoint to obtain the full list of Challanges.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ChallangeDisplayApiModel[]))]
        public override ActionResult<ChallangeDisplayApiModel[]> Get()
        {
            return base.Get();
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get a Challange", "Endpoint to obtain a Challanges by it's id.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(ChallangeDisplayApiModel))]
        public override ActionResult<ChallangeDisplayApiModel> Get(System.Guid key)
        {
            return base.Get(key);
        }

        [Swashbuckle.AspNetCore.Annotations.SwaggerOperation("Get Challange's pagination", "Endpoint to obtain the paginated list of Challanges.")]
        [Swashbuckle.AspNetCore.Annotations.SwaggerResponse((int)System.Net.HttpStatusCode.OK, null, typeof(PaginationModel<ChallangeDisplayApiModel>))]
        public override ActionResult<PaginationModel<ChallangeDisplayApiModel>> GetPaginate(int sheet = 1, int limit = 25)
        {
            return base.GetPaginate(sheet, limit);
        }
    }
}
