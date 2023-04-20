namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ApiKeyPermissionBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<ApiKey, System.Guid, Framework.Guama.Core.Security.Common.Persistence.Entities.Permission, int>
    {
        public System.Guid ProwId { get; set; }
        public ApiKey Prow { get; set; }

        public int SternId { get; set; }
        public Framework.Guama.Core.Security.Common.Persistence.Entities.Permission Stern { get; set; }
    }
}
