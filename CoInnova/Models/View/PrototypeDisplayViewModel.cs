using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;
using MOROPO.UIC.CoInnova.Persistence.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class PrototypeDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Prototype, System.Guid>
    {
        public Guid Id { get; set; }

        public Guid ImageId { get; set; }

        public string ImageExtension { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "CreatedAt")]
        public System.DateTime CreatedAt { get; set; }

        [Display(Name = "IsPublic")]
        public bool IsPublic { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Level")]
        public PrototypingLevel Level { get; set; }

        [Display(Name = "Idea")]
        public Idea Idea { get; set; }

        public ICollection<PrototypeImageBridge> ImageBridges { get; set; }

        public ICollection<PrototypeDocumentBridge> DocumentBridges { get; set; }

        public void Import(Prototype entity)
        {
            Id = entity.Id;
            ImageId = entity.FeatureImage?.Id ?? System.Guid.Empty;
            ImageExtension = entity.FeatureImage?.ContentType.ContentTypeToExtension() ?? null;
            Name = entity.Name;
            CreatedAt = entity.CreatedAt;
            IsPublic = entity.IsPublic;
            Description = entity.Description;
            ImageBridges = entity.ImageBridges;
            DocumentBridges = entity.DocumentBridges;
        }
    }
}
