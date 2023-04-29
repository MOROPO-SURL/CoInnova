namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class Collaborator : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        public Collaborator()
        {
            Id = System.Guid.NewGuid();
            CreatedAt = System.DateTime.Now;
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("UserId")]
        public Framework.Guama.Security.Personal.Persistence.Entities.User User { get; set; }
        public string UserId { get; set; }

        public Framework.Guama.Identity.Persistence.Entities.OrganizationIdentity OrganizationIdentity { get; set; }
        public System.Guid? OrganizationIdentityId { get; set; }
    }
}
