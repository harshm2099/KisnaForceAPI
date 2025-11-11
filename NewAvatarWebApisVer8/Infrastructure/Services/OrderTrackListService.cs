using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using Newtonsoft.Json;
using System.Data;
using System.Web.Http;
using System.Web.Http.Tracing;

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
    }
}
