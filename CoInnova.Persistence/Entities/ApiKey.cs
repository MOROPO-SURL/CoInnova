namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ApiKey : Framework.Guama.Core.Persistence.Entities.Contract.INamedEntity<System.Guid>, Framework.Guama.Core.Persistence.Entities.Contract.IHabilitableEntity<System.Guid>
    {
        public ApiKey()
        {
            Id = System.Guid.NewGuid();
            PermissionBridges = new System.Collections.Generic.List<ApiKeyPermissionBridge>();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Token { get; set; }

        public bool Enabled { get; set; }

        public System.DateTime ExpirateAt { get; set; }

        public string Host { get; set; }

        public System.Collections.Generic.List<ApiKeyPermissionBridge> PermissionBridges { get; set; } 
    }
}
