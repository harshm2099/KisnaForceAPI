using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Core.Domain.Common.NewAvatarWebApis.Models;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            // Now the types match, and the assignment is valid.
            _userService = userService;
        }

        [HttpPost("change-password")]
        public async Task<CommonResponse> ChangePassword(ChangePasswordParams request)
        {
            var result = await _userService.ChangePassword(request);
            return result;
        }

        [HttpPost("reset-password")]
        public async Task<CommonResponse> ResetPassword(ChangePasswordParams request)
        {
            var result = await _userService.ResetPassword(request);
            return result;
        }
    }
}


