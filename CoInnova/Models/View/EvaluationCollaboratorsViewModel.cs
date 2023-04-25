namespace MOROPO.UIC.CoInnova.Models.View
{
    public class EvaluationCollaboratorsViewModel
    {
        public EvaluationDisplayViewModel Evaluation { get; set; }

        public CollaboratorDisplayViewModel[] Collaborators { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableCollaborators { get; set; }
    }
}
