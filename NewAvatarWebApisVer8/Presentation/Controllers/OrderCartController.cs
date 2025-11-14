using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class OrderCartController : ControllerBase
    {
        private readonly IOrderCartService _orderCartService;

        public OrderCartController(IOrderCartService orderCartService)
        {
            _orderCartService = orderCartService;
        }

        [HttpPost("get-myorder-list")]
        public async Task<ResponseDetails> GetMyOrderList(MyOrderListingParams myorderlistparams)
        {
            var result = await _orderCartService.GetMyOrderList(myorderlistparams);
            return result;
        }

        [HttpPost("get-myorder-itemlist")]
        public async Task<ResponseDetails> GetMyOrderItemList(MyOrderItemListingParams myorderitemlistparams)
        {
            var result = await _orderCartService.GetMyOrderItemList(myorderitemlistparams);
            return result;
        }

        [HttpPost("order-assign-list")]
        public async Task<ResponseDetails> OrderAssignList(OrderAssignListingParams orderassignlistparams)
        {
            var result = await _orderCartService.OrderAssignList(orderassignlistparams);
            return result;
        }

        [HttpPost("cart-cancel")]
        public async Task<ResponseDetails> CartCancel(CartCancelListingParams cartcancel_params)
        {
            var result = await _orderCartService.CartCancel(cartcancel_params);
            return result;
        }

        [HttpPost("cart-single-cancel")]
        public async Task<ResponseDetails> CartSingleCancel(CartSingleCancelListingParams cartsinglecancel_params)
        {
            var result = await _orderCartService.CartSingleCancel(cartsinglecancel_params);
            return result;
        }

        [HttpPost("order-Item-cancel")]
        public async Task<ReturnResponse> OrderItemCancel(OrderItemCancelRequest param)
        {
            var result = await _orderCartService.OrderItemCancel(param);
            return result;
        }
    }
}
