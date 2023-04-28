namespace MOROPO.UIC.CoInnova.Models.View
{
    public class IdeaWorkflowIterationsViewModel
    {
        public IdeaDisplayViewModel Idea { get; set; }

        public IdeaWorkflowStepDisplayViewModel NextIdeaWorkflowStep { get; set; }

        public IdeaWorkflowStepDisplayViewModel[] PendingWorkflowSteps { get; set; }

        public IdeaWorkflowIterationDisplayViewModel[] WorkflowIterations { get; set; }
    }
}
