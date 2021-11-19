using Microsoft.AspNetCore.Mvc;

using Backtracer.Api.Resources;
using Backtracer.Application.Services;
using Backtracer.Api.Attributes;

namespace Backtracer.Api.Controllers;

/// <summary>
/// Controller for constant realted endpoints
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiKey]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public class ConstantsController : ControllerBase {
    private readonly ILogger<ConstantsController> _logger;
    private readonly IConstantsService constantsService;

    /// <summary>
    /// Constructs new ConstantsController
    /// </summary>
    public ConstantsController(ILogger<ConstantsController> logger, IConstantsService constantsService) {
        _logger = logger;
        this.constantsService = constantsService;
    }

    /// <summary>
    /// Get all constant groups
    /// </summary>
    [HttpGet(Name = nameof(GetConstantGroups))]
    public async Task<IReadOnlyList<ConstantGroupResource>> GetConstantGroups() {
        return (await constantsService.GetConstantGroups()).Select(c => new ConstantGroupResource {
            Id = c.Id,
            Name = c.Name,
            CreatedAt = c.Created,
        }).ToList();
    }

    /// <summary>
    /// Create a new constant group
    /// </summary>
    [HttpPost(Name = nameof(CreateConstantGroup))]
    public async Task<ActionResult<ConstantResource>> CreateConstantGroup([FromBody] ConstantResource constant) {
        return Ok(constant);
    }

    /// <summary>
    /// Get all constant types
    /// </summary>
    [HttpGet("types", Name = nameof(GetConstantTypes))]
    public async Task<IReadOnlyList<ConstantTypeResource>> GetConstantTypes() {
        return Enumerable.Range(1, 5).Select(index => new ConstantTypeResource(index, "aaa"))
        .ToArray();
    }

    /// <summary>
    /// Get all constants in a group
    /// </summary>
    [HttpGet("{name}", Name = nameof(GetConstantByName))]
    public async Task<ActionResult<ConstantResource>> GetConstantByName(string name) {
        return new ConstantResource(name, new Dictionary<int, double> { { 1, 1.0 } });
    }
}
