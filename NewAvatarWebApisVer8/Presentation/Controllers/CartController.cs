using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CartController :  ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("cart-count")]
        public Task<ResponseDetails> GetCartCount(CartCountListingParams cartcountparams)
        {
            var result = _cartService.GetCartCount(cartcountparams);
            return result;
        }

        [HttpPost("billing-for-type")]
        public Task<ResponseDetails> GetCartBillingForTypeAsync(CartBillingForTypeListingParams cartbillingfortypeparams)
        {
            var result = _cartService.GetCartBillingForTypeAsync(cartbillingfortypeparams);
            return result;
        }

        [HttpPost("billing-user-list")]
        public Task<ResponseDetails> GetCartBillingUserListAsync(CartBillingUserListingParams cartbillinguserlistparams)
        {
            var result = _cartService.GetCartBillingUserListAsync(cartbillinguserlistparams);
            return result;
        }

        [HttpPost("order-billing-for-type")]
        public Task<ResponseDetails> GetCartOrderBillingForTypeAsync(CartOrderBillingForTypeListingParams cartorderbillingfortypeparams)
        {
            var result = _cartService.GetCartOrderBillingForTypeAsync(cartorderbillingfortypeparams);
            return result;
        }

        [HttpPost("order-billing-user-list")]
        public Task<ResponseDetails> GetCartOrderBillingUserListAsync(CartOrderBillingUserListingParams cartorderbillinguserlistparams)
        {
            var result = _cartService.GetCartOrderBillingUserListAsync(cartorderbillinguserlistparams);
            return result;
        }

        [HttpPost("order-type-list")]
        public Task<ResponseDetails> GetCartOrderTypeListAsync(CartOrderTypeListingParams cartordertypelistparams)
        {
            var result = _cartService.GetCartOrderTypeListAsync(cartordertypelistparams);
            return result;
        }

        [HttpPost("cart-items-list")]
        public Task<ResponseDetails> GetCartItemList(CartItemListingParams cartitemlistparams)
        {
            var result = _cartService.GetCartItemList(cartitemlistparams);
            return result;
        }

        //[HttpPost("cart-insert")]
        //public Task<ResponseDetails> CartInsert(CartInsertParams cartinsert_params)
        //{
        //    var result = _cartService.CartInsert(cartinsert_params);
        //    return result;
        //}

        [HttpPost("cart-insert")]
        public async Task<ReturnResponse> CartInsert(CartInsertParams param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _cartService.CartInsert(param, commonHeader);
            return result;
        }

        [HttpPost("cart-store")]
        public Task<ResponseDetails> CartStore(CartStoreParams cartstore_params)
        {
            var result = _cartService.CartStore(cartstore_params);
            return result;
        }

        [HttpPost("cart-checkout-allot-new")]
        public Task<ResponseDetails> cartCheckOutAllotNew(CartCheckoutAllotNewParams cartcheckoutallotnew_params)
        {
            var result = _cartService.cartCheckOutAllotNew(cartcheckoutallotnew_params);
            return result;
        }

        [HttpPost("cart-checkout-no-allot-new")]
        public Task<ResponseDetails> CartCheckoutNoAllotNew(CartCheckoutNoAllotNewParams cartcheckoutnoallotnew_params)
        {
            var result = _cartService.CartCheckoutNoAllotNew(cartcheckoutnoallotnew_params);
            return result;
        }

        [HttpPost("cart-item-delete")]
        public Task<ResponseDetails> CartItemDelete(CartItemDeleteParams cartitemdelete_params)
        {
            var result = _cartService.CartItemDelete(cartitemdelete_params);
            return result;
        }

        [HttpPost("update-cart-item")]
        public Task<ResponseDetails> CartUpdateItem(CartUpdateItemParams cartupdateitem_params)
        {
            var result = _cartService.CartUpdateItem(cartupdateitem_params);
            return result;
        }

        [HttpPost("cart-child-list")]
        public Task<ResponseDetails> CartChildList(CartChildListParams cartchildlistchparams)
        {
            var result = _cartService.CartChildList(cartchildlistchparams);
            return result;
        }

        [HttpPost("order-list")]
        public Task<OrderListResponse> OrderList(OrderListParams orderlistparams)
        {
            var result = _cartService.OrderList(orderlistparams);
            return result;
        }

        [HttpPost("check-item-size-range")]
        public async Task<ReturnResponse> CheckItemSizeRange([FromBody]CheckItemSizeRangeRequest param)
        {
            var result = await _cartService.CheckItemSizeRange(param);
            return result;
        }
    }
}
