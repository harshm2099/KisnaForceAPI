using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Core.Domain.Common.NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        public Task<CommonResponse> ChangePassword(ChangePasswordParams request);
        public Task<CommonResponse> ResetPassword(ChangePasswordParams request);
    }
}
