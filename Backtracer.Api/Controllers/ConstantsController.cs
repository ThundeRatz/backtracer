using Microsoft.AspNetCore.Mvc;

using Backtracer.Api.Resources;
using Backtracer.Persistence.Model;

namespace Backtracer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConstantsController : ControllerBase {
    private readonly ILogger<ConstantsController> _logger;

    public ConstantsController(ILogger<ConstantsController> logger) {
        _logger = logger;
    }

    /// <summary>
    /// Get All Constant Groups
    /// </summary>
    [HttpGet(Name = nameof(GetConstantGroups))]
    public IEnumerable<ConstantGroupResource> GetConstantGroups() {
        return Enumerable.Range(1, 5).Select(index => new ConstantGroupResource(index, "name", DateTime.Now))
        .ToArray();
    }

    /// <summary>
    /// Create a new constant group
    /// </summary>
    [HttpPost(Name = nameof(CreateConstantGroup))]
    public async Task<ActionResult<ConstantResource>> CreateConstantGroup([FromBody] ConstantResource constant) {
        return Ok(constant);
    }

    [HttpGet("types", Name = nameof(GetConstantTypes))]
    public IEnumerable<ConstantTypeResource> GetConstantTypes() {
        return Enumerable.Range(1, 5).Select(index => new ConstantTypeResource(index, "aaa"))
        .ToArray();
    }

    [HttpGet("{name}", Name = nameof(GetConstantByName))]
    public ConstantResource GetConstantByName(string name) {
        return new ConstantResource(name, new Dictionary<int, double> { { 1, 1.0 } });
    }
}
