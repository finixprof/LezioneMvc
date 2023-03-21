using Site.Models.Entities;

namespace Site.Models.Views
{
    public class AreaRiservataViewModel : BaseViewModel
    {
        public int Id { get; set; }

        public string FileFoto { get; set; }


        public Utente Utente { get; set; }
    }
}
