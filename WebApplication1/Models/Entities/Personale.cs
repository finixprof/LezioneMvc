namespace WebApplication1.Models.Entities
{
    public class Paziente
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public DateTime DataNascita { get; set; }

        public string Provincia { get; set; }

        public char Sesso { get; set; }

    }
}
