using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using static NewAvatarWebApis.Core.Application.DTOs.DiamondCertificatePriceFilter;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SolitaireController : ControllerBase
    {
        private readonly ISolitaireService _solitaireService;

        public SolitaireController(ISolitaireService solitaireService)
        {
            _solitaireService = solitaireService;
        }

        [HttpPost("solitaire-cart-store")]
        public async Task<ResponseDetails> SoliCartStore(SoliCartStoreParams solicartstore_params)
        {
            var result = await _solitaireService.SoliCartStore(solicartstore_params);
            return result;
        }

        [HttpPost("solitaire_sort-by")]
        public async Task<ResponseDetails> SoliterSortByList()
        {
            var result = await _solitaireService.SoliterSortByList();
            return result;
        }

        [HttpPost("view-diamond-detail-new")]
        public async Task<ViewDiamondDetailNewResponse> ViewDiamondDetail_New(ViewDiamondDetailNewParams viewdiamonddetailnew_params)
        {
            var result = await _solitaireService.ViewDiamondDetail_New(viewdiamonddetailnew_params);
            return result;
        }

        [HttpPost("diamond-certificates-price-filter")]
        public async Task<SoliCertificateMaster> SoliterdiamondCertificatesPriceFilter(PayloadsSoliterCertificate payloadsolitercertificate)
        {
            var result = await _solitaireService.SoliterdiamondCertificatesPriceFilter(payloadsolitercertificate);
            return result;
        }

        [HttpPost("solitaire-price-breakup")]
        public async Task<SoliterPriceBreakup_Static> SoliterPriceBreakup(SoliterPriceBreakupPayload SoliterPriceBreakupPayload)
        {
            var result = await _solitaireService.SoliterPriceBreakup(SoliterPriceBreakupPayload);
            return result;
        }

        [HttpPost("final-mrp-new")]
        public async Task<FinalMRPNew_Static> FinalMrpNew(FinalMrpNewPayload FinalMrpNewPayload)
        {
            var result = await _solitaireService.FinalMrpNew(FinalMrpNewPayload);
            return result;
        }

        [HttpPost("diamond-certificates-price-filter-distributor")]
        public async Task<DiamondCertificatePriceFilter_Static> Diamond_Certificates_price_filter_dist(DiamondCertificatePriceFilter DCFP_Payload)
        {
            var result = await _solitaireService.Diamond_Certificates_price_filter_dist(DCFP_Payload);
            return result;
        }

        [HttpPost("diamond-filters-list")]
        public async Task<DiamondFilter_Static> DiamondFiltersList(DiamondFiltersList DUser)
        {
            var result = await _solitaireService.DiamondFiltersList(DUser);
            return result;
        }

        [HttpPost("view-diamond-detail")]
        public async Task<ResponseDetails> ViewDiamondDetail(Diamond param)
        {
            var result = await _solitaireService.ViewDiamondDetail(param);
            return result;
        }

        [HttpPost("diamond-gold-capping-get")]
        public async Task<ResponseDetails> DiamondGoldCappingGet(DiamondGoldCappingRequest param)
        {
            var result = await _solitaireService.DiamondGoldCappingGet(param);
            return result;
        }

        [HttpPost("solitaire-sub-category-filter-franSIS")]
        public async Task<ResponseDetails> SolitaireSubCategoryNewFranSIS([FromBody] SolitaireFilterRequest request)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _solitaireService.SolitaireSubCategoryNewFranSIS(request, commonHeader);
            return result;
        }

        [HttpPost("solitaire-details-franSIS")]
        public async Task<ResponseDetails> SolitaireDetailsFranSIS([FromBody] SolitaireDetailsRequest request)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _solitaireService.SolitaireDetailsFranSIS(request, commonHeader);
            return result;
        }
    }
}
