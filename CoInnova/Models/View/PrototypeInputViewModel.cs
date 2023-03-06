
using MOROPO.SAPRESSC.Core.Web.Common.ComponentModel.DataAnnotations;
using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class PrototypeInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Prototype, System.Guid>
    {
        public System.Guid Id { get; set; }

        [FormFileContentTypes(ContentTypes = new string[] { "image/png", "image/jpeg" }, ErrorMessage = "PNG_JPEG_FileAllowedOnlyMessage")]
        [FormFileMaxLength(Length = 3145728, ErrorMessage = "FileLimitSizeExcededMessage")]
        [Display(Name = "ImageFile")]
        public Microsoft.AspNetCore.Http.IFormFile ImageFile { get; set; }

        public System.Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        public string Name { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public System.Guid? LevelId { get; set; }
        public PrototypingLevel Level { get; set; }

        public System.Guid? IdeaId { get; set; }
        public Idea Idea { get; set; }

        public ICollection<PrototypeImageBridge> ImageBridges { get; set; }

        public ICollection<PrototypeDocumentBridge> DocumentBridges { get; set; }

        public Prototype Export()
        {
            var entity = new Prototype();

            Mergue(entity);

            return entity;
        }

        public void Import(Prototype entity)
        {
            Id = entity.Id;
            ImageId = entity.FeatureImage?.Id ?? System.Guid.Empty;
            ImageExtension = entity.FeatureImage?.ContentType.ContentTypeToExtension() ?? null;
            Name = entity.Name;
            CreatedAt = entity.CreatedAt;
            IsPublic = entity.IsPublic;
            Description = entity.Description;
            IdeaId = entity.IdeaId;
            LevelId = entity.LevelId;
            ImageBridges = entity.ImageBridges;
            DocumentBridges = entity.DocumentBridges;
        }

        public void Mergue(Prototype entity)
        {
            entity.Name = Name;
            entity.CreatedAt = CreatedAt;
            entity.IsPublic = IsPublic;
            entity.Description = Description;
            entity.IdeaId = IdeaId;
            entity.LevelId = LevelId;
        }
    }
}
