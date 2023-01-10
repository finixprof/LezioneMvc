namespace WebApplication1.Models.Entities
{
    public class Personale
    {
        public int Id { get; set; }
        public string Cognome { get; set; }

        public string Professione { get; set; }

        public DateTime DataNascita { get; set; }

        public string Reparto { get; set; }
        public double Stipendio { get; set; }

        public int Superiore { get; set; }
    }
}
