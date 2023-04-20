using MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Extensions;
using System.Linq;

namespace MOROPO.UIC.CoInnova.Helpers
{
    public class DataInitializationHelper : Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper
    {
        public void InitialiseData(Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            var permissionDbSet = dbContext.Set<Framework.Guama.Core.Security.Common.Persistence.Entities.Permission>();

            #region ApiKey

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ApiKeyViewer)))
            {
                permissionDbSet.Add(Constants.Permission.ApiKeyViewer.ToPermissionEntity(Constants.PermissionsFamily.ApiKeyManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ApiKeyCreator)))
            {
                permissionDbSet.Add(Constants.Permission.ApiKeyCreator.ToPermissionEntity(Constants.PermissionsFamily.ApiKeyManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ApiKeyEditor)))
            {
                permissionDbSet.Add(Constants.Permission.ApiKeyEditor.ToPermissionEntity(Constants.PermissionsFamily.ApiKeyManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ApiKeyRemover)))
            {
                permissionDbSet.Add(Constants.Permission.ApiKeyRemover.ToPermissionEntity(Constants.PermissionsFamily.ApiKeyManagement));
            }
            #endregion

            #region PrototypeLevel

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.PrototypingLevelViewer)))
            {
                permissionDbSet.Add(Constants.Permission.PrototypingLevelViewer.ToPermissionEntity(Constants.PermissionsFamily.PrototypingLevelManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.PrototypingLevelCreator)))
            {
                permissionDbSet.Add(Constants.Permission.PrototypingLevelCreator.ToPermissionEntity(Constants.PermissionsFamily.PrototypingLevelManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.PrototypingLevelEditor)))
            {
                permissionDbSet.Add(Constants.Permission.PrototypingLevelEditor.ToPermissionEntity(Constants.PermissionsFamily.PrototypingLevelManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.PrototypingLevelRemover)))
            {
                permissionDbSet.Add(Constants.Permission.PrototypingLevelRemover.ToPermissionEntity(Constants.PermissionsFamily.PrototypingLevelManagement));
            }
            #endregion

            #region Challange

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeViewer)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeViewer.ToPermissionEntity(Constants.PermissionsFamily.ChallangeManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeCreator)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeCreator.ToPermissionEntity(Constants.PermissionsFamily.ChallangeManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeEditor)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeEditor.ToPermissionEntity(Constants.PermissionsFamily.ChallangeManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeRemover)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeRemover.ToPermissionEntity(Constants.PermissionsFamily.ChallangeManagement));
            }
            #endregion

            #region ChallangeWorkflow

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeWorkflowViewer)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeWorkflowViewer.ToPermissionEntity(Constants.PermissionsFamily.ChallangeWorkflowManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeWorkflowCreator)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeWorkflowCreator.ToPermissionEntity(Constants.PermissionsFamily.ChallangeWorkflowManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeWorkflowEditor)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeWorkflowEditor.ToPermissionEntity(Constants.PermissionsFamily.ChallangeWorkflowManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeWorkflowRemover)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeWorkflowRemover.ToPermissionEntity(Constants.PermissionsFamily.ChallangeWorkflowManagement));
            }
            #endregion

            #region Idea

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaViewer)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaViewer.ToPermissionEntity(Constants.PermissionsFamily.IdeaManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaCreator)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaCreator.ToPermissionEntity(Constants.PermissionsFamily.IdeaManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaEditor)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaEditor.ToPermissionEntity(Constants.PermissionsFamily.IdeaManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaRemover)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaRemover.ToPermissionEntity(Constants.PermissionsFamily.IdeaManagement));
            }
            #endregion

            #region IdeaWorkflow

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaWorkflowViewer)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaWorkflowViewer.ToPermissionEntity(Constants.PermissionsFamily.IdeaWorkflowManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaWorkflowCreator)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaWorkflowCreator.ToPermissionEntity(Constants.PermissionsFamily.IdeaWorkflowManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaWorkflowEditor)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaWorkflowEditor.ToPermissionEntity(Constants.PermissionsFamily.IdeaWorkflowManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaWorkflowRemover)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaWorkflowRemover.ToPermissionEntity(Constants.PermissionsFamily.IdeaWorkflowManagement));
            }
            #endregion

            #region Collaborator

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.CollaboratorViewer)))
            {
                permissionDbSet.Add(Constants.Permission.CollaboratorViewer.ToPermissionEntity(Constants.PermissionsFamily.CollaboratorManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.CollaboratorCreator)))
            {
                permissionDbSet.Add(Constants.Permission.CollaboratorCreator.ToPermissionEntity(Constants.PermissionsFamily.CollaboratorManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.CollaboratorEditor)))
            {
                permissionDbSet.Add(Constants.Permission.CollaboratorEditor.ToPermissionEntity(Constants.PermissionsFamily.CollaboratorManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.CollaboratorRemover)))
            {
                permissionDbSet.Add(Constants.Permission.CollaboratorRemover.ToPermissionEntity(Constants.PermissionsFamily.CollaboratorManagement));
            }
            #endregion

            #region Evaluation

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.EvaluationViewer)))
            {
                permissionDbSet.Add(Constants.Permission.EvaluationViewer.ToPermissionEntity(Constants.PermissionsFamily.EvaluationManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.EvaluationCreator)))
            {
                permissionDbSet.Add(Constants.Permission.EvaluationCreator.ToPermissionEntity(Constants.PermissionsFamily.EvaluationManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.EvaluationEditor)))
            {
                permissionDbSet.Add(Constants.Permission.EvaluationEditor.ToPermissionEntity(Constants.PermissionsFamily.EvaluationManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.EvaluationRemover)))
            {
                permissionDbSet.Add(Constants.Permission.EvaluationRemover.ToPermissionEntity(Constants.PermissionsFamily.EvaluationManagement));
            }
            #endregion

            #region Prototype

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.PrototypeViewer)))
            {
                permissionDbSet.Add(Constants.Permission.PrototypeViewer.ToPermissionEntity(Constants.PermissionsFamily.PrototypeManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.PrototypeCreator)))
            {
                permissionDbSet.Add(Constants.Permission.PrototypeCreator.ToPermissionEntity(Constants.PermissionsFamily.PrototypeManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.PrototypeEditor)))
            {
                permissionDbSet.Add(Constants.Permission.PrototypeEditor.ToPermissionEntity(Constants.PermissionsFamily.PrototypeManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.PrototypeRemover)))
            {
                permissionDbSet.Add(Constants.Permission.PrototypeRemover.ToPermissionEntity(Constants.PermissionsFamily.PrototypeManagement));
            }
            #endregion

            #region ChallangeComment

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeCommentViewer)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeCommentViewer.ToPermissionEntity(Constants.PermissionsFamily.ChallangeCommentManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.ChallangeCommentRemover)))
            {
                permissionDbSet.Add(Constants.Permission.ChallangeCommentRemover.ToPermissionEntity(Constants.PermissionsFamily.ChallangeCommentManagement));
            }
            #endregion

            #region IdeaComment

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaCommentViewer)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaCommentViewer.ToPermissionEntity(Constants.PermissionsFamily.IdeaCommentManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.IdeaCommentRemover)))
            {
                permissionDbSet.Add(Constants.Permission.IdeaCommentRemover.ToPermissionEntity(Constants.PermissionsFamily.IdeaCommentManagement));
            }

            #endregion

            #region Sphere

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.SphereViewer)))
            {
                permissionDbSet.Add(Constants.Permission.SphereViewer.ToPermissionEntity(Constants.PermissionsFamily.SphereManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.SphereRemover)))
            {
                permissionDbSet.Add(Constants.Permission.SphereRemover.ToPermissionEntity(Constants.PermissionsFamily.SphereManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.SphereEditor)))
            {
                permissionDbSet.Add(Constants.Permission.SphereEditor.ToPermissionEntity(Constants.PermissionsFamily.SphereManagement));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.SphereRemover)))
            {
                permissionDbSet.Add(Constants.Permission.SphereRemover.ToPermissionEntity(Constants.PermissionsFamily.SphereManagement));
            }

            #endregion

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Permission.TraceViewer)))
            {
                permissionDbSet.Add(Constants.Permission.TraceViewer.ToPermissionEntity());
            }

            if (dbContext is Framework.Guama.Core.Persistence.ORM.EF.Context.Contract.ITraceableDbContext)
            {
                (dbContext as Framework.Guama.Core.Persistence.ORM.EF.Context.Contract.ITraceableDbContext).SaveChanges("system");
            }
            else
            {
                dbContext.SaveChanges();
            }
        }
    }
}
