namespace MOROPO.UIC.CoInnova.Models.View
{
    public class IdeaWorkflowInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Persistence.Entities.IdeaWorkflow, System.Guid>
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

        public void Import(Persistence.Entities.IdeaWorkflow entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }

        public void Mergue(Persistence.Entities.IdeaWorkflow entity)
        {
            entity.Name = Name;
            entity.Description = Description;
        }

        public Persistence.Entities.IdeaWorkflow Export()
        {
            var entity = new Persistence.Entities.IdeaWorkflow();

            Mergue(entity);

            return entity;
        }
    }
}