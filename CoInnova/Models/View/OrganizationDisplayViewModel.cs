using System.ComponentModel.DataAnnotations;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;


namespace MOROPO.UIC.CoInnova.Models.View
{
    public class OrganizationDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "FoundatedAt")]
        public System.DateTime? BornDate { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public void Import(SAPRESSC.Middleware.Identity.Persistence.Entities.OrganizationIdentity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            BornDate = entity.BornDate;
            About = entity.About;
            ImageId = entity.ImageId;
            ImageExtension = entity.Image?.ContentType.ContentTypeToExtension();
        }
    }
}
