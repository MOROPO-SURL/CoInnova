namespace MOROPO.UIC.CoInnova.Helpers
{
    public class ConcretModelBindHelper : Framework.Guama.Core.Presentation.Web.Common.ModelBinding.Contract.IConcretModelBindHelper
    {
        public void DoConcretBind(object model, Microsoft.AspNetCore.Mvc.ModelBinding.ModelBindingContext bindingContext)
        {
            (model as Models.View.UserProfileViewModel).Name = bindingContext.ValueProvider.GetValue("Name").FirstValue;

            (model as Models.View.UserProfileViewModel).LastName = bindingContext.ValueProvider.GetValue("LastName").FirstValue;

            (model as Models.View.UserProfileViewModel).Sex = System.Enum.Parse<Framework.Guama.Identity.Persistence.Enumerations.Sex>(bindingContext.ValueProvider.GetValue("Sex").FirstValue);

            (model as Models.View.UserProfileViewModel).About = bindingContext.ValueProvider.GetValue("About").FirstValue;

            var bornDateValue = bindingContext.ValueProvider.GetValue("BornDate").FirstValue;

            if (string.IsNullOrEmpty(bornDateValue))
            {
                (model as Models.View.UserProfileViewModel).BornDate = null;
            }
            else
            {
                (model as Models.View.UserProfileViewModel).BornDate = System.DateTime.Parse(bornDateValue);
            }
        }
    }
}
