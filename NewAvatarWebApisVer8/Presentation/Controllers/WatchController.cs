using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Presentation.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class WatchController : ControllerBase
    {
        private readonly IWatchService _watchService;
        public WatchController(IWatchService watchService)
        {
            _watchService = watchService;
        }

        [HttpPost("user-watch-list")]
        public async Task<ResponseDetails> UserWatchList(UserWatchParams userwatchparams)
        {
            var result = await _watchService.UserWatchList(userwatchparams);
            return result;
        }

        [HttpPost("watch-list-add")]
        public async Task<ResponseDetails> WatchListAdd(WatchListAddParams watchlistaddparams)
        {
            var result = await _watchService.WatchListAdd(watchlistaddparams);
            return result;
        }

        [HttpPost("watch-list-delete")]
        public async Task<ResponseDetails> WatchlistDelete(WatchlistDeleteParams watchlistdeletedparams)
        {
            var result = await _watchService.WatchlistDelete(watchlistdeletedparams);
            return result;
        }

        [HttpPost("watch-list-item-delete")]
        public async Task<ResponseDetails> WatchlistItemDelete(WatchlistItemDeleteParams watchlistitemdeletedparams)
        {
            var result = await _watchService.WatchlistItemDelete(watchlistitemdeletedparams);
            return result;
        }

        [HttpPost("watch-list-pdf-new")]
        public async Task<ResponseDetails> WatchlistDownloadPDFNew(WatchListDownloadPdfParams watchlistdownloadpdfparams)
        {
            var result = await _watchService.WatchlistDownloadPDFNew(watchlistdownloadpdfparams);
            return result;
        }

        [HttpPost("watch-list-price-pdf-new")]
        public async Task<ResponseDetails> WatchlistPricewisePDFNew(WatchListPricewisePdfParams watchlistpricewisepdfparams)
        {
            var result = await _watchService.WatchlistPricewisePDFNew(watchlistpricewisepdfparams);
            return result;
        }

        [HttpPost("watch-list-price-detail-pdf")]
        public async Task<ResponseDetails> WatchlistPricewisedetailPDF(WatchlistPricewisedetailPDFParams watchlistpricewisedetailpdfparams)
        {
            var result = await _watchService.WatchlistPricewisedetailPDF(watchlistpricewisedetailpdfparams);
            return result;
        }

        [HttpPost("watch-list-excel")]
        public async Task<ResponseDetails> WatchlistDownloadExcel(WatchlistDownloadExcelParams watchlistdownloadexcelparams)
        {
            var result = await _watchService.WatchlistDownloadExcel(watchlistdownloadexcelparams);
            return result;
        }

        [HttpPost("watch-list-image-excel")]
        public async Task<ResponseDetails> WatchlistImagewiseExcel(WatchlistImagewiseExcelParams watchlistimagewiseexcelparams)
        {
            var result = await _watchService.WatchlistImagewiseExcel(watchlistimagewiseexcelparams);
            return result;
        }

        [HttpPost("watch-item-list-on")]
        public async Task<ResponseDetails> GetWatchItemList(WatchItemListingParams watchitemlistparams)
        {
            var result = await _watchService.GetWatchItemList(watchitemlistparams);
            return result;
        }
    }
}
