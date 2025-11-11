using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using System.Data;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class WatchService : IWatchService
    {
        public string _Connection = DBCommands.CONNECTION_STRING;

        public async Task<ResponseDetails> UserWatchList(UserWatchParams userwatchparams)
        {
            var response = new ResponseDetails();
            var userwatchList = new List<UserWatchListing>();

            try
            {
                using (var dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    var cmdQuery = DBCommands.USERWATCHLIST;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        var objHelpers = new CommonHelpers();

                        int DataId = userwatchparams.data_id > 0 ? userwatchparams.data_id : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", DataId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                userwatchList.Add(new UserWatchListing
                                {
                                    watchlist_id = reader.GetSafeInt("watchlist_id").ToString(),
                                    watchlist_name = reader.GetSafeString("watchlist_name"),
                                    created_name = reader.GetSafeString("created_name"),
                                    shared_name = reader.GetSafeString("shared_name"),
                                    create_data_id = reader.GetSafeInt("create_data_id").ToString(),
                                    watch_flag = reader.GetSafeString("watch_flag"),
                                    watchCount = reader.GetSafeInt("watchCount").ToString(),
                                });
                            }
                        }
                    }
                }

                response.success = userwatchList.Any();
                response.message = userwatchList.Any() ? "User watch list successfully" : "No data found";
                response.status = "200";
                response.data = userwatchList;
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                response.data = new List<HomeScreenMasterListing>();
                return response;
            }
        }

        public async Task<ResponseDetails> WatchListAdd(WatchListAddParams watchlistaddparams)
        {
            var response = new ResponseDetails();
            try
            {
                string resmsg = string.Empty;
                int ressuccess = 0;

                using (var dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    var cmdQuery = DBCommands.ADDTOWATCHLIST_NEW;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        var objHelpers = new CommonHelpers();

                        int DataID = watchlistaddparams.data_id > 0 ? watchlistaddparams.data_id : 0;
                        int ItemID = watchlistaddparams.item_id > 0 ? watchlistaddparams.item_id : 0;
                        string watch_name = string.IsNullOrWhiteSpace(watchlistaddparams.watch_name) ? "" : watchlistaddparams.watch_name;
                        string WatchListIDs = string.IsNullOrWhiteSpace(watchlistaddparams.watchlist_id) ? "" : watchlistaddparams.watchlist_id;
                        int ColorID = watchlistaddparams.color_id > 0 ? watchlistaddparams.color_id : 0;
                        int SizeID = watchlistaddparams.size_id > 0 ? watchlistaddparams.size_id : 0;
                        string ProductItemRemarks = string.IsNullOrWhiteSpace(watchlistaddparams.product_item_remarks) ? "" : watchlistaddparams.product_item_remarks;
                        string ProductItemRemarksIDs = string.IsNullOrWhiteSpace(watchlistaddparams.product_item_remarks_ids) ? "" : watchlistaddparams.product_item_remarks_ids;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@watch_name", watch_name);
                        cmd.Parameters.AddWithValue("@WatchListIDs", WatchListIDs);
                        cmd.Parameters.AddWithValue("@ColorID", ColorID);
                        cmd.Parameters.AddWithValue("@SizeID", SizeID);
                        cmd.Parameters.AddWithValue("@ProductItemRemarks", ProductItemRemarks);
                        cmd.Parameters.AddWithValue("@ProductItemRemarksIDs", ProductItemRemarksIDs);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ressuccess = reader.GetSafeInt("success");
                                resmsg = reader.GetSafeString("message");
                            }
                        }
                    }
                }

                response.success = ressuccess == 1 ? true : false;
                response.message = resmsg;
                response.status = "200";
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                return response;
            }
        }

        public async Task<ResponseDetails> WatchlistDelete(WatchlistDeleteParams watchlistdeletedparams)
        {
            var response = new ResponseDetails();
            try
            {
                string resmsg = string.Empty;
                int ressuccess = 0;

                using (var dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    var cmdQuery = DBCommands.WATCHLISTDELETE_NEW;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        var objHelpers = new CommonHelpers();

                        int watchlist_id = watchlistdeletedparams.watchlist_id > 0 ? watchlistdeletedparams.watchlist_id : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@watchlist_id", watchlist_id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ressuccess = reader.GetSafeInt("success");
                                resmsg = reader.GetSafeString("message");
                            }
                        }
                    }
                }

                response.success = ressuccess == 1 ? true : false;
                response.message = resmsg;
                response.status = "200";
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                return response;
            }
        }
        
        public async Task<ResponseDetails> WatchlistItemDelete(WatchlistItemDeleteParams watchlistitemdeletedparams)
        {
            var response = new ResponseDetails();
            try
            {
                string resmsg = string.Empty;
                int ressuccess = 0;

                using (var dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    var cmdQuery = DBCommands.WATCHLISTITEMDELETE_NEW;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        var objHelpers = new CommonHelpers();

                        int watchlist_id = watchlistitemdeletedparams.watchlist_id > 0 ? watchlistitemdeletedparams.watchlist_id : 0;
                        string itemlist_id = string.IsNullOrWhiteSpace(watchlistitemdeletedparams.itemlist_id) ? "" : watchlistitemdeletedparams.itemlist_id;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@watchlist_id", watchlist_id);
                        cmd.Parameters.AddWithValue("@itemlist_id", itemlist_id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ressuccess = reader.GetSafeInt("success");
                                resmsg = reader.GetSafeString("message");
                            }
                        }
                    }
                }

                response.success = ressuccess == 1 ? true : false;
                response.message = resmsg;
                response.status = "200";
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                return response;
            }
        }
        
        public async Task<ResponseDetails> WatchlistDownloadPDFNew(WatchListDownloadPdfParams watchlistdownloadpdfparams)
        {
            var response = new ResponseDetails();
            try
            {
                string fileUrl = $"https://force.kisna.com/HKDB/public/uploads/allexcelpdf/New Watchlist_app_1753790825.pdf";
                response.success = true;
                response.message = "Your collection generate successfully.";
                response.file_path = fileUrl;
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                return response;
            }
        }

        public async Task<ResponseDetails> WatchlistPricewisePDFNew(WatchListPricewisePdfParams watchlistpricewisepdfparams)
        {
            var response = new ResponseDetails();
            try
            {
                string fileUrl = $"https://force.kisna.com/HKDB/public/uploads/allexcelpdf/New Watchlist_app_1753791458.pdf";
                response.success = true;
                response.message = "Your collection generate successfully.";
                response.file_path = fileUrl;
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                return response;
            }
        }

        public async Task<ResponseDetails> WatchlistPricewisedetailPDF(WatchlistPricewisedetailPDFParams watchlistpricewisedetailpdfparams)
        {
            var response = new ResponseDetails();
            try
            {
                string fileUrl = $"https://force.kisna.com/HKDB/public/uploads/allexcelpdf/New Watchlist_app_1753846864.pdf";
                response.success = true;
                response.message = "Your collection generate successfully.";
                response.file_path = fileUrl;
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                return response;
            }
        }
        
        public async Task<ResponseDetails> WatchlistDownloadExcel(WatchlistDownloadExcelParams watchlistdownloadexcelparams)
        {
            var response = new ResponseDetails();
            try
            {
                string fileUrl = $"https://force.kisna.com/HKDB/public/uploads/allexcelpdf/New Watchlist_watchlistapp_1753846178.xlsx";
                response.success = true;
                response.message = "Your collection file generate successfully. For order upload.";
                response.file_path = fileUrl;
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                return response;
            }
        }

        public async Task<ResponseDetails> WatchlistImagewiseExcel(WatchlistImagewiseExcelParams watchlistimagewiseexcelparams)
        {
            var response = new ResponseDetails();
            try
            {
                string fileUrl = $"https://force.kisna.com/HKDB/public/uploads/allexcelpdf/New Watchlist_watchlistapp_1753846615.xlsx";
                response.success = true;
                response.message = "Your collection file generate successfully. For order upload.";
                response.file_path = fileUrl;
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                return response;
            }
        }

        private WatchItemListing MapWatchItem(SqlDataReader reader, CommonHelpers objHelpers, int dataId)
        {
            int itemId = reader.GetSafeInt("item_id");
            string mstType = reader.GetSafeString("MstType");
            string itemIsSRP = reader.GetSafeString("ItemIsSRP");
            string plainGoldStatus = reader.GetSafeString("plaingold_status");
            string DataItemInfo = reader.GetSafeString("DataItemInfo");
            string priceflag = reader.GetSafeString("priceflag");

            if (DataItemInfo == "Y" && itemIsSRP == "Y")
            {
                priceflag = "Y";
            }

            decimal mrpWithTax = Math.Round(reader.GetSafeDecimal("mrp_withtax"), 0);
            decimal diamondPrice = Math.Round(reader.GetSafeDecimal("diamond_price"), 0);
            decimal goldPrice = Math.Round(reader.GetSafeDecimal("gold_price"), 0);
            decimal platinumPrice = Math.Round(reader.GetSafeDecimal("platinum_price"), 0);
            decimal labourPrice = Math.Round(reader.GetSafeDecimal("labour_price"), 0);
            decimal metalPrice = Math.Round(reader.GetSafeDecimal("metal_price"), 0);
            decimal otherPrice = Math.Round(reader.GetSafeDecimal("other_price"), 0);
            decimal stonePrice = Math.Round(reader.GetSafeDecimal("stone_price"), 0);

            string fran_diamond_price = ""
                 , fran_gold_price = ""
                 , fran_platinum_price = ""
                 , fran_labour_price = ""
                 , fran_metal_price = ""
                 , fran_other_price = ""
                 , fran_stone_price = "";

            if (priceflag == "Y" && itemIsSRP == "Y")
            {
                fran_diamond_price = diamondPrice.ToString();
                fran_gold_price = goldPrice.ToString();
                fran_platinum_price = platinumPrice.ToString();
                fran_labour_price = labourPrice.ToString();
                fran_metal_price = metalPrice.ToString();
                fran_other_price = otherPrice.ToString();
                fran_stone_price = stonePrice.ToString();
            }

            decimal distPrice;

            if (mstType == "F" || itemIsSRP == "Y" || plainGoldStatus == "Y")
            {
                var priceParams = new CartItemPriceDetailListingParams { DataID = dataId, ItemID = itemId, IsWeightCalcRequired = 0 };
                var priceList = objHelpers.GetCartItemPriceDetails(priceParams);
                distPrice = priceList.Any() ? Math.Round(priceList[0].dp_final_price, 0) : 0;
            }
            else
            {
                var dpParams = new CartItemDPRPCALCListingParams { DataID = dataId, MRP = mrpWithTax };
                var dpList = objHelpers.Get_DP_RP_Calculation(dpParams);
                distPrice = dpList.Any() ? Math.Round(dpList[0].D_Price, 0) : 0;
            }

            var tagParams = new Item_TagsListingParams { item_id = itemId };
            var tags = objHelpers.GetItemTagList(tagParams);

            return new WatchItemListing
            {
                watch_list_id = reader.GetSafeInt("watch_list_id").ToString(),
                item_id = itemId.ToString(),
                item_code = reader.GetSafeString("item_code"),
                ItemAproxDay = reader.GetSafeString("ItemAproxDay"),
                item_sku = reader.GetSafeString("item_sku"),
                item_name = reader.GetSafeString("item_name"),
                item_description = reader.GetSafeString("item_description"),
                item_mrp = reader.GetSafeDecimal("item_mrp").ToString(),
                dsg_sfx = reader.GetSafeString("dsg_sfx"),
                dsg_size = reader.GetSafeString("dsg_size"),
                dsg_kt = reader.GetSafeString("dsg_kt"),
                dsg_color = reader.GetSafeString("dsg_color"),
                ItemDisLabourPer = reader.GetSafeDecimal("ItemDisLabourPer").ToString(),
                ApproxDeliveryDate = reader.GetSafeString("ApproxDeliveryDate"),
                sub_category_id = reader.GetSafeInt("sub_category_id").ToString(),
                item_price = reader.GetSafeDecimal("item_price").ToString(),
                dist_price = distPrice.ToString(),
                item_soliter = reader.GetSafeString("item_soliter"),
                image_path = reader.GetSafeString("image_path"),
                star = reader.GetSafeInt("star").ToString(),
                mostOrder = reader.GetSafeInt("mostOrder").ToString(),
                ItemAproxDayCommonID = reader.GetSafeInt("ItemAproxDayCommonID").ToString(),
                plaingold_status = plainGoldStatus,
                ItemPlainGold = plainGoldStatus,
                labour_per = reader.GetSafeDecimal("labour_per").ToString(),
                item_wt = reader.GetSafeDecimal("item_wt").ToString(),
                watchcart_flag = reader.GetSafeString("watchcart_flag"),
                category_id = reader.GetSafeInt("category_id").ToString(),
                data_id = reader.GetSafeInt("data_id").ToString(),
                selectedColor1 = reader.GetSafeInt("selectedColor1").ToString(),
                selectedSize1 = reader.GetSafeInt("selectedSize1").ToString(),
                valid_status = reader.GetSafeString("valid_status"),
                ent_dt = reader.GetSafeString("ent_dt"),
                item_valid_status = reader.GetSafeString("item_valid_status"),
                item_ent_dt = reader.GetSafeString("item_ent_dt"),
                ItemFranchiseSts = reader.GetSafeString("ItemFranchiseSts"),
                priceflag = priceflag,
                ItemIsSRP = itemIsSRP,
                mrp_withtax = mrpWithTax.ToString(),
                diamond_price = diamondPrice.ToString(),
                gold_price = goldPrice.ToString(),
                platinum_price = platinumPrice.ToString(),
                labour_price = labourPrice.ToString(),
                metal_price = metalPrice.ToString(),
                other_price = otherPrice.ToString(),
                stone_price = stonePrice.ToString(),
                item_stock_qty = reader.GetSafeInt("item_stock_qty").ToString(),
                item_stock_colorsize_qty = reader.GetSafeInt("item_stock_colorsize_qty").ToString(),
                productTags = tags,

                fran_diamond_price = fran_diamond_price,
                fran_gold_price = fran_gold_price,
                fran_platinum_price = fran_platinum_price,
                fran_labour_price = fran_labour_price,
                fran_metal_price = fran_metal_price,
                fran_other_price = fran_other_price,
                fran_stone_price = fran_stone_price,
            };
        }

        public async Task<ResponseDetails> GetWatchItemList(WatchItemListingParams watchitemlistparams)
        {
            var response = new ResponseDetails();
            var watchItemList = new List<WatchItemListing>();
            int currentPage = watchitemlistparams.page;
            int lastPage = 1, totalItems = 0;

            try
            {
                using (var dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    var cmdQuery = DBCommands.WATCH_ITEMLIST_ON;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        var objHelpers = new CommonHelpers();

                        decimal ParseMinMax(string input, out decimal min, out decimal max)
                        {
                            var list = input
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(x => objHelpers.TryParseDecimal(x) ? Convert.ToDecimal(x) : -1)
                                .ToList();

                            if (list.Any())
                            {
                                min = list[0];
                                max = list[1];
                            }
                            else
                            {
                                min = -1;
                                max = -1;
                            }
                            return list.Any() ? list[0] : 0;
                        }

                        decimal PriceMin, PriceMax, MetalWtMin, MetalWtMax, DiaWtMin, DiaWtMax;

                        string reqamount = watchitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        string reqmetalwt = watchitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        string reqdiawt = watchitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        int Page = watchitemlistparams.page > 0 ? watchitemlistparams.page - 1 : 0;
                        int Limit = watchitemlistparams.default_limit_app_page > 0 ? watchitemlistparams.default_limit_app_page : 20;

                        int DataId = watchitemlistparams.data_id > 0 ? watchitemlistparams.data_id : 0;
                        int DataLoginTypeID = watchitemlistparams.data_login_type > 0 ? watchitemlistparams.data_login_type : 0;
                        int WatchlistID = watchitemlistparams.watchlist_id > 0 ? watchitemlistparams.watchlist_id : 0;
                        int CategoryID = watchitemlistparams.category_id > 0 ? watchitemlistparams.category_id : 0;

                        string Variant = string.IsNullOrWhiteSpace(watchitemlistparams.variant) ? "Y" : watchitemlistparams.variant;
                        string ItemName = string.IsNullOrWhiteSpace(watchitemlistparams.item_name) ? "" : watchitemlistparams.item_name;

                        string SortIds = string.IsNullOrWhiteSpace(watchitemlistparams.sort_id) ? "" : watchitemlistparams.sort_id;
                        string SubCategoryIDs = string.IsNullOrWhiteSpace(watchitemlistparams.sub_category_id) ? "" : watchitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(watchitemlistparams.dsg_size) ? "" : watchitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(watchitemlistparams.dsg_kt) ? "" : watchitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(watchitemlistparams.dsg_color) ? "" : watchitemlistparams.dsg_color;
                        string Genders = string.IsNullOrWhiteSpace(watchitemlistparams.gender_id) ? "" : watchitemlistparams.gender_id;

                        string TagWiseFilters = string.IsNullOrWhiteSpace(watchitemlistparams.item_tag) ? "" : watchitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(watchitemlistparams.brand) ? "" : watchitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(watchitemlistparams.delivery_days) ? "" : watchitemlistparams.delivery_days;

                        int ItemID = watchitemlistparams.Item_ID > 0 ? watchitemlistparams.Item_ID : 0;

                        string Stock_Av = string.IsNullOrWhiteSpace(watchitemlistparams.Stock_Av) ? "" : watchitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(watchitemlistparams.Family_Av) ? "" : watchitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(watchitemlistparams.Regular_Av) ? "" : watchitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(watchitemlistparams.wearit) ? "" : watchitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(watchitemlistparams.tryon) ? "" : watchitemlistparams.tryon;

                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(watchitemlistparams.ItemSubCtgIDs) ? "" : watchitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(watchitemlistparams.ItemSubSubCtgIDs) ? "" : watchitemlistparams.ItemSubSubCtgIDs;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
                        cmd.Parameters.AddWithValue("@WatchlistID", WatchlistID);
                        cmd.Parameters.AddWithValue("@CategoryID", CategoryID);
                        cmd.Parameters.AddWithValue("@Variant", Variant);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);
                        cmd.Parameters.AddWithValue("@SortIds", SortIds);
                        cmd.Parameters.AddWithValue("@SubCategoryIDs", SubCategoryIDs);
                        cmd.Parameters.AddWithValue("@RingSizes", RingSizes);
                        cmd.Parameters.AddWithValue("@DsgKts", DsgKts);
                        cmd.Parameters.AddWithValue("@DsgColors", DsgColors);
                        cmd.Parameters.AddWithValue("@PriceMin", PriceMin);
                        cmd.Parameters.AddWithValue("@PriceMax", PriceMax);
                        cmd.Parameters.AddWithValue("@MetalWtMin", MetalWtMin);
                        cmd.Parameters.AddWithValue("@MetalWtMax", MetalWtMax);
                        cmd.Parameters.AddWithValue("@DiaWtMin", DiaWtMin);
                        cmd.Parameters.AddWithValue("@DiaWtMax", DiaWtMax);
                        cmd.Parameters.AddWithValue("@Genders", Genders);
                        cmd.Parameters.AddWithValue("@TagWiseFilters", TagWiseFilters);
                        cmd.Parameters.AddWithValue("@BrandWiseFilters", BrandWiseFilters);
                        cmd.Parameters.AddWithValue("@DeliveryDays", DeliveryDays);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@Stock_Av", Stock_Av);
                        cmd.Parameters.AddWithValue("@Family_Av", Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", Wearit);
                        cmd.Parameters.AddWithValue("@Tryon", Tryon);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@Page", Page);
                        cmd.Parameters.AddWithValue("@Limit", Limit);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = MapWatchItem(reader, objHelpers, watchitemlistparams.data_id);
                                watchItemList.Add(item);

                                lastPage = reader["last_page"] != DBNull.Value ? Convert.ToInt32(reader["last_page"]) : lastPage;
                                totalItems = reader["total_items"] != DBNull.Value ? Convert.ToInt32(reader["total_items"]) : totalItems;
                            }
                        }
                    }
                }

                response.success = watchItemList.Any();
                response.message = watchItemList.Any() ? "Successfully" : "No data found";
                response.status = "200";
                response.current_page = currentPage.ToString();
                response.last_page = lastPage.ToString();
                response.total_items = totalItems.ToString();
                response.data = watchItemList;
                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                response.current_page = currentPage.ToString();
                response.last_page = "1";
                response.total_items = "0";
                response.data = new List<WatchItemListing>();
                return response;
            }
        }
    }
}
