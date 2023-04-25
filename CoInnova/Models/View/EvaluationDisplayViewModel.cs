using System.Linq;

namespace MOROPO.UIC.CoInnova.Models.View
{
    public class EvaluationDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<Persistence.Entities.Evaluation, System.Guid>
    {
        public System.Guid Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public IdeaDisplayViewModel Idea { get; set; }
        
        public int Valoration { get; set; }

        public string Argument { get; set; }

        public CollaboratorDisplayViewModel[] Collaborators { get; set; }

        public void Import(Persistence.Entities.Evaluation entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            Valoration = entity.Valoration;
            Argument = entity.Argument;
            Idea = new IdeaDisplayViewModel
            {
                Id = entity.Idea.Id,
                Title = entity.Idea.Title
            };
            Collaborators = entity.CollaboratorBridges.Select(item =>
            {
                var model = new CollaboratorDisplayViewModel();

                model.Import(item.Stern);

                return model;
            }).ToArray();
        }
    }
}
