namespace MOROPO.UIC.CoInnova.Models.View
{
    public class UseCaseDisplayViewModel : SAPRESSC.Core.Persistence.Models.IEntityDisplayModel<Persistence.Entities.UseCase, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string About { get; set; }

        public CollaboratorDisplayViewModel Collaborator { get; set; }

        public void Import(Persistence.Entities.UseCase entity)
        {
            Id = entity.Id;
            About = entity.About;
            Collaborator = new CollaboratorDisplayViewModel();

            Collaborator.Import(entity.Benefited);
        }
    }
}
