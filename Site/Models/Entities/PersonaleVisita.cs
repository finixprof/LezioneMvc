namespace Site.Models.Entities
{
    public class PersonaleVisita:EntityBase
    {

        public int VisitaId { get; set; }

        public Visita Visita { get; set; }
        public int PersonaleId { get; set; }

        public Personale Personale { get; set; }


        public string InQualita { get; set; }
    }
}
