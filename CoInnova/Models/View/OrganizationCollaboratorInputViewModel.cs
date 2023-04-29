using MOROPO.Framework.Guama.Core.Presentation.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.Framework.Guama.Media.Presentation.Web.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class OrganizationCollaboratorInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.Collaborator, System.Guid>
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

        [Display(Name = "Representative")]
        public string UserId { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableUsers { get; set; }

        public Persistence.Entities.Collaborator Export()
        {
            var entity = new Persistence.Entities.Collaborator
            {
                OrganizationIdentity = new Framework.Guama.Identity.Persistence.Entities.OrganizationIdentity()
            };

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Collaborator entity)
        {
            Id = entity.Id;
            ImageId = entity.OrganizationIdentity.ImageId;
            ImageExtension = entity.OrganizationIdentity.Image?.ContentType.ContentTypeToExtension();
            Name = entity.OrganizationIdentity.Name;
            BornDate = entity.OrganizationIdentity.BornDate;
            About = entity.OrganizationIdentity.About;
            UserId = entity.UserId;
        }

        public void Mergue(Persistence.Entities.Collaborator entity)
        {
            entity.OrganizationIdentity.Name = Name;
            entity.OrganizationIdentity.BornDate = BornDate;
            entity.OrganizationIdentity.About = About;
        }
    }
}
