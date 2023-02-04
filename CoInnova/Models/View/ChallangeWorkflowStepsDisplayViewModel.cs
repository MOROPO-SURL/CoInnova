namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ChallangeWorkflowStepsDisplayViewModel
    {
        public ChallangeWorkflowStepsDisplayViewModel()
        {
            Steps = new System.Collections.Generic.List<ChallangeWorkflowStepDisplayViewModel>();
        }

        public ChallangeWorkflowDisplayViewModel Workflow { get; set; }

        public System.Collections.Generic.List<ChallangeWorkflowStepDisplayViewModel> Steps { get; set; }
    }
}
