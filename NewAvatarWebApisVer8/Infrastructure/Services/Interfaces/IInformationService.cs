using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IInformationService
    {
        public ServiceResponse<IList<T_INFO_SRC_MST>> GetAllInformation();

        public Task<CommonResponse> AddInformation(T_INFO_SRC_MST info);

        public Task<CommonResponse> EditInformation(T_INFO_SRC_MST info);

        public Task<CommonResponse> DisableInformation(T_INFO_SRC_MST info);
    }
}
