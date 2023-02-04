namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ApiKeyPermissionBridge
    {
        public System.Guid ApiKeyId { get; set; }

        public int PermissionId { get; set; }

        public ApiKey ApiKey { get; set; }

        public SAPRESSC.Core.Security.Common.Persistence.Entities.Permission Permission { get; set; }
    }
}
