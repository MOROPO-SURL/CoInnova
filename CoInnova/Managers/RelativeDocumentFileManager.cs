using MOROPO.UIC.CoInnova.Extensions;

namespace MOROPO.UIC.CoInnova.Managers
{
    public class RelativeDocumentFileManager
    {
        private const string MEDIA_ROOT_DIRECTORY_NAME = "media";
        private const string DOCUMENTS_DIRECTORY_NAME = "documents";

        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment env;

        public RelativeDocumentFileManager(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            this.env = env;
        }

        public void DeleteFile(Persistence.Entities.Document entity)
        {
            System.IO.File.Delete(GetAbsoluteFilePath(entity));
        }

        public string GetAbsoluteFilePath(Persistence.Entities.Document entity)
        {
            return GetAbsoluteFilePath(entity.Id, entity.ContentType);
        }

        public byte[] GetFileContent(Persistence.Entities.Document entity)
        {
            byte[] result;

            using (var file = System.IO.File.OpenRead(GetAbsoluteFilePath(entity)))
            {
                result = new byte[file.Length];

                file.Read(result, 0, (int)file.Length);

                file.Close();
            }

            return result;
        }

        private string GetAbsoluteFilePath(System.Guid id, string contentType)
        {
            return env.WebRootFileProvider.GetFileInfo(System.IO.Path.Combine(MEDIA_ROOT_DIRECTORY_NAME, DOCUMENTS_DIRECTORY_NAME, string.Format("{0}.{1}", id, contentType.AsContentTypeToExtension()))).PhysicalPath;
        }

        public string GetRelativeFilePath(Persistence.Entities.Document entity)
        {
            return null;
        }

        public void SaveFileContent(System.Guid entityId, byte[] content, string contentType)
        {
            System.IO.File.WriteAllBytes(GetAbsoluteFilePath(entityId, contentType), content);
        }
    }
}
