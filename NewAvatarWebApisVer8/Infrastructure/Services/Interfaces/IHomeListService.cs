using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;
using static NewAvatarWebApis.Core.Application.DTOs.HomeDetailList;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IHomeListService
    {
        public Task<ResponseDetails> HomeScreenMaster(HomeScreenMasterParams homescreenmasterparams);
        public Task<HomeDataListResponse> HomeList();
        public Task<HomeDataListResponse> HomeListNew([FromBody]HomeListRequest param, [FromHeader] CommonHeader header);
    }
}
