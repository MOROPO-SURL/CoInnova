namespace MOROPO.UIC.CoInnova.Models.View
{
    public class SphereDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Sphere, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        public void Import(Persistence.Entities.Sphere entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
