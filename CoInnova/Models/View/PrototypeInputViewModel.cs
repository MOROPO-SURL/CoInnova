using MOROPO.UIC.CoInnova.Persistence.Entities;
using System.ComponentModel.DataAnnotations;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class PrototypeInputViewModel : SAPRESSC.Core.Persistence.Models.IEntityInputModel<Prototype, System.Guid>
    {
        public System.Guid Id { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "IsPublic")]
        public bool IsPublic { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "PrototypingLevel")]
        public System.Guid? LevelId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "InnovationAgent")]
        public System.Guid? InnovationAgentId { get; set; }

        [Display(Name = "Idea")]
        public System.Guid? IdeaId { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Summary")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailablePrototypingLevels { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableIdeas { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableInnovationAgents { get; set; }

        public Prototype Export()
        {
            var entity = new Prototype();

            Mergue(entity);

            return entity;
        }

        public void Import(Prototype entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Summary = entity.Summary;
            Description = entity.Description;
            IsPublic = entity.IsPublic;
            LevelId = entity.LevelId;
            InnovationAgentId = entity.InnovationAgentId;
            IdeaId = entity.IdeaId;
        }

        public void Mergue(Prototype entity)
        {
            entity.Id = Id;
            entity.Title = Title;
            entity.Summary = Summary;
            entity.Description = Description;
            entity.IsPublic = IsPublic;
        }
    }
}
