namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ChallangeWorkflowInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.ChallangeWorkflow, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string StartStepName { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string StartStepDescription { get; set; }

        public void Import(Persistence.Entities.ChallangeWorkflow entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(Persistence.Entities.ChallangeWorkflow entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }

        public Persistence.Entities.ChallangeWorkflow Export()
        {
            var entity = new Persistence.Entities.ChallangeWorkflow();

            Mergue(entity);

            return entity;
        }
    }
}
