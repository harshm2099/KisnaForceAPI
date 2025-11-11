using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Services;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using static NewAvatarWebApis.Core.Application.DTOs.HomeDetailList;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class HomeListController : ControllerBase
    {
        private readonly IHomeListService _homeListService;

        public HomeListController(IHomeListService homeListService)
        {
            _homeListService = homeListService;
        }

        [HttpPost("home-screen-master")]
        public Task<ResponseDetails> HomeScreenMaster(HomeScreenMasterParams homescreenmasterparams)
        {
            var result = _homeListService.HomeScreenMaster(homescreenmasterparams);
            return result;
        }

        [HttpPost("home-list")]
        public Task<HomeDataListResponse> HomeList()
        {
            var result = _homeListService.HomeList();
            return result;
        }

        [HttpPost("home-list-new")]
        public async Task<HomeDataListResponse> HomeListNew([FromBody]HomeListRequest param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _homeListService.HomeListNew(param, commonHeader);
            return result;
        }
    }
}
