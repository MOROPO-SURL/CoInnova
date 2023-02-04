namespace MOROPO.UIC.CoInnova.Models.Api
{
    public class SessionApiModel : SmartSolucionesCuba.AspNetCore.Authentication.ApiKey.Models.IApiKeySessionModel
    {
        public SessionApiModel(Persistence.Entities.ApiKey entity)
        {
            ApiKey = entity;

            Claims = new System.Collections.Generic.List<System.Security.Claims.Claim>();
        }

        public string Key => ApiKey.Token;

        public string OwnerName => ApiKey.Id.ToString();

        public System.Collections.Generic.IReadOnlyCollection<System.Security.Claims.Claim> Claims { get; private set; }

        public Persistence.Entities.ApiKey ApiKey { get; private set; }
    }
}
