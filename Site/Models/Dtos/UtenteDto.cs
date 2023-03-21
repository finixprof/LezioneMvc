using Site.Models.Entities;

namespace Site.Models.Dtos
{
    public class UtenteDto:Utente
    {
        public IFormFile FileFoto { get; set; }
    }
}
