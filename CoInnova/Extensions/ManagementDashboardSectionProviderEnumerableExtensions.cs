using MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Models.View;
using System.Collections.Generic;

namespace MOROPO.UIC.CoInnova.Extensions
{
    public static class ManagementDashboardSectionProviderEnumerableExtensions
    {
        public static Dictionary<string, ICollection<ManagementDashboardSectionViewModel>> ToManagementDashboardSectionGroups(this IEnumerable<Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> self)
        {
            var securitySections = new List<ManagementDashboardSectionViewModel>();

            foreach (var provider in self)
            {
                if (provider is Framework.Guama.Security.Personal.Presentation.Web.Backend.Providers.ManagementDashboardSectionProvider)
                {
                    securitySections.AddRange(provider.GetManagementDashboardSections());
                }
            }

            securitySections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Registros",
                Controller = "traces",
                Action = "index",
                Area = "security",
                Role = Constants.Permission.TraceViewer
            });

            securitySections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Llaves API",
                Controller = "apikeys",
                Action = "index",
                Area = "security",
                Role = Constants.Permission.ApiKeyViewer
            });

            var innovationSections = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Agentes de Innovación",
                    Controller = "agents",
                    Action = "index",
                    Area = "innovation",
                    Role = Constants.Permission.InnovationAgentViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Retos de Innovación",
                    Controller = "challanges",
                    Action = "index",
                    Area = "innovation",
                    Role = Constants.Permission.ChallangeViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Ideas Innovadoras",
                    Controller = "ideas",
                    Action = "index",
                    Area = "innovation",
                    Role = Constants.Permission.IdeaViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Prototipos",
                    Controller = "prototypes",
                    Action = "index",
                    Area = "innovation",
                    Role = Constants.Permission.PrototypeViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Colaboradores",
                    Controller = "personcollaborators",
                    Action = "index",
                    Area = "innovation",
                    Role = Constants.Permission.CollaboratorViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Evaluaciones",
                    Controller = "evaluations",
                    Action = "index",
                    Area = "innovation",
                    Role = Constants.Permission.EvaluationViewer
                }
            };

            var auxiliarsSections = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Niveles de Prototipado",
                    Controller = "prototypinglevels",
                    Action = "index",
                    Area = "auxiliars",
                    Role = Constants.Permission.PrototypingLevelViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Modelos de Gestión de Retos",
                    Controller = "challangeworkflows",
                    Action = "index",
                    Area = "auxiliars",
                    Role = Constants.Permission.ChallangeWorkflowViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Modelos de Gestión de Ideas",
                    Controller = "ideaworkflows",
                    Action = "index",
                    Area = "auxiliars",
                    Role = Constants.Permission.IdeaWorkflowViewer
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Esferas de Actuación",
                    Controller = "spheres",
                    Action = "index",
                    Area = "auxiliars",
                    Role = Constants.Permission.SphereViewer
                },
                //new ManagementDashboardSectionViewModel
                //{
                //    Title = "Comentarios a Retos",
                //    Controller = "challangecomments",
                //    Action = "index",
                //    Area = "auxiliars",
                //    Role = Constants.Permission.ChallangeCommentViewer
                //},
                //new ManagementDashboardSectionViewModel
                //{
                //    Title = "Comentarios a Ideas",
                //    Controller = "ideacomments",
                //    Action = "index",
                //    Area = "auxiliars",
                //    Role = Constants.Permission.IdeaCommentViewer
                //}
            };

            return new Dictionary<string, ICollection<ManagementDashboardSectionViewModel>>
            {
                { "Seguridad", securitySections },
                { "Innovación", innovationSections },
                { "Auxiliares", auxiliarsSections }
            };
        }
    }
}
