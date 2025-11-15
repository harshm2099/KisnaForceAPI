using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Models;
using static NewAvatarWebApis.Infrastructure.Services.CartService;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface ICartService
    {
        public Task<ResponseDetails> GetCartCount(CartCountListingParams cartcountparams);

        public Task<ResponseDetails> GetCartBillingForTypeAsync(CartBillingForTypeListingParams cartbillingfortypeparams);

        public Task<ResponseDetails> GetCartBillingUserListAsync(CartBillingUserListingParams cartbillinguserlistparams);

        public Task<ResponseDetails> GetCartOrderBillingForTypeAsync(CartOrderBillingForTypeListingParams cartorderbillingfortypeparams);

        public Task<ResponseDetails> GetCartOrderBillingUserListAsync(CartOrderBillingUserListingParams cartorderbillinguserlistparams);

        public Task<ResponseDetails> GetCartOrderTypeListAsync(CartOrderTypeListingParams cartordertypelistparams);

        public Task<ResponseDetails> GetCartItemList(CartItemListingParams cartitemlistparams);

        //public Task<ResponseDetails> CartInsert(CartInsertParams cartinsert_params);

        public Task<ReturnResponse> CartInsert(CartInsertParams param, CommonHeader header);

        public Task<ResponseDetails> CartStore(CartStoreParams cartstore_params);

        public int GoldDynamicPriceCart(GoldDynamicPriceCartParams golddynamicpricecartparams);

        public Task<ResponseDetails> cartCheckOutAllotNew(CartCheckoutAllotNewParams cartcheckoutallotnew_params);

        public int SaveDiamondBookServiceResponse(CartCheckoutNoAllotNewParamsPart2 cartcheckoutnoallotnew_params);

        public int SaveCartMstItemPrices(CARTCHECKOUTNOALLOTNEW_UPDATE_CARTMSTITEM_PRICESParams cartcheckouotallotnew_update_cartmstitem_prices_params);

        public int SaveSolitaireStatus(CartCheckoutNoAllotWebSaveSolitaireStatusParams cartcheckoutnoallotweb_save_solitairestatus_params);

        public int UpdateCartMst(CartCheckoutNoAllotWebUpdateCartMstParams cartcheckoutnoallotweb_update_cartmst_params);

        public int SaveCartStatusMst(CartCheckoutNoAllotWebSaveCartStatusMstParams cartcheckoutnoallotweb_save_cartstatusmst_params);

        public int DeleteAndReassignCart(CartCheckoutNoAllotWebDeleteAndReassignCartParams cartcheckoutnoallotweb_deletandreassigncart_params);

        public int DeleteCartMst(CartCheckoutNoAllotWebDeleteCartMstParams cartcheckoutnoallotweb_deletcartmst_params);

        public NoAllotResponse SaveCartCheckoutNoAllotNew_CartMstNew(CartCheckoutNoAllotNewSaveNewCartMstParams cartcheckoutnoallotnew_savenew_cartmst_params);

        public Task<ResponseDetails> CartCheckoutNoAllotNew(CartCheckoutNoAllotNewParams cartcheckoutnoallotnew_params);

        public Task<ResponseDetails> CartItemDelete(CartItemDeleteParams cartitemdelete_params);

        public Task<ResponseDetails> CartUpdateItem(CartUpdateItemParams cartupdateitem_params);

        public Task<ResponseDetails> CartChildList(CartChildListParams cartchildlistchparams);

        public Task<OrderListResponse> OrderList(OrderListParams orderlistparams);

        public Task<ReturnResponse> CheckItemSizeRange(CheckItemSizeRangeRequest param);

        public Task<ResponseDetails> CartItemList(CartItemListRequest param);
    }
}
