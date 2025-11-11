using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class InformationController : ControllerBase
    {
        private readonly IInformationService _informationService;

        public InformationController(IInformationService informationService)
        {
            _informationService = informationService;
        }

        [HttpPost("get-information")]
        public ServiceResponse<IList<T_INFO_SRC_MST>> GetAllInformation()
        {
            var result = _informationService.GetAllInformation();
            return result;
        }

        [HttpPost("add-information")]
        public Task<CommonResponse> AddInformation(T_INFO_SRC_MST info)
        {
            var result = _informationService.AddInformation(info);
            return result;
        }

        [HttpPost("edit-information")]
        public Task<CommonResponse> EditInformation(T_INFO_SRC_MST info)
        {
            var result = _informationService.EditInformation(info);
            return result;
        }

        [HttpPost("disable-information")]
        public Task<CommonResponse> DisableInformation(T_INFO_SRC_MST info)
        {
            var result = _informationService.DisableInformation(info);
            return result;
        }

    }
}
