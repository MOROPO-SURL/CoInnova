using System.Linq;

namespace MOROPO.UIC.CoInnova.Filters.Swagger
{
    public class OperationSwaggerFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        private readonly Microsoft.OpenApi.Models.OpenApiSecurityScheme openApiSecurityScheme;

        public OperationSwaggerFilter(Microsoft.OpenApi.Models.OpenApiSecurityScheme openApiSecurityScheme)
        {
            this.openApiSecurityScheme = openApiSecurityScheme;
        }

        public void Apply(Microsoft.OpenApi.Models.OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>().Any() || context.MethodInfo.GetCustomAttributes(true).OfType<Microsoft.AspNetCore.Authorization.AuthorizeAttribute>().Any();

            if (hasAuthorize)
            {
                operation.Responses.Add(((int)System.Net.HttpStatusCode.Unauthorized).ToString(), new Microsoft.OpenApi.Models.OpenApiResponse { Description = System.Enum.GetName(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.Unauthorized) });

                operation.Security = new System.Collections.Generic.List<Microsoft.OpenApi.Models.OpenApiSecurityRequirement>
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                    {
                        { openApiSecurityScheme, new System.Collections.Generic.List<string> {} }
                    }
                };
            }
        }
    }
}
