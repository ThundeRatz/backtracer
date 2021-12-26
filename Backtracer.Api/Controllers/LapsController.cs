using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

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
[Produces(MediaTypeNames.Application.Json)]
public class LapsController : ControllerBase {
    private readonly ILogger<LapsController> _logger;

    /// <summary>
    /// Constructs new LapsController
    /// </summary>
    public LapsController(ILogger<LapsController> logger, IConstantsService constantsService) {
        _logger = logger;
    }
}
