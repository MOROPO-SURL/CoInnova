namespace MOROPO.UIC.CoInnova.Models.View
{
    public class InnovationAgentDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.InnovationAgent, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public OrganizationDisplayViewModel Identity { get; set; }

        public void Import(Persistence.Entities.InnovationAgent entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            Identity = new OrganizationDisplayViewModel();

            Identity.Import(entity.Identity);
        }
    }
}
