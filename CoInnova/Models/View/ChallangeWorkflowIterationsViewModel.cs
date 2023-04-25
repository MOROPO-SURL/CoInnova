namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ChallangeWorkflowIterationsViewModel
    {
        public ChallangeDisplayViewModel Challange { get; set; }

        public ChallangeWorkflowStepDisplayViewModel NextChallangeWorkflowStep { get; set; }

        public ChallangeWorkflowStepDisplayViewModel[] PendingWorkflowSteps { get; set; }

        public ChallangeWorkflowIterationDisplayViewModel[] WorkflowIterations { get; set; }
    }
}
