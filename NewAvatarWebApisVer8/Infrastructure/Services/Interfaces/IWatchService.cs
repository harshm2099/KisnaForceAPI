using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Models;

namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IWatchService
    {
        public Task<ResponseDetails> UserWatchList(UserWatchParams userwatchparams);

        public Task<ResponseDetails> WatchListAdd(WatchListAddParams watchlistaddparams);

        public Task<ResponseDetails> WatchlistDelete(WatchlistDeleteParams watchlistdeletedparams);

        public Task<ResponseDetails> WatchlistDownloadPDFNew(WatchListDownloadPdfParams watchlistdownloadpdfparams);

        public Task<ResponseDetails> WatchlistPricewisePDFNew(WatchListPricewisePdfParams watchlistpricewisepdfparams);

        public Task<ResponseDetails> WatchlistPricewisedetailPDF(WatchlistPricewisedetailPDFParams watchlistpricewisedetailpdfparams);

        public Task<ResponseDetails> WatchlistDownloadExcel(WatchlistDownloadExcelParams watchlistdownloadexcelparams);

        public Task<ResponseDetails> WatchlistItemDelete(WatchlistItemDeleteParams watchlistitemdeletedparams);

        public Task<ResponseDetails> WatchlistImagewiseExcel(WatchlistImagewiseExcelParams watchlistimagewiseexcelparams);

        public Task<ResponseDetails> GetWatchItemList(WatchItemListingParams watchitemlistparams);
    }
}
