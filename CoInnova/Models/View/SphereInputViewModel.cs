namespace MOROPO.UIC.CoInnova.Models.View
{
    public class SphereInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.Sphere, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        public void Import(Persistence.Entities.Sphere entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public void Mergue(Persistence.Entities.Sphere entity)
        {
            entity.Name = Name;
        }

        public Persistence.Entities.Sphere Export()
        {
            var entity = new Persistence.Entities.Sphere();

            Mergue(entity);

            return entity;
        }
    }
}
