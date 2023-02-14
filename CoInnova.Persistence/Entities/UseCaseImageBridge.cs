namespace MOROPO.UIC.CoInnova.Persistence.Entities
{
    public class UseCaseImageBridge
    {
        public System.Guid UseCaseId { get; set; }

        public System.Guid ImageId { get; set; }

        public UseCase UseCase { get; set; }

        public SAPRESSC.Middleware.Media.Persistence.Entities.Image Image { get; set; }
    }
}
