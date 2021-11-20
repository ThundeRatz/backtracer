using Microsoft.AspNetCore.Mvc;

using Backtracer.Api.Resources;
using Backtracer.Application.Services;
using Backtracer.Api.Attributes;
using Backtracer.Application.Model;
using Backtracer.Application.Exceptions;
using System.Net.Mime;

namespace Backtracer.Api.Controllers;

/// <summary>
/// Controller for constant realted endpoints
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiKey]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
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
    public async Task<IEnumerable<ConstantGroupResource>> GetConstantGroups() {
        return (await constantsService.GetConstantGroups()).Select(c => new ConstantGroupResource {
            Id = c.Id,
            Name = c.Name,
            CreatedAt = c.Created,
        });
    }

    /// <summary>
    /// Create a new constant group
    /// </summary>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">In case the group name already exists or a constant type is not found</response>
    [HttpPost(Name = nameof(CreateConstantGroup))]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ConstantGroupResource>> CreateConstantGroup([FromBody] ConstantResource constant) {
        ConstantGroup created;

        try {
            created = await constantsService.CreateConstantGroup(new ConstantGroup {
                Name = constant.Name,
            }, constant.Values);
        } catch (ConstantsServiceException e) {
            return BadRequest(e.Message);
        }

        return CreatedAtAction(nameof(GetConstantByName), new { name = created.Name }, new ConstantGroupResource {
            Id = created.Id,
            Name = created.Name,
            CreatedAt = created.Created,
        });
    }

    /// <summary>
    /// Get all constant types
    /// </summary>
    [HttpGet("types", Name = nameof(GetConstantTypes))]
    public async Task<IEnumerable<ConstantTypeResource>> GetConstantTypes() {
        return (await constantsService.GetConstantTypes()).Select(t => new ConstantTypeResource {
            Id = t.Id,
            Description = t.Description,
        });
    }

    /// <summary>
    /// Get all constants in a group
    /// </summary>
    [HttpGet("{name}", Name = nameof(GetConstantByName))]
    public async Task<ActionResult<ConstantResource>> GetConstantByName(string name) {
        var group = await constantsService.GetConstantGroupByName(name);

        if (group == null) {
            return NotFound();
        }

        return Ok(new ConstantResource {
            Name = group.Name,
            Values = group.Constants
        });
    }
}
