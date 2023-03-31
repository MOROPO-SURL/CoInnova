namespace MOROPO.UIC.CoInnova.Models
{
    public class DocumentDisplayCommonModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Document, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string ContentType { get; set; }

        public string Name  { get; set; }

        public long Length { get; set; }

        public string Description { get; set; }

        public void Import(Persistence.Entities.Document entity)
        {
            Id = entity.Id;
            ContentType = entity.ContentType;
            Name = entity.Name;
            Length = entity.Length;
            Description = entity.Description;
        }
    }
}
