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
    public class WishListService : IWishListService
    {
        public string _Connection = DBCommands.CONNECTION_STRING;

        public async Task<ResponseDetails> SaveWishlistItem(WishlistInsertPayload wishlistinsert_params)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                int data_id = wishlistinsert_params.data_id as int? ?? 0;
                int item_id = wishlistinsert_params.item_id as int? ?? 0;

                if (data_id == 0 || item_id == 0)
                {
                    responseDetails.success = true;
                    responseDetails.message = "data id and item id is required!";
                    responseDetails.status = "200";
                    return responseDetails;
                }

                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    string
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.WISHLISTINSERTITEM;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@item_id", item_id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);
                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    try
                                    {
                                        var firstRow = ds.Tables[0].Rows[0];
                                        resstatus = firstRow["resstatus"] as int? ?? 0;
                                        resstatuscode = firstRow["resstatuscode"] as int? ?? 0;
                                        resmessage = firstRow["resmessage"] as string ?? string.Empty;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                        resstatus = 0;
                                        resmessage = $"error: {ex.Message}";
                                        resstatuscode = 400;
                                    }
                                }
                            }
                        }
                    }

                    responseDetails.success = (resstatus == 1 ? true : false);
                    responseDetails.message = resmessage;
                    responseDetails.status = resstatuscode.ToString();
                    return responseDetails;
                }
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                return responseDetails;
            }
        }

        private WishItemListing MapWishItem(SqlDataReader reader, CommonHelpers objHelpers, int dataId)
        {
            int itemId = reader.GetSafeInt("item_id");
            string mstType = reader.GetSafeString("MstType");
            string itemIsSRP = reader.GetSafeString("ItemIsSRP");
            string plainGoldStatus = reader.GetSafeString("plaingold_status");

            decimal mrpWithTax = Math.Round(reader.GetSafeDecimal("mrp_withtax"), 0);
            decimal diamondPrice = Math.Round(reader.GetSafeDecimal("diamond_price"), 0);
            decimal goldPrice = Math.Round(reader.GetSafeDecimal("gold_price"), 0);
            decimal platinumPrice = Math.Round(reader.GetSafeDecimal("platinum_price"), 0);
            decimal labourPrice = Math.Round(reader.GetSafeDecimal("labour_price"), 0);
            decimal metalPrice = Math.Round(reader.GetSafeDecimal("metal_price"), 0);
            decimal otherPrice = Math.Round(reader.GetSafeDecimal("other_price"), 0);
            decimal stonePrice = Math.Round(reader.GetSafeDecimal("stone_price"), 0);

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

            return new WishItemListing
            {
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
                ItemPlainGold = reader.GetSafeString("ItemPlainGold"),
                labour_per = reader.GetSafeDecimal("labour_per").ToString(),
                item_wt = reader.GetSafeDecimal("item_wt").ToString(),
                category_id = reader.GetSafeInt("category_id").ToString(),
                WishListID = reader.GetSafeInt("WishListID").ToString(),
                ItemFranchiseSts = reader.GetSafeString("ItemFranchiseSts"),
                priceflag = reader.GetSafeString("priceflag"),
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

                fran_diamond_price = diamondPrice.ToString(),
                fran_gold_price = goldPrice.ToString(),
                fran_platinum_price = platinumPrice.ToString(),
                fran_labour_price = labourPrice.ToString(),
                fran_metal_price = metalPrice.ToString(),
                fran_other_price = otherPrice.ToString(),
                fran_stone_price = stonePrice.ToString(),
            };
        }

        public async Task<ResponseDetails> GetWishItemList(WishItemListingParams wishitemlistparams)
        {
            var response = new ResponseDetails();
            var wishItemList = new List<WishItemListing>();
            int currentPage = wishitemlistparams.page;
            int lastPage = 1, totalItems = 0;

            try
            {
                using (var dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    var cmdQuery = DBCommands.WISH_ITEMLIST_ON;

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

                        // Price Range
                        string reqamount = wishitemlistparams.amount as string ?? string.Empty;
                        ParseMinMax(reqamount, out PriceMin, out PriceMax);

                        // Metal Weight Range
                        string reqmetalwt = wishitemlistparams.metalwt as string ?? string.Empty;
                        ParseMinMax(reqmetalwt, out MetalWtMin, out MetalWtMax);

                        // Diamond Weight Range
                        string reqdiawt = wishitemlistparams.diawt as string ?? string.Empty;
                        ParseMinMax(reqdiawt, out DiaWtMin, out DiaWtMax);

                        int Page = wishitemlistparams.page > 0 ? wishitemlistparams.page - 1 : 0;
                        int Limit = wishitemlistparams.default_limit_app_page > 0 ? wishitemlistparams.default_limit_app_page : 20;

                        int DataId = wishitemlistparams.data_id > 0 ? wishitemlistparams.data_id : 0;
                        int DataLoginTypeID = wishitemlistparams.data_login_type > 0 ? wishitemlistparams.data_login_type : 0;
                        int CategoryID = wishitemlistparams.category_id > 0 ? wishitemlistparams.category_id : 0;

                        string Variant = string.IsNullOrWhiteSpace(wishitemlistparams.variant) ? "Y" : wishitemlistparams.variant;
                        string ItemName = string.IsNullOrWhiteSpace(wishitemlistparams.item_name) ? "" : wishitemlistparams.item_name;

                        string SortIds = string.IsNullOrWhiteSpace(wishitemlistparams.sort_id) ? "" : wishitemlistparams.sort_id;
                        string SubCategoryIDs = string.IsNullOrWhiteSpace(wishitemlistparams.sub_category_id) ? "" : wishitemlistparams.sub_category_id;
                        string RingSizes = string.IsNullOrWhiteSpace(wishitemlistparams.dsg_size) ? "" : wishitemlistparams.dsg_size;
                        string DsgKts = string.IsNullOrWhiteSpace(wishitemlistparams.dsg_kt) ? "" : wishitemlistparams.dsg_kt;
                        string DsgColors = string.IsNullOrWhiteSpace(wishitemlistparams.dsg_color) ? "" : wishitemlistparams.dsg_color;
                        string Genders = string.IsNullOrWhiteSpace(wishitemlistparams.gender_id) ? "" : wishitemlistparams.gender_id;

                        string TagWiseFilters = string.IsNullOrWhiteSpace(wishitemlistparams.item_tag) ? "" : wishitemlistparams.item_tag;
                        string BrandWiseFilters = string.IsNullOrWhiteSpace(wishitemlistparams.brand) ? "" : wishitemlistparams.brand;
                        string DeliveryDays = string.IsNullOrWhiteSpace(wishitemlistparams.delivery_days) ? "" : wishitemlistparams.delivery_days;

                        int ItemID = wishitemlistparams.Item_ID > 0 ? wishitemlistparams.Item_ID : 0;

                        string Stock_Av = string.IsNullOrWhiteSpace(wishitemlistparams.Stock_Av) ? "" : wishitemlistparams.Stock_Av;
                        string Family_Av = string.IsNullOrWhiteSpace(wishitemlistparams.Family_Av) ? "" : wishitemlistparams.Family_Av;
                        string Regular_Av = string.IsNullOrWhiteSpace(wishitemlistparams.Regular_Av) ? "" : wishitemlistparams.Regular_Av;
                        string Wearit = string.IsNullOrWhiteSpace(wishitemlistparams.wearit) ? "" : wishitemlistparams.wearit;
                        string Tryon = string.IsNullOrWhiteSpace(wishitemlistparams.tryon) ? "" : wishitemlistparams.tryon;

                        string ItemSubCtgIDs = string.IsNullOrWhiteSpace(wishitemlistparams.ItemSubCtgIDs) ? "" : wishitemlistparams.ItemSubCtgIDs;
                        string ItemSubSubCtgIDs = string.IsNullOrWhiteSpace(wishitemlistparams.ItemSubSubCtgIDs) ? "" : wishitemlistparams.ItemSubSubCtgIDs;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", DataLoginTypeID);
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
                                var item = MapWishItem(reader, objHelpers, wishitemlistparams.data_id);
                                wishItemList.Add(item);

                                lastPage = reader["last_page"] != DBNull.Value ? Convert.ToInt32(reader["last_page"]) : lastPage;
                                totalItems = reader["total_items"] != DBNull.Value ? Convert.ToInt32(reader["total_items"]) : totalItems;
                            }
                        }
                    }
                }

                response.success = wishItemList.Any();
                response.message = wishItemList.Any() ? "Successfully" : "No data found";
                response.status = "200";
                response.current_page = currentPage.ToString();
                response.last_page = lastPage.ToString();
                response.total_items = totalItems.ToString();
                response.data = wishItemList;
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
                response.data = new List<WishItemListing>();
                return response;
            }
        }
    }
}
