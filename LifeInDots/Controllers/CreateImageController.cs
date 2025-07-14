using LifeInDots.Models;
using LifeInDots.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LifeInDots.Controllers;

[ApiController]
[Route("v1/createimage")]
public class CreateImageController : ControllerBase
{
    private readonly SvgImageGenerator _svgGenerator;

    public CreateImageController(SvgImageGenerator svgGenerator)
    {
        _svgGenerator = svgGenerator;
    }

    [HttpPost]
    public IActionResult Post([FromQuery] bool download, [FromBody] CreateImageRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var svg = _svgGenerator.Generate(request);

        if (download)
        {
            var svgBytes = Encoding.UTF8.GetBytes(svg);
            var filename = $"life-in-dots-{DateTime.UtcNow:yyyyMMddHHmmss}.svg";

            return File(svgBytes, "image/svg+xml", filename);
        }

        return Content(svg, "image/svg+xml");
    }
}
