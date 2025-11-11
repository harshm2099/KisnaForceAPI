using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Core.Domain.Common.NewAvatarWebApis.Models;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ScreenSaverController : ControllerBase
    {
        private readonly IScreenSaverService _screenSaverService;
        public ScreenSaverController(IScreenSaverService screenSaverService)
        {
            _screenSaverService = screenSaverService;
        }

        [HttpPost("screen-saver-video-list")]
        public async Task<ScreenSaverVideoResponse> ScreenSaverVideoList(ScreenSaverVideoParams screensaveview_params)
        {
            var result = await _screenSaverService.ScreenSaverVideoList(screensaveview_params);
            return result;
        }
    }
}
