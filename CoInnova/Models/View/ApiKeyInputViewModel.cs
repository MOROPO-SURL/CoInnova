using MOROPO.SAPRESSC.Core.Common.Models.View;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ApiKeyInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.ApiKey, System.Guid>
    {
        public ApiKeyInputViewModel()
        {
            ExpirateAt = System.DateTime.Now.AddMonths(3);
        }

        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Token")]
        public string Token { get; set; }

        [Display(Name = "Enabled")]
        public bool Enabled { get; set; }

        [Display(Name = "ExpirateAt")]
        public System.DateTime ExpirateAt { get; set; }

        [Display(Name = "Host")]
        public string Host { get; set; }

        [Display(Name = "Permissions")]
        public SelectionDescriptedItemInputViewModel<int>[] UngroupedSelectablePermissions { get; set; }

        public KeyValuePair<string, SelectionDescriptedItemInputViewModel<int>[]>[] GroupedSelectablePermissions { get; set; }

        public Persistence.Entities.ApiKey Export()
        {
            var entity = new Persistence.Entities.ApiKey();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.ApiKey entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Token = entity.Token;
            Enabled = entity.Enabled;
            ExpirateAt = entity.ExpirateAt;
            Host = entity.Host;
        }

        public void Mergue(Persistence.Entities.ApiKey entity)
        {
            entity.Name = Name;
            entity.Token = Token;
            entity.ExpirateAt = ExpirateAt;
            entity.Enabled = Enabled;
            entity.Host = Host;
        }
    }
}
