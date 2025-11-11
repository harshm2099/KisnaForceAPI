using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Text;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class CartBulkUploadService : ICartBulkUploadService
    {
        public string _connection = DBCommands.CONNECTION_STRING;
        public string _customerID = DBCommands.CustomerID;

        public async Task<ResponseDetails> GetColourBulk()
        {
            var responseDetails = new ResponseDetails();
            int total_items = 0;

            IList<MasterColor> colorList = new List<MasterColor>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GETCOLORBULK;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int ColorID = dataReader["ColorID"] as int? ?? 0;
                                    string ColorCode = dataReader["ColorCode"] as string ?? string.Empty;
                                    string ColorName = dataReader["ColorName"] as string ?? string.Empty;
                                    string Description = dataReader["Description"] as string ?? string.Empty;
                                    int SortOrder = dataReader["SortOrder"] as int? ?? 0;

                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    colorList.Add(new MasterColor
                                    {
                                        ColorID = ColorID,
                                        ColorCode = ColorCode,
                                        ColorName = ColorName,
                                        Description = Description,
                                        SortOrder = SortOrder,
                                    });

                                }
                            }
                        }
                    }
                }

                if (colorList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Success";
                    responseDetails.status = "200";
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.current_page = "1";
                    responseDetails.last_page = "1";
                    responseDetails.data = colorList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "no record";
                    responseDetails.status = "200";
                    responseDetails.total_items = "0";
                    responseDetails.current_page = "0";
                    responseDetails.last_page = "0";
                    responseDetails.data = new List<MasterColor>();
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
                responseDetails.data = new List<MasterColor>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetCategorySizeBulk()
        {
            var responseDetails = new ResponseDetails();
            int total_items = 0;

            IList<MasterCategorySize> categorySizeList = new List<MasterCategorySize>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GETCATEGORYSIZEBULK;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int MstID = dataReader["MstID"] as int? ?? 0;
                                    string MstCd = dataReader["MstCd"] as string ?? string.Empty;
                                    string MstName = dataReader["MstName"] as string ?? string.Empty;
                                    string MstDesc = dataReader["MstDesc"] as string ?? string.Empty;
                                    string MstValidSts = dataReader["MstValidSts"] as string ?? string.Empty;
                                    int MstFlagID = dataReader["MstFlagID"] as int? ?? 0;
                                    int MstImgID = dataReader["MstImgID"] as int? ?? 0;
                                    int MstSortBy = dataReader["MstSortBy"] as int? ?? 0;
                                    string MstIconImgPath = dataReader["MstIconImgPath"] as string ?? string.Empty;
                                    string MstTyp = dataReader["MstTyp"] as string ?? string.Empty;
                                    string SyncFlg = dataReader["SyncFlg"] as string ?? string.Empty;

                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    categorySizeList.Add(new MasterCategorySize
                                    {
                                        MstID = MstID,
                                        MstCd = MstCd,
                                        MstName = MstName,
                                        MstDesc = MstDesc,
                                        MstValidSts = MstValidSts,
                                        MstFlagID = MstFlagID,
                                        MstImgID = MstImgID,
                                        MstSortBy = MstSortBy,
                                        MstIconImgPath = MstIconImgPath,
                                        MstTyp = MstTyp,
                                        SyncFlg = SyncFlg,
                                    });
                                }
                            }
                        }
                    }
                }

                if (categorySizeList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Success";
                    responseDetails.status = "200";
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.current_page = "1";
                    responseDetails.last_page = "1";
                    responseDetails.data = categorySizeList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "no record";
                    responseDetails.status = "200";
                    responseDetails.total_items = "0";
                    responseDetails.current_page = "0";
                    responseDetails.last_page = "0";
                    responseDetails.data = new List<MasterCategorySize>();
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
                responseDetails.data = new List<MasterCategorySize>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetSizeBulk(ItemSizeListingParams itemsizelistparams)
        {
            var responseDetails = new ResponseDetails();
            int total_items = 0;

            IList<MasterItemSize> itemSizeList = new List<MasterItemSize>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GETITEMSIZEBULKBY_SIZEID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SizeID", itemsizelistparams.SizeID);

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int MstID = dataReader["MstID"] as int? ?? 0;
                                    string MstCd = dataReader["MstCd"] as string ?? string.Empty;
                                    string MstName = dataReader["MstName"] as string ?? string.Empty;
                                    string MstDesc = dataReader["MstDesc"] as string ?? string.Empty;
                                    string MstValidSts = dataReader["MstValidSts"] as string ?? string.Empty;
                                    int MstFlagID = dataReader["MstFlagID"] as int? ?? 0;
                                    int MstImgID = dataReader["MstImgID"] as int? ?? 0;
                                    int MstSortBy = dataReader["MstSortBy"] as int? ?? 0;
                                    string MstIconImgPath = dataReader["MstIconImgPath"] as string ?? string.Empty;
                                    string MstTyp = dataReader["MstTyp"] as string ?? string.Empty;
                                    string SyncFlg = dataReader["SyncFlg"] as string ?? string.Empty;

                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    itemSizeList.Add(new MasterItemSize
                                    {
                                        MstID = MstID,
                                        MstCd = MstCd,
                                        MstName = MstName,
                                        MstDesc = MstDesc,
                                        MstValidSts = MstValidSts,
                                        MstFlagID = MstFlagID,
                                        MstImgID = MstImgID,
                                        MstSortBy = MstSortBy,
                                        MstIconImgPath = MstIconImgPath,
                                        MstTyp = MstTyp,
                                        SyncFlg = SyncFlg,
                                    });
                                }
                            }
                        }
                    }
                }

                if (itemSizeList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Success";
                    responseDetails.status = "200";
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.current_page = "1";
                    responseDetails.last_page = "1";
                    responseDetails.data = itemSizeList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "no record";
                    responseDetails.status = "200";
                    responseDetails.total_items = "0";
                    responseDetails.current_page = "0";
                    responseDetails.last_page = "0";
                    responseDetails.data = new List<MasterItemSize>();
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
                responseDetails.data = new List<MasterItemSize>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetOrderTypevalue(OrderTypeListingParams ordertypelistparams)
        {
            var responseDetails = new ResponseDetails();
            int total_items = 0;

            IList<OrderTypeListing> ordertypeList = new List<OrderTypeListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.BULKUPLOAD_ORDERTYPEVALUE;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", ordertypelistparams.DataID);

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int ordertype_mst_id = dataReader["ordertype_mst_id"] as int? ?? 0;
                                    string ordertype_mst_code = dataReader["ordertype_mst_code"] as string ?? string.Empty;
                                    string ordertype_mst_name = dataReader["ordertype_mst_name"] as string ?? string.Empty;

                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    ordertypeList.Add(new OrderTypeListing
                                    {
                                        ordertype_mst_id = ordertype_mst_id,
                                        ordertype_mst_code = ordertype_mst_code,
                                        ordertype_mst_name = ordertype_mst_name,
                                    });
                                }
                            }
                        }
                    }
                }

                if (ordertypeList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Success";
                    responseDetails.status = "200";
                    responseDetails.total_items = total_items.ToString();
                    responseDetails.current_page = "1";
                    responseDetails.last_page = "1";
                    responseDetails.data = ordertypeList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "no record";
                    responseDetails.status = "200";
                    responseDetails.total_items = "0";
                    responseDetails.current_page = "0";
                    responseDetails.last_page = "0";
                    responseDetails.data = new List<OrderTypeListing>();
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
                responseDetails.data = new List<OrderTypeListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> BulkUploadVerifyData(BulkUploadVerifyDataParams bulkuploadverifydata_params)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = bulkuploadverifydata_params.DataID as int? ?? 0;

                    string
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.BULKUPLOAD_VERIFYDATA;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);

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
                    responseDetails.data = new[]
                    {
                        new
                        {
                            hasError = (resstatus == 1 ? true : false)
                        }
                    };
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

        public async Task<ResponseDetails> BulkUploadDelete(BulkUploadDeleteParams bulkuploaddelete_params)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = bulkuploaddelete_params.DataID as int? ?? 0;
                    string temps_id = bulkuploaddelete_params.temps_id as string ?? string.Empty;

                    string
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.BULKUPLOADDELETE;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@temps_id", temps_id);

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

        int DeleteAndInsertLog_TEMPTABLE(int DataID)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    string cmdQuery = DBCommands.BULKIMPORTITEMS_DELETE_TEMP_AND_INSERT_LOG;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex.Message}");
                retVal = 0;
            }
            return retVal;
        }

        int SaveBulkImportItems(BulkImportInsertItemParams bulkimportinsertitem_params)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = bulkimportinsertitem_params.DataID as int? ?? 0;
                    int DiscontinueItemAllowUsers_Sts = bulkimportinsertitem_params.DiscontinueItemAllowUsers_Sts as int? ?? 0;
                    string ItemFranchiseSts = bulkimportinsertitem_params.ItemFranchiseSts as string ?? string.Empty;
                    int Premium_collection_check_flag = bulkimportinsertitem_params.Premium_collection_check_flag as int? ?? 0;
                    string style = bulkimportinsertitem_params.style as string ?? string.Empty;
                    string brand = bulkimportinsertitem_params.brand as string ?? string.Empty;
                    string size = bulkimportinsertitem_params.size as string ?? string.Empty;
                    string color = bulkimportinsertitem_params.color as string ?? string.Empty;
                    int qty = bulkimportinsertitem_params.qty as int? ?? 0;
                    string Remarks = bulkimportinsertitem_params.Remarks as string ?? string.Empty;
                    string insdat = bulkimportinsertitem_params.insdat as string ?? string.Empty;

                    string cmdQuery = DBCommands.BULKIMPORTITEMS_INSERT_ITEMWISE_DATA;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@DiscontinueItemAllowUsers_Sts", DiscontinueItemAllowUsers_Sts);
                        cmd.Parameters.AddWithValue("@ItemFranchiseSts", ItemFranchiseSts);
                        cmd.Parameters.AddWithValue("@Premium_collection_check_flag", Premium_collection_check_flag);
                        cmd.Parameters.AddWithValue("@style", style);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@size", size);
                        cmd.Parameters.AddWithValue("@color", color);
                        cmd.Parameters.AddWithValue("@qty", qty);
                        cmd.Parameters.AddWithValue("@Remarks", Remarks);
                        cmd.Parameters.AddWithValue("@insdat", insdat);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL error: {ex.Message}");
                retVal = 0;
            }
            return retVal;
        }

        public async Task<ResponseDetails> BulkImportItems(BulkImportItemsParams bulkimportitems_params)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = bulkimportitems_params.DataID as int? ?? 0;
                    int BillingForDataID = bulkimportitems_params.BillingForDataID as int? ?? 0;
                    List<ItemRequest> items = bulkimportitems_params.items;

                    string
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.BULKIMPORTITEMS_INITIALCHECK;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@BillingForDataID", BillingForDataID);

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
                                        int DiscontinueItemAllowUsers_Sts = firstRow["DiscontinueItemAllowUsers_Sts"] as int? ?? 0;
                                        string ItemFranchiseSts = firstRow["ItemFranchiseSts"] as string ?? string.Empty;
                                        int Premium_collection_check_flag = firstRow["Premium_collection_check_flag"] as int? ?? 0;

                                        // Premium_collection
                                        if (Premium_collection_check_flag == 1 && ds.Tables.Count > 1)
                                        {
                                            DataTable dt = new DataTable();
                                            dt = ds.Tables[0];
                                            var dbItems = dt.AsEnumerable()
                                                .Select(row => new
                                                {
                                                    Style = row.Field<string>("style"),
                                                    Brand = row.Field<string>("brand")
                                                })
                                                .ToList();

                                            var matchedItems = items
                                                .Where(item => dbItems
                                                    .Any(dbItem => dbItem.Style == item.Style && dbItem.Brand == item.Brand))
                                                .ToList();

                                            if (matchedItems.Any())
                                            {
                                                responseDetails.success = false;
                                                responseDetails.message = "Warning: You cannot add Kisna Premium Collection.";
                                                responseDetails.status = "200";
                                                return responseDetails;
                                            }
                                        }

                                        // DELETE FROM T_TEMP AND INSERT INTO LOG
                                        DeleteAndInsertLog_TEMPTABLE(DataID);

                                        var insdat = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                        foreach (var item in items)
                                        {
                                            string
                                                style = item.Style,
                                                brand = item.Brand,
                                                size = item.Size,
                                                color = item.Color,
                                                Remarks = item.Remarks;
                                            int qty = item.Qty;

                                            BulkImportInsertItemParams bulkimportinsertitem_params = new BulkImportInsertItemParams();
                                            bulkimportinsertitem_params.DataID = DataID;
                                            bulkimportinsertitem_params.DiscontinueItemAllowUsers_Sts = DiscontinueItemAllowUsers_Sts;
                                            bulkimportinsertitem_params.ItemFranchiseSts = ItemFranchiseSts;
                                            bulkimportinsertitem_params.Premium_collection_check_flag = Premium_collection_check_flag;
                                            bulkimportinsertitem_params.style = style;
                                            bulkimportinsertitem_params.brand = brand;
                                            bulkimportinsertitem_params.size = size;
                                            bulkimportinsertitem_params.color = color;
                                            bulkimportinsertitem_params.qty = qty;
                                            bulkimportinsertitem_params.Remarks = Remarks;
                                            bulkimportinsertitem_params.insdat = insdat;

                                            SaveBulkImportItems(bulkimportinsertitem_params);

                                            resstatus = 1;
                                            resstatuscode = 200;
                                            resmessage = "Excel Data Imported successfully.";
                                        }
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

        public async Task<ResponseDetails> BulkImportGetItemData(BulkImportGetDataParams bulkimportgetdata_params)
        {
            var responseDetails = new ResponseDetails();
            int total_items = 0;

            IList<BulkImportGetDataListing> bulkimportdataList = new List<BulkImportGetDataListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = bulkimportgetdata_params.DataID as int? ?? 0;

                    string cmdQuery = DBCommands.BULKIMPORTITEMS_GETDATA;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int id = dataReader["id"] as int? ?? 0;
                                    string style = dataReader["style"] as string ?? string.Empty;
                                    string brand = dataReader["brand"] as string ?? string.Empty;
                                    string size = dataReader["size"] as string ?? string.Empty;
                                    string color = dataReader["color"] as string ?? string.Empty;
                                    int qty = dataReader["qty"] as int? ?? 0;
                                    string remarks = dataReader["remarks"] as string ?? string.Empty;
                                    int ItemName = dataReader["ItemName"] as int? ?? 0;
                                    string ItemSfx = dataReader["ItemSfx"] as string ?? string.Empty;
                                    int ItemColor = dataReader["ItemColor"] as int? ?? 0;
                                    int ItemSize = dataReader["ItemSize"] as int? ?? 0;
                                    int ItemQty = dataReader["ItemQty"] as int? ?? 0;
                                    string ItemDate = dataReader["ItemDate"] as string ?? string.Empty;
                                    int ItemUserId = dataReader["ItemUserId"] as int? ?? 0;
                                    string ItemRemarks = dataReader["ItemRemarks"] as string ?? string.Empty;
                                    string ItemError = dataReader["ItemError"] as string ?? string.Empty;

                                    total_items = dataReader["total_items"] as int? ?? 0;

                                    bulkimportdataList.Add(new BulkImportGetDataListing
                                    {
                                        id = id.ToString(),
                                        style = style,
                                        brand = brand,
                                        size = size,
                                        color = color,
                                        qty = qty.ToString(),
                                        remarks = remarks,
                                        ItemName = ItemName.ToString(),
                                        ItemSfx = ItemSfx,
                                        ItemColor = ItemColor.ToString(),
                                        ItemSize = ItemSize.ToString(),
                                        ItemQty = ItemQty.ToString(),
                                        ItemDate = ItemDate,
                                        ItemUserId = ItemUserId.ToString(),
                                        ItemRemarks = ItemRemarks,
                                        ItemError = ItemError,
                                    });
                                }
                            }
                        }
                    }

                    if (bulkimportdataList.Any())
                    {
                        responseDetails.success = true;
                        responseDetails.message = "Items fetched successfully";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.current_page = "1";
                        responseDetails.last_page = "1";
                        responseDetails.data = bulkimportdataList;
                    }
                    else
                    {
                        responseDetails.success = false;
                        responseDetails.message = "No data found";
                        responseDetails.status = "200";
                        responseDetails.total_items = "0";
                        responseDetails.current_page = "0";
                        responseDetails.last_page = "0";
                        responseDetails.data = new List<BulkImportGetDataListing>();
                    }
                    return responseDetails;
                }
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.total_items = "0";
                responseDetails.current_page = "0";
                responseDetails.last_page = "0";
                responseDetails.data = new List<BulkImportGetDataListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CheckOutBulkUploadNew(CheckOutBulkUploadnewParams checkoutbulkuploadnew_params)
        {
            var responseDetails = new ResponseDetails();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int data_id = checkoutbulkuploadnew_params.data_id > 0 ? checkoutbulkuploadnew_params.data_id : 0;
                    int cart_order_data_id = checkoutbulkuploadnew_params.cart_order_data_id > 0 ? checkoutbulkuploadnew_params.cart_order_data_id : 0;
                    int cart_billing_data_id = checkoutbulkuploadnew_params.cart_billing_data_id > 0 ? checkoutbulkuploadnew_params.cart_billing_data_id : 0;
                    int cart_order_login_type_id = checkoutbulkuploadnew_params.cart_order_login_type_id > 0 ? checkoutbulkuploadnew_params.cart_order_login_type_id : 0;
                    int cart_billing_login_type_id = checkoutbulkuploadnew_params.cart_billing_login_type_id > 0 ? checkoutbulkuploadnew_params.cart_billing_login_type_id : 0;
                    int ordertypeid = checkoutbulkuploadnew_params.ordertypeid > 0 ? checkoutbulkuploadnew_params.ordertypeid : 0;
                    string cart_remarks = string.IsNullOrWhiteSpace(checkoutbulkuploadnew_params.cart_remarks) ? "" : checkoutbulkuploadnew_params.cart_remarks;
                    string cart_delivery_date = string.IsNullOrWhiteSpace(checkoutbulkuploadnew_params.cart_delivery_date) ? "" : checkoutbulkuploadnew_params.cart_delivery_date;
                    string netip = string.IsNullOrWhiteSpace(checkoutbulkuploadnew_params.netip) ? "" : checkoutbulkuploadnew_params.netip;

                    CommonHelpers objHelpers = new CommonHelpers();
                    string
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.CHECKOUTBULKUPLOAD_NEW;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@cart_order_data_id", cart_order_data_id);
                        cmd.Parameters.AddWithValue("@cart_billing_data_id", cart_billing_data_id);
                        cmd.Parameters.AddWithValue("@cart_order_login_type_id", cart_order_login_type_id);
                        cmd.Parameters.AddWithValue("@cart_billing_login_type_id", cart_billing_login_type_id);
                        cmd.Parameters.AddWithValue("@ordertypeid", ordertypeid);
                        cmd.Parameters.AddWithValue("@netip", netip);
                        cmd.Parameters.AddWithValue("@SourceType", "BULKWEB");
                        BrandDataResponse brandDataResponse = new BrandDataResponse();
                        brandDataResponse.KisnaGoldData = new KisnaGoldData();
                        brandDataResponse.SilverData = new SilverData();
                        brandDataResponse.OroData = new OroData();
                        brandDataResponse.KisnaData = new KisnaData();

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

                                        int cart_id = 0;

                                        if (ds.Tables.Count > 1)
                                        {
                                            decimal
                                                CartPrice = 0,
                                                CartMRPrice = 0,
                                                CartRPrice = 0,
                                                CartDPrice = 0,
                                                ItemMetalWt = 0,
                                                ItemMRP = 0,
                                                TotalMrp = 0,
                                                ItemDPrice = 0,
                                                TotalDMrp = 0,
                                                tmpItemGrossWt = 0,
                                                tmpItemMetalWt = 0;

                                            int
                                                tmpItemID = 0,
                                                // tmpCartMstID = 0,
                                                tmpCartQty = 0,
                                                tmpCartColorCommonID = 0,
                                                tmpCartConfCommonID = 0,
                                                tmpqty = 0,
                                                tmpItemSize = 0,
                                                tmpItemCtgCommonID = 0,
                                                tmpItemBrandCommonID = 0,
                                                tmpItemGenderCommonID = 0,
                                                tmpItemAproxDayCommonID = 0,
                                                dMaster_DataLoginTypeID = 0,
                                                DataLoginTypeID_ToCompare = 0,
                                                billingData_DataLoginTypeID = 0;

                                            string
                                                tmpCartItemInstruction = "",
                                                tmpItemPlainGold = "",
                                                tmpItemSoliterSts = "";

                                            CartItemPriceDetailListingParams cartitempricedetaillistparams;
                                            IList<CartItemPriceDetailListing> cartItemPriceDetailList;
                                            IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold;

                                            CartItemDPRPCALCListingParams cartitemDPRPCALClistparams;
                                            IList<CartItemDPRPCALCListing> cartItemDPRPCALCList;

                                            // products
                                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                            {
                                                cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                                cartItemPriceDetailList = new List<CartItemPriceDetailListing>();
                                                cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();
                                                cartitemDPRPCALClistparams = new CartItemDPRPCALCListingParams();
                                                cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                                                var productrow = ds.Tables[1].Rows[j];
                                                CartPrice = 0;
                                                CartMRPrice = 0;
                                                CartRPrice = 0;
                                                CartDPrice = 0;

                                                cart_id = productrow["cart_id"] as int? ?? 0;
                                                dMaster_DataLoginTypeID = productrow["dMaster_DataLoginTypeID"] != DBNull.Value ? Convert.ToInt32(productrow["dMaster_DataLoginTypeID"]) : 0;
                                                billingData_DataLoginTypeID = productrow["billingData_DataLoginTypeID"] != DBNull.Value ? Convert.ToInt32(productrow["billingData_DataLoginTypeID"]) : 0;
                                                DataLoginTypeID_ToCompare = productrow["DataLoginTypeID_ToCompare"] != DBNull.Value ? Convert.ToInt32(productrow["DataLoginTypeID_ToCompare"]) : 0;

                                                if (ds.Tables.Count > 2)
                                                {
                                                    // Item Master
                                                    for (int k = 0; k < ds.Tables[2].Rows.Count; k++)
                                                    {
                                                        var itemrow = ds.Tables[2].Rows[k];
                                                        tmpItemID = itemrow["ItemID"] != DBNull.Value ? Convert.ToInt32(itemrow["ItemID"]) : 0;
                                                        tmpItemSize = itemrow["ItemSize"] != DBNull.Value ? Convert.ToInt32(itemrow["ItemSize"]) : 0;
                                                        tmpqty = itemrow["qty"] != DBNull.Value ? Convert.ToInt32(itemrow["qty"]) : 0;
                                                        tmpItemCtgCommonID = itemrow["ItemCtgCommonID"] != DBNull.Value ? Convert.ToInt32(itemrow["ItemCtgCommonID"]) : 0;
                                                        tmpItemBrandCommonID = itemrow["ItemBrandCommonID"] != DBNull.Value ? Convert.ToInt32(itemrow["ItemBrandCommonID"]) : 0;
                                                        tmpItemGenderCommonID = itemrow["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(itemrow["ItemGenderCommonID"]) : 0;
                                                        tmpItemAproxDayCommonID = itemrow["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(itemrow["ItemAproxDayCommonID"]) : 0;
                                                        tmpItemGrossWt = itemrow["ItemGrossWt"] != DBNull.Value ? Convert.ToDecimal(itemrow["ItemGrossWt"]) : 0;
                                                        tmpItemMetalWt = itemrow["ItemMetalWt"] != DBNull.Value ? Convert.ToDecimal(itemrow["ItemMetalWt"]) : 0;
                                                        tmpItemPlainGold = itemrow["ItemPlainGold"] as string ?? string.Empty;
                                                        tmpItemSoliterSts = itemrow["ItemSoliterSts"] as string ?? string.Empty;

                                                        cartitempricedetaillistparams.DataID = data_id;
                                                        cartitempricedetaillistparams.ItemID = tmpItemID;
                                                        cartitempricedetaillistparams.SizeID = tmpItemSize;
                                                        cartitempricedetaillistparams.CategoryID = tmpItemCtgCommonID;
                                                        cartitempricedetaillistparams.ItemBrandCommonID = tmpItemBrandCommonID;
                                                        cartitempricedetaillistparams.ItemGrossWt = tmpItemGrossWt;
                                                        cartitempricedetaillistparams.ItemMetalWt = tmpItemMetalWt;
                                                        cartitempricedetaillistparams.ItemGenderCommonID = tmpItemGenderCommonID;

                                                        if (dMaster_DataLoginTypeID == DataLoginTypeID_ToCompare && tmpItemPlainGold == "Y")
                                                        {
                                                            cartitempricedetaillistparams.IsWeightCalcRequired = 1;
                                                            cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                                            if (cartItemPriceDetailList.Any())
                                                            {
                                                                ItemMRP = Math.Round(cartItemPriceDetailList[0].total_price, 0);
                                                                TotalMrp = cartItemPriceDetailList[0].total_price * tmpqty;
                                                                ItemDPrice = Math.Round(cartItemPriceDetailList[0].dp_final_price, 0);
                                                                TotalDMrp = cartItemPriceDetailList[0].dp_final_price * tmpqty;

                                                                CartPrice = cartItemPriceDetailList[0].total_price;
                                                                CartMRPrice = cartItemPriceDetailList[0].total_price;
                                                                CartRPrice = cartItemPriceDetailList[0].total_price;
                                                                CartDPrice = cartItemPriceDetailList[0].dp_final_price;
                                                                ItemMetalWt = cartItemPriceDetailList[0].gold_wt;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (dMaster_DataLoginTypeID == DataLoginTypeID_ToCompare)
                                                            {
                                                                cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                                                cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                                                if (cartItemPriceDetailList.Any())
                                                                {
                                                                    ItemMRP = Math.Round(cartItemPriceDetailList[0].total_price, 0);
                                                                    TotalMrp = cartItemPriceDetailList[0].total_price * tmpqty;
                                                                    ItemDPrice = Math.Round(cartItemPriceDetailList[0].dp_final_price, 0);
                                                                    TotalDMrp = cartItemPriceDetailList[0].dp_final_price * tmpqty;

                                                                    CartPrice = itemrow["ItemMRP"] as decimal? ?? 0;
                                                                    CartMRPrice = cartItemPriceDetailList[0].total_price;
                                                                    CartRPrice = cartItemPriceDetailList[0].total_price;
                                                                    CartDPrice = cartItemPriceDetailList[0].dp_final_price;
                                                                    ItemMetalWt = cartItemPriceDetailList[0].gold_wt;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (tmpItemPlainGold == "Y")
                                                                {
                                                                    cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                                                    cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                                                    if (cartItemPriceDetailList.Any())
                                                                    {
                                                                        ItemMRP = Math.Round(cartItemPriceDetailList[0].total_price, 0);
                                                                        TotalMrp = cartItemPriceDetailList[0].total_price * tmpqty;
                                                                        ItemDPrice = Math.Round(cartItemPriceDetailList[0].dp_final_price, 0);
                                                                        TotalDMrp = cartItemPriceDetailList[0].dp_final_price * tmpqty;

                                                                        CartPrice = itemrow["ItemMRP"] as decimal? ?? 0;
                                                                        CartMRPrice = cartItemPriceDetailList[0].total_price;
                                                                        CartRPrice = cartItemPriceDetailList[0].total_price;
                                                                        CartDPrice = cartItemPriceDetailList[0].dp_final_price;
                                                                        ItemMetalWt = cartItemPriceDetailList[0].gold_wt;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    CartPrice = itemrow["ItemMRP"] != DBNull.Value ? Convert.ToDecimal(itemrow["ItemMRP"]) : 0;
                                                                    CartMRPrice = itemrow["ItemMRP"] != DBNull.Value ? Convert.ToDecimal(itemrow["ItemMRP"]) : 0;
                                                                    ItemMetalWt = itemrow["ItemMetalWt"] != DBNull.Value ? Convert.ToDecimal(itemrow["ItemMetalWt"]) : 0;
                                                                    var allowedLoginTypes = new HashSet<int> { 10, 11, 12 };
                                                                    if (tmpItemSoliterSts == "N" && allowedLoginTypes.Contains(billingData_DataLoginTypeID))
                                                                    {
                                                                        cartitemDPRPCALClistparams.DataID = cart_billing_data_id;
                                                                        cartitemDPRPCALClistparams.MRP = CartPrice;
                                                                        cartItemDPRPCALCList = objHelpers.Get_DP_RP_Calculation(cartitemDPRPCALClistparams);
                                                                        if (cartItemDPRPCALCList.Any())
                                                                        {
                                                                            CartDPrice = cartItemDPRPCALCList[0].D_Price;
                                                                            CartRPrice = cartItemDPRPCALCList[0].R_Price;
                                                                        }
                                                                        else
                                                                        {
                                                                            CartRPrice = itemrow["ItemRPrice"] as decimal? ?? 0;
                                                                            CartDPrice = itemrow["ItemDPrice"] as decimal? ?? 0;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    // ItemMaster
                                                }

                                                tmpItemID = productrow["ItemName"] != DBNull.Value ? Convert.ToInt32(productrow["ItemName"]) : 0;
                                                tmpCartQty = productrow["ItemQty"] != DBNull.Value ? Convert.ToInt32(productrow["ItemQty"]) : 0;
                                                tmpCartColorCommonID = productrow["ItemColor"] != DBNull.Value ? Convert.ToInt32(productrow["ItemColor"]) : 0;
                                                tmpCartConfCommonID = productrow["ItemSize"] != DBNull.Value ? Convert.ToInt32(productrow["ItemSize"]) : 0;
                                                tmpCartItemInstruction = productrow["ItemRemarks"] as string ?? string.Empty;

                                                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                                                {
                                                    tmpItemPlainGold = ds.Tables[2].Rows[0]["ItemPlainGold"] as string ?? string.Empty;
                                                    tmpItemBrandCommonID = ds.Tables[2].Rows[0]["ItemBrandCommonID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[2].Rows[0]["ItemBrandCommonID"]) : 0;
                                                    tmpItemAproxDayCommonID = ds.Tables[2].Rows[0]["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[2].Rows[0]["ItemAproxDayCommonID"]) : 0;
                                                }

                                                string cmdQueryNew = DBCommands.CHECKOUTBULKUPLOAD_NEW_INSERTCARTMSTITEM;
                                                using (SqlCommand cmdNew = new SqlCommand(cmdQueryNew, dbConnection))
                                                {
                                                    cmdNew.CommandType = System.Data.CommandType.StoredProcedure;
                                                    cmdNew.Parameters.AddWithValue("@DataID", data_id);
                                                    cmdNew.Parameters.AddWithValue("@CartMstID", cart_id);
                                                    cmdNew.Parameters.AddWithValue("@CartItemMstID", tmpItemID);
                                                    cmdNew.Parameters.AddWithValue("@CartPrice", CartPrice);
                                                    cmdNew.Parameters.AddWithValue("@CartQty", tmpCartQty);
                                                    cmdNew.Parameters.AddWithValue("@CartMRPrice", CartMRPrice);
                                                    cmdNew.Parameters.AddWithValue("@CartRPrice", CartRPrice);
                                                    cmdNew.Parameters.AddWithValue("@CartDPrice", CartDPrice);
                                                    cmdNew.Parameters.AddWithValue("@CartColorCommonID", tmpCartColorCommonID);
                                                    cmdNew.Parameters.AddWithValue("@CartConfCommonID", tmpCartConfCommonID);
                                                    cmdNew.Parameters.AddWithValue("@CartItemInstruction", tmpCartItemInstruction);
                                                    cmdNew.Parameters.AddWithValue("@SourceType", "BULKWEB");
                                                    cmdNew.Parameters.AddWithValue("@ApproxDaysID", tmpItemAproxDayCommonID);
                                                    cmdNew.Parameters.AddWithValue("@CartItemMetalWt", ItemMetalWt);

                                                    using (SqlDataAdapter daNew = new SqlDataAdapter(cmdNew))
                                                    {
                                                        DataSet dsNew = new DataSet();
                                                        daNew.Fill(dsNew);
                                                        if (dsNew.Tables.Count > 0)
                                                        {
                                                            if (dsNew.Tables[0].Rows.Count > 0)
                                                            {
                                                                try
                                                                {
                                                                    var tmpRow = dsNew.Tables[0].Rows[0];
                                                                    int resstatusNew = tmpRow["resstatus"] != DBNull.Value ? Convert.ToInt32(tmpRow["resstatus"]) : 0;
                                                                    int Cart_Item_ID = tmpRow["Cart_Item_ID"] != DBNull.Value ? Convert.ToInt32(tmpRow["Cart_Item_ID"]) : 0;
                                                                    int CartOldQty = tmpRow["CartOldQty"] != DBNull.Value ? Convert.ToInt32(tmpRow["CartOldQty"]) : 0;

                                                                    if (resstatusNew == 1)
                                                                    {
                                                                        if (dsNew.Tables.Count > 1 && dsNew.Tables[1].Rows.Count > 0)
                                                                        {
                                                                            var itemdetailRow = dsNew.Tables[1].Rows[0];

                                                                            string
                                                                                ItemOdSfx = "";
                                                                            decimal
                                                                                ItemGoldLabourper = 0,
                                                                                ItemDisLabourPer = 0;

                                                                            // Cart_Item_ID = itemdetailRow["Cart_Item_ID"] as int? ?? 0;
                                                                            // CartOldQty = itemdetailRow["CartOldQty"] as int? ?? 0;
                                                                            ItemOdSfx = itemdetailRow["ItemOdSfx"] as string ?? string.Empty;
                                                                            ItemGoldLabourper = itemdetailRow["ItemGoldLabourper"] != DBNull.Value ? Convert.ToDecimal(itemdetailRow["ItemGoldLabourper"]) : 0;
                                                                            ItemDisLabourPer = itemdetailRow["ItemDisLabourPer"] != DBNull.Value ? Convert.ToDecimal(itemdetailRow["ItemDisLabourPer"]) : 0;

                                                                            string
                                                                                design_kt = "",
                                                                                making_per_gram = "";
                                                                            decimal
                                                                                gold_price = 0,
                                                                                gst_price = 0,
                                                                                item_price = 0,
                                                                                total_price = 0,
                                                                                gold_ktprice = 0,
                                                                                pure_gold = 0,
                                                                                labour_price = 0;

                                                                            cartitempricedetaillistparams.DataID = data_id;
                                                                            cartitempricedetaillistparams.ItemID = tmpItemID;
                                                                            cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                                                            cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                                                            if (cartItemPriceDetailList_gold.Count > 0)
                                                                            {
                                                                                pure_gold = cartItemPriceDetailList_gold[0].pure_gold;
                                                                                //making_per_gram = cartItemPriceDetailList_gold[0].labour;
                                                                                making_per_gram = Math.Ceiling(pure_gold * ((ItemGoldLabourper + ItemDisLabourPer) / 100)).ToString();
                                                                                design_kt = cartItemPriceDetailList_gold[0].design_kt;
                                                                                gold_ktprice = cartItemPriceDetailList_gold[0].gold_ktprice;
                                                                                gold_price = cartItemPriceDetailList_gold[0].gold_price;
                                                                                labour_price = cartItemPriceDetailList_gold[0].labour_price;
                                                                                item_price = cartItemPriceDetailList_gold[0].item_price;
                                                                                gst_price = cartItemPriceDetailList_gold[0].GST;
                                                                                total_price = cartItemPriceDetailList_gold[0].total_price;

                                                                                GoldCartInsertParams goldcartparams = new GoldCartInsertParams();
                                                                                goldcartparams.DataId = data_id;
                                                                                // goldcartparams.CartMstId = tmpCartMstID;
                                                                                goldcartparams.CartMstId = cart_id;
                                                                                goldcartparams.CartItemId = Cart_Item_ID;
                                                                                goldcartparams.ItemId = tmpItemID;
                                                                                goldcartparams.design_kt = design_kt;
                                                                                goldcartparams.pure_gold = pure_gold;
                                                                                goldcartparams.gold_ktprice = gold_ktprice;
                                                                                goldcartparams.gold_price = gold_price;
                                                                                goldcartparams.labour_price = labour_price;
                                                                                goldcartparams.item_price = item_price;
                                                                                goldcartparams.gst_price = gst_price;
                                                                                goldcartparams.total_price = total_price;
                                                                                goldcartparams.CartQty = tmpCartQty;
                                                                                goldcartparams.making_per_gram = making_per_gram;
                                                                                goldcartparams.ItemOdSfx = ItemOdSfx;
                                                                                objHelpers.SaveGoldCartItems(goldcartparams);
                                                                            }
                                                                        }

                                                                        LogInsertParams logparams = new LogInsertParams();
                                                                        logparams.DataId = data_id;
                                                                        // logparams.CartMstId = tmpCartMstID;
                                                                        logparams.CartMstId = cart_id;
                                                                        logparams.CartItemId = Cart_Item_ID;
                                                                        logparams.ItemId = tmpItemID;
                                                                        logparams.CartQty = tmpCartQty;
                                                                        logparams.CartOldQty = CartOldQty;
                                                                        logparams.CartColorCommonID = tmpCartColorCommonID;
                                                                        logparams.CartConfCommonID = tmpCartConfCommonID;
                                                                        logparams.CartMRPrice = CartMRPrice;
                                                                        logparams.CartRPrice = CartRPrice;
                                                                        logparams.CartDPrice = CartDPrice;
                                                                        objHelpers.SaveLogValues(logparams);

                                                                        if (Cart_Item_ID > 0)
                                                                        {
                                                                            if (tmpItemBrandCommonID == 18845 || tmpItemBrandCommonID == 18846)
                                                                            {
                                                                                if (tmpItemAproxDayCommonID == 6564)
                                                                                {
                                                                                    // 7 Days
                                                                                    brandDataResponse.KisnaGoldData.SevenDay.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                                else if (tmpItemAproxDayCommonID == 6565)
                                                                                {
                                                                                    // 48 Hours
                                                                                    brandDataResponse.KisnaGoldData.Hours.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                                else if (tmpItemAproxDayCommonID == 6566)
                                                                                {
                                                                                    // 15 Days
                                                                                    brandDataResponse.KisnaGoldData.FifteenDay.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                                else if (tmpItemAproxDayCommonID == 18501)
                                                                                {
                                                                                    // 21 Days
                                                                                    brandDataResponse.KisnaGoldData.TwentyOneDay.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                                else if (tmpItemAproxDayCommonID == 18627)
                                                                                {
                                                                                    // 5 Days
                                                                                    brandDataResponse.KisnaGoldData.FiveDay.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                            }
                                                                            else if (tmpItemBrandCommonID == 19516)
                                                                            {
                                                                                if (tmpItemAproxDayCommonID == 6564)
                                                                                {
                                                                                    // 7 Days
                                                                                    brandDataResponse.SilverData.SevenDay.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                                else if (tmpItemAproxDayCommonID == 6565)
                                                                                {
                                                                                    // 48 Hours
                                                                                    brandDataResponse.SilverData.Hours.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                                else if (tmpItemAproxDayCommonID == 6566)
                                                                                {
                                                                                    // 15 Days
                                                                                    brandDataResponse.SilverData.FifteenDay.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                                else if (tmpItemAproxDayCommonID == 18501)
                                                                                {
                                                                                    // 21 Days
                                                                                    brandDataResponse.SilverData.TwentyOneDay.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                                else if (tmpItemAproxDayCommonID == 18627)
                                                                                {
                                                                                    // 5 Days
                                                                                    brandDataResponse.SilverData.FiveDay.Add(Cart_Item_ID.ToString());
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                if (tmpItemPlainGold == "Y")
                                                                                {
                                                                                    if (tmpItemAproxDayCommonID == 6564)
                                                                                    {
                                                                                        // 7 Days
                                                                                        brandDataResponse.OroData.SevenDay.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                    else if (tmpItemAproxDayCommonID == 6565)
                                                                                    {
                                                                                        // 48 Hours
                                                                                        brandDataResponse.OroData.Hours.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                    else if (tmpItemAproxDayCommonID == 6566)
                                                                                    {
                                                                                        // 15 Days
                                                                                        brandDataResponse.OroData.FifteenDay.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                    else if (tmpItemAproxDayCommonID == 18501)
                                                                                    {
                                                                                        // 21 Days
                                                                                        brandDataResponse.OroData.TwentyOneDay.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                    else if (tmpItemAproxDayCommonID == 18627)
                                                                                    {
                                                                                        // 5 Days
                                                                                        brandDataResponse.OroData.FiveDay.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (tmpItemAproxDayCommonID == 6564)
                                                                                    {
                                                                                        // 7 Days
                                                                                        brandDataResponse.KisnaData.SevenDay.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                    else if (tmpItemAproxDayCommonID == 6565)
                                                                                    {
                                                                                        // 48 Hours
                                                                                        brandDataResponse.KisnaData.Hours.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                    else if (tmpItemAproxDayCommonID == 6566)
                                                                                    {
                                                                                        // 15 Days
                                                                                        brandDataResponse.KisnaData.FifteenDay.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                    else if (tmpItemAproxDayCommonID == 18501)
                                                                                    {
                                                                                        // 21 Days
                                                                                        brandDataResponse.KisnaData.TwentyOneDay.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                    else if (tmpItemAproxDayCommonID == 18627)
                                                                                    {
                                                                                        // 5 Days
                                                                                        brandDataResponse.KisnaData.FiveDay.Add(Cart_Item_ID.ToString());
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                catch (Exception ex)
                                                                {
                                                                    Console.WriteLine(ex.Message);
                                                                }
                                                            }
                                                        }
                                                    }



                                                }

                                            }   //Products

                                            TempTableLogInsertParams temptablelogparams = new TempTableLogInsertParams();
                                            temptablelogparams.DataId = data_id;
                                            temptablelogparams.OperationName = "DELETE";
                                            objHelpers.SaveTempTableLogValues(temptablelogparams);
                                        }

                                        // CartCheckoutNoAllotWEB
                                        CartCheckoutNoAllotWebParams cartcheckoutnoallotweb_params = new CartCheckoutNoAllotWebParams();
                                        cartcheckoutnoallotweb_params.cart_id = cart_id;
                                        cartcheckoutnoallotweb_params.data_id = data_id;
                                        cartcheckoutnoallotweb_params.cart_order_data_id = cart_order_data_id;
                                        cartcheckoutnoallotweb_params.cart_billing_data_id = cart_billing_data_id;
                                        cartcheckoutnoallotweb_params.cart_order_login_type_id = cart_order_login_type_id;
                                        cartcheckoutnoallotweb_params.cart_billing_login_type_id = cart_billing_login_type_id;
                                        cartcheckoutnoallotweb_params.cart_remarks = cart_remarks;
                                        cartcheckoutnoallotweb_params.cart_delivery_date = cart_delivery_date;
                                        cartcheckoutnoallotweb_params.itemall = "Y";
                                        cartcheckoutnoallotweb_params.ordertypeid = ordertypeid;
                                        cartcheckoutnoallotweb_params.devicetype = "";
                                        cartcheckoutnoallotweb_params.devicename = "";
                                        cartcheckoutnoallotweb_params.appversion = "";
                                        cartcheckoutnoallotweb_params.SourceType = "BULKWEB";
                                        cartcheckoutnoallotweb_params.brandDataResponse = brandDataResponse;
                                        CartCheckoutNoAllotWeb(cartcheckoutnoallotweb_params);
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

        static async Task<string> SendHttpRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws an exception if status code is not 2xx
                return await response.Content.ReadAsStringAsync();
            }
        }

        static async Task<string> SendSoapRequest(string url, string soapXml)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("SOAPAction", "http://tempuri.org/ICheckStoneAvailability");
                request.Content = new StringContent(soapXml, Encoding.UTF8, "text/xml");

                HttpResponseMessage response = await client.SendAsync(request);
                return await response.Content.ReadAsStringAsync();
            }
        }

        public class SoapResponse
        {
            public List<DataItem> Data { get; set; }
        }

        public class DataItem
        {
            public string Status { get; set; }
        }

        int SaveDiamondBookServiceResponse(CartCheckoutNoAllotNewParamsPart2 cartcheckoutnoallotnew_params)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = cartcheckoutnoallotnew_params.data_id;
                    int CartID = cartcheckoutnoallotnew_params.cart_id;
                    int ItemID = cartcheckoutnoallotnew_params.item_id;
                    int CartItemID = cartcheckoutnoallotnew_params.cart_item_id;
                    string CartSoliStkNoData = cartcheckoutnoallotnew_params.CartSoliStkNoData;
                    string DiaBookRespose = cartcheckoutnoallotnew_params.DiaBookRespose;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTWEB_SAVEDIABOOKRESPONSE;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@CartID", CartID);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@CartItemID", CartItemID);
                        cmd.Parameters.AddWithValue("@CartSoliStkNoData", CartSoliStkNoData);
                        cmd.Parameters.AddWithValue("@DiaBookRespose", DiaBookRespose);
                        cmd.Parameters.AddWithValue("@SourceType", "APP");

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                retVal = 0;
            }
            return retVal;
        }

        int SaveCartMstItemPrices(CARTCHECKOUTNOALLOTNEW_UPDATE_CARTMSTITEM_PRICESParams cartcheckouotallotnew_update_cartmstitem_prices_params)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int CartItemID = cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemID;
                    decimal CartMRPrice = cartcheckouotallotnew_update_cartmstitem_prices_params.CartMRPrice;
                    decimal CartPrice = cartcheckouotallotnew_update_cartmstitem_prices_params.CartPrice;
                    decimal CartDPrice = cartcheckouotallotnew_update_cartmstitem_prices_params.CartDPrice;
                    decimal FranMarginCurDP = cartcheckouotallotnew_update_cartmstitem_prices_params.FranMarginCurDP;
                    decimal CartItemMetalWt = cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemMetalWt;
                    int FlagValue = cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTNEW_UPDATE_CARTMSTITEM_PRICES;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartItemID", CartItemID);
                        cmd.Parameters.AddWithValue("@CartMRPrice", CartMRPrice);
                        cmd.Parameters.AddWithValue("@CartPrice", CartPrice);
                        cmd.Parameters.AddWithValue("@CartDPrice", CartDPrice);
                        cmd.Parameters.AddWithValue("@FranMarginCurDP", FranMarginCurDP);
                        cmd.Parameters.AddWithValue("@CartItemMetalWt", CartItemMetalWt);
                        cmd.Parameters.AddWithValue("@Flag", FlagValue);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                retVal = 0;
            }
            return retVal;
        }

        int GoldDynamicPriceCart(GoldDynamicPriceCartParams golddynamicpricecartparams)
        {
            int resstatus = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GOLDDYNAMICPRICECART;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", golddynamicpricecartparams.DataId);
                        cmd.Parameters.AddWithValue("@ItemId", golddynamicpricecartparams.ItemId);
                        cmd.Parameters.AddWithValue("@CartItemId", golddynamicpricecartparams.CartItemId);
                        cmd.Parameters.AddWithValue("@CartMstId", golddynamicpricecartparams.CartMstId);
                        cmd.Parameters.AddWithValue("@CartQty", golddynamicpricecartparams.CartQty);
                        cmd.Parameters.AddWithValue("@gold_price", golddynamicpricecartparams.gold_price);
                        cmd.Parameters.AddWithValue("@making_per_gram", golddynamicpricecartparams.making_per_gram);
                        cmd.Parameters.AddWithValue("@total_goldvalue", golddynamicpricecartparams.total_goldvalue);
                        cmd.Parameters.AddWithValue("@total_labour", golddynamicpricecartparams.total_labour);
                        cmd.Parameters.AddWithValue("@finalGoldValue", golddynamicpricecartparams.finalGoldValue);
                        cmd.Parameters.AddWithValue("@item_price", golddynamicpricecartparams.item_price);
                        cmd.Parameters.AddWithValue("@total_price", golddynamicpricecartparams.total_price);
                        cmd.Parameters.AddWithValue("@gst_price", golddynamicpricecartparams.gst_price);
                        cmd.Parameters.AddWithValue("@dp_final_price", golddynamicpricecartparams.dp_final_price);
                        cmd.Parameters.AddWithValue("@design_kt", golddynamicpricecartparams.design_kt);
                        cmd.Parameters.AddWithValue("@labour", golddynamicpricecartparams.labour);

                        dbConnection.Open();

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
                                        resstatus = ds.Tables[0].Rows[0]["resstatus"] as int? ?? 0;
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
            catch (SqlException)
            {
                resstatus = 0;
            }
            return resstatus;
        }

        int SaveSolitaireStatus(CartCheckoutNoAllotWebSaveSolitaireStatusParams cartcheckoutnoallotweb_save_solitairestatus_params)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = cartcheckoutnoallotweb_save_solitairestatus_params.DataID;
                    int CartID = cartcheckoutnoallotweb_save_solitairestatus_params.CartID;
                    int CartItemID = cartcheckoutnoallotweb_save_solitairestatus_params.CartItemID;
                    string Stage = cartcheckoutnoallotweb_save_solitairestatus_params.Stage;
                    string DiaStkNo = cartcheckoutnoallotweb_save_solitairestatus_params.DiaStkNo;
                    string SourceType = cartcheckoutnoallotweb_save_solitairestatus_params.SourceType;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTWEB_SAVE_SOLITAIRESTATUS;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@CartID", CartID);
                        cmd.Parameters.AddWithValue("@CartItemID", CartItemID);
                        cmd.Parameters.AddWithValue("@Stage", Stage);
                        cmd.Parameters.AddWithValue("@DiaStkNo", DiaStkNo);
                        cmd.Parameters.AddWithValue("@SourceType", SourceType);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                retVal = 0;
            }
            return retVal;
        }

        int UpdateCartMst(CartCheckoutNoAllotWebUpdateCartMstParams cartcheckoutnoallotweb_update_cartmst_params)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int cart_id = cartcheckoutnoallotweb_update_cartmst_params.cart_id;
                    int cart_order_data_id = cartcheckoutnoallotweb_update_cartmst_params.cart_order_data_id;
                    int cart_billing_data_id = cartcheckoutnoallotweb_update_cartmst_params.cart_billing_data_id;
                    string cart_remarks = cartcheckoutnoallotweb_update_cartmst_params.cart_remarks;
                    int data_id = cartcheckoutnoallotweb_update_cartmst_params.data_id;
                    string cart_delivery_date = cartcheckoutnoallotweb_update_cartmst_params.cart_delivery_date;
                    string cartno = cartcheckoutnoallotweb_update_cartmst_params.cartno;
                    int count = cartcheckoutnoallotweb_update_cartmst_params.count;
                    int total = cartcheckoutnoallotweb_update_cartmst_params.total;
                    int ordertypeid = cartcheckoutnoallotweb_update_cartmst_params.ordertypeid;
                    int goldcnt = cartcheckoutnoallotweb_update_cartmst_params.goldcnt;
                    int goldvalue = cartcheckoutnoallotweb_update_cartmst_params.goldvalue;
                    int goldpremium = cartcheckoutnoallotweb_update_cartmst_params.goldpremium;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTWEB_UPDATE_CARTMST;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@cart_id", cart_id);
                        cmd.Parameters.AddWithValue("@cart_order_data_id", cart_order_data_id);
                        cmd.Parameters.AddWithValue("@cart_billing_data_id", cart_billing_data_id);
                        cmd.Parameters.AddWithValue("@cart_remarks", cart_remarks);
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@cart_delivery_date", cart_delivery_date);
                        cmd.Parameters.AddWithValue("@cartno", cartno);
                        cmd.Parameters.AddWithValue("@count", count);
                        cmd.Parameters.AddWithValue("@total", total);
                        cmd.Parameters.AddWithValue("@ordertypeid", ordertypeid);
                        cmd.Parameters.AddWithValue("@goldcnt", goldcnt);
                        cmd.Parameters.AddWithValue("@goldvalue", goldvalue);
                        cmd.Parameters.AddWithValue("@goldpremium", goldpremium);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                retVal = 0;
            }
            return retVal;
        }

        int SaveCartStatusMst(CartCheckoutNoAllotWebSaveCartStatusMstParams cartcheckoutnoallotweb_save_cartstatusmst_params)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = cartcheckoutnoallotweb_save_cartstatusmst_params.DataID;
                    int CartID = cartcheckoutnoallotweb_save_cartstatusmst_params.CartID;
                    string Stage = cartcheckoutnoallotweb_save_cartstatusmst_params.Stage;
                    string SourceType = cartcheckoutnoallotweb_save_cartstatusmst_params.SourceType;
                    string Data = string.IsNullOrWhiteSpace(cartcheckoutnoallotweb_save_cartstatusmst_params.Data) ? "" : cartcheckoutnoallotweb_save_cartstatusmst_params.Data;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTWEB_SAVE_CARTSTATUSMST;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@CartID", CartID);
                        cmd.Parameters.AddWithValue("@Stage", Stage);
                        cmd.Parameters.AddWithValue("@SourceType", SourceType);
                        cmd.Parameters.AddWithValue("@Data", Data);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                retVal = 0;
            }
            return retVal;
        }

        int DeleteAndReassignCart(CartCheckoutNoAllotWebDeleteAndReassignCartParams cartcheckoutnoallotweb_deletandreassigncart_params)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = cartcheckoutnoallotweb_deletandreassigncart_params.DataID;
                    int CartID = cartcheckoutnoallotweb_deletandreassigncart_params.CartID;
                    string MsgDetail = cartcheckoutnoallotweb_deletandreassigncart_params.MsgDetail;
                    string SourceType = cartcheckoutnoallotweb_deletandreassigncart_params.SourceType;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTWEB_DELETEANDREASSIGNCART;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@CartID", CartID);
                        cmd.Parameters.AddWithValue("@MsgDetail", MsgDetail);
                        cmd.Parameters.AddWithValue("@SourceType", SourceType);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                retVal = 0;
            }
            return retVal;
        }

        int DeleteCartMst(CartCheckoutNoAllotWebDeleteCartMstParams cartcheckoutnoallotweb_deletcartmst_params)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int CartID = cartcheckoutnoallotweb_deletcartmst_params.CartID;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTWEB_DELETECARTMST;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartID", CartID);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                retVal = 0;
            }
            return retVal;
        }

        int SaveCartCheckoutAllotWeb_CartMst(CartCheckoutNoAllotWebSaveCartMstParams cartcheckouotallotweb_save_cartmst_params)
        {
            int retVal = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = cartcheckouotallotweb_save_cartmst_params.DataID;
                    int CartID = cartcheckouotallotweb_save_cartmst_params.CartID;
                    int OrderTypeID = cartcheckouotallotweb_save_cartmst_params.OrderTypeID;
                    int cart_billing_login_type_id = cartcheckouotallotweb_save_cartmst_params.cart_billing_login_type_id;
                    int cart_order_data_id = cartcheckouotallotweb_save_cartmst_params.cart_order_data_id;
                    int cart_billing_data_id = cartcheckouotallotweb_save_cartmst_params.cart_billing_data_id;
                    string cart_remarks = cartcheckouotallotweb_save_cartmst_params.cart_remarks;
                    string cart_delivery_date = cartcheckouotallotweb_save_cartmst_params.cart_delivery_date;
                    string devicetype = cartcheckouotallotweb_save_cartmst_params.devicetype;
                    string devicename = cartcheckouotallotweb_save_cartmst_params.devicename;
                    string appversion = cartcheckouotallotweb_save_cartmst_params.appversion;
                    string SourceType = cartcheckouotallotweb_save_cartmst_params.SourceType;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTWEB_SAVECARTMST;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@CartID", CartID);
                        cmd.Parameters.AddWithValue("@OrderTypeID", OrderTypeID);
                        cmd.Parameters.AddWithValue("@cart_billing_login_type_id", cart_billing_login_type_id);
                        cmd.Parameters.AddWithValue("@cart_order_data_id", cart_order_data_id);
                        cmd.Parameters.AddWithValue("@cart_billing_data_id", cart_billing_data_id);
                        cmd.Parameters.AddWithValue("@cart_remarks", cart_remarks);
                        cmd.Parameters.AddWithValue("@cart_delivery_date", cart_delivery_date);
                        cmd.Parameters.AddWithValue("@devicetype", devicetype);
                        cmd.Parameters.AddWithValue("@devicename", devicename);
                        cmd.Parameters.AddWithValue("@appversion", appversion);
                        cmd.Parameters.AddWithValue("@SourceType", SourceType);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    retVal = dataReader["resstatus"] as int? ?? 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                retVal = 0;
            }
            return retVal;
        }

        public class NoAllotResponse
        {
            public int status { get; set; }
            public int status_code { get; set; }
            public string message { get; set; }
        }

        NoAllotResponse SaveCartCheckoutAllotWeb_CartMstNew(CartCheckoutNoAllotWebSaveNewCartMstParams cartcheckouotallotweb_savenew_cartmst_params)
        {
            var objNoAllotResponse = new NoAllotResponse();
            objNoAllotResponse.status = 0;
            objNoAllotResponse.status_code = 400;
            objNoAllotResponse.message = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = cartcheckouotallotweb_savenew_cartmst_params.DataID;
                    int CartID = cartcheckouotallotweb_savenew_cartmst_params.CartID;
                    int OrderTypeID = cartcheckouotallotweb_savenew_cartmst_params.OrderTypeID;
                    int cart_order_login_type_id = cartcheckouotallotweb_savenew_cartmst_params.cart_order_login_type_id;
                    int cart_billing_login_type_id = cartcheckouotallotweb_savenew_cartmst_params.cart_billing_login_type_id;
                    int cart_order_data_id = cartcheckouotallotweb_savenew_cartmst_params.cart_order_data_id;
                    int cart_billing_data_id = cartcheckouotallotweb_savenew_cartmst_params.cart_billing_data_id;
                    string cart_remarks = cartcheckouotallotweb_savenew_cartmst_params.cart_remarks;
                    string cart_delivery_date = cartcheckouotallotweb_savenew_cartmst_params.cart_delivery_date;
                    string itemall = cartcheckouotallotweb_savenew_cartmst_params.itemall;
                    string devicetype = cartcheckouotallotweb_savenew_cartmst_params.devicetype;
                    string devicename = cartcheckouotallotweb_savenew_cartmst_params.devicename;
                    string appversion = cartcheckouotallotweb_savenew_cartmst_params.appversion;
                    string SourceType = cartcheckouotallotweb_savenew_cartmst_params.SourceType;

                    int oroseven_cnt = cartcheckouotallotweb_savenew_cartmst_params.oroseven_cnt;
                    string orosevenString = cartcheckouotallotweb_savenew_cartmst_params.orosevenString;
                    int orofifty_cnt = cartcheckouotallotweb_savenew_cartmst_params.orofifty_cnt;
                    string orofiftyString = cartcheckouotallotweb_savenew_cartmst_params.orofiftyString;
                    int orohours_cnt = cartcheckouotallotweb_savenew_cartmst_params.orohours_cnt;
                    string orohoursString = cartcheckouotallotweb_savenew_cartmst_params.orohoursString;
                    int orotwentyone_cnt = cartcheckouotallotweb_savenew_cartmst_params.orotwentyone_cnt;
                    string orotwentyoneString = cartcheckouotallotweb_savenew_cartmst_params.orotwentyoneString;
                    int orofive_cnt = cartcheckouotallotweb_savenew_cartmst_params.orofive_cnt;
                    string orofiveString = cartcheckouotallotweb_savenew_cartmst_params.orofiveString;

                    int kisnaseven_cnt = cartcheckouotallotweb_savenew_cartmst_params.kisnaseven_cnt;
                    string kisnasevenString = cartcheckouotallotweb_savenew_cartmst_params.kisnasevenString;
                    int kisnafifty_cnt = cartcheckouotallotweb_savenew_cartmst_params.kisnafifty_cnt;
                    string kisnafiftyString = cartcheckouotallotweb_savenew_cartmst_params.kisnafiftyString;
                    int kisnahours_cnt = cartcheckouotallotweb_savenew_cartmst_params.kisnahours_cnt;
                    string kisnahoursString = cartcheckouotallotweb_savenew_cartmst_params.kisnahoursString;
                    int kisnatwentyone_cnt = cartcheckouotallotweb_savenew_cartmst_params.kisnatwentyone_cnt;
                    string kisnatwentyoneString = cartcheckouotallotweb_savenew_cartmst_params.kisnatwentyoneString;
                    int kisnafive_cnt = cartcheckouotallotweb_savenew_cartmst_params.kisnafive_cnt;
                    string kisnafiveString = cartcheckouotallotweb_savenew_cartmst_params.kisnafiveString;

                    int kgseven_cnt = cartcheckouotallotweb_savenew_cartmst_params.kgseven_cnt;
                    string kgsevenString = cartcheckouotallotweb_savenew_cartmst_params.kgsevenString;
                    int kgfifty_cnt = cartcheckouotallotweb_savenew_cartmst_params.kgfifty_cnt;
                    string kgfiftyString = cartcheckouotallotweb_savenew_cartmst_params.kgfiftyString;
                    int kghours_cnt = cartcheckouotallotweb_savenew_cartmst_params.kghours_cnt;
                    string kghoursString = cartcheckouotallotweb_savenew_cartmst_params.kghoursString;
                    int kgtwentyone_cnt = cartcheckouotallotweb_savenew_cartmst_params.kgtwentyone_cnt;
                    string kgtwentyoneString = cartcheckouotallotweb_savenew_cartmst_params.kgtwentyoneString;
                    int kgfive_cnt = cartcheckouotallotweb_savenew_cartmst_params.kgfive_cnt;
                    string kgfiveString = cartcheckouotallotweb_savenew_cartmst_params.kgfiveString;

                    int silverseven_cnt = cartcheckouotallotweb_savenew_cartmst_params.silverseven_cnt;
                    string silversevenString = cartcheckouotallotweb_savenew_cartmst_params.silversevenString;
                    int silverfifty_cnt = cartcheckouotallotweb_savenew_cartmst_params.silverfifty_cnt;
                    string silverfiftyString = cartcheckouotallotweb_savenew_cartmst_params.silverfiftyString;
                    int silverhours_cnt = cartcheckouotallotweb_savenew_cartmst_params.silverhours_cnt;
                    string silverhoursString = cartcheckouotallotweb_savenew_cartmst_params.silverhoursString;
                    int silvertwentyone_cnt = cartcheckouotallotweb_savenew_cartmst_params.silvertwentyone_cnt;
                    string silvertwentyoneString = cartcheckouotallotweb_savenew_cartmst_params.silvertwentyoneString;
                    int silverfive_cnt = cartcheckouotallotweb_savenew_cartmst_params.silverfive_cnt;
                    string silverfiveString = cartcheckouotallotweb_savenew_cartmst_params.silverfiveString;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTWEB_SAVECARTMST;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@CartID", CartID);
                        cmd.Parameters.AddWithValue("@OrderTypeID", OrderTypeID);
                        cmd.Parameters.AddWithValue("@cart_order_login_type_id", cart_order_login_type_id);
                        cmd.Parameters.AddWithValue("@cart_billing_login_type_id", cart_billing_login_type_id);
                        cmd.Parameters.AddWithValue("@cart_order_data_id", cart_order_data_id);
                        cmd.Parameters.AddWithValue("@cart_billing_data_id", cart_billing_data_id);
                        cmd.Parameters.AddWithValue("@cart_remarks", cart_remarks);
                        cmd.Parameters.AddWithValue("@cart_delivery_date", cart_delivery_date);
                        cmd.Parameters.AddWithValue("@devicetype", devicetype);
                        cmd.Parameters.AddWithValue("@devicename", devicename);
                        cmd.Parameters.AddWithValue("@appversion", appversion);
                        cmd.Parameters.AddWithValue("@SourceType", SourceType);

                        #region "OroData"
                        cmd.Parameters.AddWithValue("@oroseven_cnt", oroseven_cnt);
                        cmd.Parameters.AddWithValue("@orosevenString", orosevenString);
                        cmd.Parameters.AddWithValue("@orofifty_cnt", orofifty_cnt);
                        cmd.Parameters.AddWithValue("@orofiftyString", orofiftyString);
                        cmd.Parameters.AddWithValue("@orohours_cnt", orohours_cnt);
                        cmd.Parameters.AddWithValue("@orohoursString", orohoursString);
                        cmd.Parameters.AddWithValue("@orotwentyone_cnt", orotwentyone_cnt);
                        cmd.Parameters.AddWithValue("@orotwentyoneString", orotwentyoneString);
                        cmd.Parameters.AddWithValue("@orofive_cnt", orofive_cnt);
                        cmd.Parameters.AddWithValue("@orofiveString", orofiveString);
                        #endregion

                        #region "KisnaData"
                        cmd.Parameters.AddWithValue("@kisnaseven_cnt", kisnaseven_cnt);
                        cmd.Parameters.AddWithValue("@kisnasevenString", kisnasevenString);
                        cmd.Parameters.AddWithValue("@kisnafifty_cnt", kisnafifty_cnt);
                        cmd.Parameters.AddWithValue("@kisnafiftyString", kisnafiftyString);
                        cmd.Parameters.AddWithValue("@kisnahours_cnt", kisnahours_cnt);
                        cmd.Parameters.AddWithValue("@kisnahoursString", kisnahoursString);
                        cmd.Parameters.AddWithValue("@kisnatwentyone_cnt", kisnatwentyone_cnt);
                        cmd.Parameters.AddWithValue("@kisnatwentyoneString", kisnatwentyoneString);
                        cmd.Parameters.AddWithValue("@kisnafive_cnt", kisnafive_cnt);
                        cmd.Parameters.AddWithValue("@kisnafiveString", kisnafiveString);
                        #endregion

                        #region "KisnaGoldData"
                        cmd.Parameters.AddWithValue("@kgseven_cnt", kgseven_cnt);
                        cmd.Parameters.AddWithValue("@kgsevenString", kgsevenString);
                        cmd.Parameters.AddWithValue("@kgfifty_cnt", kgfifty_cnt);
                        cmd.Parameters.AddWithValue("@kgfiftyString", kgfiftyString);
                        cmd.Parameters.AddWithValue("@kghours_cnt", kghours_cnt);
                        cmd.Parameters.AddWithValue("@kghoursString", kghoursString);
                        cmd.Parameters.AddWithValue("@kgtwentyone_cnt", kgtwentyone_cnt);
                        cmd.Parameters.AddWithValue("@kgtwentyoneString", kgtwentyoneString);
                        cmd.Parameters.AddWithValue("@kgfive_cnt", kgfive_cnt);
                        cmd.Parameters.AddWithValue("@kgfiveString", kgfiveString);
                        #endregion

                        #region "SivlerData"
                        cmd.Parameters.AddWithValue("@silverseven_cnt", silverseven_cnt);
                        cmd.Parameters.AddWithValue("@silversevenString", silversevenString);
                        cmd.Parameters.AddWithValue("@silverfifty_cnt", silverfifty_cnt);
                        cmd.Parameters.AddWithValue("@silverfiftyString", silverfiftyString);
                        cmd.Parameters.AddWithValue("@silverhours_cnt", silverhours_cnt);
                        cmd.Parameters.AddWithValue("@silverhoursString", silverhoursString);
                        cmd.Parameters.AddWithValue("@silvertwentyone_cnt", silvertwentyone_cnt);
                        cmd.Parameters.AddWithValue("@silvertwentyoneString", silvertwentyoneString);
                        cmd.Parameters.AddWithValue("@silverfive_cnt", silverfive_cnt);
                        cmd.Parameters.AddWithValue("@silverfiveString", silverfiveString);
                        #endregion

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
                                        CommonHelpers objHelpers = new CommonHelpers();

                                        // Error lists
                                        List<string> error = new List<string>();
                                        List<bool> errStatus = new List<bool>();

                                        int resstatus = ds.Tables[0].Rows[0]["resstatus"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["resstatus"]) : 0;
                                        if (resstatus == 1)
                                        {
                                            if (ds.Tables.Count > 1)
                                            {
                                                int tmpCartID = 0, totalrows = 0, goldcnt = 0, goldvalue = 0, goldpremium = 0;

                                                string orderCheckoutTypeMsg = "";
                                                string orderCheckoutDataMsg = "";
                                                string newcartno = "";

                                                // START - foreach ($cartidarray as $key => $cartid)
                                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                                {
                                                    var tmprow = ds.Tables[1].Rows[i];
                                                    tmpCartID = tmprow["CartID"] as int? ?? 0;
                                                    totalrows = tmprow["TotalRows"] as int? ?? 0;
                                                    goldcnt = tmprow["PureGoldCnt"] as int? ?? 0;
                                                    goldvalue = tmprow["GoldValue"] as int? ?? 0;
                                                    goldpremium = tmprow["GoldPremium"] as int? ?? 0;

                                                    orderCheckoutTypeMsg = tmprow["orderCheckoutTypeMsg"] as string ?? string.Empty;
                                                    orderCheckoutDataMsg = tmprow["orderCheckoutDataMsg"] as string ?? string.Empty;
                                                    newcartno = tmprow["NewCartNo"] as string ?? string.Empty;

                                                    if (cart_order_login_type_id == 10 && cart_billing_login_type_id != 12)
                                                    {
                                                        error.Add($"{orderCheckoutTypeMsg}-{tmpCartID},");
                                                        errStatus.Add(false);
                                                    }
                                                    else if (cart_order_login_type_id == 11 && cart_billing_login_type_id != 11)
                                                    {
                                                        error.Add($"{orderCheckoutTypeMsg}-{tmpCartID},");
                                                        errStatus.Add(false);
                                                    }
                                                    else if (cart_order_login_type_id == 12 && cart_billing_login_type_id != 12)
                                                    {
                                                        error.Add($"{orderCheckoutTypeMsg}-{tmpCartID},");
                                                        errStatus.Add(false);
                                                    }
                                                    else if (cart_order_login_type_id == 69 &&
                                                             !(cart_billing_login_type_id == 10 || cart_billing_login_type_id == 11))
                                                    {
                                                        error.Add($"{orderCheckoutTypeMsg}-{tmpCartID},");
                                                        errStatus.Add(false);
                                                    }

                                                    if (cart_order_login_type_id == 10 && cart_order_data_id == cart_billing_data_id)
                                                    {
                                                        error.Add($"{orderCheckoutDataMsg}-{tmpCartID},");
                                                        errStatus.Add(false);
                                                    }
                                                    else if (cart_order_login_type_id == 11 && cart_order_data_id != cart_billing_data_id)
                                                    {
                                                        error.Add($"{orderCheckoutDataMsg}-{tmpCartID},");
                                                        errStatus.Add(false);
                                                    }
                                                    else if (cart_order_login_type_id == 12 && cart_order_data_id != cart_billing_data_id)
                                                    {
                                                        error.Add($"{orderCheckoutDataMsg}-{tmpCartID},");
                                                        errStatus.Add(false);
                                                    }
                                                    else if (cart_order_login_type_id == 69 && cart_order_data_id == cart_billing_data_id)
                                                    {
                                                        error.Add($"{orderCheckoutDataMsg}-{tmpCartID},");
                                                        errStatus.Add(false);
                                                    }

                                                    if (!errStatus.Contains(false))
                                                    {
                                                        List<string> notArray = new List<string>();
                                                        List<int> available = new List<int>();
                                                        List<int> notAvailable = new List<int>();
                                                        List<string> totalstoke = new List<string>();
                                                        string CartSoliStkNoData = "";
                                                        int
                                                            tmpItemID = 0,
                                                            tmpCartItemID = 0,
                                                            tmpItemCtgCommonID = 0,
                                                            tmpCartMstID = 0,
                                                            tmpCartQty = 0;

                                                        string
                                                            tmpItemPlainGold = "",
                                                            tmpItemSoliterSts = "";

                                                        if (ds.Tables.Count > 3)
                                                        {
                                                            var filteredRows = ds.Tables[3].AsEnumerable().Where(x => x.Field<int>("CartID") == tmpCartID);
                                                            DataTable dt = filteredRows.Any()
                                                                ? filteredRows.CopyToDataTable()
                                                                : ds.Tables[3].Clone();

                                                            for (int j = 0; j < dt.Rows.Count; j++)
                                                            {
                                                                var tbl3Row = dt.Rows[j];

                                                                tmpItemID = tbl3Row["ItemID"] as int? ?? 0;
                                                                tmpCartItemID = tbl3Row["CartItemID"] as int? ?? 0;
                                                                tmpItemCtgCommonID = tbl3Row["ItemCtgCommonID"] as int? ?? 0;
                                                                tmpCartMstID = tbl3Row["CartMstID"] as int? ?? 0;
                                                                tmpCartQty = tbl3Row["CartQty"] as int? ?? 0;

                                                                tmpItemPlainGold = tbl3Row["ItemPlainGold"] as string ?? string.Empty;
                                                                tmpItemSoliterSts = tbl3Row["ItemSoliterSts"] as string ?? string.Empty;

                                                                if (tmpItemSoliterSts == "Y" && (tmpItemCtgCommonID == 4 || tmpItemCtgCommonID == 21 || tmpItemCtgCommonID == 22 || tmpItemCtgCommonID == 25))
                                                                {
                                                                    CartCheckoutNoAllotWebSaveSolitaireStatusParams cartcheckoutnoallotweb_save_solitairestatus_params = new CartCheckoutNoAllotWebSaveSolitaireStatusParams();
                                                                    cartcheckoutnoallotweb_save_solitairestatus_params.DataID = DataID;
                                                                    cartcheckoutnoallotweb_save_solitairestatus_params.CartID = tmpCartID;
                                                                    cartcheckoutnoallotweb_save_solitairestatus_params.CartItemID = tmpCartItemID;
                                                                    cartcheckoutnoallotweb_save_solitairestatus_params.Stage = "Solitaire Item Available";
                                                                    cartcheckoutnoallotweb_save_solitairestatus_params.SourceType = SourceType;
                                                                    SaveSolitaireStatus(cartcheckoutnoallotweb_save_solitairestatus_params);

                                                                    CartSoliStkNoData = tbl3Row["CartSoliStkNoData"] as string ?? string.Empty;

                                                                    List<string> soliterStokeList = new List<string>(CartSoliStkNoData.Split(','));
                                                                    string wsdlUrl = "http://service.hkerp.co/HKE.WCFService.svc?singleWsdl";

                                                                    foreach (var soliterCheck in soliterStokeList)
                                                                    {
                                                                        totalstoke.Add(soliterCheck);

                                                                        var soapRequest = $@"
                                                                        <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
                                                                            <s:Body>
                                                                                <CheckStoneAvailability xmlns=""http://tempuri.org/"">
                                                                                    <CustomerID>{_customerID}</CustomerID>
                                                                                    <PacketNo>{soliterCheck}</PacketNo>
                                                                                </CheckStoneAvailability>
                                                                            </s:Body>
                                                                        </s:Envelope>";

                                                                        // string responseXml = await SendSoapRequest(wsdlUrl, soapRequest);
                                                                        string responseXml = "";
                                                                        var soapResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SoapResponse>(responseXml);
                                                                        if (soapResponse?.Data?[0]?.Status == "Available")
                                                                        {
                                                                            available.Add(1);
                                                                        }
                                                                        else
                                                                        {
                                                                            notArray.Add(soliterCheck);
                                                                            notAvailable.Add(1);
                                                                        }
                                                                    }

                                                                    cartcheckoutnoallotweb_save_solitairestatus_params.Stage = "Check Diamond Available";
                                                                    cartcheckoutnoallotweb_save_solitairestatus_params.DiaStkNo = CartSoliStkNoData;
                                                                    SaveSolitaireStatus(cartcheckoutnoallotweb_save_solitairestatus_params);

                                                                }

                                                                if (tmpItemPlainGold == "Y")
                                                                {
                                                                    string
                                                                        design_kt = "",
                                                                        making_per_gram = "",
                                                                        labour = "";
                                                                    decimal
                                                                        gold_price = 0,
                                                                        total_goldvalue = 0,
                                                                        finalGoldValue = 0,
                                                                        total_labour = 0,
                                                                        gst_price = 0,
                                                                        item_price = 0,
                                                                        total_price = 0,
                                                                        dp_final_price = 0;

                                                                    IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();
                                                                    CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();

                                                                    cartitempricedetaillistparams.DataID = cart_billing_data_id;
                                                                    cartitempricedetaillistparams.ItemID = tmpItemID;
                                                                    cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                                                    cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                                                    if (cartItemPriceDetailList_gold.Count > 0)
                                                                    {
                                                                        gold_price = cartItemPriceDetailList_gold[0].pure_gold;
                                                                        making_per_gram = cartItemPriceDetailList_gold[0].labour;
                                                                        total_goldvalue = cartItemPriceDetailList_gold[0].gold_ktprice;
                                                                        total_labour = cartItemPriceDetailList_gold[0].labour_price;
                                                                        finalGoldValue = cartItemPriceDetailList_gold[0].gold_price;
                                                                        item_price = cartItemPriceDetailList_gold[0].item_price;
                                                                        total_price = cartItemPriceDetailList_gold[0].total_price;
                                                                        design_kt = cartItemPriceDetailList_gold[0].design_kt;
                                                                        labour = cartItemPriceDetailList_gold[0].labour;
                                                                        gst_price = cartItemPriceDetailList_gold[0].GST;
                                                                        dp_final_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                                                    }

                                                                    GoldDynamicPriceCartParams golddynamicpricecartparams = new GoldDynamicPriceCartParams();
                                                                    golddynamicpricecartparams.DataId = cart_billing_data_id;
                                                                    golddynamicpricecartparams.ItemId = tmpItemID;
                                                                    golddynamicpricecartparams.CartItemId = tmpCartItemID;
                                                                    golddynamicpricecartparams.CartMstId = tmpCartMstID;
                                                                    golddynamicpricecartparams.CartQty = tmpCartQty;
                                                                    golddynamicpricecartparams.gold_price = gold_price;
                                                                    golddynamicpricecartparams.making_per_gram = making_per_gram;
                                                                    golddynamicpricecartparams.total_goldvalue = total_goldvalue;
                                                                    golddynamicpricecartparams.total_labour = total_labour;
                                                                    golddynamicpricecartparams.finalGoldValue = finalGoldValue;
                                                                    golddynamicpricecartparams.item_price = item_price;
                                                                    golddynamicpricecartparams.total_price = total_price;
                                                                    golddynamicpricecartparams.gst_price = gst_price;
                                                                    golddynamicpricecartparams.dp_final_price = dp_final_price;
                                                                    golddynamicpricecartparams.design_kt = design_kt;
                                                                    golddynamicpricecartparams.labour = labour;
                                                                    GoldDynamicPriceCart(golddynamicpricecartparams);
                                                                }

                                                            }

                                                            if (notAvailable.Count > 0 && notAvailable.Any())
                                                            {
                                                                string tags = string.Join(", ", notArray);
                                                                error.Add($"This Diamond stock is not available {tags}-{tmpCartID},");
                                                                errStatus.Add(false);
                                                            }
                                                            else
                                                            {
                                                                var filteredRowsNew = dt.AsEnumerable().Where(x => x.Field<string>("ItemSoliterSts") == "Y" && new[] { 4, 21, 22, 25 }.Contains(x.Field<int>("ItemCtgCommonID")));

                                                                DataTable dtNew = filteredRowsNew.Any()
                                                                    ? filteredRowsNew.CopyToDataTable()
                                                                    : new DataTable();

                                                                for (int j = 0; j < dtNew.Rows.Count; j++)
                                                                {
                                                                    var tbl3RowDetails = dtNew.Rows[j];

                                                                    CartSoliStkNoData = tbl3RowDetails["CartSoliStkNoData"] as string ?? string.Empty;
                                                                    tmpCartItemID = tbl3RowDetails["CartItemID"] as int? ?? 0;
                                                                    tmpItemID = tbl3RowDetails["ItemID"] as int? ?? 0;

                                                                    string apiUrl = $"https://service.hk.co/StockApprove?user={_customerID}&pkts={CartSoliStkNoData}&remark={tmpCartItemID}";

                                                                    //string response = await SendHttpRequest(apiUrl);
                                                                    string response = "";

                                                                    CartCheckoutNoAllotNewParamsPart2 cartcheckoutnoallotnewpart2_params = new CartCheckoutNoAllotNewParamsPart2();
                                                                    cartcheckoutnoallotnewpart2_params.cart_id = tmpCartID;
                                                                    cartcheckoutnoallotnewpart2_params.data_id = DataID;
                                                                    cartcheckoutnoallotnewpart2_params.CartSoliStkNoData = CartSoliStkNoData;
                                                                    cartcheckoutnoallotnewpart2_params.DiaBookRespose = response;
                                                                    cartcheckoutnoallotnewpart2_params.item_id = tmpItemID;
                                                                    cartcheckoutnoallotnewpart2_params.cart_item_id = tmpCartItemID;
                                                                    SaveDiamondBookServiceResponse(cartcheckoutnoallotnewpart2_params);
                                                                }
                                                            }
                                                        }

                                                        if (!errStatus.Contains(false))
                                                        {
                                                            CartCheckoutNoAllotWebUpdateCartMstParams cartcheckoutnoallotweb_update_cartmst_params = new CartCheckoutNoAllotWebUpdateCartMstParams();

                                                            cartcheckoutnoallotweb_update_cartmst_params.cart_id = tmpCartID;
                                                            cartcheckoutnoallotweb_update_cartmst_params.cart_order_data_id = cart_order_data_id;
                                                            cartcheckoutnoallotweb_update_cartmst_params.cart_billing_data_id = cart_billing_data_id;
                                                            cartcheckoutnoallotweb_update_cartmst_params.cart_remarks = cart_remarks;
                                                            cartcheckoutnoallotweb_update_cartmst_params.data_id = DataID;
                                                            cartcheckoutnoallotweb_update_cartmst_params.cart_delivery_date = cart_delivery_date;
                                                            cartcheckoutnoallotweb_update_cartmst_params.cartno = newcartno;
                                                            cartcheckoutnoallotweb_update_cartmst_params.count = (i + 1);
                                                            cartcheckoutnoallotweb_update_cartmst_params.total = totalrows;
                                                            cartcheckoutnoallotweb_update_cartmst_params.ordertypeid = OrderTypeID;
                                                            cartcheckoutnoallotweb_update_cartmst_params.goldcnt = goldcnt;
                                                            cartcheckoutnoallotweb_update_cartmst_params.goldvalue = goldvalue;
                                                            cartcheckoutnoallotweb_update_cartmst_params.goldpremium = goldpremium;

                                                            int tmpReturnVal = UpdateCartMst(cartcheckoutnoallotweb_update_cartmst_params);
                                                            if (tmpReturnVal == 1)
                                                            {
                                                                error.Add($"Your product checkout successfully.! -{tmpCartID},");
                                                                errStatus.Add(true);
                                                            }
                                                            else
                                                            {
                                                                error.Add($"Something went wrong, Please try again.! -{tmpCartID},");
                                                                errStatus.Add(false);
                                                            }

                                                        }

                                                    }
                                                }
                                                // END - foreach ($cartidarray as $key => $cartid)
                                            }
                                        }

                                        CartCheckoutNoAllotWebSaveCartStatusMstParams cartcheckoutnoallotweb_save_cartstatusmst_params = new CartCheckoutNoAllotWebSaveCartStatusMstParams();
                                        cartcheckoutnoallotweb_save_cartstatusmst_params.DataID = DataID;
                                        cartcheckoutnoallotweb_save_cartstatusmst_params.CartID = CartID;
                                        cartcheckoutnoallotweb_save_cartstatusmst_params.Stage = "Cart Process End-14";
                                        cartcheckoutnoallotweb_save_cartstatusmst_params.SourceType = SourceType;
                                        SaveCartStatusMst(cartcheckoutnoallotweb_save_cartstatusmst_params);

                                        if (errStatus.Contains(false))
                                        {
                                            string msgdetail = string.Empty;
                                            for (int i = 0; i < errStatus.Count; i++)
                                            {
                                                if (!errStatus[i])
                                                {
                                                    msgdetail += error[i] + "; ";
                                                }
                                            }
                                            string jsonData = JsonConvert.SerializeObject(new { Message = msgdetail });

                                            cartcheckoutnoallotweb_save_cartstatusmst_params.DataID = DataID;
                                            cartcheckoutnoallotweb_save_cartstatusmst_params.CartID = CartID;
                                            cartcheckoutnoallotweb_save_cartstatusmst_params.Stage = "Error-12";
                                            cartcheckoutnoallotweb_save_cartstatusmst_params.Data = jsonData;
                                            cartcheckoutnoallotweb_save_cartstatusmst_params.SourceType = SourceType;
                                            SaveCartStatusMst(cartcheckoutnoallotweb_save_cartstatusmst_params);

                                            CartCheckoutNoAllotWebDeleteAndReassignCartParams cartcheckoutnoallotweb_deletandreassigncart_params = new CartCheckoutNoAllotWebDeleteAndReassignCartParams();
                                            cartcheckoutnoallotweb_deletandreassigncart_params.DataID = DataID;
                                            cartcheckoutnoallotweb_deletandreassigncart_params.CartID = CartID;
                                            cartcheckoutnoallotweb_deletandreassigncart_params.MsgDetail = jsonData;
                                            cartcheckoutnoallotweb_deletandreassigncart_params.SourceType = SourceType;
                                            DeleteAndReassignCart(cartcheckoutnoallotweb_deletandreassigncart_params);

                                            //return response()->json(['status'=>false, 'message'=>$msg]);
                                            // retVal = 1;
                                            objNoAllotResponse.status = 0;
                                            objNoAllotResponse.status_code = 201;
                                            objNoAllotResponse.message = jsonData;
                                        }
                                        else
                                        {
                                            if (itemall == "Y")
                                            {
                                                CartCheckoutNoAllotWebDeleteCartMstParams cartcheckoutnoallotweb_deletcartmst_params = new CartCheckoutNoAllotWebDeleteCartMstParams();
                                                cartcheckoutnoallotweb_deletcartmst_params.CartID = CartID;
                                                DeleteCartMst(cartcheckoutnoallotweb_deletcartmst_params);
                                            }
                                            //return response()->json(['status'=>true, 'message'=>'Your product checkout successfully.!']);
                                            // retVal = 1;
                                            objNoAllotResponse.status = 1;
                                            objNoAllotResponse.status_code = 201;
                                            objNoAllotResponse.message = "Your product checkout successfully.!";
                                        }

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
            catch (SqlException sqlEx)
            {
                // retVal = 0;
                objNoAllotResponse.status = 0;
                objNoAllotResponse.status_code = 201;
                objNoAllotResponse.message = $"SQL error: {sqlEx.Message}";
            }
            // return retVal;
            return objNoAllotResponse;
        }

        public async Task<ResponseDetails> CartCheckoutNoAllotWeb(CartCheckoutNoAllotWebParams cartcheckoutnoallotweb_params)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                string
                    resmessage = "";
                int
                    resstatus = 0,
                    resstatuscode = 400;

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int CartId = cartcheckoutnoallotweb_params.cart_id > 0 ? cartcheckoutnoallotweb_params.cart_id : 0;
                    int DataId = cartcheckoutnoallotweb_params.data_id > 0 ? cartcheckoutnoallotweb_params.data_id : 0;

                    int cart_order_data_id = cartcheckoutnoallotweb_params.cart_order_data_id > 0 ? cartcheckoutnoallotweb_params.cart_order_data_id : 0;
                    int cart_billing_data_id = cartcheckoutnoallotweb_params.cart_billing_data_id > 0 ? cartcheckoutnoallotweb_params.cart_billing_data_id : 0;
                    int cart_order_login_type_id = cartcheckoutnoallotweb_params.cart_order_login_type_id > 0 ? cartcheckoutnoallotweb_params.cart_order_login_type_id : 0;
                    int cart_billing_login_type_id = cartcheckoutnoallotweb_params.cart_billing_login_type_id > 0 ? cartcheckoutnoallotweb_params.cart_billing_login_type_id : 0;
                    string cart_remarks = string.IsNullOrWhiteSpace(cartcheckoutnoallotweb_params.cart_remarks) ? "" : cartcheckoutnoallotweb_params.cart_remarks;

                    string cart_delivery_date = "";
                    DateTime parsed_cart_delivery_date;
                    bool isValidCartDeliveryDate = DateTime.TryParseExact(cartcheckoutnoallotweb_params.cart_delivery_date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed_cart_delivery_date);
                    if (isValidCartDeliveryDate)
                    {
                        cart_delivery_date = parsed_cart_delivery_date.ToString("yyyy-MM-dd");
                    }

                    string itemall = string.IsNullOrWhiteSpace(cartcheckoutnoallotweb_params.itemall) ? "" : cartcheckoutnoallotweb_params.itemall;
                    // int ordertypeid = cartcheckoutnoallotweb_params.ordertypeid;
                    var ordertypeid = int.TryParse(cartcheckoutnoallotweb_params.ordertypeid.ToString(), out int parsedValue) ? parsedValue : 2498;

                    string SourceType = string.IsNullOrWhiteSpace(cartcheckoutnoallotweb_params.SourceType) ? "" : cartcheckoutnoallotweb_params.SourceType;

                    CommonHelpers objHelpers = new CommonHelpers();
                    BrandDataResponse datas = cartcheckoutnoallotweb_params.brandDataResponse;

                    #region kisna_data
                    List<string> kisnaFifty = new List<string>();
                    List<string> kisnaHours = new List<string>();
                    List<string> kisnaSeven = new List<string>();
                    List<string> kisnaTwentyone = new List<string>();
                    List<string> kisnaFive = new List<string>();

                    // Check if 'kisna_data' exists and is not empty
                    if (datas != null && datas.KisnaData != null)
                    {
                        var kisnaData = datas.KisnaData;
                        kisnaFifty = kisnaData.FifteenDay;
                        kisnaHours = kisnaData.Hours;
                        kisnaSeven = kisnaData.SevenDay;
                        kisnaTwentyone = kisnaData.TwentyOneDay;
                        kisnaFive = kisnaData.FiveDay;
                    }
                    #endregion

                    #region oro_data
                    List<string> oroFifteen = new List<string>();
                    List<string> oroHours = new List<string>();
                    List<string> oroSeven = new List<string>();
                    List<string> oroTwentyOne = new List<string>();
                    List<string> oroFive = new List<string>();

                    // Check if 'oro_data' exists and is not empty
                    int oro_data_cnt = 0;
                    if (datas != null && datas.OroData != null)
                    {
                        var oroData = datas.OroData;
                        oro_data_cnt = datas.OroData.FifteenDay.Count + datas.OroData.Hours.Count + datas.OroData.SevenDay.Count + datas.OroData.TwentyOneDay.Count + datas.OroData.FiveDay.Count;
                        oroFifteen = oroData.FifteenDay;
                        oroHours = oroData.Hours;
                        oroSeven = oroData.SevenDay;
                        oroTwentyOne = oroData.TwentyOneDay;
                        oroFive = oroData.FiveDay;
                    }
                    #endregion

                    #region kg_data
                    List<string> kgFifty = new List<string>();
                    List<string> kgHours = new List<string>();
                    List<string> kgSeven = new List<string>();
                    List<string> kgTwentyOne = new List<string>();
                    List<string> kgFive = new List<string>();

                    // Check if 'kisna_gold_data' exists and is not empty
                    if (datas != null && datas.KisnaGoldData != null)
                    {
                        var kgData = datas.KisnaGoldData;
                        kgFifty = kgData.FifteenDay;
                        kgHours = kgData.Hours;
                        kgSeven = kgData.SevenDay;
                        kgTwentyOne = kgData.TwentyOneDay;
                        kgFive = kgData.FiveDay;
                    }
                    #endregion

                    //Silver Coin 999 Data
                    #region silver_data
                    List<string> silverFifteen = new List<string>();
                    List<string> silverHours = new List<string>();
                    List<string> silverSeven = new List<string>();
                    List<string> silverTwentyOne = new List<string>();
                    List<string> silverFive = new List<string>();

                    // Check if 'silver_data' exists and is not empty
                    if (datas != null && datas.SilverData != null)
                    {
                        var silverData = datas.SilverData;
                        silverFifteen = silverData.FifteenDay;
                        silverHours = silverData.Hours;
                        silverSeven = silverData.SevenDay;
                        silverTwentyOne = silverData.TwentyOneDay;
                        silverFive = silverData.FiveDay;
                    }
                    #endregion

                    var cartMstItemIds = new List<string>();
                    cartMstItemIds.AddRange(kisnaFifty);
                    cartMstItemIds.AddRange(kisnaHours);
                    cartMstItemIds.AddRange(kisnaSeven);
                    cartMstItemIds.AddRange(kisnaTwentyone);
                    cartMstItemIds.AddRange(kisnaFive);
                    cartMstItemIds.AddRange(oroFifteen);
                    cartMstItemIds.AddRange(oroHours);
                    cartMstItemIds.AddRange(oroSeven);
                    cartMstItemIds.AddRange(oroTwentyOne);
                    cartMstItemIds.AddRange(oroFive);
                    cartMstItemIds.AddRange(kgFifty);
                    cartMstItemIds.AddRange(kgHours);
                    cartMstItemIds.AddRange(kgSeven);
                    cartMstItemIds.AddRange(kgTwentyOne);
                    cartMstItemIds.AddRange(kgFive);
                    cartMstItemIds.AddRange(silverFifteen);
                    cartMstItemIds.AddRange(silverHours);
                    cartMstItemIds.AddRange(silverSeven);
                    cartMstItemIds.AddRange(silverTwentyOne);
                    cartMstItemIds.AddRange(silverFive);

                    var filteredCartMstItemIds = cartMstItemIds.Where(id => !string.IsNullOrEmpty(id)).ToList();
                    string implodeCartMstItemIds = string.Join(",", filteredCartMstItemIds);

                    #region CheckItemIsSolitaireCombo
                    // Checks items are from solitaire collection or not,
                    // If so then checks total of those collection's items is grether than or equal to 10,00,000
                    IList<CheckItemIsSolitaireComboListing> checkitemissolitairecomboList = new List<CheckItemIsSolitaireComboListing>();
                    CheckItemIsSolitaireComboParams checkitemissolitairecomboparams = new CheckItemIsSolitaireComboParams();
                    checkitemissolitairecomboparams.cart_id = CartId;
                    checkitemissolitairecomboparams.cart_billing_data_id = cart_billing_data_id;
                    checkitemissolitairecomboparams.ordertypeid = ordertypeid;
                    checkitemissolitairecomboparams.implodeCartMstItemIds = implodeCartMstItemIds;
                    checkitemissolitairecomboList = objHelpers.CheckItemIsSolitaireCombo(checkitemissolitairecomboparams);

                    string msg = string.Empty;
                    CheckItemIsSolitaireComboListing isSolitaireCombo = new CheckItemIsSolitaireComboListing();
                    if (checkitemissolitairecomboList.Count > 0)
                    {
                        isSolitaireCombo = checkitemissolitairecomboList[0];
                    }

                    bool is_valid = isSolitaireCombo.is_valid == 0 ? false : true;
                    if (!is_valid)
                    {
                        if (isSolitaireCombo.min_amount == 0 && isSolitaireCombo.cart_price == 0)
                        {
                            resmessage = "Cart is empty, Please add items into cart and then checkout.";
                        }
                        else
                        {
                            resmessage = string.Format(
                                "Solitaire combo price ({0}) should be minimum {1}.",
                                isSolitaireCombo.cart_price.ToString("N0"),
                                isSolitaireCombo.min_amount.ToString("N0")
                            );
                        }
                        // return Ok(new { success = false, message = msg, status_code = 200 });
                        responseDetails.success = false;
                        responseDetails.message = resmessage;
                        responseDetails.status = "200";
                        return responseDetails;
                    }
                    #endregion

                    #region CheckItem_IsKisnaPremiumCollection
                    // Checks items are from kisna premium or not,
                    // If so then checks total of those collection's items is grether than or equal to 10,00,000
                    IList<CheckItemIsNewPremiumCollectionListing> checkitemisnewpremiumcollectionList = new List<CheckItemIsNewPremiumCollectionListing>();
                    CheckItemIsNewPremiumCollectionParams checkitemisnewpremiumcollectionparams = new CheckItemIsNewPremiumCollectionParams();
                    checkitemisnewpremiumcollectionparams.cart_id = CartId;
                    checkitemisnewpremiumcollectionparams.cart_billing_data_id = cart_billing_data_id;
                    checkitemisnewpremiumcollectionparams.ordertypeid = ordertypeid;
                    checkitemisnewpremiumcollectionparams.implodeCartMstItemIds = implodeCartMstItemIds;
                    checkitemisnewpremiumcollectionList = objHelpers.CheckItem_IsNewPremiumCollection(checkitemisnewpremiumcollectionparams);

                    CheckItemIsNewPremiumCollectionListing isKisnaPremium = new CheckItemIsNewPremiumCollectionListing();
                    if (checkitemisnewpremiumcollectionList.Count > 0)
                    {
                        isKisnaPremium = checkitemisnewpremiumcollectionList[0];
                    }

                    is_valid = isKisnaPremium.is_valid == 0 ? false : true;
                    if (!is_valid)
                    {
                        if (isKisnaPremium.min_amount == 0 && isKisnaPremium.cart_price == 0)
                        {
                            resmessage = "Cart is empty, Please add items into cart and then checkout.";
                        }
                        else
                        {
                            resmessage = string.Format(
                                "Kisna Premium price ({0}) should be minimum {1}.",
                                isKisnaPremium.cart_price.ToString("N0"),
                                isKisnaPremium.min_amount.ToString("N0")
                            );
                        }
                        //return Ok(new { success = false, message = msg, status_code = 200 });
                        responseDetails.success = false;
                        responseDetails.message = resmessage;
                        responseDetails.status = "200";
                        return responseDetails;
                    }
                    #endregion

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTWEB;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataId);
                        cmd.Parameters.AddWithValue("@CartID", CartId);
                        cmd.Parameters.AddWithValue("@OrderTypeID", ordertypeid);
                        cmd.Parameters.AddWithValue("@cart_order_login_type_id", cart_order_login_type_id);
                        cmd.Parameters.AddWithValue("@cart_billing_login_type_id", cart_billing_login_type_id);
                        cmd.Parameters.AddWithValue("@cart_order_data_id", cart_order_data_id);
                        cmd.Parameters.AddWithValue("@cart_billing_data_id", cart_billing_data_id);
                        cmd.Parameters.AddWithValue("@cart_remarks", cart_remarks);
                        cmd.Parameters.AddWithValue("@cart_delivery_date", cart_delivery_date);
                        cmd.Parameters.AddWithValue("@oro_data_cnt", oro_data_cnt);
                        cmd.Parameters.AddWithValue("@SourceType", "APP");

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
                                        int returntype = firstRow["returntype"] as int? ?? 0;

                                        if (resstatus == 1 && ds.Tables.Count > 1)
                                        {
                                            List<string> totalstoke = new List<string>();
                                            List<int> available = new List<int>();
                                            List<int> notAvailable = new List<int>();
                                            List<string> notArray = new List<string>();
                                            string CartSoliStkNoData;
                                            int tmp_ItemId = 0, tmp_CartItemId = 0;

                                            if (returntype == 1)
                                            {
                                                /* @OrderTypeID NOT IN (6596, 2499) */
                                            }
                                            else
                                            {
                                                /* @OrderTypeID IN (6596, 2499) */
                                                DataTable dt = new DataTable();
                                                dt = ds.Tables[1].AsEnumerable()
                                                    .Where(x => new[] { 4, 21, 22, 25 }.Contains(x.Field<int>("ItemCtgCommonID")))
                                                    .CopyToDataTable();

                                                for (int i = 0; i < dt.Rows.Count; i++)
                                                {
                                                    CartSoliStkNoData = dt.Rows[i]["CartSoliStkNoData"] as string ?? string.Empty;
                                                    List<string> soliterStokeList = new List<string>(CartSoliStkNoData.Split(','));
                                                    string wsdlUrl = "http://service.hkerp.co/HKE.WCFService.svc?singleWsdl";

                                                    foreach (var soliterCheck in soliterStokeList)
                                                    {
                                                        totalstoke.Add(soliterCheck);

                                                        var soapRequest = $@"
                                                    <s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/"">
                                                        <s:Body>
                                                            <CheckStoneAvailability xmlns=""http://tempuri.org/"">
                                                                <CustomerID>{_customerID}</CustomerID>
                                                                <PacketNo>{soliterCheck}</PacketNo>
                                                            </CheckStoneAvailability>
                                                        </s:Body>
                                                    </s:Envelope>";

                                                        // string responseXml = await SendSoapRequest(wsdlUrl, soapRequest);
                                                        string responseXml = "";
                                                        var soapResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SoapResponse>(responseXml);
                                                        if (soapResponse?.Data?[0]?.Status == "Available")
                                                        {
                                                            available.Add(1);
                                                        }
                                                        else
                                                        {
                                                            notArray.Add(soliterCheck);
                                                            notAvailable.Add(1);
                                                        }
                                                    }
                                                }

                                                if (notAvailable.Count > 0 && notAvailable.Any())
                                                {
                                                    string tags = string.Join(", ", notAvailable);
                                                    responseDetails.success = false;
                                                    responseDetails.message = $"These diamond stocks are not available: {tags}";
                                                    responseDetails.status = "200";
                                                    return responseDetails;
                                                }
                                                else
                                                {
                                                    for (int i = 0; i < dt.Rows.Count; i++)
                                                    {
                                                        CartSoliStkNoData = dt.Rows[i]["CartSoliStkNoData"] as string ?? string.Empty;
                                                        tmp_CartItemId = dt.Rows[i]["CartItemID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["CartItemID"]) : 0;
                                                        tmp_ItemId = dt.Rows[i]["ItemID"] != DBNull.Value ? Convert.ToInt32(dt.Rows[i]["ItemID"]) : 0;

                                                        string apiUrl = $"https://service.hk.co/StockApprove?user={_customerID}&pkts={CartSoliStkNoData}&remark={tmp_CartItemId}";

                                                        // Send HTTP Request
                                                        //string response = await SendHttpRequest(apiUrl);
                                                        string response = "";

                                                        CartCheckoutNoAllotNewParamsPart2 cartcheckoutnoallotnewpart2_params = new CartCheckoutNoAllotNewParamsPart2();
                                                        cartcheckoutnoallotnewpart2_params.cart_id = CartId;
                                                        cartcheckoutnoallotnewpart2_params.data_id = DataId;
                                                        cartcheckoutnoallotnewpart2_params.CartSoliStkNoData = CartSoliStkNoData;
                                                        cartcheckoutnoallotnewpart2_params.DiaBookRespose = response;
                                                        cartcheckoutnoallotnewpart2_params.item_id = tmp_ItemId;
                                                        cartcheckoutnoallotnewpart2_params.cart_item_id = tmp_CartItemId;
                                                        SaveDiamondBookServiceResponse(cartcheckoutnoallotnewpart2_params);
                                                    }
                                                }
                                            }

                                            int
                                                tmpItemID = 0,
                                                tmpCartItemID = 0,
                                                tmpCartMstID = 0,
                                                tmpCartQty = 0,
                                                tmpItemCtgCommonID = 0,
                                                tmpCartConfCommonID = 0,
                                                tmpItemBrandCommonID = 0,
                                                tmpItemGenderCommonID = 0,
                                                DataLoginTypeID_CONST = 0,
                                                LoginId = 0;
                                            decimal
                                                tmpItemGrossWt = 0,
                                                tmpItemMetalWt = 0,
                                                tmpCartMRPrice = 0,
                                                tmpCartPrice = 0,
                                                tmpCartDPrice = 0,
                                                tmpCartItemMetalWt = 0;
                                            string
                                                tmpItemPlainGold = "",
                                                tmpMstType = "",
                                                tmpDataItemInfo = "",
                                                tmpItemIsSRP = "",
                                                tmpItemIsMRP = "";

                                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                            {
                                                var rowDetails = ds.Tables[1].Rows[j];
                                                tmpItemID = rowDetails["ItemID"] != DBNull.Value ? Convert.ToInt32(rowDetails["ItemID"]) : 0;
                                                tmpCartItemID = rowDetails["CartItemID"] != DBNull.Value ? Convert.ToInt32(rowDetails["CartItemID"]) : 0;
                                                tmpCartMstID = rowDetails["CartMstID"] != DBNull.Value ? Convert.ToInt32(rowDetails["CartMstID"]) : 0;
                                                tmpCartQty = rowDetails["CartQty"] != DBNull.Value ? Convert.ToInt32(rowDetails["CartQty"]) : 0;
                                                tmpItemCtgCommonID = rowDetails["ItemCtgCommonID"] != DBNull.Value ? Convert.ToInt32(rowDetails["ItemCtgCommonID"]) : 0;
                                                tmpCartConfCommonID = rowDetails["CartConfCommonID"] != DBNull.Value ? Convert.ToInt32(rowDetails["CartConfCommonID"]) : 0;
                                                tmpItemBrandCommonID = rowDetails["ItemBrandCommonID"] != DBNull.Value ? Convert.ToInt32(rowDetails["ItemBrandCommonID"]) : 0;
                                                tmpItemGenderCommonID = rowDetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(rowDetails["ItemGenderCommonID"]) : 0;
                                                DataLoginTypeID_CONST = rowDetails["DataLoginTypeID_CONST"] != DBNull.Value ? Convert.ToInt32(rowDetails["DataLoginTypeID_CONST"]) : 0;
                                                LoginId = rowDetails["LoginId"] != DBNull.Value ? Convert.ToInt32(rowDetails["LoginId"]) : 0;

                                                tmpItemGrossWt = rowDetails["ItemGrossWt"] != DBNull.Value ? Convert.ToDecimal(rowDetails["ItemGrossWt"]) : 0;
                                                tmpItemMetalWt = rowDetails["ItemMetalWt"] != DBNull.Value ? Convert.ToDecimal(rowDetails["ItemMetalWt"]) : 0;

                                                tmpCartMRPrice = rowDetails["CartMRPrice"] != DBNull.Value ? Convert.ToDecimal(rowDetails["CartMRPrice"]) : 0;
                                                tmpCartPrice = rowDetails["CartPrice"] != DBNull.Value ? Convert.ToDecimal(rowDetails["CartPrice"]) : 0;
                                                tmpCartDPrice = rowDetails["CartDPrice"] != DBNull.Value ? Convert.ToDecimal(rowDetails["CartDPrice"]) : 0;
                                                tmpCartItemMetalWt = rowDetails["CartItemMetalWt"] != DBNull.Value ? Convert.ToDecimal(rowDetails["CartItemMetalWt"]) : 0;

                                                tmpItemPlainGold = rowDetails["ItemPlainGold"] as string ?? string.Empty;
                                                tmpMstType = rowDetails["MstType"] as string ?? string.Empty;
                                                tmpDataItemInfo = rowDetails["DataItemInfo"] as string ?? string.Empty;
                                                tmpItemIsSRP = rowDetails["ItemIsSRP"] as string ?? string.Empty;

                                                IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();
                                                IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold_requiredweight = new List<CartItemPriceDetailListing>();
                                                CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                                CARTCHECKOUTNOALLOTNEW_UPDATE_CARTMSTITEM_PRICESParams cartcheckouotallotnew_update_cartmstitem_prices_params = new CARTCHECKOUTNOALLOTNEW_UPDATE_CARTMSTITEM_PRICESParams();
                                                ItemDynamicPriceCartParams itemdynamicpricecart_params = new ItemDynamicPriceCartParams();

                                                string
                                                    design_kt = "",
                                                    making_per_gram = "",
                                                    labour = "";
                                                decimal
                                                    gold_price = 0,
                                                    total_goldvalue = 0,
                                                    finalGoldValue = 0,
                                                    total_labour = 0,
                                                    gst_price = 0,
                                                    item_price = 0,
                                                    total_price = 0,
                                                    dp_final_price = 0,
                                                    dp_maring_percent = 0,
                                                    gold_wt = 0,
                                                    diamond_price = 0,
                                                    platinum_wt = 0,
                                                    platinum = 0,
                                                    platinum_price = 0;

                                                // order typeid not in (6596, 2499)
                                                if (returntype == 1)
                                                {
                                                    if (@cart_billing_login_type_id == DataLoginTypeID_CONST && tmpItemPlainGold == "Y")
                                                    {
                                                        cartitempricedetaillistparams.DataID = cart_billing_data_id;
                                                        cartitempricedetaillistparams.ItemID = tmpItemID;
                                                        cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                                        cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                                        if (cartItemPriceDetailList_gold.Count > 0)
                                                        {
                                                            gold_price = cartItemPriceDetailList_gold[0].pure_gold;
                                                            making_per_gram = cartItemPriceDetailList_gold[0].labour;
                                                            total_goldvalue = cartItemPriceDetailList_gold[0].gold_ktprice;
                                                            total_labour = cartItemPriceDetailList_gold[0].labour_price;
                                                            finalGoldValue = cartItemPriceDetailList_gold[0].gold_price;
                                                            item_price = cartItemPriceDetailList_gold[0].item_price;
                                                            total_price = cartItemPriceDetailList_gold[0].total_price;
                                                            design_kt = cartItemPriceDetailList_gold[0].design_kt;
                                                            labour = cartItemPriceDetailList_gold[0].labour;
                                                            gst_price = cartItemPriceDetailList_gold[0].GST;
                                                            dp_final_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                                            gold_wt = cartItemPriceDetailList_gold[0].gold_wt;
                                                            diamond_price = cartItemPriceDetailList_gold[0].diamond_price;
                                                            platinum_wt = cartItemPriceDetailList_gold[0].platinum_wt;
                                                            platinum = cartItemPriceDetailList_gold[0].platinum;
                                                            platinum_price = cartItemPriceDetailList_gold[0].platinum_price;
                                                        }

                                                        GoldDynamicPriceCartParams golddynamicpricecartparams = new GoldDynamicPriceCartParams();
                                                        golddynamicpricecartparams.DataId = cart_billing_data_id;
                                                        golddynamicpricecartparams.ItemId = tmpItemID;
                                                        golddynamicpricecartparams.CartItemId = tmpCartItemID;
                                                        golddynamicpricecartparams.CartMstId = tmpCartMstID;
                                                        golddynamicpricecartparams.CartQty = tmpCartQty;
                                                        golddynamicpricecartparams.gold_price = gold_price;
                                                        golddynamicpricecartparams.making_per_gram = making_per_gram;
                                                        golddynamicpricecartparams.total_goldvalue = total_goldvalue;
                                                        golddynamicpricecartparams.total_labour = total_labour;
                                                        golddynamicpricecartparams.finalGoldValue = finalGoldValue;
                                                        golddynamicpricecartparams.item_price = item_price;
                                                        golddynamicpricecartparams.total_price = total_price;
                                                        golddynamicpricecartparams.gst_price = gst_price;
                                                        golddynamicpricecartparams.dp_final_price = dp_final_price;
                                                        golddynamicpricecartparams.design_kt = design_kt;
                                                        golddynamicpricecartparams.labour = labour;
                                                        GoldDynamicPriceCart(golddynamicpricecartparams);

                                                        if (tmpItemIsMRP == "Y")
                                                        {
                                                            itemdynamicpricecart_params.DataID = cart_billing_data_id;
                                                            itemdynamicpricecart_params.ItemID = tmpItemID;
                                                            itemdynamicpricecart_params.CartID = CartId;
                                                            itemdynamicpricecart_params.CartItemID = tmpCartItemID;
                                                            itemdynamicpricecart_params.diamond_price = diamond_price;
                                                            itemdynamicpricecart_params.gold_wt = gold_wt;
                                                            itemdynamicpricecart_params.pure_gold = gold_price;
                                                            itemdynamicpricecart_params.gold_ktprice = total_goldvalue;
                                                            itemdynamicpricecart_params.gold_price = finalGoldValue;
                                                            itemdynamicpricecart_params.platinum_wt = platinum_wt;
                                                            itemdynamicpricecart_params.platinum = platinum;
                                                            itemdynamicpricecart_params.platinum_price = platinum_price;
                                                            itemdynamicpricecart_params.labour_price = total_labour;
                                                            objHelpers.ItemDynamicPriceCart(itemdynamicpricecart_params);

                                                            cartitempricedetaillistparams.DataID = cart_billing_data_id;
                                                            cartitempricedetaillistparams.ItemID = tmpItemID;
                                                            cartitempricedetaillistparams.SizeID = tmpCartConfCommonID;
                                                            cartitempricedetaillistparams.CategoryID = tmpItemCtgCommonID;
                                                            cartitempricedetaillistparams.ItemBrandCommonID = tmpItemBrandCommonID;
                                                            cartitempricedetaillistparams.ItemGrossWt = tmpItemGrossWt;
                                                            cartitempricedetaillistparams.ItemMetalWt = tmpItemMetalWt;
                                                            cartitempricedetaillistparams.ItemGenderCommonID = tmpItemGenderCommonID;
                                                            cartitempricedetaillistparams.IsWeightCalcRequired = 1;

                                                            cartItemPriceDetailList_gold_requiredweight = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                                            if (cartItemPriceDetailList_gold_requiredweight.Count > 0)
                                                            {
                                                                total_price = Math.Round(cartItemPriceDetailList_gold_requiredweight[0].total_price, 0);
                                                                dp_final_price = Math.Round(cartItemPriceDetailList_gold_requiredweight[0].dp_final_price, 0);
                                                                dp_maring_percent = cartItemPriceDetailList_gold_requiredweight[0].dp_maring_percent;

                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemID = tmpCartItemID;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartMRPrice = total_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartPrice = total_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartDPrice = dp_final_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FranMarginCurDP = dp_maring_percent;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemMetalWt = gold_wt;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 5;
                                                                SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        if (LoginId == DataLoginTypeID_CONST)
                                                        {
                                                            if (tmpItemPlainGold == "Y")
                                                            {
                                                                cartitempricedetaillistparams.DataID = cart_billing_data_id;
                                                                cartitempricedetaillistparams.ItemID = tmpItemID;
                                                                cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                                                cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                                                if (cartItemPriceDetailList_gold.Count > 0)
                                                                {
                                                                    gold_price = cartItemPriceDetailList_gold[0].pure_gold;
                                                                    making_per_gram = cartItemPriceDetailList_gold[0].labour;
                                                                    total_goldvalue = cartItemPriceDetailList_gold[0].gold_ktprice;
                                                                    total_labour = cartItemPriceDetailList_gold[0].labour_price;
                                                                    finalGoldValue = cartItemPriceDetailList_gold[0].gold_price;
                                                                    item_price = cartItemPriceDetailList_gold[0].item_price;
                                                                    total_price = cartItemPriceDetailList_gold[0].total_price;
                                                                    design_kt = cartItemPriceDetailList_gold[0].design_kt;
                                                                    labour = cartItemPriceDetailList_gold[0].labour;
                                                                    gst_price = cartItemPriceDetailList_gold[0].GST;
                                                                    dp_final_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                                                    gold_wt = cartItemPriceDetailList_gold[0].gold_wt;
                                                                    diamond_price = cartItemPriceDetailList_gold[0].diamond_price;
                                                                    platinum_wt = cartItemPriceDetailList_gold[0].platinum_wt;
                                                                    platinum = cartItemPriceDetailList_gold[0].platinum;
                                                                    platinum_price = cartItemPriceDetailList_gold[0].platinum_price;
                                                                }

                                                                GoldDynamicPriceCartParams golddynamicpricecartparams = new GoldDynamicPriceCartParams();
                                                                golddynamicpricecartparams.DataId = cart_billing_data_id;
                                                                golddynamicpricecartparams.ItemId = tmpItemID;
                                                                golddynamicpricecartparams.CartItemId = tmpCartItemID;
                                                                golddynamicpricecartparams.CartMstId = tmpCartMstID;
                                                                golddynamicpricecartparams.CartQty = tmpCartQty;
                                                                golddynamicpricecartparams.gold_price = gold_price;
                                                                golddynamicpricecartparams.making_per_gram = making_per_gram;
                                                                golddynamicpricecartparams.total_goldvalue = total_goldvalue;
                                                                golddynamicpricecartparams.total_labour = total_labour;
                                                                golddynamicpricecartparams.finalGoldValue = finalGoldValue;
                                                                golddynamicpricecartparams.item_price = item_price;
                                                                golddynamicpricecartparams.total_price = total_price;
                                                                golddynamicpricecartparams.gst_price = gst_price;
                                                                golddynamicpricecartparams.dp_final_price = dp_final_price;
                                                                golddynamicpricecartparams.design_kt = design_kt;
                                                                golddynamicpricecartparams.labour = labour;
                                                                GoldDynamicPriceCart(golddynamicpricecartparams);

                                                                if (tmpItemIsMRP == "Y")
                                                                {
                                                                    itemdynamicpricecart_params.DataID = cart_billing_data_id;
                                                                    itemdynamicpricecart_params.ItemID = tmpItemID;
                                                                    itemdynamicpricecart_params.CartID = CartId;
                                                                    itemdynamicpricecart_params.CartItemID = tmpCartItemID;
                                                                    itemdynamicpricecart_params.diamond_price = diamond_price;
                                                                    itemdynamicpricecart_params.gold_wt = gold_wt;
                                                                    itemdynamicpricecart_params.pure_gold = gold_price;
                                                                    itemdynamicpricecart_params.gold_ktprice = total_goldvalue;
                                                                    itemdynamicpricecart_params.gold_price = finalGoldValue;
                                                                    itemdynamicpricecart_params.platinum_wt = platinum_wt;
                                                                    itemdynamicpricecart_params.platinum = platinum;
                                                                    itemdynamicpricecart_params.platinum_price = platinum_price;
                                                                    itemdynamicpricecart_params.labour_price = total_labour;
                                                                    objHelpers.ItemDynamicPriceCart(itemdynamicpricecart_params);


                                                                    if (cartItemPriceDetailList_gold.Count > 0)
                                                                    {
                                                                        total_price = Math.Round(cartItemPriceDetailList_gold[0].total_price, 0);
                                                                        dp_final_price = Math.Round(cartItemPriceDetailList_gold[0].dp_final_price, 0);
                                                                        dp_maring_percent = cartItemPriceDetailList_gold[0].dp_maring_percent;

                                                                        cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemID = tmpCartItemID;
                                                                        cartcheckouotallotnew_update_cartmstitem_prices_params.CartMRPrice = total_price;
                                                                        cartcheckouotallotnew_update_cartmstitem_prices_params.CartPrice = total_price;
                                                                        cartcheckouotallotnew_update_cartmstitem_prices_params.CartDPrice = dp_final_price;
                                                                        cartcheckouotallotnew_update_cartmstitem_prices_params.FranMarginCurDP = dp_maring_percent;
                                                                        cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 1;
                                                                        SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // order typeid in (6596, 2499)
                                                    if (@cart_billing_login_type_id == DataLoginTypeID_CONST && tmpItemPlainGold == "Y")
                                                    {
                                                        cartitempricedetaillistparams.DataID = cart_billing_data_id;
                                                        cartitempricedetaillistparams.ItemID = tmpItemID;
                                                        cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                                        cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                                        if (cartItemPriceDetailList_gold.Count > 0)
                                                        {
                                                            gold_price = cartItemPriceDetailList_gold[0].pure_gold;
                                                            making_per_gram = cartItemPriceDetailList_gold[0].labour;
                                                            total_goldvalue = cartItemPriceDetailList_gold[0].gold_ktprice;
                                                            total_labour = cartItemPriceDetailList_gold[0].labour_price;
                                                            finalGoldValue = cartItemPriceDetailList_gold[0].gold_price;
                                                            item_price = cartItemPriceDetailList_gold[0].item_price;
                                                            total_price = cartItemPriceDetailList_gold[0].total_price;
                                                            design_kt = cartItemPriceDetailList_gold[0].design_kt;
                                                            labour = cartItemPriceDetailList_gold[0].labour;
                                                            gst_price = cartItemPriceDetailList_gold[0].GST;
                                                            dp_final_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                                            gold_wt = cartItemPriceDetailList_gold[0].gold_wt;
                                                            diamond_price = cartItemPriceDetailList_gold[0].diamond_price;
                                                            platinum_wt = cartItemPriceDetailList_gold[0].platinum_wt;
                                                            platinum = cartItemPriceDetailList_gold[0].platinum;
                                                            platinum_price = cartItemPriceDetailList_gold[0].platinum_price;
                                                        }

                                                        GoldDynamicPriceCartParams golddynamicpricecartparams = new GoldDynamicPriceCartParams();
                                                        golddynamicpricecartparams.DataId = cart_billing_data_id;
                                                        golddynamicpricecartparams.ItemId = tmpItemID;
                                                        golddynamicpricecartparams.CartItemId = tmpCartItemID;
                                                        golddynamicpricecartparams.CartMstId = tmpCartMstID;
                                                        golddynamicpricecartparams.CartQty = tmpCartQty;
                                                        golddynamicpricecartparams.gold_price = gold_price;
                                                        golddynamicpricecartparams.making_per_gram = making_per_gram;
                                                        golddynamicpricecartparams.total_goldvalue = total_goldvalue;
                                                        golddynamicpricecartparams.total_labour = total_labour;
                                                        golddynamicpricecartparams.finalGoldValue = finalGoldValue;
                                                        golddynamicpricecartparams.item_price = item_price;
                                                        golddynamicpricecartparams.total_price = total_price;
                                                        golddynamicpricecartparams.gst_price = gst_price;
                                                        golddynamicpricecartparams.dp_final_price = dp_final_price;
                                                        golddynamicpricecartparams.design_kt = design_kt;
                                                        golddynamicpricecartparams.labour = labour;
                                                        GoldDynamicPriceCart(golddynamicpricecartparams);

                                                        if (tmpItemIsMRP == "Y")
                                                        {
                                                            itemdynamicpricecart_params.DataID = cart_billing_data_id;
                                                            itemdynamicpricecart_params.ItemID = tmpItemID;
                                                            itemdynamicpricecart_params.CartID = CartId;
                                                            itemdynamicpricecart_params.CartItemID = tmpCartItemID;
                                                            itemdynamicpricecart_params.diamond_price = diamond_price;
                                                            itemdynamicpricecart_params.gold_wt = gold_wt;
                                                            itemdynamicpricecart_params.pure_gold = gold_price;
                                                            itemdynamicpricecart_params.gold_ktprice = total_goldvalue;
                                                            itemdynamicpricecart_params.gold_price = finalGoldValue;
                                                            itemdynamicpricecart_params.platinum_wt = platinum_wt;
                                                            itemdynamicpricecart_params.platinum = platinum;
                                                            itemdynamicpricecart_params.platinum_price = platinum_price;
                                                            itemdynamicpricecart_params.labour_price = total_labour;
                                                            objHelpers.ItemDynamicPriceCart(itemdynamicpricecart_params);


                                                            cartitempricedetaillistparams.DataID = cart_billing_data_id;
                                                            cartitempricedetaillistparams.ItemID = tmpItemID;
                                                            cartitempricedetaillistparams.SizeID = tmpCartConfCommonID;
                                                            cartitempricedetaillistparams.CategoryID = tmpItemCtgCommonID;
                                                            cartitempricedetaillistparams.ItemBrandCommonID = tmpItemBrandCommonID;
                                                            cartitempricedetaillistparams.ItemGrossWt = tmpItemGrossWt;
                                                            cartitempricedetaillistparams.ItemMetalWt = tmpItemMetalWt;
                                                            cartitempricedetaillistparams.ItemGenderCommonID = tmpItemGenderCommonID;
                                                            cartitempricedetaillistparams.IsWeightCalcRequired = 1;

                                                            cartItemPriceDetailList_gold_requiredweight = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                                            if (cartItemPriceDetailList_gold_requiredweight.Count > 0)
                                                            {
                                                                total_price = Math.Round(cartItemPriceDetailList_gold_requiredweight[0].total_price, 0);
                                                                dp_final_price = Math.Round(cartItemPriceDetailList_gold_requiredweight[0].dp_final_price, 0);
                                                                dp_maring_percent = cartItemPriceDetailList_gold_requiredweight[0].dp_maring_percent;

                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemID = tmpCartItemID;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartMRPrice = total_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartPrice = total_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartDPrice = dp_final_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FranMarginCurDP = dp_maring_percent;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemMetalWt = gold_wt;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 5;
                                                                SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        cartitempricedetaillistparams.DataID = cart_billing_data_id;
                                                        cartitempricedetaillistparams.ItemID = tmpItemID;
                                                        cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                                        cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                                        if (cartItemPriceDetailList_gold.Count > 0)
                                                        {
                                                            gold_price = cartItemPriceDetailList_gold[0].pure_gold;
                                                            making_per_gram = cartItemPriceDetailList_gold[0].labour;
                                                            total_goldvalue = cartItemPriceDetailList_gold[0].gold_ktprice;
                                                            total_labour = cartItemPriceDetailList_gold[0].labour_price;
                                                            finalGoldValue = cartItemPriceDetailList_gold[0].gold_price;
                                                            item_price = cartItemPriceDetailList_gold[0].item_price;
                                                            total_price = cartItemPriceDetailList_gold[0].total_price;
                                                            design_kt = cartItemPriceDetailList_gold[0].design_kt;
                                                            labour = cartItemPriceDetailList_gold[0].labour;
                                                            gst_price = cartItemPriceDetailList_gold[0].GST;
                                                            dp_final_price = cartItemPriceDetailList_gold[0].dp_final_price;
                                                            gold_wt = cartItemPriceDetailList_gold[0].gold_wt;
                                                            diamond_price = cartItemPriceDetailList_gold[0].diamond_price;
                                                            platinum_wt = cartItemPriceDetailList_gold[0].platinum_wt;
                                                            platinum = cartItemPriceDetailList_gold[0].platinum;
                                                            platinum_price = cartItemPriceDetailList_gold[0].platinum_price;
                                                        }

                                                        if (tmpItemPlainGold == "Y")
                                                        {
                                                            GoldDynamicPriceCartParams golddynamicpricecartparams = new GoldDynamicPriceCartParams();
                                                            golddynamicpricecartparams.DataId = cart_billing_data_id;
                                                            golddynamicpricecartparams.ItemId = tmpItemID;
                                                            golddynamicpricecartparams.CartItemId = tmpCartItemID;
                                                            golddynamicpricecartparams.CartMstId = tmpCartMstID;
                                                            golddynamicpricecartparams.CartQty = tmpCartQty;
                                                            golddynamicpricecartparams.gold_price = gold_price;
                                                            golddynamicpricecartparams.making_per_gram = making_per_gram;
                                                            golddynamicpricecartparams.total_goldvalue = total_goldvalue;
                                                            golddynamicpricecartparams.total_labour = total_labour;
                                                            golddynamicpricecartparams.finalGoldValue = finalGoldValue;
                                                            golddynamicpricecartparams.item_price = item_price;
                                                            golddynamicpricecartparams.total_price = total_price;
                                                            golddynamicpricecartparams.gst_price = gst_price;
                                                            golddynamicpricecartparams.dp_final_price = dp_final_price;
                                                            golddynamicpricecartparams.design_kt = design_kt;
                                                            golddynamicpricecartparams.labour = labour;
                                                            GoldDynamicPriceCart(golddynamicpricecartparams);
                                                        }

                                                        if (tmpItemIsMRP == "Y")
                                                        {
                                                            itemdynamicpricecart_params.DataID = cart_billing_data_id;
                                                            itemdynamicpricecart_params.ItemID = tmpItemID;
                                                            itemdynamicpricecart_params.CartID = CartId;
                                                            itemdynamicpricecart_params.CartItemID = tmpCartItemID;
                                                            itemdynamicpricecart_params.diamond_price = diamond_price;
                                                            itemdynamicpricecart_params.gold_wt = gold_wt;
                                                            itemdynamicpricecart_params.pure_gold = gold_price;
                                                            itemdynamicpricecart_params.gold_ktprice = total_goldvalue;
                                                            itemdynamicpricecart_params.gold_price = finalGoldValue;
                                                            itemdynamicpricecart_params.platinum_wt = platinum_wt;
                                                            itemdynamicpricecart_params.platinum = platinum;
                                                            itemdynamicpricecart_params.platinum_price = platinum_price;
                                                            itemdynamicpricecart_params.labour_price = total_labour;
                                                            objHelpers.ItemDynamicPriceCart(itemdynamicpricecart_params);


                                                            if (cartItemPriceDetailList_gold.Count > 0)
                                                            {
                                                                total_price = Math.Round(cartItemPriceDetailList_gold[0].total_price, 0);
                                                                dp_final_price = Math.Round(cartItemPriceDetailList_gold[0].dp_final_price, 0);
                                                                dp_maring_percent = cartItemPriceDetailList_gold[0].dp_maring_percent;
                                                                gold_wt = cartItemPriceDetailList_gold[0].gold_wt;

                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemID = tmpCartItemID;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartMRPrice = total_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartPrice = total_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartDPrice = dp_final_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FranMarginCurDP = dp_maring_percent;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemMetalWt = gold_wt;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 5;
                                                                SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                            }
                                                        }
                                                    }

                                                }
                                            } // FOR ITEM FOR LOOP

                                            if (returntype == 1)
                                            {
                                                CartCheckoutNoAllotWebSaveNewCartMstParams cartcheckoutnoallotweb_savenewcartmstparams = new CartCheckoutNoAllotWebSaveNewCartMstParams();
                                                cartcheckoutnoallotweb_savenewcartmstparams.DataID = DataId;
                                                cartcheckoutnoallotweb_savenewcartmstparams.CartID = CartId;
                                                cartcheckoutnoallotweb_savenewcartmstparams.OrderTypeID = ordertypeid;
                                                cartcheckoutnoallotweb_savenewcartmstparams.cart_order_login_type_id = cart_order_login_type_id;
                                                cartcheckoutnoallotweb_savenewcartmstparams.cart_billing_login_type_id = cart_billing_login_type_id;
                                                cartcheckoutnoallotweb_savenewcartmstparams.cart_order_data_id = cart_order_data_id;
                                                cartcheckoutnoallotweb_savenewcartmstparams.cart_billing_data_id = cart_billing_data_id;
                                                cartcheckoutnoallotweb_savenewcartmstparams.cart_remarks = cart_remarks;
                                                cartcheckoutnoallotweb_savenewcartmstparams.cart_delivery_date = cart_delivery_date;
                                                cartcheckoutnoallotweb_savenewcartmstparams.itemall = itemall;
                                                cartcheckoutnoallotweb_savenewcartmstparams.devicetype = "WEB";
                                                cartcheckoutnoallotweb_savenewcartmstparams.devicename = "";
                                                cartcheckoutnoallotweb_savenewcartmstparams.appversion = "";
                                                cartcheckoutnoallotweb_savenewcartmstparams.SourceType = "BULKWEB";

                                                cartcheckoutnoallotweb_savenewcartmstparams.oroseven_cnt = oroSeven.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.orosevenString = string.Join(",", oroSeven);
                                                cartcheckoutnoallotweb_savenewcartmstparams.orofifty_cnt = oroFifteen.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.orofiftyString = string.Join(",", oroFive);
                                                cartcheckoutnoallotweb_savenewcartmstparams.orohours_cnt = oroHours.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.orohoursString = string.Join(",", oroHours);
                                                cartcheckoutnoallotweb_savenewcartmstparams.orotwentyone_cnt = oroTwentyOne.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.orotwentyoneString = string.Join(",", oroTwentyOne);
                                                cartcheckoutnoallotweb_savenewcartmstparams.orofive_cnt = oroFive.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.orofiveString = string.Join(",", oroFive);

                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnaseven_cnt = kisnaSeven.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnasevenString = string.Join(",", kisnaSeven);
                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnafifty_cnt = kisnaFifty.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnafiftyString = string.Join(",", kisnaFifty);
                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnahours_cnt = kisnaHours.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnahoursString = string.Join(",", kisnaHours);
                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnatwentyone_cnt = kisnaTwentyone.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnatwentyoneString = string.Join(",", kisnaTwentyone);
                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnafive_cnt = kisnaFive.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kisnafiveString = string.Join(",", kisnaFive);

                                                cartcheckoutnoallotweb_savenewcartmstparams.kgseven_cnt = kgSeven.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kgsevenString = string.Join(",", kgSeven);
                                                cartcheckoutnoallotweb_savenewcartmstparams.kgfifty_cnt = kgFifty.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kgfiftyString = string.Join(",", kgFifty);
                                                cartcheckoutnoallotweb_savenewcartmstparams.kghours_cnt = kgHours.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kghoursString = string.Join(",", kgHours);
                                                cartcheckoutnoallotweb_savenewcartmstparams.kgtwentyone_cnt = kgTwentyOne.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kgtwentyoneString = string.Join(",", kgTwentyOne);
                                                cartcheckoutnoallotweb_savenewcartmstparams.kgfive_cnt = kgFive.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.kgfiveString = string.Join(",", kgFive);

                                                cartcheckoutnoallotweb_savenewcartmstparams.silverseven_cnt = silverSeven.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.silversevenString = string.Join(",", silverSeven);
                                                cartcheckoutnoallotweb_savenewcartmstparams.silverfifty_cnt = silverFifteen.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.silverfiftyString = string.Join(",", silverFifteen);
                                                cartcheckoutnoallotweb_savenewcartmstparams.silverhours_cnt = silverHours.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.silverhoursString = string.Join(",", silverHours);
                                                cartcheckoutnoallotweb_savenewcartmstparams.silvertwentyone_cnt = silverTwentyOne.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.silvertwentyoneString = string.Join(",", silverTwentyOne);
                                                cartcheckoutnoallotweb_savenewcartmstparams.silverfive_cnt = silverFive.Count;
                                                cartcheckoutnoallotweb_savenewcartmstparams.silverfiveString = string.Join(",", silverFive);

                                                SaveCartCheckoutAllotWeb_CartMstNew(cartcheckoutnoallotweb_savenewcartmstparams);
                                            }
                                            else
                                            {
                                                CartCheckoutNoAllotWebSaveCartMstParams cartcheckoutnoallotweb_savecartmstparams = new CartCheckoutNoAllotWebSaveCartMstParams();
                                                cartcheckoutnoallotweb_savecartmstparams.DataID = DataId;
                                                cartcheckoutnoallotweb_savecartmstparams.CartID = CartId;
                                                cartcheckoutnoallotweb_savecartmstparams.OrderTypeID = ordertypeid;
                                                cartcheckoutnoallotweb_savecartmstparams.cart_billing_login_type_id = cart_billing_login_type_id;
                                                cartcheckoutnoallotweb_savecartmstparams.cart_order_data_id = cart_order_data_id;
                                                cartcheckoutnoallotweb_savecartmstparams.cart_billing_data_id = cart_billing_data_id;
                                                cartcheckoutnoallotweb_savecartmstparams.cart_remarks = cart_remarks;
                                                cartcheckoutnoallotweb_savecartmstparams.cart_delivery_date = cart_delivery_date;
                                                cartcheckoutnoallotweb_savecartmstparams.devicetype = "WEB";
                                                cartcheckoutnoallotweb_savecartmstparams.devicename = "";
                                                cartcheckoutnoallotweb_savecartmstparams.appversion = "";
                                                cartcheckoutnoallotweb_savecartmstparams.SourceType = "BULKWEB";
                                                int tempresval = SaveCartCheckoutAllotWeb_CartMst(cartcheckoutnoallotweb_savecartmstparams);
                                                // sendNotification($cart_id,$data)
                                                // checkoutCartOrderInvoiceSend($cart_id);
                                                // responseDetails.success = (tempresval == 1 ? true : false);
                                                // responseDetails.message = resmessage;
                                                // responseDetails.status = resstatuscode;
                                                // return Ok(responseDetails);
                                            }

                                        }
                                        // Main IF
                                    }
                                    catch (Exception ex)
                                    {
                                        resstatus = 0;
                                        resstatuscode = 200;
                                        resmessage = $"SQL error: {ex.Message}";
                                    }
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
            catch (SqlException sqlEx)
            {
                //return BadRequest(sqlEx.Message);
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                return responseDetails;
            }
        }
    }
}
