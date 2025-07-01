namespace LifeInDots.Models
{
    public class Event
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public string ColourDescription { get; set; }
        public bool IsSingleDay => !EndDate.HasValue || EndDate.Value.Date == StartDate.Date;
    }
}
