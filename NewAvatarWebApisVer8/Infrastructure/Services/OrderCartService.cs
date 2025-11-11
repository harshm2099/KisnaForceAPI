using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using System.Data;
using NewAvatarWebApis.Core.Application.DTOs;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class OrderCartService : IOrderCartService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        public async Task<ResponseDetails> GetMyOrderList(MyOrderListingParams myorderlistparams)
        {
            var responseDetails = new ResponseDetails();
            int total_items = 0;

            IList<MyOrderListing> myOrderList = new List<MyOrderListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GET_MYORDERS;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", myorderlistparams.data_id);

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int CartID = dataReader["CartID"] as int? ?? 0;
                                    string CartNo = dataReader["CartNo"] as string ?? string.Empty;
                                    int CartDataID = dataReader["DataID"] as int? ?? 0;
                                    int DataLoginTypeID = dataReader["LogintypeID"] as int? ?? 0;
                                    string PONo = dataReader["PONo"] as string ?? string.Empty;
                                    string UserName = dataReader["UserName"] as string ?? string.Empty;

                                    DateTime CartChkOutDt = DateTime.MinValue;
                                    if (dataReader["CartChkOutDt"] == null || dataReader["CartChkOutDt"] is System.DBNull)
                                    {
                                        CartChkOutDt = DateTime.MinValue;
                                    }
                                    else
                                    {
                                        CartChkOutDt = Convert.ToDateTime(dataReader["CartChkOutDt"]);
                                    }

                                    string CartRemarks = dataReader["CartRemarks"] as string ?? string.Empty;
                                    string Cart_Status = dataReader["Cart_Status"] as string ?? string.Empty;
                                    int CartQty = dataReader["CartQty"] as int? ?? 0;
                                    string Total_Items_Price = dataReader["Total_Items_Price"] as string ?? string.Empty;
                                    string CartStatus = dataReader["CartStatus"] as string ?? string.Empty;

                                    total_items = dataReader["recordsTotal"] as int? ?? 0;

                                    myOrderList.Add(new MyOrderListing
                                    {
                                        CartID = CartID.ToString(),
                                        CartNo = CartNo,
                                        CartDataID = CartDataID.ToString(),
                                        DataLoginTypeID = DataLoginTypeID.ToString(),
                                        PONo = PONo,
                                        UserName = UserName,
                                        CartChkOutDt = CartChkOutDt.ToString(),
                                        CartRemarks = CartRemarks,
                                        Cart_Status = Cart_Status,
                                        CartQty = CartQty.ToString(),
                                        Total_Items_Price = Total_Items_Price,
                                        CartStatus = CartStatus,
                                    });

                                }
                            }
                        }
                    }
                }

                if (myOrderList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "order list successfully";
                    responseDetails.status = "200";
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.current_page = "1";
                    responseDetails.last_page = "1";
                    responseDetails.data = myOrderList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.total_items = "0";
                    responseDetails.current_page = "0";
                    responseDetails.last_page = "0";
                    responseDetails.data = new List<MyOrderListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.total_items = "0";
                responseDetails.current_page = "0";
                responseDetails.last_page = "0";
                responseDetails.data = new List<MyOrderListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetMyOrderItemList(MyOrderItemListingParams myorderitemlistparams)
        {
            var responseDetails = new ResponseDetails();
            int total_items = 0;

            IList<MyOrderItemListing> myOrderItemList = new List<MyOrderItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GETMYORDERITEMS;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartID", myorderitemlistparams.cart_id);
                        cmd.Parameters.AddWithValue("@DataID", myorderitemlistparams.data_id > 0 ? myorderitemlistparams.data_id : 7);

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int cart_auto_id = dataReader["cart_auto_id"] as int? ?? 0;
                                    int data_id = dataReader["data_id"] as int? ?? 0;
                                    int item_id = dataReader["item_id"] as int? ?? 0;

                                    string item_code = dataReader["item_code"] as string ?? string.Empty;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    string item_sku = dataReader["item_sku"] as string ?? string.Empty;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;
                                    string item_text = dataReader["item_text"] as string ?? string.Empty;
                                    string indentNumber = dataReader["indentNumber"] as string ?? string.Empty;
                                    string more_item_details = dataReader["more_item_details"] as string ?? string.Empty;
                                    int cart_item_qty = dataReader["cart_item_qty"] as int? ?? 0;
                                    int cancel_quantity = dataReader["cancel_quantity"] as int? ?? 0;

                                    double Cart_Price = dataReader["Cart_Price"] as double? ?? 0;
                                    double total_amount = dataReader["total_amount"] as double? ?? 0;
                                    string more_item_details_new = dataReader["more_item_details_new"] as string ?? string.Empty;

                                    string CartPriceDetails = dataReader["CartPriceDetails"] as string ?? string.Empty;
                                    string CartPriceWithQtyDetails = dataReader["CartPriceWithQtyDetails"] as string ?? string.Empty;
                                    string FormattedCartPrice = dataReader["FormattedCartPrice"] as string ?? string.Empty;
                                    string Formattedtotal_amount = dataReader["Formattedtotal_amount"] as string ?? string.Empty;

                                    total_items = dataReader["recordsTotal"] as int? ?? 0;

                                    myOrderItemList.Add(new MyOrderItemListing
                                    {
                                        cart_auto_id = cart_auto_id.ToString(),
                                        data_id = data_id.ToString(),
                                        item_id = item_id.ToString(),
                                        item_code = item_code,
                                        item_name = item_name,
                                        item_sku = item_sku,
                                        item_description = item_description,
                                        image_path = image_path,
                                        item_text = item_text,
                                        indentNumber = indentNumber,
                                        more_item_details = more_item_details,
                                        cart_item_qty = cart_item_qty.ToString(),
                                        cancel_quantity = cancel_quantity.ToString(),
                                        Cart_Price = Cart_Price.ToString(),
                                        total_amount = total_amount.ToString(),
                                        more_item_details_new = more_item_details_new,
                                        CartPriceDetails = CartPriceDetails,
                                        CartPriceWithQtyDetails = CartPriceWithQtyDetails,
                                        FormattedCartPrice = FormattedCartPrice,
                                        Formattedtotal_amount = Formattedtotal_amount,
                                    });

                                }
                            }
                        }
                    }
                }

                if (myOrderItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "order item list successfully";
                    responseDetails.status = "200";
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.current_page = "1";
                    responseDetails.last_page = "1";
                    responseDetails.data = myOrderItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.total_items = "0";
                    responseDetails.current_page = "0";
                    responseDetails.last_page = "0";
                    responseDetails.data = new List<MyOrderItemListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.total_items = "0";
                responseDetails.current_page = "0";
                responseDetails.last_page = "0";
                responseDetails.data = new List<MyOrderItemListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> OrderAssignList(OrderAssignListingParams orderassignlistparams)
        {
            var responseDetails = new ResponseDetails();
            int total_items = 0;

            IList<OrderAssignListing> orderAssignList = new List<OrderAssignListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ORDERASSIGNLIST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartID", orderassignlistparams.CartID);

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    string DataName = dataReader["DataName"] as string ?? string.Empty;
                                    string MstName = dataReader["MstName"] as string ?? string.Empty;

                                    total_items = dataReader["recordsTotal"] as int? ?? 0;

                                    orderAssignList.Add(new OrderAssignListing
                                    {
                                        DataName = DataName,
                                        MstName = MstName,
                                    });

                                }
                            }
                        }
                    }
                }

                if (orderAssignList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Success";
                    responseDetails.status = "200";
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.current_page = "1";
                    responseDetails.last_page = "1";
                    responseDetails.data = orderAssignList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "no record";
                    responseDetails.status = "200";
                    responseDetails.total_items = "0";
                    responseDetails.current_page = "0";
                    responseDetails.last_page = "0";
                    responseDetails.data = new List<OrderAssignListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.total_items = "0";
                responseDetails.current_page = "0";
                responseDetails.last_page = "0";
                responseDetails.data = new List<OrderAssignListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CartCancel(CartCancelListingParams cartcancel_params)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = cartcancel_params.DataID as int? ?? 0;
                    int CartID = cartcancel_params.CartID as int? ?? 0;

                    string
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.CARTCANCEL;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@CartID", CartID);

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

        public async Task<ResponseDetails> CartSingleCancel(CartSingleCancelListingParams cartsinglecancel_params)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int CartItemID = cartsinglecancel_params.CartItemID as int? ?? 0;
                    int DataID = cartsinglecancel_params.DataID as int? ?? 0;
                    int CartID = cartsinglecancel_params.CartID as int? ?? 0;

                    string
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.CARTSINGLECANCEL;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartItemID", CartItemID);
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@CartID", CartID);

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
    }
}
 