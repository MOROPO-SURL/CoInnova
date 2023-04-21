namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ChallangeCollaboratorsViewModel
    {
        public ChallangeDisplayViewModel Challange { get; set; }

        public CollaboratorDisplayViewModel[] Collaborators { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableCollaborators { get; set; }
    }
}
