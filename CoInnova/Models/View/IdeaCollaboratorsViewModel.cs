namespace MOROPO.UIC.CoInnova.Models.View
{
    public class IdeaCollaboratorsViewModel
    {
        public IdeaDisplayViewModel Idea { get; set; }

        public CollaboratorDisplayViewModel[] Collaborators { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableCollaborators { get; set; }
    }
}
