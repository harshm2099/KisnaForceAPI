using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using Newtonsoft.Json;
using System.Data;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class ItemViewService : IItemViewService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        public async Task<ResponseDetails> ItemList(ItemViewItemListParams request)
        {
            var responseDetails = new ResponseDetails();
            IList<ItemViewListResponse> itemViewList = new List<ItemViewListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ItemViewList;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? dataId = string.IsNullOrWhiteSpace(request.dataId) ? null : request.dataId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(request.dataLoginType) ? null : request.dataLoginType;
                        string? page = string.IsNullOrWhiteSpace(request.page) ? null : request.page;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);
                        cmd.Parameters.AddWithValue("@page", page);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[0].Rows[i];
                                            string view_id = rowdetails["view_id"] != DBNull.Value ? Convert.ToString(rowdetails["view_id"]) : string.Empty;
                                            string item_soliter = rowdetails["item_soliter"] != DBNull.Value ? Convert.ToString(rowdetails["item_soliter"]) : string.Empty;
                                            string ItemDisLabourPer = rowdetails["ItemDisLabourPer"] != DBNull.Value ? Convert.ToString(rowdetails["ItemDisLabourPer"]) : string.Empty;
                                            string ItemAproxDay = rowdetails["ItemAproxDay"] != DBNull.Value ? Convert.ToString(rowdetails["ItemAproxDay"]) : string.Empty;
                                            string labour_per = rowdetails["labour_per"] != DBNull.Value ? Convert.ToString(rowdetails["labour_per"]) : string.Empty;
                                            string plaingold_status = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string cat_id = rowdetails["cat_id"] != DBNull.Value ? Convert.ToString(rowdetails["cat_id"]) : string.Empty;
                                            string data_id = rowdetails["data_id"] != DBNull.Value ? Convert.ToString(rowdetails["data_id"]) : string.Empty;
                                            string item_id = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string item_code = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string item_name = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string item_sku = rowdetails["item_sku"] != DBNull.Value ? Convert.ToString(rowdetails["item_sku"]) : string.Empty;
                                            string item_description = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string item_mrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string item_price = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string dist_price = rowdetails["dist_price"] != DBNull.Value ? Convert.ToString(rowdetails["dist_price"]) : string.Empty;
                                            string image_path = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string image_thumb_path = rowdetails["image_thumb_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_thumb_path"]) : string.Empty;
                                            string dsg_sfx = rowdetails["dsg_sfx"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_sfx"]) : string.Empty;
                                            string dsg_size = rowdetails["dsg_size"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_size"]) : string.Empty;
                                            string dsg_kt = rowdetails["dsg_kt"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_kt"]) : string.Empty;
                                            string dsg_color = rowdetails["dsg_color"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_color"]) : string.Empty;
                                            string star = rowdetails["star"] != DBNull.Value ? Convert.ToString(rowdetails["star"]) : string.Empty;
                                            string cart_img = rowdetails["cart_img"] != DBNull.Value ? Convert.ToString(rowdetails["cart_img"]) : string.Empty;
                                            string img_cart_title = rowdetails["img_cart_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_cart_title"]) : string.Empty;
                                            string watch_img = rowdetails["watch_img"] != DBNull.Value ? Convert.ToString(rowdetails["watch_img"]) : string.Empty;
                                            string img_watch_title = rowdetails["img_watch_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_watch_title"]) : string.Empty;
                                            string wish_count = rowdetails["wish_count"] != DBNull.Value ? Convert.ToString(rowdetails["wish_count"]) : string.Empty;
                                            string wearit_count = rowdetails["wearit_count"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_count"]) : string.Empty;
                                            string wearit_status = rowdetails["wearit_status"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_status"]) : string.Empty;
                                            string wearit_img = rowdetails["wearit_img"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_img"]) : string.Empty;
                                            string wearit_none_img = rowdetails["wearit_none_img"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_none_img"]) : string.Empty;
                                            string wearit_color = rowdetails["wearit_color"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_color"]) : string.Empty;
                                            string wearit_text = rowdetails["wearit_text"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_text"]) : string.Empty;
                                            string img_wearit_title = rowdetails["img_wearit_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wearit_title"]) : string.Empty;
                                            string wish_default_img = rowdetails["wish_default_img"] != DBNull.Value ? Convert.ToString(rowdetails["wish_default_img"]) : string.Empty;
                                            string wish_fill_img = rowdetails["wish_fill_img"] != DBNull.Value ? Convert.ToString(rowdetails["wish_fill_img"]) : string.Empty;
                                            string img_wish_title = rowdetails["img_wish_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wish_title"]) : string.Empty;
                                            string item_review = rowdetails["item_review"] != DBNull.Value ? Convert.ToString(rowdetails["item_review"]) : string.Empty;
                                            string item_size = rowdetails["item_size"] != DBNull.Value ? Convert.ToString(rowdetails["item_size"]) : string.Empty;
                                            string item_kt = rowdetails["item_kt"] != DBNull.Value ? Convert.ToString(rowdetails["item_kt"]) : string.Empty;
                                            string item_color = rowdetails["item_color"] != DBNull.Value ? Convert.ToString(rowdetails["item_color"]) : string.Empty;
                                            string item_metal = rowdetails["item_metal"] != DBNull.Value ? Convert.ToString(rowdetails["item_metal"]) : string.Empty;
                                            string item_wt = rowdetails["item_wt"] != DBNull.Value ? Convert.ToString(rowdetails["item_wt"]) : string.Empty;
                                            string item_stone = rowdetails["item_stone"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone"]) : string.Empty;
                                            string item_stone_wt = rowdetails["item_stone_wt"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone_wt"]) : string.Empty;
                                            string item_stone_qty = rowdetails["item_stone_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone_qty"]) : string.Empty;
                                            string price_text = rowdetails["price_text"] != DBNull.Value ? Convert.ToString(rowdetails["price_text"]) : string.Empty;
                                            string cart_price = rowdetails["cart_price"] != DBNull.Value ? Convert.ToString(rowdetails["cart_price"]) : string.Empty;
                                            string item_color_id = rowdetails["item_color_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_color_id"]) : string.Empty;
                                            string item_details = rowdetails["item_details"] != DBNull.Value ? Convert.ToString(rowdetails["item_details"]) : string.Empty;
                                            string item_diamond_details = rowdetails["item_diamond_details"] != DBNull.Value ? Convert.ToString(rowdetails["item_diamond_details"]) : string.Empty;
                                            string item_text = rowdetails["item_text"] != DBNull.Value ? Convert.ToString(rowdetails["item_text"]) : string.Empty;
                                            string more_item_details = rowdetails["more_item_details"] != DBNull.Value ? Convert.ToString(rowdetails["more_item_details"]) : string.Empty;
                                            string item_stock = rowdetails["item_stock"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock"]) : string.Empty;
                                            string item_removecart_img = rowdetails["item_removecart_img"] != DBNull.Value ? Convert.ToString(rowdetails["item_removecart_img"]) : string.Empty;
                                            string item_removecard_title = rowdetails["item_removecard_title"] != DBNull.Value ? Convert.ToString(rowdetails["item_removecard_title"]) : string.Empty;
                                            string rupy_symbol = rowdetails["rupy_symbol"] != DBNull.Value ? Convert.ToString(rowdetails["rupy_symbol"]) : string.Empty;
                                            string recent_view = rowdetails["recent_view"] != DBNull.Value ? Convert.ToString(rowdetails["recent_view"]) : string.Empty;
                                            string category_id = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string cart_id = rowdetails["cart_id"] != DBNull.Value ? Convert.ToString(rowdetails["cart_id"]) : string.Empty;
                                            string item_stock_qty = rowdetails["item_stock_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock_qty"]) : string.Empty;
                                            string item_stock_colorsize_qty = rowdetails["item_stock_colorsize_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock_colorsize_qty"]) : string.Empty;
                                            string variantCount = rowdetails["variantCount"] != DBNull.Value ? Convert.ToString(rowdetails["variantCount"]) : string.Empty;
                                            string ItemTypeCommonID = rowdetails["ItemTypeCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemTypeCommonID"]) : string.Empty;
                                            string ItemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string ItemFranchiseSts = rowdetails["ItemFranchiseSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemFranchiseSts"]) : string.Empty;
                                            string item_illumine = rowdetails["item_illumine"] != DBNull.Value ? Convert.ToString(rowdetails["item_illumine"]) : string.Empty;
                                            string isSolitaireOtherCollection = rowdetails["isSolitaireOtherCollection"] != DBNull.Value ? Convert.ToString(rowdetails["isSolitaireOtherCollection"]) : string.Empty;
                                            string ItemGenderCommonID = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string weight = rowdetails["weight"] != DBNull.Value ? Convert.ToString(rowdetails["weight"]) : string.Empty;
                                            string totalLabourPer = rowdetails["totalLabourPer"] != DBNull.Value ? Convert.ToString(rowdetails["totalLabourPer"]) : string.Empty;
                                            string FinalDMrp = rowdetails["FinalDMrp"] != DBNull.Value ? Convert.ToString(rowdetails["FinalDMrp"]) : string.Empty;
                                            string selectedColor = rowdetails["selectedColor"] != DBNull.Value ? Convert.ToString(rowdetails["selectedColor"]) : string.Empty;
                                            string selectedSize = rowdetails["selectedSize"] != DBNull.Value ? Convert.ToString(rowdetails["selectedSize"]) : string.Empty;
                                            string selectedColor1 = rowdetails["selectedColor1"] != DBNull.Value ? Convert.ToString(rowdetails["selectedColor1"]) : string.Empty;
                                            string selectedSize1 = rowdetails["selectedSize1"] != DBNull.Value ? Convert.ToString(rowdetails["selectedSize1"]) : string.Empty;
                                            string field_name = rowdetails["field_name"] != DBNull.Value ? Convert.ToString(rowdetails["field_name"]) : string.Empty;
                                            string color_name = rowdetails["color_name"] != DBNull.Value ? Convert.ToString(rowdetails["color_name"]) : string.Empty;
                                            string default_color_name = rowdetails["default_color_name"] != DBNull.Value ? Convert.ToString(rowdetails["default_color_name"]) : string.Empty;
                                            string sizeList = rowdetails["sizeList"] != DBNull.Value ? Convert.ToString(rowdetails["sizeList"]) : string.Empty;
                                            string colorList = rowdetails["colorList"] != DBNull.Value ? Convert.ToString(rowdetails["colorList"]) : string.Empty;
                                            string itemsColorSizeList = rowdetails["itemsColorSizeList"] != DBNull.Value ? Convert.ToString(rowdetails["itemsColorSizeList"]) : string.Empty;
                                            string itemOrderInstructionList = rowdetails["itemOrderInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderInstructionList"]) : string.Empty;
                                            string itemOrderCustomInstructionList = rowdetails["itemOrderCustomInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderCustomInstructionList"]) : string.Empty;
                                            string item_images_color = rowdetails["item_images_color"] != DBNull.Value ? Convert.ToString(rowdetails["item_images_color"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            List<Dictionary<string, object>> sizeListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> colorListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemsColorSizeListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemOrderInstructionListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> itemOrderCustomInstructionListDynamic = new List<Dictionary<string, object>>();
                                            List<Dictionary<string, object>> item_images_colorDynamic = new List<Dictionary<string, object>>();

                                            if (!string.IsNullOrEmpty(sizeList))
                                            {
                                                try
                                                {
                                                    sizeListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(sizeList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing sizeList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(colorList))
                                            {
                                                try
                                                {
                                                    colorListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(colorList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing colorList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(itemsColorSizeList))
                                            {
                                                try
                                                {
                                                    itemsColorSizeListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(itemsColorSizeList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing itemsColorSizeList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(itemOrderInstructionList))
                                            {
                                                try
                                                {
                                                    itemOrderInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(itemOrderInstructionList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing itemOrderInstructionList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(itemOrderCustomInstructionList))
                                            {
                                                try
                                                {
                                                    itemOrderCustomInstructionListDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(itemOrderCustomInstructionList);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing itemOrderCustomInstructionList: " + ex.Message);
                                                }
                                            }

                                            if (!string.IsNullOrEmpty(item_images_color))
                                            {
                                                try
                                                {
                                                    item_images_colorDynamic = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(item_images_color);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine("Error deserializing item_images_color: " + ex.Message);
                                                }
                                            }

                                            itemViewList.Add(new ItemViewListResponse
                                            {
                                                ViewId = view_id,
                                                ItemSoliter = item_soliter,
                                                ItemDisLabourPer = ItemDisLabourPer,
                                                ItemAproxDay = ItemAproxDay,
                                                LabourPer = labour_per,
                                                PlainGoldStatus = plaingold_status,
                                                CartId = cat_id,
                                                DataId = data_id,
                                                ItemId = item_id,
                                                ItemCode = item_code,
                                                ItemName = item_name,
                                                ItemSku = item_sku,
                                                ItemDescription = item_description,
                                                ItemMrp = item_mrp,
                                                ItemPrice = item_price,
                                                DistPrice = dist_price,
                                                ImagePath = image_path,
                                                ImageThumbPath = image_thumb_path,
                                                DsgSfx = dsg_sfx,
                                                DsgSize = dsg_size,
                                                DsgKt = dsg_kt,
                                                DsgColor = dsg_color,
                                                Star = star,
                                                CartImg = cart_img,
                                                ImgCartTitle = img_cart_title,
                                                WatchImg = watch_img,
                                                ImgWatchTitle = img_watch_title,
                                                WishCount = wish_count,
                                                WearitCount = wearit_count,
                                                WearitStatus = wearit_status,
                                                WearitImg = wearit_img,
                                                WearitNoneImg = wearit_none_img,
                                                WearitColor = wearit_color,
                                                WearitText = wearit_text,
                                                ImgWearitTitle = img_wearit_title,
                                                WishDefaultImg = wish_default_img,
                                                WishFillImg = wish_fill_img,
                                                ImgWishTitle = img_wish_title,
                                                ItemReview = item_review,
                                                ItemSize = item_size,
                                                ItemKt = item_kt,
                                                ItemColor = item_color,
                                                ItemMetal = item_metal,
                                                ItemWt = item_wt,
                                                ItemStone = item_stone,
                                                ItemStoneWt = item_stone_wt,
                                                ItemStoneQty = item_stone_qty,
                                                PriceText = price_text,
                                                CartPrice = cart_price,
                                                ItemColorId = item_color_id,
                                                ItemDetails = item_details,
                                                ItemDiamondDetails = item_diamond_details,
                                                ItemText = item_text,
                                                MoreItemDetails = more_item_details,
                                                ItemStock = item_stock,
                                                ItemRemoveCartImg = item_removecart_img,
                                                ItemRemoveCartTitle = item_removecard_title,
                                                RupySymbol = rupy_symbol,
                                                RecentView = recent_view,
                                                CategoryId = category_id,
                                                CartIds = cart_id,
                                                ItemStockQty = item_stock_qty,
                                                ItemStockColorsizeQty = item_stock_colorsize_qty,
                                                VariantCount = variantCount,
                                                ItemTypeCommonID = ItemTypeCommonID,
                                                ItemNosePinScrewSts = ItemNosePinScrewSts,
                                                ItemFranchiseSts = ItemFranchiseSts,
                                                ItemIllumine = item_illumine,
                                                IsSolitaireOtherCollection = isSolitaireOtherCollection,
                                                ItemGenderCommonID = ItemGenderCommonID,
                                                ProductTags = productTags,
                                                Weight = weight,
                                                TotalLabourPer = totalLabourPer,
                                                FinalDMrp = FinalDMrp,
                                                SelectedColor = selectedColor,
                                                SelectedSize = selectedSize,
                                                SelectedColor1 = selectedColor1,
                                                SelectedSize1 = selectedSize1,
                                                FieldName = field_name,
                                                ColorName = color_name,
                                                DefaultColorName = default_color_name,
                                                SizeList = sizeListDynamic,
                                                ColorList = colorListDynamic,
                                                ItemsColorSizeList = itemsColorSizeListDynamic,
                                                ItemOrderInstructionList = itemOrderInstructionListDynamic,
                                                ItemOrderCustomInstructionList = itemOrderCustomInstructionListDynamic,
                                                ItemImagesColor = item_images_colorDynamic
                                            });
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (itemViewList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = itemViewList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<ItemViewListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<ItemViewListResponse>();
                return responseDetails;
            }
        }
    }
}