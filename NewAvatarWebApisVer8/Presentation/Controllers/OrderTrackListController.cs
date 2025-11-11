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
    public class OrderTrackListController : ControllerBase
    {
        private readonly IOrderTrackListService _orderTrackListService;

        public OrderTrackListController(IOrderTrackListService orderTrackListService)
        {
            _orderTrackListService = orderTrackListService;
        }

        [HttpPost("get-total-order-tracking-data")]
        public async Task<ResponseDetails> GetTotalOrderTrackingData(OrderTrackingDataListParams param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _orderTrackListService.GetTotalOrderTrackingData(param);
            return result;
        }

        [HttpPost("get-track-single-data-list")]
        public async Task<ResponseDetails> GetTrackSingleDataList(OrderTrackingSingleDataListParams param)
        {
            var commonHeader = HttpContext.Items["CommonHeader"] as CommonHeader;
            var result = await _orderTrackListService.GetTrackSingleDataList(param);
            return result;
        }
    }
}
