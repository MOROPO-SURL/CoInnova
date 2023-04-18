namespace MOROPO.UIC.CoInnova.Models.View
{
    public class ChallangeDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.Challange, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Title { get; set; }

        public bool IsPublic { get; set; }

        public string Description { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public ChallangeWorkflowDisplayViewModel Workflow { get; set; }

        public void Import(Persistence.Entities.Challange entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            CreatedAt = entity.CreatedAt;
            IsPublic = entity.IsPublic;
            Workflow = new ChallangeWorkflowDisplayViewModel();

            Workflow.Import(entity.Workflow);
        }
    }
}
