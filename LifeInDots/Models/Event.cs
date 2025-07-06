using System.ComponentModel.DataAnnotations;

namespace LifeInDots.Models;

public class Event
{
    [Required]
    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ColourDescription { get; set; } = string.Empty;

    public bool IsSingleDay =>
        !EndDate.HasValue || EndDate.Value == StartDate;
}
