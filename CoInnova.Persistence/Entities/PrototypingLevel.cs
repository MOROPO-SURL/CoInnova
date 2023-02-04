namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class PrototypingLevel : SAPRESSC.Core.Persistence.Entities.INamedEntity<System.Guid>
    {
        public PrototypingLevel()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        public int Numerator { get; set; }

        public string Description { get; set; }
    }
}
