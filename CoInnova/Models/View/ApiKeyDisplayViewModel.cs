using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ApiKeyDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.ApiKey, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Token")]
        public string Token { get; set; }

        [Display(Name = "Enabled")]
        public bool Enabled { get; set; }

        [Display(Name = "ExpirateAt")]
        public System.DateTime ExpirateAt { get; set; }

        [Display(Name = "Host")]
        public string Host { get; set; }

        [Display(Name = "Permissions")]
        public SAPRESSC.Core.Security.Common.Persistence.Entities.Permission[] UngroupedPermissions { get; set; }

        public System.Collections.Generic.KeyValuePair<string, SAPRESSC.Core.Security.Common.Persistence.Entities.Permission[]>[] GroupedPermissions { get; set; }

        public void Import(Persistence.Entities.ApiKey entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Token = entity.Token;
            Enabled = entity.Enabled;
            ExpirateAt = entity.ExpirateAt;
            Host = entity.Host;
        }
    }
}
