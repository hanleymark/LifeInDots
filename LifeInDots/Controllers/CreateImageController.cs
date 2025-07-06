using LifeInDots.Models;
using LifeInDots.Services;
using Microsoft.AspNetCore.Mvc;

namespace LifeInDots.Controllers;

[ApiController]
[Route("[controller]")]
public class CreateImageController : ControllerBase
{
    private readonly SvgImageGenerator _svgGenerator;

    public CreateImageController(SvgImageGenerator svgGenerator)
    {
        _svgGenerator = svgGenerator;
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateImageRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var svg = _svgGenerator.Generate(request);

        return Content(svg, "image/svg+xml");
    }
}
