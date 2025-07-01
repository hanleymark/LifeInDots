namespace LifeInDots.Models
{
    public class CreateImageRequest
    {
        public string Version { get; set; }
        public string AspectRatio { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<PaletteColour> Palette { get; set; }
        public List<Event> Events { get; set; } = new();
    }
}
