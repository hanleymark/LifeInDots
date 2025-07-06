using System.ComponentModel.DataAnnotations;

namespace LifeInDots.Models;

public class CreateImageRequest
{
    [Required]
    [StringLength(10)]
    public string Version { get; set; } = "1.0";

    [Required]
    [RegularExpression(@"^\d+:\d+$", ErrorMessage = "AspectRatio must be in the format W:H, e.g. 16:9")]
    public string AspectRatio { get; set; } = "1:1";

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    [Required]
    [MinLength(1)]
    public List<PaletteColour> Palette { get; set; } = new();

    public List<Event> Events { get; set; } = new();
}
