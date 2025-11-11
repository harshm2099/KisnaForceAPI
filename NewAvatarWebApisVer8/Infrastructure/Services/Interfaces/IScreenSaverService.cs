using NewAvatarWebApis.Core.Application.DTOs;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IScreenSaverService
    {
        public Task<ScreenSaverVideoResponse> ScreenSaverVideoList(ScreenSaverVideoParams screensaveview_params);
    }
}
