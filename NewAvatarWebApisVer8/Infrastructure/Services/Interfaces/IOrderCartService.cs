using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IOrderCartService
    {
        public Task<ResponseDetails> GetMyOrderList(MyOrderListingParams myorderlistparams);

        public Task<ResponseDetails> GetMyOrderItemList(MyOrderItemListingParams myorderitemlistparams);

        public Task<ResponseDetails> OrderAssignList(OrderAssignListingParams orderassignlistparams);

        public Task<ResponseDetails> CartCancel(CartCancelListingParams cartcancel_params);

        public Task<ResponseDetails> CartSingleCancel(CartSingleCancelListingParams cartsinglecancel_params);
    }
}
