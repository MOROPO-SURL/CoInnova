namespace MOROPO.UIC.CoInnova.Models.View
{
    public class IdeaDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Idea, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "CreatedAt")]
        public System.DateTime CreatedAt { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Title")]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "IsPublic")]
        public bool IsPublic { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "Description")]
        public string Description { get; set; }

        public void Import(Persistence.Entities.Idea entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            IsPublic = entity.IsPublic;
        }
    }
}
