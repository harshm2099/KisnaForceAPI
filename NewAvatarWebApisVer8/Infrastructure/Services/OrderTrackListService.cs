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
using System.Web.Http.Tracing;
using Xunit.Abstractions;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class OrderTrackListService : IOrderTrackListService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        public async Task<ResponseDetails> GetTotalOrderTrackingData(OrderTrackingDataListParams param)
        {
            var responseDetails = new ResponseDetails();
            IList<OrderTrackingDataListResponse> ordertrackinglist = new List<OrderTrackingDataListResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TotalOrderTrackingData;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? trackId = string.IsNullOrWhiteSpace(param.TrackCartMstId) ? null : param.TrackCartMstId;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@trackcartmstid", trackId);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataSet ds = new DataSet();
                            da.Fill(ds);

                            if (ds.Tables.Count > 0)
                            {
                                if (ds.Tables[1].Rows.Count > 0)
                                {
                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                    {
                                        try
                                        {
                                            var rowdetails = ds.Tables[1].Rows[i];
                                            string trackingType = rowdetails["TrackingType"] != DBNull.Value ? Convert.ToString(rowdetails["TrackingType"]) : string.Empty;
                                            string trackingCartId = rowdetails["TrackCartMstId"] != DBNull.Value ? Convert.ToString(rowdetails["TrackCartMstId"]) : string.Empty;
                                            string trackingCartLocalId = rowdetails["TrackCartLocalID"] != DBNull.Value ? Convert.ToString(rowdetails["TrackCartLocalID"]) : string.Empty;
                                            string totalRec = rowdetails["TotalRec"] != DBNull.Value ? Convert.ToString(rowdetails["TotalRec"]) : string.Empty;
                                            string sortBy = rowdetails["MstSortBY"] != DBNull.Value ? Convert.ToString(rowdetails["MstSortBY"]) : string.Empty;

                                            ordertrackinglist.Add(new OrderTrackingDataListResponse
                                            {
                                                TrackingType = trackingType,
                                                TrackCartMstId = trackingCartId,
                                                TrackCartLocalID = trackingCartLocalId,
                                                TotalRec = totalRec,
                                                MstSortBY = sortBy,
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
                if (ordertrackinglist.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = ordertrackinglist;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<OrderTrackingDataListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<OrderTrackingDataListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetTrackSingleDataList(OrderTrackingSingleDataListParams param)
        {
            var responseDetails = new ResponseDetails();
            IList<OrderTrackingSingleDataItemResponse> ordertrackingsinglelist = new List<OrderTrackingSingleDataItemResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GetTrackSingleDataList;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? cartItemId = string.IsNullOrWhiteSpace(param.CartItemId) ? null : param.CartItemId;
                        string? cartId = string.IsNullOrWhiteSpace(param.CartId) ? null : param.CartId;
                        string? cartStatus = string.IsNullOrWhiteSpace(param.CartStatus) ? null : param.CartStatus;
                        string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@cartitemid", cartItemId);
                        cmd.Parameters.AddWithValue("@cartid", cartId);
                        cmd.Parameters.AddWithValue("@cart_sts", cartStatus);
                        cmd.Parameters.AddWithValue("@dataid", dataId);

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
                                            string trackType = rowdetails["TrackType"] != DBNull.Value ? Convert.ToString(rowdetails["TrackType"]) : string.Empty;
                                            string trackId = rowdetails["TrackId"] != DBNull.Value ? Convert.ToString(rowdetails["TrackId"]) : string.Empty;
                                            string cartItmId = rowdetails["CartItemID"] != DBNull.Value ? Convert.ToString(rowdetails["CartItemID"]) : string.Empty;
                                            string cartIds = rowdetails["CartID"] != DBNull.Value ? Convert.ToString(rowdetails["CartID"]) : string.Empty;
                                            string trackCartCommonId = rowdetails["TrackCartCommonId"] != DBNull.Value ? Convert.ToString(rowdetails["TrackCartCommonId"]) : string.Empty;
                                            string trackDate = rowdetails["TrackDate"] != DBNull.Value ? Convert.ToString(rowdetails["TrackDate"]) : string.Empty;
                                            string orderStatus = rowdetails["OrderStatus"] != DBNull.Value ? Convert.ToString(rowdetails["OrderStatus"]) : string.Empty;
                                            string linkOption = rowdetails["isLinkOption"] != DBNull.Value ? Convert.ToString(rowdetails["isLinkOption"]) : string.Empty;
                                            string iconImage = rowdetails["iconimage"] != DBNull.Value ? Convert.ToString(rowdetails["iconimage"]) : string.Empty;
                                            string parcelId = rowdetails["ParcelId"] != DBNull.Value ? Convert.ToString(rowdetails["ParcelId"]) : string.Empty;
                                            string couriorName = rowdetails["CouriorName"] != DBNull.Value ? Convert.ToString(rowdetails["CouriorName"]) : string.Empty;
                                            string docketNo = rowdetails["DocketNo"] != DBNull.Value ? Convert.ToString(rowdetails["DocketNo"]) : string.Empty;
                                            string parcelCustomerName = rowdetails["ParcelCustName"] != DBNull.Value ? Convert.ToString(rowdetails["ParcelCustName"]) : string.Empty;
                                            string parcelLink = rowdetails["ParcelLink"] != DBNull.Value ? Convert.ToString(rowdetails["ParcelLink"]) : string.Empty;
                                            string acknowledgmentUrl = rowdetails["AcknowledgmentUrl"] != DBNull.Value ? Convert.ToString(rowdetails["AcknowledgmentUrl"]) : string.Empty;
                                            string orderNo = rowdetails["OrderNo"] != DBNull.Value ? Convert.ToString(rowdetails["OrderNo"]) : string.Empty;
                                            string emrOrderNo = rowdetails["EmrOrderNo"] != DBNull.Value ? Convert.ToString(rowdetails["EmrOrderNo"]) : string.Empty;
                                            string orderDate = rowdetails["OrderDate"] != DBNull.Value ? Convert.ToString(rowdetails["OrderDate"]) : string.Empty;

                                            ordertrackingsinglelist.Add(new OrderTrackingSingleDataItemResponse
                                            {
                                                TrackType = trackType,
                                                TrackId = trackId,
                                                CartItemId = cartItmId,
                                                CartId = cartIds,
                                                TrackCartCommonId = trackCartCommonId,
                                                TrackDate = trackDate,
                                                OrderStatus = orderStatus,
                                                IsLinkOption = linkOption,
                                                IconImage = iconImage,
                                                ParcelId = parcelId,
                                                CouriorName = couriorName,
                                                DocketNo = docketNo,
                                                ParcelCustomerName = parcelCustomerName,
                                                ParcelLink = parcelLink,
                                                AcknowledgmentUrl = acknowledgmentUrl,
                                                OrderNo = orderNo,
                                                EmrOrderNo = emrOrderNo,
                                                OrderDate = orderDate
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
                if (ordertrackingsinglelist.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = ordertrackingsinglelist;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<OrderTrackingSingleDataItemResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<OrderTrackingSingleDataItemResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetTrackItemDetailData(OrderTrackingItemDetailDataRequest param)
        {
            var responseDetails = new ResponseDetails();
            IList<OrderTrackingItemDetailDataResponse> ordertrackingitemdetaillist = new List<OrderTrackingItemDetailDataResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GetTrackingItemDetailData;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? trackCartMstId = string.IsNullOrWhiteSpace(param.TrackCartMstId) ? null : param.TrackCartMstId;
                        string? trackCartCommonId = string.IsNullOrWhiteSpace(param.TrackCartCommonId) ? null : param.TrackCartCommonId;
                        string? dataLoginType = string.IsNullOrWhiteSpace(param.DataLoginType) ? null : param.DataLoginType;
                        string? dataId = string.IsNullOrWhiteSpace(param.DataId) ? null : param.DataId;
                        string? masterCommonId = string.IsNullOrWhiteSpace(param.MasterCommonId) ? null : param.MasterCommonId;
                        string? cartStatus = string.IsNullOrWhiteSpace(param.CartStatus) ? null : param.CartStatus;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@trackcartmstid", trackCartMstId);
                        cmd.Parameters.AddWithValue("@trackcartcommonid", trackCartCommonId);
                        cmd.Parameters.AddWithValue("@data_login_type", dataLoginType);
                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@master_common_id", masterCommonId);
                        cmd.Parameters.AddWithValue("@cart_sts", cartStatus);

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
                                            string? cartAutoId = rowdetails["cart_auto_id"] != DBNull.Value ? Convert.ToString(rowdetails["cart_auto_id"]) : string.Empty;
                                            string? cartId = rowdetails["cart_id"] != DBNull.Value ? Convert.ToString(rowdetails["cart_id"]) : string.Empty;
                                            string? dataIds = rowdetails["data_id"] != DBNull.Value ? Convert.ToString(rowdetails["data_id"]) : string.Empty;
                                            string? itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string? entDt = rowdetails["ent_dt"] != DBNull.Value ? Convert.ToString(rowdetails["ent_dt"]) : string.Empty;
                                            string? itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string? itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string? itemAproxDay = rowdetails["ItemAproxDay"] != DBNull.Value ? Convert.ToString(rowdetails["ItemAproxDay"]) : string.Empty;
                                            string? itemSku = rowdetails["item_sku"] != DBNull.Value ? Convert.ToString(rowdetails["item_sku"]) : string.Empty;
                                            string? itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string? itemSoliter = rowdetails["item_soliter"] != DBNull.Value ? Convert.ToString(rowdetails["item_soliter"]) : string.Empty;
                                            string? plaingoldStatus = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string? itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string? itemPrice = rowdetails["item_price"] != DBNull.Value ? Convert.ToString(rowdetails["item_price"]) : string.Empty;
                                            string? distPrice = rowdetails["dist_price"] != DBNull.Value ? Convert.ToString(rowdetails["dist_price"]) : string.Empty;
                                            string? dsgSfx = rowdetails["dsg_sfx"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_sfx"]) : string.Empty;
                                            string? dsgSize = rowdetails["dsg_size"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_size"]) : string.Empty;
                                            string? dsgKt = rowdetails["dsg_kt"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_kt"]) : string.Empty;
                                            string? dsgColor = rowdetails["dsg_color"] != DBNull.Value ? Convert.ToString(rowdetails["dsg_color"]) : string.Empty;
                                            string? star = rowdetails["star"] != DBNull.Value ? Convert.ToString(rowdetails["star"]) : string.Empty;
                                            string? cartImg = rowdetails["cart_img"] != DBNull.Value ? Convert.ToString(rowdetails["cart_img"]) : string.Empty;
                                            string? imgCartTitle = rowdetails["img_cart_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_cart_title"]) : string.Empty;
                                            string? watchImg = rowdetails["watch_img"] != DBNull.Value ? Convert.ToString(rowdetails["watch_img"]) : string.Empty;
                                            string? imgWatchTitle = rowdetails["img_watch_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_watch_title"]) : string.Empty;
                                            string? wishCount = rowdetails["wish_count"] != DBNull.Value ? Convert.ToString(rowdetails["wish_count"]) : string.Empty;
                                            string? wearitCount = rowdetails["wearit_count"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_count"]) : string.Empty;
                                            string? wearitStatus = rowdetails["wearit_status"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_status"]) : string.Empty;
                                            string? wearitImg = rowdetails["wearit_img"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_img"]) : string.Empty;
                                            string? wearitNoneImg = rowdetails["wearit_none_img"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_none_img"]) : string.Empty;
                                            string? wearitColor = rowdetails["wearit_color"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_color"]) : string.Empty;
                                            string? wearitText = rowdetails["wearit_text"] != DBNull.Value ? Convert.ToString(rowdetails["wearit_text"]) : string.Empty;
                                            string? imgWearitTitle = rowdetails["img_wearit_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wearit_title"]) : string.Empty;
                                            string? wishDefaultImg = rowdetails["wish_default_img"] != DBNull.Value ? Convert.ToString(rowdetails["wish_default_img"]) : string.Empty;
                                            string? wishFillImg = rowdetails["wish_fill_img"] != DBNull.Value ? Convert.ToString(rowdetails["wish_fill_img"]) : string.Empty;
                                            string? imgWishTitle = rowdetails["img_wish_title"] != DBNull.Value ? Convert.ToString(rowdetails["img_wish_title"]) : string.Empty;
                                            string? itemReview = rowdetails["item_review"] != DBNull.Value ? Convert.ToString(rowdetails["item_review"]) : string.Empty;
                                            string? itemSize = rowdetails["item_size"] != DBNull.Value ? Convert.ToString(rowdetails["item_size"]) : string.Empty;
                                            string? itemKt = rowdetails["item_kt"] != DBNull.Value ? Convert.ToString(rowdetails["item_kt"]) : string.Empty;
                                            string? itemColor = rowdetails["item_color"] != DBNull.Value ? Convert.ToString(rowdetails["item_color"]) : string.Empty;
                                            string? itemMetal = rowdetails["item_metal"] != DBNull.Value ? Convert.ToString(rowdetails["item_metal"]) : string.Empty;
                                            string? itemWt = rowdetails["item_wt"] != DBNull.Value ? Convert.ToString(rowdetails["item_wt"]) : string.Empty;
                                            string? itemStone = rowdetails["item_stone"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone"]) : string.Empty;
                                            string? itemStoneWt = rowdetails["item_stone_wt"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone_wt"]) : string.Empty;
                                            string? itemStoneQty = rowdetails["item_stone_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stone_qty"]) : string.Empty;
                                            string? starColor = rowdetails["star_color"] != DBNull.Value ? Convert.ToString(rowdetails["star_color"]) : string.Empty;
                                            string? priceText = rowdetails["price_text"] != DBNull.Value ? Convert.ToString(rowdetails["price_text"]) : string.Empty;
                                            string? cartPrice = rowdetails["cart_price"] != DBNull.Value ? Convert.ToString(rowdetails["cart_price"]) : string.Empty;
                                            string? itemColorId = rowdetails["item_color_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_color_id"]) : string.Empty;
                                            string? itemDetails = rowdetails["item_details"] != DBNull.Value ? Convert.ToString(rowdetails["item_details"]) : string.Empty;
                                            string? itemDiamondDetails = rowdetails["item_diamond_details"] != DBNull.Value ? Convert.ToString(rowdetails["item_diamond_details"]) : string.Empty;
                                            string? itemText = rowdetails["item_text"] != DBNull.Value ? Convert.ToString(rowdetails["item_text"]) : string.Empty;
                                            string? moreItemDetails = rowdetails["more_item_details"] != DBNull.Value ? Convert.ToString(rowdetails["more_item_details"]) : string.Empty;
                                            string? itemStock = rowdetails["item_stock"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock"]) : string.Empty;
                                            string? cartItemQty = rowdetails["cart_item_qty"] != DBNull.Value ? Convert.ToString(rowdetails["cart_item_qty"]) : string.Empty;
                                            string? itemRemovecartImg = rowdetails["item_removecart_img"] != DBNull.Value ? Convert.ToString(rowdetails["item_removecart_img"]) : string.Empty;
                                            string? itemRemovecardTitle = rowdetails["item_removecard_title"] != DBNull.Value ? Convert.ToString(rowdetails["item_removecard_title"]) : string.Empty;
                                            string? rupySymbol = rowdetails["rupy_symbol"] != DBNull.Value ? Convert.ToString(rowdetails["rupy_symbol"]) : string.Empty;
                                            string? categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string? colorCommonId = rowdetails["color_common_id"] != DBNull.Value ? Convert.ToString(rowdetails["color_common_id"]) : string.Empty;
                                            string? sizeCommonId = rowdetails["size_common_id"] != DBNull.Value ? Convert.ToString(rowdetails["size_common_id"]) : string.Empty;
                                            string? colorCommonName = rowdetails["color_common_name"] != DBNull.Value ? Convert.ToString(rowdetails["color_common_name"]) : string.Empty;
                                            string? sizeCommonName = rowdetails["size_common_name"] != DBNull.Value ? Convert.ToString(rowdetails["size_common_name"]) : string.Empty;
                                            string? colorCommonName1 = rowdetails["color_common_name1"] != DBNull.Value ? Convert.ToString(rowdetails["color_common_name1"]) : string.Empty;
                                            string? sizeCommonName1 = rowdetails["size_common_name1"] != DBNull.Value ? Convert.ToString(rowdetails["size_common_name1"]) : string.Empty;
                                            string? itemGenderCommonID = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string? itemStockQty = rowdetails["item_stock_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock_qty"]) : string.Empty;
                                            string? itemStockColorSizeQty = rowdetails["item_stock_colorsize_qty"] != DBNull.Value ? Convert.ToString(rowdetails["item_stock_colorsize_qty"]) : string.Empty;
                                            string? variantCount = rowdetails["variantCount"] != DBNull.Value ? Convert.ToString(rowdetails["variantCount"]) : string.Empty;
                                            string? cartCancelQty = rowdetails["cart_cancel_qty"] != DBNull.Value ? Convert.ToString(rowdetails["cart_cancel_qty"]) : string.Empty;
                                            string? cartCancelDate = rowdetails["cart_cancel_date"] != DBNull.Value ? Convert.ToString(rowdetails["cart_cancel_date"]) : string.Empty;
                                            string? cartCancelBy = rowdetails["cart_cancel_by"] != DBNull.Value ? Convert.ToString(rowdetails["cart_cancel_by"]) : string.Empty;
                                            string? cartCancelSts = rowdetails["cart_cancel_sts"] != DBNull.Value ? Convert.ToString(rowdetails["cart_cancel_sts"]) : string.Empty;
                                            string? cartCancelName = rowdetails["cart_cancel_name"] != DBNull.Value ? Convert.ToString(rowdetails["cart_cancel_name"]) : string.Empty;
                                            string? itemTypeCommonID = rowdetails["ItemTypeCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemTypeCommonID"]) : string.Empty;
                                            string? itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string? imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string? sizeList = rowdetails["sizeList"] != DBNull.Value ? Convert.ToString(rowdetails["sizeList"]) : string.Empty;
                                            string? colorList = rowdetails["colorList"] != DBNull.Value ? Convert.ToString(rowdetails["colorList"]) : string.Empty;
                                            string? itemsColorSizeList = rowdetails["itemsColorSizeList"] != DBNull.Value ? Convert.ToString(rowdetails["itemsColorSizeList"]) : string.Empty;
                                            string? itemOrderInstructionList = rowdetails["itemOrderInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderInstructionList"]) : string.Empty;
                                            string? itemOrderCustomInstructionList = rowdetails["itemOrderCustomInstructionList"] != DBNull.Value ? Convert.ToString(rowdetails["itemOrderCustomInstructionList"]) : string.Empty;
                                            string? itemImagesColor = rowdetails["item_images_color"] != DBNull.Value ? Convert.ToString(rowdetails["item_images_color"]) : string.Empty;

                                            ordertrackingitemdetaillist.Add(new OrderTrackingItemDetailDataResponse
                                            {
                                                CartAutoId = cartAutoId,
                                                CartId = cartId,
                                                DataId = dataIds,
                                                ItemId = itemId,
                                                EntDt = entDt,
                                                ItemCode = itemCode,
                                                ItemName = itemName,
                                                ItemAproxDay = itemAproxDay,
                                                ItemSku = itemSku,
                                                ItemDescription = itemDescription,
                                                ItemSoliter = itemSoliter,
                                                PlaingoldStatus = plaingoldStatus,
                                                ItemMrp = itemMrp,
                                                ItemPrice = itemPrice,
                                                DistPrice = distPrice,
                                                DsgSfx = dsgSfx,
                                                DsgSize = dsgSize,
                                                DsgKt = dsgKt,
                                                DsgColor = dsgColor,
                                                Star = star,
                                                CartImg = cartImg,
                                                ImgCartTitle = imgCartTitle,
                                                WatchImg = watchImg,
                                                ImgWatchTitle = imgWatchTitle,
                                                WishCount = wishCount,
                                                WearitCount = wearitCount,
                                                WearitStatus = wearitStatus,
                                                WearitImg = wearitImg,
                                                WearitNoneImg = wearitNoneImg,
                                                WearitColor = wearitColor,
                                                WearitText = wearitText,
                                                ImgWearitTitle = imgWearitTitle,
                                                WishDefaultImg = wishDefaultImg,
                                                WishFillImg = wishFillImg,
                                                ImgWishTitle = imgWishTitle,
                                                ItemReview = itemReview,
                                                ItemSize = itemSize,
                                                ItemKt = itemKt,
                                                ItemColor = itemColor,
                                                ItemMetal = itemMetal,
                                                ItemWt = itemWt,
                                                ItemStone = itemStone,
                                                ItemStoneWt = itemStoneWt,
                                                ItemStoneQty = itemStoneQty,
                                                StarColor = starColor,
                                                PriceText = priceText,
                                                CartPrice = cartPrice,
                                                ItemColorId = itemColorId,
                                                ItemDiamondDetails = itemDiamondDetails,
                                                ItemText = itemText,
                                                MoreItemDetails = moreItemDetails,
                                                ItemStock = itemStock,
                                                CartItemQty = cartItemQty,
                                                ItemRemovecartImg = itemRemovecartImg,
                                                ItemRemovecardTitle = itemRemovecardTitle,
                                                RupySymbol = rupySymbol,
                                                CategoryId = categoryId,
                                                ColorCommonId = colorCommonId,
                                                SizeCommonId = sizeCommonId,
                                                ColorCommonName  = colorCommonName,
                                                SizeCommonName = sizeCommonName,
                                                ColorCommonName1 = colorCommonName1,
                                                SizeCommonName1 = sizeCommonName1,
                                                ItemGenderCommonID = itemGenderCommonID,
                                                ItemStockQty = itemStockQty,
                                                ItemStockColorSizeQty = itemStockColorSizeQty,
                                                VariantCount = variantCount,
                                                CartCancelQty = cartCancelQty,
                                                CartCancelDate = cartCancelDate,
                                                CartCancelBy = cartCancelBy,
                                                CartCancelSts = cartCancelSts,
                                                CartCancelName = cartCancelName,
                                                ItemTypeCommonID = itemTypeCommonID,
                                                ItemNosePinScrewSts = itemNosePinScrewSts,
                                                ImagePath = imagePath,
                                                SizeList = sizeList,
                                                ColorList = colorList,
                                                ItemsColorSizeList = itemsColorSizeList,
                                                ItemOrderInstructionList = itemOrderInstructionList,
                                                ItemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                                ItemImagesColor = itemImagesColor
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
                if (ordertrackingitemdetaillist.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = ordertrackingitemdetaillist;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<OrderTrackingItemDetailDataResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<OrderTrackingItemDetailDataResponse>();
                return responseDetails;
            }
        }
    }
}
