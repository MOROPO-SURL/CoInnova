using MOROPO.SAPRESSC.Middleware.Media.Web.Common.Extensions;

namespace MOROPO.UIC.CoInnova.Models
{
    public class ImageDisplayCommonModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<SAPRESSC.Middleware.Media.Persistence.Entities.Image, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Extension { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public void Import(SAPRESSC.Middleware.Media.Persistence.Entities.Image entity)
        {
            Id = entity.Id;
            Extension = entity.ContentType.ContentTypeToExtension();
            Width = entity.Width;
            Height = entity.Height;
        }
    }
}
