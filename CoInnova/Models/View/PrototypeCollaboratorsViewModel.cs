namespace MOROPO.UIC.CoInnova.Models.View
{
    public class PrototypeCollaboratorsViewModel
    {
        public PrototypeDisplayViewModel Prototype { get; set; }

        public CollaboratorDisplayViewModel[] Collaborators { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableCollaborators { get; set; }
    }
}
