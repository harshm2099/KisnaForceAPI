using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IOrderTrackListService
    {
        public Task<ResponseDetails> GetTotalOrderTrackingData(OrderTrackingDataListParams param);
        public Task<ResponseDetails> GetTrackSingleDataList(OrderTrackingSingleDataListParams param);
        public Task<ResponseDetails> GetTrackItemDetailData(OrderTrackingItemDetailDataRequest param);
    }
}
