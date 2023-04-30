namespace MOROPO.UIC.CoInnova.Models.View
{
    public class InnovationAgentContactsViewModel
    {
        public InnovationAgentDisplayViewModel Agent { get; set; }

        public System.Collections.Generic.List<PhoneContactDisplayViewModel> PhoneContacts { get; set; }

        public System.Collections.Generic.List<EmailContactDisplayViewModel> EmailContacts { get; set; }
    }
}
