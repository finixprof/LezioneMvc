namespace Site.Models.Entities
{
    public class Utente: EntityBase
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsMailConfermata { get; set; }
    }
}
