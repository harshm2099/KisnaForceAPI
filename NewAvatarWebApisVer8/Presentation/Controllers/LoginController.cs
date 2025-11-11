using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Core.Domain.Common.NewAvatarWebApis.Models;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using System.Reflection.PortableExecutable;

namespace NewAvatarWebApis.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class LoginController : ControllerBase
    {
        // CHANGE THIS LINE: Change the field type to the interface.
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<CommonResponse> Login(LoginParams data)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            // Pass the extracted values to your service
            var result = await _loginService.Login(data, commonHeader);
            return result;
        }

        [Route("send-otp")]
        [HttpPost]
        public async Task<CommonResponse> SendOtp(SendOtpParams request)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _loginService.SendOtp(request, commonHeader);
            return result;
        }

        [Route("send-email-otp")]
        [HttpPost]
        public async Task<CommonResponse> SendEmailOtp([FromBody] SendEmailOtpParams request)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _loginService.SendEmailOtp(request, commonHeader);
            return result;
        }

        [Route("send-otp-me")]
        [HttpPost]
        public async Task<CommonResponse> sendOtpme(SendOtpmeParams request)
        {
            var result = await _loginService.sendOtpme(request);
            return result;
        }

        [Route("verify-email")]
        [HttpPost]
        public async Task<CommonResponse> VerifyEmail(VerifyEmailParams request)
        {
            var result = await _loginService.VerifyEmail(request);
            return result;
        }

        [Route("login-with-mobile-otp")]
        [HttpPost]
        public async Task<CommonResponse> LoginWithMobileOTP([FromBody] LoginParamsWithMobileOtp login_params)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _loginService.LoginWithMobileOTP(login_params, commonHeader);
            return result;
        }

        [Route("login-new-emob")]
        [HttpPost]
        public async Task<CommonResponse> LoginNewEMob(LoginNewEMobParams request)
        {
            var result = await _loginService.LoginNewEMob(request);
            return result;
        }

        [Route("login-with-email-otp")]
        [HttpPost]
        public async Task<CommonResponse> LoginWithEmailOTP([FromBody] LoginParams login_params)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _loginService.LoginWithEmailOTP(login_params, commonHeader);
            return result;
        }

        [Route("login-with-userid")]
        [HttpPost]
        public async Task<CommonResponse> LoginWithUserID([FromBody] LoginWithUserIdParams login_params)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _loginService.LoginWithUserID(login_params, commonHeader);
            return result;
        }
    }
}