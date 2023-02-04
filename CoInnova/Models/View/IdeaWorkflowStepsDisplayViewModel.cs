namespace MOROPO.UIC.CoInnova.Models.View
{
    public class IdeaWorkflowStepsDisplayViewModel
    {
        public IdeaWorkflowStepsDisplayViewModel()
        {
            Steps = new System.Collections.Generic.List<IdeaWorkflowStepDisplayViewModel>();
        }

        public IdeaWorkflowDisplayViewModel Workflow { get; set; }

        public System.Collections.Generic.List<IdeaWorkflowStepDisplayViewModel> Steps { get; set; }
    }
}
