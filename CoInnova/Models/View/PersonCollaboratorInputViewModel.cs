namespace MOROPO.UIC.CoInnova.Models.View
{
    public class PersonCollaboratorInputViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityInputModel<Persistence.Entities.Collaborator, System.Guid>
    {
        public System.Guid Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "RequiredValidationErrorMessage")]
        [System.ComponentModel.DataAnnotations.Display(Name = "User")]
        public string UserId { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableUsers { get; set; }

        public Persistence.Entities.Collaborator Export()
        {
            var entity = new Persistence.Entities.Collaborator();

            Mergue(entity);

            return entity;
        }

        public void Import(Persistence.Entities.Collaborator entity)
        {
            UserId = entity.UserId;
        }

        public void Mergue(Persistence.Entities.Collaborator entity)
        {
        }
    }
}
