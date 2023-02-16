namespace Site.Models.Entities
{
    public class VisitaPersonale:EntityBase
    {

        public int IdVisita { get; set; }

        public Visita Visita { get; set; }
        public int IdPersonale { get; set; }

        public Personale Personale { get; set; }


        public string InQualita { get; set; }
    }
}
