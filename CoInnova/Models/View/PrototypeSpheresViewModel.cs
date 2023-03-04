namespace MOROPO.UIC.CoInnova.Models.View
{
    public class PrototypeSpheresViewModel
    {
        public PrototypeDisplayViewModel Prototype { get; set; }

        public SphereDisplayViewModel[] Spheres { get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableSpheres { get; set; }
    }
}
