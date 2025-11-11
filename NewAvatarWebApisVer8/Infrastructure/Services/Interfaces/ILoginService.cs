using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Core.Domain.Common.NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    // Rather than having the class create the service itself,
    // We interface for loose coupling means your code is less dependent on specific implementation.
    // We use concept of Dependency Injection and Loose Coupling.
    public interface ILoginService
    {
        public Task<CommonResponse> Login([FromBody]LoginParams request, CommonHeader header);
        public Task<CommonResponse> SendOtp([FromBody]SendOtpParams request, CommonHeader header);
        public Task<CommonResponse> SendEmailOtp(SendEmailOtpParams request, CommonHeader header);
        public Task<CommonResponse> sendOtpme(SendOtpmeParams request);
        public Task<CommonResponse> VerifyEmail(VerifyEmailParams request);
        public Task<CommonResponse> LoginWithMobileOTP(LoginParamsWithMobileOtp login_params, CommonHeader header);
        public Task<CommonResponse> LoginNewEMob(LoginNewEMobParams request);
        public Task<CommonResponse> LoginWithEmailOTP(LoginParams login_params, CommonHeader header);
        public Task<CommonResponse> LoginWithUserID(LoginWithUserIdParams login_params, CommonHeader header);
    }
}    
