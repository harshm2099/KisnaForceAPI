using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IGoldService
    {
        public Task<GoldView_Static> ViewPlainGoldDetails(PayloadGoldDetails Details);

        public Task<ResponseDetails> PlainGoldItemFilterFranSIS([FromBody]PlainGoldFilter request, CommonHeader header);

        public Task<ResponseDetails> PlainGoldItemDetailsFran(PlainGoldItemDetailsRequest request, CommonHeader header);

        public Task<ResponseDetails> TotalGoldDiaaWeight(TotalGoldDiaaWeightRequest param);

        public Task<ResponseDetails> ExtraGoldRateWiseRate(ExtraGoldRateWiseRateRequest param);
    }
}
