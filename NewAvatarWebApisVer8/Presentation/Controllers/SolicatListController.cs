using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using static NewAvatarWebApis.Core.Application.DTOs.SoliCatList;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SolicatListController : ControllerBase
    {
        private readonly ISolicatListService _solicatListService;
        public SolicatListController(ISolicatListService solicatListService)
        {
            _solicatListService = solicatListService;
        }

        [HttpGet("solitaire-category-list")]
        public async Task<SoliCatList_Static> GetSolitaireCategoryList(SoliCatList SCUser)
        {
            var result = await _solicatListService.GetSolitaireCategoryList(SCUser);
            return result;
        }
    }
}
