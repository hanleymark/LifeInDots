using System.ComponentModel.DataAnnotations;

namespace LifeInDots.Models;

public class PaletteColor
{
    [Required]
    [StringLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [RegularExpression("^#(?:[0-9a-fA-F]{3}){1,2}$", ErrorMessage = "Hex must be a valid colour code (e.g. #FF9900)")]
    public string Hex { get; set; } = string.Empty;
}
