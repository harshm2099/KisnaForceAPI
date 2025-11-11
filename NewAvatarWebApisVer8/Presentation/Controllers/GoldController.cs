using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class GoldController :  ControllerBase
    {
        private readonly IGoldService _goldService;

        public GoldController(IGoldService goldService)
        {
            _goldService = goldService;
        }

        [HttpPost("plain-gold-item-detail")]
        public Task<GoldView_Static> ViewPlainGoldDetails(PayloadGoldDetails Details)
        {
            var result = _goldService.ViewPlainGoldDetails(Details);
            return result;
        }

        [HttpPost("plain-gold-item-filter-fransis")]
        public async Task<ResponseDetails> PlainGoldItemFilterFranSIS([FromBody] PlainGoldFilter request)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _goldService.PlainGoldItemFilterFranSIS(request, commonHeader);
            return result;
        }

        [HttpPost("plain-gold-item-detail-fran")]
        public async Task<ResponseDetails> PlainGoldItemDetailsFran([FromBody] PlainGoldItemDetailsRequest request)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _goldService.PlainGoldItemDetailsFran(request, commonHeader);
            return result;
        }

        [HttpPost("total-gold-diaa-weight")]
        public async Task<ResponseDetails> TotalGoldDiaaWeight(TotalGoldDiaaWeightRequest param)
        {
            var result = await _goldService.TotalGoldDiaaWeight(param);
            return result;
        }

        [HttpPost("extra-gold-rate-wise-rate")]
        public async Task<ResponseDetails> ExtraGoldRateWiseRate(ExtraGoldRateWiseRateRequest param)
        {
            var result = await _goldService.ExtraGoldRateWiseRate(param);
            return result;
        }
    }
}
