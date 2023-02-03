namespace WebApplication1.Models.Entities
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime? DataCreazione { get; set; }

        public DateTime? DataUltimaModifica { get; set; }
    }
}
