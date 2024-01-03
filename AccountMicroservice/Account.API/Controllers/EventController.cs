using Account.Application.DTOs;
using Account.Application.DTOs.APIDataFormatters;
using Account.Application.Interfaces.Services;
using Account.Application.ViewModels.QueryFilters;
using Account.Application.ViewModels.Requests;
using Account.Application.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.DTO.Pagination;

namespace Account.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class EventController : BaseController
    {
        private readonly IBackgroundService _backgroundService;
        public EventController(IBackgroundService backgroundService)
        {
            _backgroundService = backgroundService;
        }

        [HttpGet("autoExpireAccount")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<SingleCountDto>))]
        public async Task<IActionResult> TriggerAutoExpireAccount(CancellationToken cancellationToken = default) => Response(await _backgroundService.AutoExpireAccount(cancellationToken));

        [HttpGet("synchronizeSapData")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SwaggerResponse<SingleCountDto>))]
        public async Task<IActionResult> TriggerSynchronizeSapData(CancellationToken cancellationToken = default) => Response(await _backgroundService.SynchronizeSapData(cancellationToken));
    }
}
