using MOROPO.SAPRESSC.Core.Persistence.Repositories;

namespace MOROPO.UIC.CoInnova.Providers
{
    public class ApiKeySessionProvider : SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Providers.Facade.IApiKeySessionProviderFacade
    {
        private readonly IEntityRepository<Persistence.Entities.ApiKey, System.Guid> apiKeyRepository;
        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor;

        public ApiKeySessionProvider(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor, IEntityRepository<Persistence.Entities.ApiKey, System.Guid> apiKeyRepository)
        {
            this.apiKeyRepository = apiKeyRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public System.Threading.Tasks.Task<SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Models.IApiKeySessionModel> ProvideAsync(string key)
        {
            var apiKey = apiKeyRepository.FindOneBy(entity => entity.Enabled && entity.Token == key);

            if (apiKey == null || !apiKey.Enabled) return System.Threading.Tasks.Task.FromResult<SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Models.IApiKeySessionModel>(null);

            if (!string.IsNullOrEmpty(apiKey.Host) && httpContextAccessor.HttpContext.Request.Host.Host != apiKey.Host) return System.Threading.Tasks.Task.FromResult<SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Models.IApiKeySessionModel>(null);

            var apiKeySession = new Models.Api.SessionApiModel(apiKey);

            httpContextAccessor.HttpContext.Features.Set<SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Models.IApiKeySessionModel>(apiKeySession);

            return System.Threading.Tasks.Task.FromResult<SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Models.IApiKeySessionModel>(apiKeySession);
        }
    }
}
