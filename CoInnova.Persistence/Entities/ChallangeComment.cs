namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class ChallangeComment : SAPRESSC.Core.Persistence.Entities.IEntity<long>
    {
        public ChallangeComment()
        {
            CreatedAt = System.DateTime.Now;
        }

        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("AuthorId")]
        public SAPRESSC.Middleware.Security.Identity.Persistence.Entities.User Author { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string AuthorId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("ChallangeId")]
        public Challange Challange { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public System.Guid? ChallangeId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Message { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("UpstreamId")]
        public ChallangeComment Upstream { get; set; }
        public long? UpstreamId { get; set; }
    }
}
