using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class InnovationAgentInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.InnovationAgent, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "FoundatedAt")]
        public System.DateTime? BornDate { get; set; }

        [Display(Name = "About")]
        public string About { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 3145728, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid? ImageId { get; set; }

        public string ImageExtension { get; set; }

        public bool ResetImage { get; set; }

        public Persistence.Entities.InnovationAgent Export()
        {
            var entity = new Persistence.Entities.InnovationAgent
            {
                Identity = new Framework.Guama.Identity.Persistence.Entities.OrganizationIdentity()
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.InnovationAgent entity)
        {
            Id = entity.Id;
            ImageId = entity.Identity.ImageId;
            ImageExtension = entity.Identity.Image?.ContentType.ContentTypeToExtension();
            Name = entity.Identity.Name;
            BornDate = entity.Identity.BornDate;
            About = entity.Identity.About;
        }

        public void Mergue(Persistence.Entities.InnovationAgent entity)
        {
            entity.Identity.Name = Name;
            entity.Identity.BornDate = BornDate;
            entity.Identity.About = About;
        }
    }
}
