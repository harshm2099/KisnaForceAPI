using Dapper;
using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using System.Data;
using System.Net;
using System.Net.Mail;
using static Twilio.Rest.Content.V1.ContentResource;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class OrderCartService : IOrderCartService
    {
        public string _connection = DBCommands.CONNECTION_STRING;
        private readonly ILogger<OrderCartService> _logger;

        // Inject logger and connection string in constructor
        public OrderCartService(ILogger<OrderCartService> logger)
        {
            _logger = logger;
        }

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

        public async Task<ReturnResponse> OrderItemCancel(OrderItemCancelRequest param)
        {
            var response = new ReturnResponse();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string? cartId = param.CartId as string ?? string.Empty;
                    string? cartItemId = param.CartItemId as string ?? string.Empty;
                    string? itemId = param.ItemId as string ?? string.Empty;
                    string? cartQty = param.CartQty as string ?? string.Empty;
                    string? dataId = param.DataId as string ?? string.Empty;
                    string? email = param.Email as string ?? string.Empty;

                    string cmdQuery = DBCommands.OrderItemCancel;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cart_id", cartId);
                        cmd.Parameters.AddWithValue("@cart_item_id", cartItemId);
                        cmd.Parameters.AddWithValue("@item_id", itemId);
                        cmd.Parameters.AddWithValue("@cart_qty", cartQty);
                        cmd.Parameters.AddWithValue("@data_id", dataId);
                        cmd.Parameters.AddWithValue("@your_email", email);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                response.success = reader["success"]?.ToString();
                                response.message = reader["message"]?.ToString();
                            }
                            else
                            {
                                response.success = "0";
                                response.message = "No data found.";
                            }
                        }
                    }

                    // AFTER STORED PROCEDURE EXECUTION - CALL THE EMAIL FUNCTION
                    if (response.success == "3") // If cancellation was successful
                    {
                        try
                        {
                            // Convert comma-separated string to list of integers
                            var selectedCartItemIDs = cartItemId.Split(',')
                                .Where(x => !string.IsNullOrEmpty(x) && int.TryParse(x, out _))
                                .Select(int.Parse)
                                .ToList();

                            // Create email request
                            var emailRequest = new EmailRequest
                            {
                                CartId = int.Parse(cartId),
                                K = "C", // Cancellation flag
                                YourEmail = email,
                                SelectedCartItemIDs = selectedCartItemIDs
                            };

                            // Call the email function (fire and forget - don't wait for completion)
                            _ = Task.Run(async () =>
                            {
                                try
                                {
                                    await CheckoutCartOrderInvoiceSend(emailRequest);
                                    _logger.LogInformation("Order cancellation email sent successfully for cart: {CartId}", cartId);
                                }
                                catch (Exception emailEx)
                                {
                                    _logger.LogError(emailEx, "Failed to send order cancellation email for cart: {CartId}", cartId);
                                }
                            });
                        }
                        catch (Exception emailInitEx)
                        {
                            _logger.LogError(emailInitEx, "Error initializing email sending for cart: {CartId}", cartId);
                            // Don't fail the main operation if email fails
                        }
                    }

                    return response;
                }
            }
            catch (SqlException sqlEx)
            {
                response.success = "0";
                response.message = $"SQL error: {sqlEx.Message}";
                return response;
            }
            catch (Exception ex)
            {
                response.success = "0";
                response.message = $"Unexpected error: {ex.Message}";
                return response;
            }
        }

        // Helper method to get login type
        private async Task<int> GetLoginType(int dataID)
        {
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    var query = @"
                SELECT DataLoginTypeID 
                FROM T_DATA_MST WITH(NOLOCK) 
                WHERE DataID = @DataID AND DataValidSts = 'Y'";

                    var result = await connection.QueryFirstOrDefaultAsync<int?>(query, new { DataID = dataID });
                    return result ?? 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetLoginType for DataID: {DataID}", dataID);
                return 0;
            }
        }

        // Helper method to get MstType
        private async Task<string> GetMstType(int dataLoginType)
        {
            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    var query = @"
                SELECT MstTyp 
                FROM T_COMMON_MASTER WITH(NOLOCK) 
                WHERE MstID = @MstID AND MstValidSts = 'Y' AND MstFlagID = @MstFlagID";

                    var parameters = new
                    {
                        MstID = dataLoginType,
                        MstFlagID = AppConstants.LOGIN_TYPE_COMMON_ID
                    };

                    var result = await connection.QueryFirstOrDefaultAsync<string>(query, parameters);
                    return result ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetMstType for LoginType: {LoginType}", dataLoginType);
                return string.Empty;
            }
        }

        // Helper method to get email list
        private async Task<List<string>> SendingMailList(int sendCommonId, string emailType)
        {
            var emailData = new List<string>();

            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    var query = @"
                SELECT 
                    CASE 
                        WHEN RIGHT(REPLACE(REPLACE(Main.mailids,' ',''),',,',','),1) = ',' 
                        THEN LEFT(REPLACE(REPLACE(Main.mailids,' ',''),',,',','), LEN(REPLACE(REPLACE(Main.mailids,' ',''),',,',',')) -1)
                        ELSE REPLACE(REPLACE(Main.mailids,' ',''),',,',',') 
                    END As email_ids
                FROM (
                    SELECT DISTINCT (
                        SELECT MstName + ',' AS [text()] 
                        FROM T_STRU_COMMON_EMAIL_MST WITH(NOLOCK) 
                        JOIN T_COMMON_MASTER WITH(NOLOCK) ON MstID = EmailCommonId
                        WHERE EmailSendCommonid = @SendCommonId 
                        AND Emailtype = @EmailType 
                        AND MstValidSts = 'Y' 
                        AND MstName is not NULL 
                        FOR XML PATH ('')
                    ) mailids 
                ) [Main]";

                    var parameters = new { SendCommonId = sendCommonId, EmailType = emailType };

                    var result = await connection.QueryFirstOrDefaultAsync<EmailData>(query, parameters);

                    if (result != null && !string.IsNullOrEmpty(result.EmailIds))
                    {
                        emailData = result.EmailIds.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SendingMailList for SendCommonId: {SendCommonId}, EmailType: {EmailType}",
                    sendCommonId, emailType);
            }

            return emailData;
        }

        // Helper method to generate PDF
        private async Task<string> GetPDF(int cartId, string cartNo, string whereCancel)
        {
            try
            {
                // Implement your PDF generation logic here
                var fileName = $"Order_{cartNo}_{DateTime.Now:yyyyMMddHHmmss}.pdf";

                // Your PDF generation logic would go here
                _logger.LogInformation("PDF generated: {FileName} for cart {CartId}", fileName, cartId);

                return fileName;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating PDF for cart {CartId}", cartId);
                return string.Empty;
            }
        }

        private async Task CheckoutCartOrderInvoiceSend(EmailRequest request)
        {
            if (request.CartId <= 0)
            {
                _logger.LogWarning("Invalid cart ID in CheckoutCartOrderInvoiceSend: {CartId}", request.CartId);
                return;
            }

            try
            {
                _logger.LogInformation("Starting CheckoutCartOrderInvoiceSend for cart: {CartId}", request.CartId);

                // Build where clause for cancellation - reference the base table columns
                var whereCancel = "";
                if (request.K?.ToUpper() == "C" && request.SelectedCartItemIDs.Any())
                {
                    var selectedCartItemIDStr = string.Join(",", request.SelectedCartItemIDs);
                    whereCancel = $" AND ISNULL(t1.CartCancelSts, '')='C' AND t1.CartItemID IN ({selectedCartItemIDStr})";
                }

                using (var connection = new SqlConnection(_connection))
                {
                    _logger.LogInformation("Opening database connection for cart: {CartId}", request.CartId);
                    await connection.OpenAsync();
                    _logger.LogInformation("Database connection opened successfully for cart: {CartId}", request.CartId);

                    // FIXED QUERY - Use proper table references
                    var query = @"
                SELECT 
                    cart.OrderTypeId AS ordertyid
                    ,'FOUND FOUND' as title
                    ,concat(billForTitle.mstName,'. ',billFor.DataName) as BillName
                    ,cartfortitle.mstName as orderName
                    ,orderType.MstCd as orderType
                    ,CONCAT(cartForNameTitle.mstName,' ',cartfor.DataContactName) as OrderCartName
                    ,CONCAT(billForNameTitle.mstName,' ',billFor.DataContactName) as userName
                    ,billFor.DataAddr1 as BillAddress
                    ,cart.CartNo
                    ,format(cart.CartChkOutDt,'dd-MMM-yyyy') as cart_chkout_date
                    ,itemName
                    ,item_id
                    ,item_soliter
                    ,CartQty
                    ,CartDPrice
                    ,CartRPrice
                    ,CartMRPrice
                    ,colorCode
                    ,colorName
                    ,sizeCode
                    ,sizeName
                    ,isnull(CartItemInstruction,'') as CartItemInstruction
                    ,ISNULL(reverse(stuff(reverse(cart_item_remarks), 1, 1, '')),'') as cart_item_remarks
                    ,TAB1.CartItemID
                    ,TAB1.CartMstID
                    ,TAB1.CartItemMstID
                    ,CartPrice
                    ,CartItemEntDt
                    ,CartColorCommonID
                    ,CartConfCommonID
                    ,cart.CartBillingDataID
                    ,billType.mstname as BillType
                    ,CONCAT(billForTitle.mstName,'. ',billFor.DataName,' - ',billType.mstname,' ',FORMAT(case when TAB1.CartCancelSts = 'C' then TAB1.CartCancelDate ELSE cart.CartChkOutDt end,'yyyy-MM-dd')) as subject_old
                    ,CONCAT('Order# ',cart.CartNo,' Dated ',FORMAT(cart.CartChkOutDt,'yyyy-MM-dd'),' (',billForTitle.mstName,'. ',billFor.DataName,')') as subject
                    ,item_category
                    ,ItemOdSfx
                    ,isnull(TAB1.CartCancelSts,'') as cart_cancel_sts
                    ,isnull(TAB1.CartCancelQty,'') as cart_cancel_qty
                    ,FORMAT(TAB1.CartCancelDate,'dd-MMM-yyyy') as cart_cancel_date
                    ,cart.CartCreatedDataID
                    ,cart.CartDataID
                    ,TAB1.CartCancelRemark
                    ,CASE WHEN (
                        SELECT COUNT(*) As Cnt 
                        FROM T_STRU_COMMON_ITEM_MST with(nolock)
                        WHERE StruItemID=TAB1.item_id 
                        AND StruItemCommonID IN (" + string.Join(",", AppConstants.ILLUMINE_COLLECTION) + @")) > 0 THEN 'Y' ELSE 'N' 
                    END AS item_illumine
                FROM (
                    SELECT 
                        t1.*
                        ,color.mstname as colorName
                        ,color.mstcd as colorCode
                        ,size.Mstname AS sizeName
                        ,size.MstCd as sizeCode
                        ,it.itemName as itemName
                        ,it.ItemID as item_id
                        ,it.ItemSoliterSts as item_soliter
                        ,itemCtg.MstName as item_category
                        ,ItemOdSfx
                    FROM T_CART_MST_ITEM as t1 with(nolock)
                    LEFT JOIN T_ITEM_MST as it with(nolock) on t1.CartItemMstID = it.itemid
                    LEFT JOIN T_COMMON_MASTER as itemCtg with(nolock) ON it.ItemCtgCommonID = itemCtg.mstid
                    LEFT JOIN T_COMMON_MASTER as color with(nolock) ON t1.CartColorCommonID=color.mstid
                    LEFT JOIN T_COMMON_MASTER as size with(nolock) ON t1.CartConfCommonID=size.mstid
                    WHERE t1.cartmstid = @CartId
                    " + whereCancel + @"
                ) AS TAB1
                LEFT JOIN (
                    SELECT 
                        CartItemMstID
                        ,(
                            ISNULL(
                                (
                                    SELECT COALESCE(rm.MstName+',','')
                                    FROM dbo.T_CART_MST_ITEM_REMARKS AS C with(nolock)
                                    JOIN T_COMMON_MASTER as rm with(nolock) ON CartItemRemarksIDS=rm.MSTID
                                    WHERE C.CartItemMstID = B.CartItemMstID 
                                    GROUP BY 
                                        CartItemMstID
                                        ,rm.MstName FOR XML PATH('')
                                ),''
                            )
                        ) AS cart_item_remarks
                    FROM dbo.T_CART_MST_ITEM_REMARKS AS B with(nolock) 
                    GROUP BY CartItemMstID
                ) AS TAB2 ON TAB1.CartItemID=TAB2.CartItemMstID
                LEFT JOIN T_CART_MST as cart with(nolock) on TAB1.CartMstID=cart.CartID
                LEFT JOIN T_DATA_MST as billFor with(nolock) ON cart.CartBillingDataID=billFor.DataID
                LEFT JOIN dbo.T_DATA_MST AS cartfor with(nolock) ON cart.CartCreatedDataID = cartfor.DataID
                LEFT JOIN dbo.T_COMMON_MASTER AS cartfortitle with(nolock) ON cartfor.DataLoginTypeID = cartfortitle.MstID
                LEFT JOIN T_COMMON_MASTER as cartForNameTitle with(nolock) on cartfor.DataContactTitleCommonID=cartForNameTitle.MstId
                LEFT JOIN T_COMMON_MASTER as billForTitle with(nolock) on billFor.DataNameTitleCommonID=billForTitle.MstId
                LEFT JOIN T_COMMON_MASTER as billForNameTitle with(nolock) on billFor.DataContactTitleCommonID=billForNameTitle.MstId
                LEFT JOIN T_COMMON_MASTER as billType with(nolock) on billFor.DataLoginTypeID=billType.MstId
                LEFT JOIN T_COMMON_MASTER as orderType with(nolock) on cart.OrderTypeId=orderType.MstId";

                    _logger.LogInformation("Executing query for cart: {CartId}", request.CartId);
                    _logger.LogDebug("Query with where clause: {WhereClause}", whereCancel);

                    // Use CommandDefinition with timeout
                    var command = new CommandDefinition(
                        query,
                        new { CartId = request.CartId },
                        commandTimeout: 300,
                        commandType: CommandType.Text
                    );

                    var proformaInvoice = (await connection.QueryAsync<CartItem>(command)).ToList();
                    _logger.LogInformation("Query completed successfully. Found {Count} items for cart: {CartId}",
                        proformaInvoice.Count, request.CartId);

                    if (proformaInvoice.Any())
                    {
                        _logger.LogInformation("Processing {Count} invoice items for cart: {CartId}",
                            proformaInvoice.Count, request.CartId);

                        // Process solitaire items and send emails
                        var itemSoliValue = 0;
                        var cartItemIds = new List<int>();
                        var firstItem = proformaInvoice.First();

                        foreach (var item in proformaInvoice)
                        {
                            if (item.item_soliter == "Y" || item.item_illumine == "Y")
                            {
                                itemSoliValue = 1;
                                cartItemIds.Add(item.CartItemID);

                                _logger.LogInformation("Processing solitaire item {CartItemID} for cart: {CartId}",
                                    item.CartItemID, request.CartId);

                                await ProcessSolitaireEmail(item, request.CartId, request.K);
                            }
                        }

                        // Send main order email
                        _logger.LogInformation("Processing main order email for cart: {CartId}", request.CartId);
                        await ProcessMainOrderEmail(firstItem, request, itemSoliValue, cartItemIds);
                    }
                    else
                    {
                        _logger.LogWarning("No invoice items found for cart: {CartId}", request.CartId);
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                _logger.LogError(sqlEx, "SQL error in CheckoutCartOrderInvoiceSend for cart {CartId}. Error Number: {ErrorNumber}, State: {State}",
                    request.CartId, sqlEx.Number, sqlEx.State);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in CheckoutCartOrderInvoiceSend for cart {CartId}", request.CartId);
            }
            finally
            {
                _logger.LogInformation("Completed CheckoutCartOrderInvoiceSend for cart: {CartId}", request.CartId);
            }
        }

        //private async Task ProcessSolitaireEmail(CartItem item, int cartId, string k)
        //{
        //    try
        //    {
        //        var toEmailDiamond = await SendingMailList(6532, "to");

        //        // Add additional email logic
        //        var loginTypeCommonID = await GetLoginType(item.CartBillingDataID);
        //        var mstType = await GetMstType(loginTypeCommonID);

        //        if (mstType == "D")
        //        {
        //            if (new[] { 2499, 6596, 2500, 20397, 20834 }.Contains(item.OrderTypeId))
        //            {
        //                toEmailDiamond.Add("snmcc@kisna.com");
        //            }
        //        }

        //        // Your email sending logic here
        //        _logger.LogInformation("Solitaire email processed for item {CartItemID}", item.CartItemID);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error processing solitaire email for item {CartItemID}", item.CartItemID);
        //    }
        //}

        //private async Task ProcessMainOrderEmail(CartItem data, EmailRequest request, int itemSoliValue, List<int> cartItemIds)
        //{
        //    try
        //    {
        //        // Determine email lists based on order type
        //        List<string> bccEmail, ccEmail;

        //        if (new[] { 2499, 27587, 27594, 2500, 20397 }.Contains(data.OrderTypeId))
        //        {
        //            bccEmail = await SendingMailList(3103, "bcc");
        //            ccEmail = await SendingMailList(3103, "cc");
        //        }
        //        else
        //        {
        //            if (itemSoliValue == 1)
        //            {
        //                bccEmail = await SendingMailList(3104, "bcc");
        //                ccEmail = await SendingMailList(3104, "cc");
        //            }
        //            else
        //            {
        //                bccEmail = await SendingMailList(3129, "bcc");
        //                ccEmail = await SendingMailList(3129, "cc");
        //            }
        //        }

        //        // Generate PDF
        //        var pdfFileName = await GetPDF(request.CartId, data.CartNo, request.K == "C" ? "cancel_condition" : "");

        //        _logger.LogInformation("Main order email processed for cart {CartId}", request.CartId);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error processing main order email for cart {CartId}", request.CartId);
        //    }
        //}

        private async Task ProcessSolitaireEmail(CartItem item, int cartId, string k)
        {
            try
            {
                var toEmailDiamond = await SendingMailList(6532, "to");

                // Add additional email logic
                var loginTypeCommonID = await GetLoginType(item.CartBillingDataID);
                var mstType = await GetMstType(loginTypeCommonID);

                if (mstType == "D")
                {
                    if (new[] { 2499, 6596, 2500, 20397, 20834 }.Contains(item.ordertyid)) // Use ordertyid instead of OrderTypeId
                    {
                        toEmailDiamond.Add("snmcc@kisna.com");
                    }
                }

                // Determine subject based on cancellation status
                var subject = k?.ToUpper() == "C"
                    ? $"Solitaire Order Cancellation - #{item.CartNo} By {item.OrderCartName}"
                    : $"Solitaire Order Booking - #{item.CartNo} By {item.OrderCartName}";

                // Create email body
                var body = k?.ToUpper() == "C"
                    ? $@"
                SOLITAIRE ORDER CANCELLATION NOTIFICATION

                Order Number: {item.CartNo}
                Customer: {item.OrderCartName}
                Item: {item.itemName}  // Use itemName instead of ItemName
                Cancellation Date: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}

                This is a test notification for solitaire order cancellation.

                ---
                This is a test email from Mailtrap
                "
                    : $@"
                SOLITAIRE ORDER BOOKING NOTIFICATION

                Order Number: {item.CartNo}
                Customer: {item.OrderCartName}
                Item: {item.itemName}  // Use itemName instead of ItemName
                Booking Date: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}

                This is a test notification for solitaire order booking.

                ---
                This is a test email from Mailtrap
                ";

                // Send email using Mailtrap for testing
                foreach (var email in toEmailDiamond)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        var emailSent = await SendTestEmailAsync(email, subject, body);
                        if (emailSent)
                        {
                            _logger.LogInformation("Solitaire test email sent successfully to: {Email}", email);
                        }
                        else
                        {
                            _logger.LogWarning("Failed to send solitaire test email to: {Email}", email);
                        }
                    }
                }

                _logger.LogInformation("Solitaire email processed for item {CartItemID}", item.CartItemID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing solitaire email for item {CartItemID}", item.CartItemID);
            }
        }

        private async Task ProcessMainOrderEmail(CartItem data, EmailRequest request, int itemSoliValue, List<int> cartItemIds)
        {
            try
            {
                // Determine email lists based on order type
                List<string> bccEmail, ccEmail;

                // Use data.ordertyid instead of data.OrderTypeId
                if (new[] { 2499, 27587, 27594, 2500, 20397 }.Contains(data.ordertyid))
                {
                    bccEmail = await SendingMailList(3103, "bcc");
                    ccEmail = await SendingMailList(3103, "cc");
                }
                else
                {
                    if (itemSoliValue == 1)
                    {
                        bccEmail = await SendingMailList(3104, "bcc");
                        ccEmail = await SendingMailList(3104, "cc");
                    }
                    else
                    {
                        bccEmail = await SendingMailList(3129, "bcc");
                        ccEmail = await SendingMailList(3129, "cc");
                    }
                }

                // Generate PDF
                var pdfFileName = await GetPDF(request.CartId, data.CartNo, request.K == "C" ? "cancel_condition" : "");

                // Determine subject and body based on cancellation
                // Use data.subject instead of data.Subject
                var subject = request.K == "" ? data.subject : $"ORDER CANCELLATION - {data.CartNo}";

                // Use data.cart_chkout_date instead of data.CartChkOutDate
                // Use data.orderType instead of data.OrderType
                var body = request.K == ""
                    ? $@"
ORDER CONFIRMATION - TEST

Order Number: {data.CartNo}
Customer: {data.BillName}
Order Date: {data.cart_chkout_date}
Order Type: {data.orderType}

This is a TEST email for order confirmation.

PDF Attachment: {pdfFileName}

---
This is a test email from Mailtrap
"
                    : $@"
ORDER CANCELLATION - TEST

Order Number: {data.CartNo}
Customer: {data.BillName}
Cancellation Date: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}
Original Order Date: {data.cart_chkout_date}

This is a TEST email for order cancellation.

Cancelled Items: {cartItemIds.Count} items

---
This is a test email from Mailtrap
";

                // Get recipient emails
                var toEmail = (await GetDataEmailsAsync(data.CartBillingDataID, data.CartDataID, data.CartCreatedDataID)).ToList();

                // Override emails if specific email provided
                if (!string.IsNullOrEmpty(request.YourEmail))
                {
                    toEmail = new List<string> { request.YourEmail };
                    bccEmail = new List<string> { request.YourEmail };
                }

                // Additional email logic based on user type
                var loginTypeCommonID = await GetLoginType(data.CartBillingDataID);
                var mstType = await GetMstType(loginTypeCommonID);

                if (mstType == "D")
                {
                    if (new[] { 2499, 27587, 27594, 6596, 2500, 20397, 20834 }.Contains(data.ordertyid))
                    {
                        ccEmail = ccEmail.Concat(new[] { "snmcc@kisna.com" }).ToList();
                        if (data.ordertyid == 6596)
                        {
                            ccEmail = ccEmail.Concat(new[] { "umesh.a@kisna.com", "order@kisna.com" }).ToList();
                        }
                    }
                }

                _logger.LogInformation("CC mail: {CCEmails}", string.Join(", ", ccEmail));
                _logger.LogInformation("BCC mail: {BCCEmails}", string.Join(", ", bccEmail));
                _logger.LogInformation("To mail: {ToEmails}", string.Join(", ", toEmail));

                // Send to primary recipients
                foreach (var email in toEmail)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        var emailSent = await SendTestEmailAsync(email, subject, body);
                        if (emailSent)
                        {
                            _logger.LogInformation("Main order test email sent successfully to: {Email}", email);
                        }
                        else
                        {
                            _logger.LogWarning("Failed to send main order test email to: {Email}", email);
                        }
                    }
                }

                // Send to CC recipients
                foreach (var email in ccEmail)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        var emailSent = await SendTestEmailAsync(email, $"[CC] {subject}", body);
                        if (emailSent)
                        {
                            _logger.LogInformation("CC email sent successfully to: {Email}", email);
                        }
                    }
                }

                // Send to BCC recipients
                foreach (var email in bccEmail)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        var emailSent = await SendTestEmailAsync(email, $"[BCC] {subject}", body);
                        if (emailSent)
                        {
                            _logger.LogInformation("BCC email sent successfully to: {Email}", email);
                        }
                    }
                }

                _logger.LogInformation("Main order email processed for cart {CartId}", request.CartId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing main order email for cart {CartId}", request.CartId);
            }
        }

        private async Task<bool> SendTestEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525))
                {
                    client.Credentials = new NetworkCredential("d0915810d0b717", "62b1e1959aa61e");
                    client.EnableSsl = true;

                    var mail = new MailMessage
                    {
                        From = new MailAddress("noreply@kisna.com", "KISNA ORDER SYSTEM"),
                        Subject = $"[TEST] {subject}",
                        Body = $@"
{body}

----------------------------------------------
THIS IS A TEST EMAIL - NO ACTION REQUIRED
Sent via Mailtrap Testing Environment
Time: {DateTime.Now:dd-MMM-yyyy HH:mm:ss}
----------------------------------------------",
                        IsBodyHtml = false
                    };
                    mail.To.Add(toEmail);

                    await client.SendMailAsync(mail);
                    _logger.LogInformation("Test email sent successfully to: {ToEmail}", toEmail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mailtrap email error sending to: {ToEmail}", toEmail);
                return false;
            }
        }

        private async Task<IEnumerable<string>> GetDataEmailsAsync(params int[] dataIds)
        {
            var emails = new List<string>();

            try
            {
                using (var connection = new SqlConnection(_connection))
                {
                    await connection.OpenAsync();

                    var query = @"
                SELECT DISTINCT DataEmail 
                FROM T_DATA_MST WITH(NOLOCK) 
                WHERE DataID IN @DataIds AND DataEmail IS NOT NULL AND DataEmail != ''";

                    var results = await connection.QueryAsync<string>(query, new { DataIds = dataIds });
                    emails.AddRange(results);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting data emails for IDs: {DataIds}", string.Join(",", dataIds));
            }

            return emails;
        }
    }
}
