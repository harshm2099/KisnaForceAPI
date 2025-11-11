using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;
using static NewAvatarWebApis.Core.Application.DTOs.DiamondCertificatePriceFilter;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface ISolitaireService
    {
        public Task<ResponseDetails> SoliCartStore(SoliCartStoreParams solicartstore_params);

        public Task<ResponseDetails> SoliterSortByList();

        public Task<ViewDiamondDetailNewResponse> ViewDiamondDetail_New(ViewDiamondDetailNewParams viewdiamonddetailnew_params);

        public Task<SoliCertificateMaster> SoliterdiamondCertificatesPriceFilter(PayloadsSoliterCertificate payloadsolitercertificate);

        public Task<SoliterPriceBreakup_Static> SoliterPriceBreakup(SoliterPriceBreakupPayload SoliterPriceBreakupPayload);

        public Task<FinalMRPNew_Static> FinalMrpNew(FinalMrpNewPayload FinalMrpNewPayload);

        public Task<DiamondCertificatePriceFilter_Static> Diamond_Certificates_price_filter_dist(DiamondCertificatePriceFilter DCFP_Payload);

        public Task<DiamondFilter_Static> DiamondFiltersList(DiamondFiltersList DUser);

        public Task<ResponseDetails> ViewDiamondDetail(Diamond param);

        public Task<ResponseDetails> DiamondGoldCappingGet(DiamondGoldCappingRequest param);

        public Task<ResponseDetails> SolitaireSubCategoryNewFranSIS(SolitaireFilterRequest request, CommonHeader header);

        public Task<ResponseDetails> SolitaireDetailsFranSIS(SolitaireDetailsRequest request, CommonHeader header);
    }
}
