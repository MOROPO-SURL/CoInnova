namespace MOROPO.UIC.CoInnova.Models.View
{
    public class IdeaWorkflowDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.IdeaWorkflow, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Name")]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        public void Import(Persistence.Entities.IdeaWorkflow entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
        }
    }
}
