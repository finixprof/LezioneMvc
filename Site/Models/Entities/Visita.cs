namespace Site.Models.Entities
{
    public class Visita: EntityBase
    {
        public DateTime DataVisita { get; set; }

        public int Peso { get; set; }
        public int Altezza { get; set; }
        public int PressioneMin { get; set; }

        public int PressioneMax { get; set; }

        public int IdPaziente { get; set; }



    }
}
