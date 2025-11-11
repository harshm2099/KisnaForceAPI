using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using System.Text;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class CartService : ICartService
    {
        public string _connection = DBCommands.CONNECTION_STRING;
        public string _customerID = DBCommands.CustomerID;

        public async Task<ResponseDetails> GetCartCount(CartCountListingParams cartcountparams)
        {
            var responseDetails = new ResponseDetails();
            string default_cartid = "0",
                   default_item_count = "0",
                   default_cart_color = "#3f0a8f",
                   default_cart_img = "http://force.kisna.com/HKDB/public/assets/Icons/cart.png";

            var objTemp = new CartCountListing
            {
                cart_id = default_cartid,
                item_count = default_item_count,
                cart_color = default_cart_color,
                cart_img = default_cart_img,
            };

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(DBCommands.CART_COUNT_NEW, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_id", cartcountparams.data_id);

                        using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                await dataReader.ReadAsync();
                                var cartCountListing = new CartCountListing
                                {
                                    cart_id = dataReader["cart_id"]?.ToString() ?? default_cartid,
                                    item_count = dataReader["item_count"]?.ToString() ?? default_item_count,
                                    cart_color = dataReader["cart_color"]?.ToString() ?? default_cart_color,
                                    cart_img = dataReader["cart_img"]?.ToString() ?? default_cart_img,
                                };

                                responseDetails.success = true;
                                responseDetails.message = "Successfully";
                                responseDetails.status = "200";
                                responseDetails.data = cartCountListing;
                            }
                            else
                            {
                                responseDetails.success = false;
                                responseDetails.message = "No data found.";
                                responseDetails.status = "200";
                                responseDetails.data = objTemp;
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = objTemp;
            }
            catch (Exception ex)
            {
                responseDetails.success = false;
                responseDetails.message = $"Unexpected error: {ex.Message}";
                responseDetails.status = "400";
                responseDetails.data = objTemp;
            }

            return responseDetails;
        }

        public async Task<ResponseDetails> GetCartBillingForTypeAsync(CartBillingForTypeListingParams cartbillingfortypeparams)
        {
            var responseDetails = new ResponseDetails();
            var cartBillingForTypelisting = new List<CartBillingForTypeListing>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(DBCommands.CART_BILLINGFORTYPE, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_login_type_dtldata_id", cartbillingfortypeparams.data_login_type_dtldata_id);

                        using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                while (await dataReader.ReadAsync())
                                {
                                    cartBillingForTypelisting.Add(new CartBillingForTypeListing
                                    {
                                        parent_type_id = dataReader["parent_type_id"]?.ToString() ?? "0",
                                        parent_type_code = dataReader["parent_type_code"]?.ToString() ?? string.Empty,
                                        parent_type_name = dataReader["parent_type_name"]?.ToString() ?? string.Empty,
                                        DataLoginTypeDtlSeqNo = dataReader["DataLoginTypeDtlSeqNo"]?.ToString() ?? "0",
                                        DataLoginTypeDtlCommonID = dataReader["DataLoginTypeDtlCommonID"]?.ToString() ?? "0",
                                        billing_data_id = dataReader["billing_data_id"]?.ToString() ?? "0",
                                    });
                                }
                            }
                        }
                    }
                }

                if (cartBillingForTypelisting.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = cartBillingForTypelisting;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CartBillingForTypeListing>(); // Empty list
                }

                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                // Handle specific SQL exceptions
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CartBillingForTypeListing>();  // Return empty list on error
                return responseDetails;
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                responseDetails.success = false;
                responseDetails.message = $"Unexpected error: {ex.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CartBillingForTypeListing>();  // Return empty list on error
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetCartBillingUserListAsync(CartBillingUserListingParams cartbillinguserlistparams)
        {
            var responseDetails = new ResponseDetails();
            var cartBillingUserlist = new List<CartBillingUserListing>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(DBCommands.CART_BILLINGUSERLIST, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_login_type_dtldata_id", cartbillinguserlistparams.data_login_type_dtldata_id);

                        using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                while (await dataReader.ReadAsync())
                                {
                                    cartBillingUserlist.Add(new CartBillingUserListing
                                    {
                                        data_id = dataReader["data_id"]?.ToString() ?? "0",
                                        data_shop_name = dataReader["data_shop_name"]?.ToString() ?? string.Empty,
                                        data_contact_name = dataReader["data_contact_name"]?.ToString() ?? string.Empty,
                                        data_contact_no = dataReader["data_contact_no"]?.ToString() ?? string.Empty,
                                        user_data_id = dataReader["user_data_id"]?.ToString() ?? "0",
                                        DataLoginTypeDtlSeqNo = dataReader["DataLoginTypeDtlSeqNo"]?.ToString() ?? "0",
                                        DataLoginTypeDtlCommonID = dataReader["DataLoginTypeDtlCommonID"]?.ToString() ?? "0",
                                        DataLoginTypeDtlDataID = dataReader["DataLoginTypeDtlDataID"]?.ToString() ?? "0",
                                        DataLoginTypeOrgCommonID = dataReader["DataLoginTypeOrgCommonID"]?.ToString() ?? "0",
                                        DataLoginTypeCommonID = dataReader["DataLoginTypeCommonID"]?.ToString() ?? "0",
                                        DataLoginTypeSeqNo = dataReader["DataLoginTypeSeqNo"]?.ToString() ?? "0",
                                        DataLoginTypePart = dataReader["DataLoginTypePart"]?.ToString() ?? string.Empty,
                                        DataLoginTypeValidSts = dataReader["DataLoginTypeValidSts"]?.ToString() ?? string.Empty,
                                        DataAddr1 = dataReader["DataAddr1"]?.ToString() ?? string.Empty,
                                        DataAddr2 = dataReader["DataAddr2"]?.ToString() ?? string.Empty,
                                        DataAddrState = dataReader["DataAddrState"]?.ToString() ?? string.Empty,
                                        DataAddrCity = dataReader["DataAddrCity"]?.ToString() ?? string.Empty,
                                        DataAddrPinCode = dataReader["DataAddrPinCode"]?.ToString() ?? string.Empty,
                                    });
                                }
                            }
                        }
                    }
                }

                if (cartBillingUserlist.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = cartBillingUserlist;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = cartBillingUserlist;
                }

                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = cartBillingUserlist;
                return responseDetails;
            }
            catch (Exception ex)
            {
                responseDetails.success = false;
                responseDetails.message = $"Unexpected error: {ex.Message}";
                responseDetails.status = "400";
                responseDetails.data = cartBillingUserlist;
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetCartOrderBillingForTypeAsync(CartOrderBillingForTypeListingParams cartorderbillingfortypeparams)
        {
            var responseDetails = new ResponseDetails();
            var cartOrderBillingForTypelisting = new List<CartOrderBillingForTypeListing>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(DBCommands.CART_ORDERBILLINGFORTYPE, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_login_type_dtldata_id", cartorderbillingfortypeparams.data_login_type_dtldata_id);
                        cmd.Parameters.AddWithValue("@fill_type", cartorderbillingfortypeparams.fill_type);
                        cmd.Parameters.AddWithValue("@parent_type_id", cartorderbillingfortypeparams.parent_type_id);

                        using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                while (await dataReader.ReadAsync())
                                {
                                    cartOrderBillingForTypelisting.Add(new CartOrderBillingForTypeListing
                                    {
                                        parent_type_id = dataReader["parent_type_id"]?.ToString() ?? "0",
                                        parent_type_code = dataReader["parent_type_code"]?.ToString() ?? string.Empty,
                                        parent_type_name = dataReader["parent_type_name"]?.ToString() ?? string.Empty,
                                        DataLoginTypeDtlDataID = dataReader["DataLoginTypeDtlDataID"]?.ToString() ?? "0",
                                        DataLoginTypePart = dataReader["DataLoginTypePart"]?.ToString() ?? string.Empty,
                                        DataLoginTypeValidSts = dataReader["DataLoginTypeValidSts"]?.ToString() ?? string.Empty,
                                    });
                                }
                            }
                        }
                    }
                }

                if (cartOrderBillingForTypelisting.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = cartOrderBillingForTypelisting;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CartOrderBillingForTypeListing>(); // Return empty list if no data
                }

                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CartOrderBillingForTypeListing>();  // Return empty list on error
                return responseDetails;
            }
            catch (Exception ex)
            {
                responseDetails.success = false;
                responseDetails.message = $"Unexpected error: {ex.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CartOrderBillingForTypeListing>();  // Return empty list on error
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetCartOrderBillingUserListAsync(CartOrderBillingUserListingParams cartorderbillinguserlistparams)
        {
            var cartOrderBillingUserList = new List<CartOrderBillingUserListing>();
            var responseDetails = new ResponseDetails();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    await dbConnection.OpenAsync();

                    using (var cmd = new SqlCommand(DBCommands.CART_ORDERBILLINGUSERLIST, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_login_type_dtldata_id", cartorderbillinguserlistparams.data_login_type_dtldata_id);
                        cmd.Parameters.AddWithValue("@parent_type_id", cartorderbillinguserlistparams.parent_type_id);
                        cmd.Parameters.AddWithValue("@data_id", cartorderbillinguserlistparams.data_id == 0 ? DBNull.Value : cartorderbillinguserlistparams.data_id);


                        using (SqlDataReader dataReader = await cmd.ExecuteReaderAsync())
                        {
                            if (dataReader.HasRows)
                            {
                                while (await dataReader.ReadAsync())
                                {
                                    cartOrderBillingUserList.Add(new CartOrderBillingUserListing
                                    {
                                        data_id = dataReader["data_id"]?.ToString() ?? "0",
                                        parent_type_id = dataReader["parent_type_id"]?.ToString() ?? "0",
                                        parent_type_code = dataReader["parent_type_code"]?.ToString() ?? string.Empty,
                                        parent_type_name = dataReader["parent_type_name"]?.ToString() ?? string.Empty,
                                        data_code = dataReader["data_code"]?.ToString() ?? string.Empty,
                                        data_shop_name = dataReader["data_shop_name"]?.ToString() ?? string.Empty,
                                        data_latitude = dataReader["data_latitude"]?.ToString() ?? string.Empty,
                                        data_longitude = dataReader["data_longitude"]?.ToString() ?? string.Empty,
                                        data_alt_contact_no = dataReader["data_alt_contact_no"]?.ToString() ?? string.Empty,
                                        data_alt_email = dataReader["data_alt_email"]?.ToString() ?? string.Empty,
                                        data_tel_no = dataReader["data_tel_no"]?.ToString() ?? string.Empty,
                                        data_gstno = dataReader["data_gstno"]?.ToString() ?? string.Empty,
                                        data_remarks = dataReader["data_remarks"]?.ToString() ?? string.Empty,
                                        data_name = dataReader["data_name"]?.ToString() ?? string.Empty,
                                        data_contact_no = dataReader["data_contact_no"]?.ToString() ?? string.Empty,
                                        data_email = dataReader["data_email"]?.ToString() ?? string.Empty,
                                        profile_image = dataReader["profile_image"]?.ToString() ?? string.Empty,
                                        login_type_id = dataReader["login_type_id"]?.ToString() ?? "0",
                                        area_id = dataReader["area_id"]?.ToString() ?? "0",
                                        img_id = dataReader["img_id"]?.ToString() ?? "0",
                                        DataAddr1 = dataReader["DataAddr1"]?.ToString() ?? string.Empty,
                                        DataAddr2 = dataReader["DataAddr2"]?.ToString() ?? string.Empty,
                                        DataAddrState = dataReader["DataAddrState"]?.ToString() ?? string.Empty,
                                        DataAddrCity = dataReader["DataAddrCity"]?.ToString() ?? string.Empty,
                                        DataAddrPinCode = dataReader["DataAddrPinCode"]?.ToString() ?? string.Empty,
                                        Latitude = dataReader["Latitude"]?.ToString() ?? string.Empty,
                                        Longitude = dataReader["Longitude"]?.ToString() ?? string.Empty,
                                        data_alt_name = dataReader["data_alt_name"]?.ToString() ?? string.Empty,
                                    });
                                }
                            }
                        }
                    }
                }

                if (cartOrderBillingUserList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = cartOrderBillingUserList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = cartOrderBillingUserList;
                }

                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = cartOrderBillingUserList;
                return responseDetails;
            }
            catch (Exception ex)
            {
                responseDetails.success = false;
                responseDetails.message = $"Unexpected error: {ex.Message}";
                responseDetails.status = "400";
                responseDetails.data = cartOrderBillingUserList;
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetCartOrderTypeListAsync(CartOrderTypeListingParams cartordertypelistparams)
        {
            var cartOrderTypeList = new List<CartOrderTypeListing>();
            var responseDetails = new ResponseDetails();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    await dbConnection.OpenAsync();

                    using (var cmd = new SqlCommand(DBCommands.CART_ORDERTYPELIST, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@datalogintypeid", cartordertypelistparams.datalogintypeid);
                        cmd.Parameters.AddWithValue("@data_id", cartordertypelistparams.data_id);

                        using (var dataReader = await cmd.ExecuteReaderAsync())
                        {
                            while (await dataReader.ReadAsync())
                            {
                                cartOrderTypeList.Add(new CartOrderTypeListing
                                {
                                    ordertype_mst_id = dataReader["ordertype_mst_id"]?.ToString() ?? "0",
                                    ordertype_mst_code = dataReader["ordertype_mst_code"]?.ToString() ?? string.Empty,
                                    ordertype_mst_name = dataReader["ordertype_mst_name"]?.ToString() ?? string.Empty,
                                    ordertype_mst_color = dataReader["ordertype_mst_color"]?.ToString() ?? string.Empty,
                                });
                            }
                        }
                    }
                }

                responseDetails.success = cartOrderTypeList.Any();
                responseDetails.message = cartOrderTypeList.Any() ? "Successfully" : "No data found";
                responseDetails.status = "200";
                responseDetails.data = cartOrderTypeList;

                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = cartOrderTypeList;
                return responseDetails;
            }
            catch (Exception ex)
            {
                responseDetails.success = false;
                responseDetails.message = $"Unexpected error: {ex.Message}";
                responseDetails.status = "400";
                responseDetails.data = cartOrderTypeList;
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> GetCartItemList(CartItemListingParams cartitemlistparams)
        {
            var responseDetails = new ResponseDetails();

            IList<CartItemListing> cartItemList = new List<CartItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CART_ITEMLISTING;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", cartitemlistparams.data_id);
                        cmd.Parameters.AddWithValue("@Cart_Id", cartitemlistparams.cart_id);
                        cmd.Parameters.AddWithValue("@DataLoginTypeID", cartitemlistparams.data_login_type);
                        cmd.Parameters.AddWithValue("@page", cartitemlistparams.page);
                        cmd.Parameters.AddWithValue("@Cart_Sts", cartitemlistparams.cart_sts);

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int cart_auto_id = dataReader["cart_auto_id"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_auto_id"]) : 0;
                                    decimal labour_per = dataReader["labour_per"] != DBNull.Value ? Convert.ToDecimal(dataReader["labour_per"]) : 0;
                                    decimal dislabour_per = dataReader["dislabour_per"] != DBNull.Value ? Convert.ToDecimal(dataReader["dislabour_per"]) : 0;
                                    string plaingold_status = dataReader["plaingold_status"] as string ?? string.Empty;
                                    decimal ExtraGoldWeight = dataReader["ExtraGoldWeight"] != DBNull.Value ? Convert.ToDecimal(dataReader["ExtraGoldWeight"]) : 0;
                                    decimal ExtraGoldPrice = dataReader["ExtraGoldPrice"] != DBNull.Value ? Convert.ToDecimal(dataReader["ExtraGoldPrice"]) : 0;
                                    int cart_id = dataReader["cart_id"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_id"]) : 0;
                                    string approx_day = dataReader["ItemAproxDay"] as string ?? string.Empty;
                                    string ItemDAproxDay = dataReader["ItemDAproxDay"] as string ?? string.Empty;
                                    int data_id = dataReader["data_id"] != DBNull.Value ? Convert.ToInt32(dataReader["data_id"]) : 0;
                                    string designkt = dataReader["dsg_kt"] as string ?? string.Empty;
                                    int itemid = dataReader["item_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_id"]) : 0;
                                    string item_code = dataReader["item_code"] as string ?? string.Empty;
                                    string item_name = dataReader["item_name"] as string ?? string.Empty;
                                    string item_sku = dataReader["item_sku"] as string ?? string.Empty;
                                    string item_description = dataReader["item_description"] as string ?? string.Empty;
                                    string item_soliter = dataReader["item_soliter"] as string ?? string.Empty;
                                    string is_stock = dataReader["is_stock"] as string ?? string.Empty;
                                    string product_item_custom_remarks_status = dataReader["product_item_custom_remarks_status"] as string ?? string.Empty;
                                    int brand_id = dataReader["ItemBrandCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemBrandCommonID"]) : 0;
                                    string ItemPlainGold = dataReader["ItemPlainGold"] as string ?? string.Empty;
                                    string ItemSoliterSts = dataReader["ItemSoliterSts"] as string ?? string.Empty;
                                    decimal item_mrp = dataReader["item_mrp"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_mrp"]) : 0;
                                    decimal item_price = dataReader["item_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_price"]) : 0;
                                    decimal dist_price = dataReader["dist_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["dist_price"]) : 0;
                                    string price_text = dataReader["price_text"] as string ?? string.Empty;
                                    decimal cart_price = dataReader["cart_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["cart_price"]) : 0;
                                    string image_path = dataReader["image_path"] as string ?? string.Empty;
                                    string dsg_sfx = dataReader["dsg_sfx"] as string ?? string.Empty;
                                    string dsg_size = dataReader["dsg_size"] as string ?? string.Empty;
                                    string dsg_kt = dataReader["dsg_kt"] as string ?? string.Empty;
                                    string dsg_color = dataReader["dsg_color"] as string ?? string.Empty;
                                    int star = dataReader["star"] != DBNull.Value ? Convert.ToInt32(dataReader["star"]) : 0;
                                    string cart_img = dataReader["cart_img"] as string ?? string.Empty;
                                    string img_cart_title = dataReader["img_cart_title"] as string ?? string.Empty;
                                    string watch_img = dataReader["watch_img"] as string ?? string.Empty;
                                    string img_watch_title = dataReader["img_watch_title"] as string ?? string.Empty;
                                    int wish_count = dataReader["wish_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wish_count"]) : 0;
                                    int wearit_count = dataReader["wearit_count"] != DBNull.Value ? Convert.ToInt32(dataReader["wearit_count"]) : 0;
                                    string wearit_status = dataReader["wearit_status"] as string ?? string.Empty;
                                    string wearit_img = dataReader["wearit_img"] as string ?? string.Empty;
                                    string wearit_none_img = dataReader["wearit_none_img"] as string ?? string.Empty;
                                    string wearit_color = dataReader["wearit_color"] as string ?? string.Empty;
                                    string wearit_text = dataReader["wearit_text"] as string ?? string.Empty;
                                    string img_wearit_title = dataReader["img_wearit_title"] as string ?? string.Empty;
                                    string wish_default_img = dataReader["wish_default_img"] as string ?? string.Empty;
                                    string wish_fill_img = dataReader["wish_fill_img"] as string ?? string.Empty;
                                    string img_wish_title = dataReader["img_wish_title"] as string ?? string.Empty;
                                    string item_review = dataReader["item_review"] as string ?? string.Empty;
                                    string item_size = dataReader["item_size"] as string ?? string.Empty;
                                    string item_kt = dataReader["item_kt"] as string ?? string.Empty;
                                    string item_color = dataReader["item_color"] as string ?? string.Empty;
                                    string item_metal = dataReader["item_metal"] as string ?? string.Empty;
                                    decimal item_wt = dataReader["item_wt"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_wt"]) : 0;
                                    string item_stone = dataReader["item_stone"] as string ?? string.Empty;
                                    decimal item_stone_wt = dataReader["item_stone_wt"] != DBNull.Value ? Convert.ToDecimal(dataReader["item_stone_wt"]) : 0;
                                    int item_stone_qty = dataReader["item_stone_qty"] != DBNull.Value ? Convert.ToInt32(dataReader["item_stone_qty"]) : 0;
                                    string star_color = dataReader["star_color"] as string ?? string.Empty;
                                    int item_color_id = dataReader["item_color_id"] != DBNull.Value ? Convert.ToInt32(dataReader["item_color_id"]) : 0;
                                    string item_details = dataReader["item_details"] as string ?? string.Empty;
                                    string item_diamond_details = dataReader["item_diamond_details"] as string ?? string.Empty;
                                    string item_text = dataReader["item_text"] as string ?? string.Empty;
                                    string more_item_details = dataReader["more_item_details"] as string ?? string.Empty;
                                    string item_stock = dataReader["item_stock"] as string ?? string.Empty;
                                    decimal cart_item_qty = Convert.ToDecimal(dataReader["cart_item_qty"]);
                                    int ItemMetalCommonID = dataReader["ItemMetalCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemMetalCommonID"]) : 0;
                                    string item_removecart_img = dataReader["item_removecart_img"] as string ?? string.Empty;
                                    string item_removecard_title = dataReader["item_removecard_title"] as string ?? string.Empty;
                                    string rupy_symbol = dataReader["rupy_symbol"] as string ?? string.Empty;
                                    int CategoryID = dataReader["category_id"] != DBNull.Value ? Convert.ToInt32(dataReader["category_id"]) : 0;
                                    int color_common_id = dataReader["color_common_id"] != DBNull.Value ? Convert.ToInt32(dataReader["color_common_id"]) : 0;
                                    int size_common_id = dataReader["size_common_id"] != DBNull.Value ? Convert.ToInt32(dataReader["size_common_id"]) : 0;
                                    string color_common_name = dataReader["color_common_name"] as string ?? string.Empty;
                                    string size_common_name = dataReader["size_common_name"] as string ?? string.Empty;
                                    string color_common_name1 = dataReader["color_common_name1"] as string ?? string.Empty;
                                    string size_common_name1 = dataReader["size_common_name1"] as string ?? string.Empty;
                                    int ItemGenderCommonID = dataReader["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemGenderCommonID"]) : 0;
                                    int item_stock_qty = dataReader["item_stock_qty"] != DBNull.Value ? Convert.ToInt32(dataReader["item_stock_qty"]) : 0;
                                    int item_stock_colorsize_qty = dataReader["item_stock_colorsize_qty"] != DBNull.Value ? Convert.ToInt32(dataReader["item_stock_colorsize_qty"]) : 0;
                                    int variantCount = dataReader["variantCount"] != DBNull.Value ? Convert.ToInt32(dataReader["variantCount"]) : 0;
                                    decimal cart_cancel_qty = dataReader["cart_cancel_qty"] != DBNull.Value ? Convert.ToDecimal(dataReader["cart_cancel_qty"]) : 0;
                                    int cart_cancel_by = dataReader["cart_cancel_by"] != DBNull.Value ? Convert.ToInt32(dataReader["cart_cancel_by"]) : 0;
                                    string cart_cancel_sts = dataReader["cart_cancel_sts"] as string ?? string.Empty;
                                    string cart_cancel_name = dataReader["cart_cancel_name"] as string ?? string.Empty;
                                    int master_common_id = dataReader["ItemTypeCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemTypeCommonID"]) : 0;
                                    string ItemNosePinScrewSts = dataReader["ItemNosePinScrewSts"] as string ?? string.Empty;
                                    string priceflag = dataReader["priceflag"] as string ?? string.Empty;
                                    string SNMCCFlag = dataReader["SNMCCFlag"] as string ?? string.Empty;
                                    string ItemFranchiseSts = dataReader["ItemFranchiseSts"] as string ?? string.Empty;
                                    int ItemAproxDayCommonID = dataReader["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemAproxDayCommonID"]) : 0;
                                    int CartItemID = dataReader["CartItemID"] != DBNull.Value ? Convert.ToInt32(dataReader["CartItemID"]) : 0;
                                    string ItemValidSts = dataReader["ItemValidSts"] as string ?? string.Empty;
                                    string indentNumber = dataReader["indentNumber"] as string ?? "-";
                                    string item_illumine = dataReader["item_illumine"] as string ?? string.Empty;
                                    string ItemIsSRP = dataReader["ItemIsSRP"] as string ?? string.Empty;
                                    string DataGoldFlag = dataReader["DataGoldFlag"] as string ?? string.Empty;
                                    decimal ItemMetalWt = dataReader["ItemMetalWt"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemMetalWt"]) : 0;
                                    decimal ItemGrossWt = dataReader["ItemGrossWt"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemGrossWt"]) : 0;
                                    decimal CartSoliGWt = dataReader["CartSoliGWt"] != DBNull.Value ? Convert.ToDecimal(dataReader["CartSoliGWt"]) : 0;
                                    decimal CartSoliDiaWt = dataReader["CartSoliDiaWt"] != DBNull.Value ? Convert.ToDecimal(dataReader["CartSoliDiaWt"]) : 0;
                                    string isAutoInserted = dataReader["isAutoInserted"] as string ?? string.Empty;
                                    string isCOOInserted = dataReader["isCOOInserted"] as string ?? string.Empty;
                                    string ItemSizeAvailable = dataReader["ItemSizeAvailable"] as string ?? string.Empty;
                                    string IsSolitaireCombo = dataReader["IsSolitaireCombo"] as string ?? string.Empty;
                                    int selectedColor = dataReader["selectedColor"] != DBNull.Value ? Convert.ToInt32(dataReader["selectedColor"]) : 0;
                                    string selectedSize = dataReader["selectedSize"] as string ?? string.Empty;
                                    int selectedColor1 = dataReader["selectedColor1"] != DBNull.Value ? Convert.ToInt32(dataReader["selectedColor1"]) : 0;
                                    int selectedSize1 = dataReader["selectedSize1"] != DBNull.Value ? Convert.ToInt32(dataReader["selectedSize1"]) : 0;
                                    string field_name = dataReader["field_name"] as string ?? string.Empty;
                                    string color_name = dataReader["color_name"] as string ?? string.Empty;
                                    string default_color_name = dataReader["default_color_name"] as string ?? string.Empty;
                                    int default_color_code = dataReader["default_color_code"] != DBNull.Value ? Convert.ToInt32(dataReader["default_color_code"]) : 0;
                                    int default_size_name = dataReader["default_size_name"] != DBNull.Value ? Convert.ToInt32(dataReader["default_size_name"]) : 0;
                                    string DataItemInfo = dataReader["DataItemInfo"] as string ?? string.Empty;
                                    int data_login_type_const = dataReader["data_login_type"] != DBNull.Value ? Convert.ToInt32(dataReader["data_login_type"]) : 0;
                                    string DeliveryStatus = dataReader["DeliveryStatus"] as string ?? string.Empty;
                                    int ItemDAproxDay_MstDesc = dataReader["ItemDAproxDay_MstDesc"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemDAproxDay_MstDesc"]) : 0;
                                    string ALLOWED_MARGIN_IDS = dataReader["ALLOWED_MARGIN_IDS"] as string ?? string.Empty;
                                    string MstType = dataReader["MstType"] as string ?? string.Empty;
                                    string CartSoliStkNo = dataReader["CartSoliStkNo"] as string ?? string.Empty;

                                    string colorname = item_color;
                                    int metalid = ItemMetalCommonID;
                                    string approxday_detail = approx_day.Trim().Length > 0 ? " Approx Delivery: " + approx_day + "." : "";
                                    int ItemBrandCommonID = brand_id;
                                    string ItemAproxDay = approx_day;

                                    string totalLabourPer = "";
                                    string weight = "";
                                    decimal item_mrp_new = 0;
                                    decimal fran_diamond_price = 0;
                                    decimal fran_gold_price = 0;
                                    decimal fran_platinum_price = 0;
                                    decimal fran_labour_price = 0;
                                    decimal fran_metal_price = 0;
                                    decimal fran_other_price = 0;
                                    decimal fran_stone_price = 0;

                                    CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                    IList<CartItemPriceDetailListing> cartItemPriceDetailList = new List<CartItemPriceDetailListing>();

                                    cartitempricedetaillistparams.DataID = data_id;
                                    cartitempricedetaillistparams.ItemID = itemid;
                                    cartitempricedetaillistparams.SizeID = size_common_id;
                                    cartitempricedetaillistparams.CategoryID = CategoryID;
                                    cartitempricedetaillistparams.ItemBrandCommonID = ItemBrandCommonID;
                                    cartitempricedetaillistparams.ItemGrossWt = ItemGrossWt;
                                    cartitempricedetaillistparams.ItemMetalWt = ItemMetalWt;
                                    cartitempricedetaillistparams.ItemGenderCommonID = ItemGenderCommonID;

                                    if (DataGoldFlag == "Y")
                                    {
                                        if (plaingold_status == "Y" && isCOOInserted == "N")
                                        {
                                            cartitempricedetaillistparams.IsWeightCalcRequired = 1;
                                            cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                            decimal total_goldvalue = 0;
                                            decimal gold_wt = 0;
                                            if (cartItemPriceDetailList.Count > 0)
                                            {
                                                // gold_ktprice
                                                total_goldvalue = cartItemPriceDetailList[0].gold_ktprice;

                                                // gold_wt
                                                gold_wt = cartItemPriceDetailList[0].gold_wt;

                                                weight = "Gold Weight: " + Math.Round(gold_wt, 2);
                                                weight += ", Gold Price : " + total_goldvalue;
                                                weight += ", KT : " + designkt;
                                                weight += ", " + approxday_detail;

                                                totalLabourPer = "L(" + Math.Round(labour_per, 1);
                                                totalLabourPer += " + ";
                                                totalLabourPer += Math.Round(dislabour_per, 1) + "%)";
                                            }
                                        }
                                        else
                                        {
                                            weight = "Metal Weight: " + Math.Round(item_wt, 2);
                                            weight += ", Diamond Weight: " + Math.Round(item_stone_wt, 2);
                                            weight += ", " + approxday_detail;
                                        }
                                    }

                                    if (item_soliter == "Y" || item_illumine == "Y")
                                    {
                                        if (CartSoliGWt > 0)
                                        {
                                            item_wt = CartSoliGWt;
                                        }
                                        else
                                        {
                                            item_wt = 0;
                                        }
                                        if (CartSoliDiaWt > 0)
                                        {
                                            item_wt = CartSoliDiaWt;
                                        }
                                        else
                                        {
                                            item_stone_wt = 0;
                                        }
                                    }

                                    if ((priceflag == "Y" && isCOOInserted == "N" && item_illumine == "N") || (ItemIsSRP == "Y" && item_illumine == "N"))
                                    {
                                        if (MstType == "F" && plaingold_status == "Y" && isCOOInserted == "N")
                                        {
                                            cartitempricedetaillistparams.IsWeightCalcRequired = 1;
                                            cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                        }
                                        else
                                        {
                                            cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                            cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                        }
                                        if (cartItemPriceDetailList.Count > 0)
                                        {
                                            item_mrp_new = cartitemlistparams.cart_sts == "P" ? Math.Round(item_mrp, 0) : Math.Round(cartItemPriceDetailList[0].total_price, 0);
                                            fran_diamond_price = Math.Round(cartItemPriceDetailList[0].diamond_price, 0);
                                            fran_gold_price = Math.Round(cartItemPriceDetailList[0].gold_price, 0);
                                            fran_platinum_price = Math.Round(cartItemPriceDetailList[0].platinum_price, 0);
                                            fran_labour_price = Math.Round(cartItemPriceDetailList[0].labour_price, 0);
                                            fran_metal_price = Math.Round(cartItemPriceDetailList[0].metal_price, 0);
                                            fran_other_price = Math.Round(cartItemPriceDetailList[0].other_price, 0);
                                            fran_stone_price = Math.Round(cartItemPriceDetailList[0].stone_price, 0);
                                            item_wt = cartItemPriceDetailList[0].gold_wt;
                                        }
                                    }
                                    else
                                    {
                                        if (MstType == "F" && isCOOInserted == "N")
                                        {
                                            cartitempricedetaillistparams.IsWeightCalcRequired = 1;
                                            cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                            if (cartItemPriceDetailList.Count > 0)
                                            {
                                                item_wt = cartItemPriceDetailList[0].gold_wt;
                                                if (cartitemlistparams.cart_sts == "P")
                                                {
                                                    item_mrp_new = Math.Round(item_mrp, 0);
                                                }
                                                else
                                                {
                                                    item_mrp_new = (item_soliter == "Y" || item_illumine == "Y") ? Math.Round(item_mrp, 0) : Math.Round(cartItemPriceDetailList[0].total_price, 0);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (plaingold_status == "Y" && isCOOInserted == "N")
                                            {
                                                cartitempricedetaillistparams.IsWeightCalcRequired = 1;
                                                cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                                if (cartItemPriceDetailList.Count > 0)
                                                {
                                                    item_mrp_new = cartitemlistparams.cart_sts == "P" ? Math.Round(item_mrp, 0) : Math.Round(cartItemPriceDetailList[0].total_price, 0);
                                                    item_wt = cartItemPriceDetailList[0].gold_wt;
                                                }
                                            }
                                            else
                                            {
                                                item_mrp_new = Math.Round(item_mrp, 0);
                                                if (ItemPlainGold == "N" && ItemSoliterSts == "N" && ALLOWED_MARGIN_IDS.Split(',').Contains(cartitemlistparams.data_login_type.ToString()))  //ALLOWED_MARGIN_IDS
                                                {
                                                    CartItemDPRPCALCListingParams cartitemDPRPCALClistparams = new CartItemDPRPCALCListingParams();
                                                    IList<CartItemDPRPCALCListing> cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

                                                    cartitemDPRPCALClistparams.DataID = data_id;
                                                    cartitemDPRPCALClistparams.MRP = item_mrp_new;
                                                    cartItemDPRPCALCList = objHelpers.Get_DP_RP_Calculation(cartitemDPRPCALClistparams);

                                                    if (cartItemDPRPCALCList.Count > 0)
                                                    {
                                                        dist_price = cartItemDPRPCALCList[0].D_Price;
                                                        item_price = cartItemDPRPCALCList[0].R_Price;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    cart_price = item_mrp_new;
                                    price_text = "MRP.₹" + Math.Round(item_mrp, 0) + "/-";
                                    item_mrp = item_mrp_new;
                                    item_price = Math.Round(item_price, 0);
                                    dist_price = Math.Round(dist_price, 0);

                                    // PRODUCT SIZELIST
                                    CartProduct_sizeListingParams cartproductsizelistparams = new CartProduct_sizeListingParams();
                                    List<CartProduct_sizeListing> cartProductSizeList = new List<CartProduct_sizeListing>();

                                    if (ItemSizeAvailable == "N")
                                    {
                                        selectedSize = "";
                                        default_size_name = 0;
                                    }
                                    else
                                    {
                                        cartproductsizelistparams.ItemID = itemid;
                                        cartproductsizelistparams.CategoryID = CategoryID;
                                        cartproductsizelistparams.BranID = ItemBrandCommonID;
                                        cartproductsizelistparams.ItemNosePinScrewSts = ItemNosePinScrewSts;
                                        cartproductsizelistparams.ItemGenderCommonID = ItemGenderCommonID;
                                        cartproductsizelistparams.field_name = field_name;
                                        cartproductsizelistparams.selectedSize = selectedSize;
                                        cartproductsizelistparams.default_size_name = default_size_name;

                                        cartProductSizeList = objHelpers.GetCartProductSizeList(cartproductsizelistparams);

                                        if (cartProductSizeList.Count > 0)
                                        {
                                            field_name = cartProductSizeList[0].field_name;
                                            selectedSize = cartProductSizeList[0].selectedSize;
                                            default_size_name = cartProductSizeList[0].default_size_name;
                                        }
                                    }
                                    // PRODUCT SIZELIST

                                    // COLOR LIST
                                    CartItem_colorListingParams cartitemcolorlistparams = new CartItem_colorListingParams();
                                    List<CartItem_colorListing> cartItemColorList = new List<CartItem_colorListing>();

                                    cartitemcolorlistparams.item_id = itemid;
                                    cartitemcolorlistparams.default_color_code = colorname;
                                    cartitemcolorlistparams.metalid = metalid;

                                    cartItemColorList = objHelpers.GetCartItemsColorList(cartitemcolorlistparams);
                                    // COLOR LIST

                                    // COLOR SIZELIST
                                    CartItem_itemsColorSizeListingParams cartitemcolorsizelistparams = new CartItem_itemsColorSizeListingParams();
                                    List<CartItem_itemsColorSizeListing> cartItemColorSizeList = new List<CartItem_itemsColorSizeListing>();

                                    cartitemcolorsizelistparams.itemid = itemid;
                                    cartitemcolorsizelistparams.cart_auto_id = cart_auto_id;
                                    cartitemcolorsizelistparams.CartID = cart_id;

                                    cartItemColorSizeList = objHelpers.GetCartItemsColorSizeList(cartitemcolorsizelistparams);
                                    // COLOR SIZELIST

                                    // ORDER INSTRUCTION LIST
                                    List<Item_itemOrderInstructionListing> itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();
                                    itemOrderInstructionList = objHelpers.GetItemOrderInstructionList();
                                    // ORDER INSTRUCTION LIST

                                    // ORDER CUSTOM INSTRUCTION LIST
                                    List<Item_itemOrderCustomInstructionListing> itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();
                                    itemOrderCustomInstructionList = objHelpers.GetItemOrderCustomInstructionList();
                                    // ORDER CUSTOM INSTRUCTION LIST

                                    // IMAGE COLORS
                                    CartItem_item_images_colorListingParams cartitemimagecolorlistparams = new CartItem_item_images_colorListingParams();
                                    List<CartItem_item_images_colorListing> cartItemImageColorList = new List<CartItem_item_images_colorListing>();

                                    cartitemimagecolorlistparams.itemid = itemid;
                                    cartitemimagecolorlistparams.master_common_id = master_common_id;
                                    cartItemImageColorList = objHelpers.GetCartItemImageColorList(cartitemimagecolorlistparams);
                                    // IMAGE COLORS

                                    // PRODUCT TAGS
                                    Item_TagsListingParams itemtaglistparams = new Item_TagsListingParams();
                                    List<Item_TagsListing> itemTagList = new List<Item_TagsListing>();

                                    itemtaglistparams.item_id = itemid;
                                    itemTagList = objHelpers.GetItemTagList(itemtaglistparams);
                                    // PRODUCT TAGS

                                    // GET APPROX DAYS
                                    string stockType = "", orderType = "";
                                    int tmpItemAproxDay = 0;
                                    DateTime startDate, endDate = DateTime.Now.AddDays(101);

                                    if (dataReader["ent_dt"] == null || dataReader["ent_dt"] is System.DBNull)
                                    {
                                        startDate = DateTime.Now.AddDays(1);
                                    }
                                    else
                                    {
                                        startDate = Convert.ToDateTime(dataReader["ent_dt"]).AddDays(1);
                                    }
                                    if (cartitemlistparams.cart_sts.ToLower() == "p")
                                    {
                                        stockType = DeliveryStatus.ToLower() == "immediate" ? "InStock" : "NotInStock";
                                        orderType = "pre_order";
                                        startDate = DateTime.Now.AddDays(1);
                                    }
                                    else
                                    {
                                        stockType = (item_stock_qty > 0) ? "InStock" : "NotInStock";
                                        orderType = "post_order";
                                    }

                                    if (ItemAproxDay.Trim().Length > 0)
                                    {
                                        if (ItemAproxDay.ToString().Contains(" Hours"))
                                        {
                                            tmpItemAproxDay = (int)Math.Ceiling(double.Parse(ItemAproxDay.ToString().Replace(" Hours", "")) / 24);
                                        }
                                        else
                                        {
                                            tmpItemAproxDay = (int)double.Parse(ItemAproxDay.ToString().Replace(" Days", ""));
                                        }
                                    }
                                    else
                                    {
                                        tmpItemAproxDay = 0;
                                    }
                                    int ItemAproxDay_Final = (stockType == "NotInStock") ? tmpItemAproxDay : 0;

                                    var holidayData = objHelpers.GetHoliDaysList(startDate, endDate);
                                    var dates = new List<string>();
                                    var current = startDate;
                                    while (current <= endDate)
                                    {
                                        if (current.DayOfWeek != DayOfWeek.Sunday && !holidayData.Contains(current))
                                        {
                                            dates.Add(current.ToString("d-M-yyyy"));
                                        }
                                        current = current.AddDays(1);
                                    }

                                    int sumApproxAndDeliveryDays = ItemAproxDay_Final + ItemDAproxDay_MstDesc;


                                    CartItem_approxDaysListing approxDates = new CartItem_approxDaysListing();
                                    approxDates.manufactureStartDate = startDate.ToString("d-M-yyyy");
                                    approxDates.manufactureEndDate = ItemAproxDay_Final > 0 ? dates[ItemAproxDay_Final - 1] : dates[ItemAproxDay_Final];
                                    approxDates.deliveryStartDate = "";
                                    approxDates.deliveryEndDate = "";
                                    approxDates.deliveryInDays = "";

                                    if (cartitemlistparams.data_login_type == data_login_type_const || orderType == "pre_order")
                                    {
                                        if (orderType == "post_order")
                                        {
                                            approxDates.deliveryStartDate = dates[ItemAproxDay_Final];
                                            approxDates.deliveryEndDate = dates[sumApproxAndDeliveryDays - 1];
                                            approxDates.deliveryInDays = $"{sumApproxAndDeliveryDays} Days";
                                        }
                                        else
                                        {
                                            if (dataReader["ent_dt"] == null || dataReader["ent_dt"] is System.DBNull)
                                            {
                                                approxDates.deliveryStartDate = "";
                                                approxDates.deliveryEndDate = "";
                                                approxDates.deliveryInDays = "";
                                            }
                                            else
                                            {
                                                approxDates.deliveryStartDate = dates[ItemAproxDay_Final];
                                                approxDates.deliveryEndDate = dates[sumApproxAndDeliveryDays - 1];
                                                approxDates.deliveryInDays = $"{sumApproxAndDeliveryDays} Days";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (stockType == "InStock")
                                        {
                                            approxDates.deliveryStartDate = dates[ItemAproxDay_Final];
                                            approxDates.deliveryEndDate = dates[sumApproxAndDeliveryDays - 1];
                                            approxDates.deliveryInDays = $"{sumApproxAndDeliveryDays} Days";
                                        }
                                        else
                                        {
                                            approxDates.deliveryStartDate = "";
                                            approxDates.deliveryEndDate = "";
                                            approxDates.deliveryInDays = "";
                                        }
                                    }

                                    cartItemList.Add(new CartItemListing
                                    {
                                        cart_auto_id = cart_auto_id.ToString(),
                                        labour_per = labour_per.ToString(),
                                        dislabour_per = dislabour_per.ToString(),
                                        plaingold_status = plaingold_status,
                                        ExtraGoldWeight = ExtraGoldWeight.ToString(),
                                        ExtraGoldPrice = ExtraGoldPrice.ToString(),
                                        cart_id = cart_id.ToString(),
                                        ItemAproxDay = ItemAproxDay,
                                        ItemDAproxDay = ItemDAproxDay,
                                        data_id = data_id.ToString(),
                                        dsg_kt = dsg_kt,
                                        item_id = itemid.ToString(),
                                        ent_dt = (dataReader["ent_dt"] == null || dataReader["ent_dt"] is System.DBNull) ? DateTime.MinValue : Convert.ToDateTime(dataReader["ent_dt"]),
                                        item_code = item_code,
                                        item_name = item_name,
                                        item_sku = item_sku,
                                        item_description = item_description,
                                        item_soliter = item_soliter,
                                        is_stock = is_stock,
                                        product_item_custom_remarks_status = product_item_custom_remarks_status,
                                        ItemBrandCommonID = ItemBrandCommonID.ToString(),
                                        ItemPlainGold = ItemPlainGold,
                                        ItemSoliterSts = ItemSoliterSts,
                                        item_mrp = item_mrp.ToString(),
                                        item_price = item_price.ToString(),
                                        dist_price = dist_price.ToString(),
                                        price_text = price_text,
                                        cart_price = cart_price.ToString(),
                                        image_path = image_path,
                                        dsg_sfx = dsg_sfx,
                                        dsg_size = dsg_size,
                                        dsg_color = dsg_color,
                                        star = star.ToString(),
                                        cart_img = cart_img,
                                        img_cart_title = img_cart_title,
                                        watch_img = watch_img,
                                        img_watch_title = img_watch_title,
                                        wish_count = wish_count.ToString(),
                                        wearit_count = wearit_count.ToString(),
                                        wearit_status = wearit_status,
                                        wearit_img = wearit_img,
                                        wearit_none_img = wearit_none_img,
                                        wearit_color = wearit_color,
                                        wearit_text = wearit_text,
                                        img_wearit_title = img_wearit_title,
                                        wish_default_img = wish_default_img,
                                        wish_fill_img = wish_fill_img,
                                        img_wish_title = img_wish_title,
                                        item_review = item_review,
                                        item_size = item_size,
                                        item_kt = item_kt,
                                        item_color = item_color,
                                        item_metal = item_metal,
                                        item_wt = item_wt.ToString(),
                                        item_stone = item_stone,
                                        item_stone_wt = item_stone_wt.ToString(),
                                        item_stone_qty = item_stone_qty.ToString(),
                                        star_color = star_color,
                                        item_color_id = item_color_id.ToString(),
                                        item_details = item_details,
                                        item_diamond_details = item_diamond_details,
                                        item_text = item_text,
                                        more_item_details = more_item_details,
                                        item_stock = item_stock,
                                        cart_item_qty = cart_item_qty.ToString(),
                                        ItemMetalCommonID = ItemMetalCommonID.ToString(),
                                        item_removecart_img = item_removecart_img,
                                        item_removecard_title = item_removecard_title,
                                        rupy_symbol = rupy_symbol,
                                        category_id = CategoryID.ToString(),
                                        color_common_id = color_common_id.ToString(),
                                        size_common_id = size_common_id.ToString(),
                                        color_common_name = color_common_name,
                                        size_common_name = size_common_name,
                                        color_common_name1 = color_common_name1,
                                        size_common_name1 = size_common_name1,
                                        ItemGenderCommonID = ItemGenderCommonID.ToString(),
                                        item_stock_qty = item_stock_qty.ToString(),
                                        item_stock_colorsize_qty = item_stock_colorsize_qty.ToString(),
                                        variantCount = variantCount.ToString(),
                                        cart_cancel_qty = cart_cancel_qty.ToString(),
                                        cart_cancel_date = (dataReader["cart_cancel_date"] == null || dataReader["cart_cancel_date"] is System.DBNull) ? DateTime.MinValue : Convert.ToDateTime(dataReader["cart_cancel_date"]),
                                        cart_cancel_by = cart_cancel_by.ToString(),
                                        cart_cancel_sts = cart_cancel_sts,
                                        cart_cancel_name = cart_cancel_name,
                                        ItemTypeCommonID = master_common_id.ToString(),
                                        ItemNosePinScrewSts = ItemNosePinScrewSts,
                                        priceflag = priceflag,
                                        SNMCCFlag = SNMCCFlag,
                                        ItemFranchiseSts = ItemFranchiseSts,
                                        ItemAproxDayCommonID = ItemAproxDayCommonID.ToString(),
                                        CartItemID = CartItemID.ToString(),
                                        ItemValidSts = ItemValidSts,
                                        indentNumber = indentNumber,
                                        item_illumine = item_illumine,
                                        ItemIsSRP = ItemIsSRP,
                                        isAutoInserted = isAutoInserted,
                                        isCOOInserted = isCOOInserted,
                                        ItemSizeAvailable = ItemSizeAvailable,
                                        weight = weight,
                                        totalLabourPer = totalLabourPer,
                                        IsSolitaireCombo = field_name,
                                        selectedColor = selectedColor.ToString(),
                                        selectedSize = selectedSize.ToString(),
                                        selectedColor1 = selectedColor1.ToString(),
                                        selectedSize1 = selectedSize1.ToString(),
                                        field_name = field_name,
                                        color_name = color_name,
                                        default_color_name = default_color_name,
                                        default_color_code = default_color_code.ToString(),
                                        default_size_name = default_size_name.ToString(),

                                        fran_diamond_price = fran_diamond_price.ToString(),
                                        fran_gold_price = fran_gold_price.ToString(),
                                        fran_platinum_price = fran_platinum_price.ToString(),
                                        fran_labour_price = fran_labour_price.ToString(),
                                        fran_metal_price = fran_metal_price.ToString(),
                                        fran_other_price = fran_other_price.ToString(),
                                        fran_stone_price = fran_stone_price.ToString(),
                                        sizeList = cartProductSizeList,
                                        colorList = cartItemColorList,
                                        itemsColorSizeList = cartItemColorSizeList,
                                        itemOrderCustomInstructionList = itemOrderCustomInstructionList,
                                        itemOrderInstructionList = itemOrderInstructionList,
                                        item_images_color = cartItemImageColorList,
                                        productTags = itemTagList,
                                        approxDays = approxDates,
                                        CartSoliStkNo = CartSoliStkNo,
                                    });

                                }
                            }
                        }
                    }
                }

                if (cartItemList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = cartItemList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CartItemListing>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CartItemListing>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CartInsert(CartInsertParams cartinsert_params)
        {
            CartStore cartstoreDetails = new CartStore();
            var responseDetails = new ResponseDetails();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataId = cartinsert_params.data_id > 0 ? cartinsert_params.data_id : 0;
                    string NetIp = string.IsNullOrWhiteSpace(cartinsert_params.net_ip) ? "" : cartinsert_params.net_ip;
                    string CartRemark = string.IsNullOrWhiteSpace(cartinsert_params.cart_remark) ? "" : cartinsert_params.cart_remark;
                    int CartId = cartinsert_params.cart_id > 0 ? cartinsert_params.cart_id : 0;
                    int ItemId = cartinsert_params.item_id > 0 ? cartinsert_params.item_id : 0;
                    decimal CartPrice = cartinsert_params.cart_price > 0 ? cartinsert_params.cart_price : 0;
                    int CartQTY = cartinsert_params.cart_qty > 0 ? cartinsert_params.cart_qty : 0;
                    int is_stock = cartinsert_params.is_stock > 0 ? cartinsert_params.is_stock : 0;
                    decimal CartMRPrice = cartinsert_params.cart_mrprice > 0 ? cartinsert_params.cart_mrprice : 0;
                    decimal CartRPrice = cartinsert_params.cart_rprice > 0 ? cartinsert_params.cart_rprice : 0;
                    //decimal CartDPrice = cartinsert_params.cart_dprice > 0 ? cartinsert_params.cart_dprice : 0;
                    string CartDPrice_str = "";
                    if (string.IsNullOrWhiteSpace(cartinsert_params.cart_dprice.ToString()))
                    {
                        CartDPrice_str = "0";
                    }
                    else
                    {
                        CartDPrice_str = cartinsert_params.cart_dprice.ToString().Replace("₹", "").Trim();
                    }
                    Decimal.TryParse(CartDPrice_str, out decimal CartDPrice);

                    int CartColorCommonID = cartinsert_params.product_color_mst_id > 0 ? cartinsert_params.product_color_mst_id : 0;
                    int CartConfCommonID = cartinsert_params.product_size_mst_id > 0 ? cartinsert_params.product_size_mst_id : 0;
                    string CartItemInfoselect = string.IsNullOrWhiteSpace(cartinsert_params.product_item_remarks) ? "" : cartinsert_params.product_item_remarks;
                    string CartItemInfoselectIDS = string.IsNullOrWhiteSpace(cartinsert_params.product_item_remarks_ids) ? "" : cartinsert_params.product_item_remarks_ids;
                    string CartItemInfoCustomselect = string.IsNullOrWhiteSpace(cartinsert_params.product_item_custom_remarks) ? "" : cartinsert_params.product_item_custom_remarks;
                    string CartItemInfoCustomselectIDS = string.IsNullOrWhiteSpace(cartinsert_params.product_item_custom_remarks_ids) ? "" : cartinsert_params.product_item_custom_remarks_ids;
                    //string CatrItemCustomStatus = string.IsNullOrWhiteSpace(cartinsert_params.product_item_custom_remarks_status) ? "" : cartinsert_params.product_item_custom_remarks_status;
                    int CatrItemCustomStatus = cartinsert_params.product_item_custom_remarks_status > 0 ? 0 : cartinsert_params.product_item_custom_remarks_status;
                    decimal ExtraGold = cartinsert_params.extra_gold > 0 ? cartinsert_params.extra_gold : 0;
                    decimal ExtraGoldPrice = cartinsert_params.extra_gold_price > 0 ? cartinsert_params.extra_gold_price : 0;
                    int ItemAproxDayCommonID = cartinsert_params.ItemAproxDayCommonID > 0 ? cartinsert_params.ItemAproxDayCommonID : 0;
                    string devicetype = string.IsNullOrWhiteSpace(cartinsert_params.devicetype) ? "" : cartinsert_params.devicetype;
                    string devicename = string.IsNullOrWhiteSpace(cartinsert_params.devicename) ? "" : cartinsert_params.devicename;
                    string appversion = string.IsNullOrWhiteSpace(cartinsert_params.appversion) ? "" : cartinsert_params.appversion;

                    CommonHelpers objHelpers = new CommonHelpers();
                    string
                        ItemPlainGold = "",
                        MstType = "",
                        ItemOdSfx = "",
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.GET_ITEMDETAILS_BYITEMID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@ItemId", ItemId);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ItemPlainGold = dataReader["ItemPlainGold"] as string ?? string.Empty;
                                    MstType = dataReader["MstType"] as string ?? string.Empty;
                                    ItemOdSfx = dataReader["ItemOdSfx"] as string ?? string.Empty;
                                }
                            }
                        }
                    }



                    cmdQuery = DBCommands.CART_INSERT;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        // ItemLivePriceCalculation
                        IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

                        string
                            designkt = "",
                            making_per_gram = "";
                        decimal
                            Gold_price = 0,
                            total_goldvalue = 0,
                            finalGoldValue = 0,
                            total_labour = 0,
                            gst_price = 0,
                            item_price = 0,
                            total_price = 0;


                        if (ItemPlainGold == "Y")
                        {
                            if (MstType == "F")
                            {
                                CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                cartitempricedetaillistparams.DataID = DataId;
                                cartitempricedetaillistparams.ItemID = ItemId;
                                cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                if (cartItemPriceDetailList_gold.Count > 0)
                                {
                                    designkt = cartItemPriceDetailList_gold[0].ItemOdSfx;
                                    Gold_price = cartItemPriceDetailList_gold[0].pure_gold;
                                    total_goldvalue = cartItemPriceDetailList_gold[0].gold_ktprice;
                                    finalGoldValue = cartItemPriceDetailList_gold[0].gold_price;
                                    making_per_gram = cartItemPriceDetailList_gold[0].labour;
                                    total_labour = cartItemPriceDetailList_gold[0].labour_price;
                                    gst_price = cartItemPriceDetailList_gold[0].GST;
                                    item_price = cartItemPriceDetailList_gold[0].item_price;
                                    total_price = cartItemPriceDetailList_gold[0].total_price;
                                }
                            }
                            else
                            {
                                designkt = ItemOdSfx;
                            }
                        }

                        Decimal.TryParse(making_per_gram, out decimal making_per_gram_decimal);

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@NetIp", NetIp);
                        cmd.Parameters.AddWithValue("@CartRemark", CartRemark);
                        cmd.Parameters.AddWithValue("@CartId", CartId);
                        cmd.Parameters.AddWithValue("@ItemId", ItemId);
                        cmd.Parameters.AddWithValue("@CartPrice", CartPrice);
                        cmd.Parameters.AddWithValue("@CartQTY", CartQTY);
                        cmd.Parameters.AddWithValue("@is_stock", is_stock);
                        cmd.Parameters.AddWithValue("@CartMRPrice", CartMRPrice);
                        cmd.Parameters.AddWithValue("@CartRPrice", CartRPrice);
                        cmd.Parameters.AddWithValue("@CartDPrice", CartDPrice);
                        cmd.Parameters.AddWithValue("@CartColorCommonID", CartColorCommonID);
                        cmd.Parameters.AddWithValue("@CartConfCommonID", CartConfCommonID);
                        cmd.Parameters.AddWithValue("@CartItemInfoselect", CartItemInfoselect);
                        cmd.Parameters.AddWithValue("@CartItemInfoselectIDS", CartItemInfoselectIDS);
                        cmd.Parameters.AddWithValue("@CartItemInfoCustomselect", CartItemInfoCustomselect);
                        cmd.Parameters.AddWithValue("@CartItemInfoCustomselectIDS", CartItemInfoCustomselectIDS);
                        cmd.Parameters.AddWithValue("@CatrItemCustomStatus", CatrItemCustomStatus);
                        cmd.Parameters.AddWithValue("@ExtraGold", ExtraGold);
                        cmd.Parameters.AddWithValue("@ExtraGoldPrice", ExtraGoldPrice);
                        cmd.Parameters.AddWithValue("@ItemAproxDayCommonID", ItemAproxDayCommonID);
                        cmd.Parameters.AddWithValue("@devicetype", devicetype);
                        cmd.Parameters.AddWithValue("@devicename", devicename);
                        cmd.Parameters.AddWithValue("@appversion", appversion);
                        cmd.Parameters.AddWithValue("@SourceType", "APP");
                        cmd.Parameters.AddWithValue("@designkt", designkt);
                        cmd.Parameters.AddWithValue("@Gold_price", Gold_price);
                        cmd.Parameters.AddWithValue("@total_goldvalue", total_goldvalue);
                        cmd.Parameters.AddWithValue("@making_per_gram", making_per_gram_decimal);
                        cmd.Parameters.AddWithValue("@finalGoldValue", finalGoldValue);
                        cmd.Parameters.AddWithValue("@total_labour", total_labour);
                        cmd.Parameters.AddWithValue("@item_price", item_price);
                        cmd.Parameters.AddWithValue("@gst_price", gst_price);
                        cmd.Parameters.AddWithValue("@total_price", total_price);

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

        public async Task<ResponseDetails> CartStore(CartStoreParams cartstore_params)
        {
            CartStore cartstoreDetails = new CartStore();
            var responseDetails = new ResponseDetails();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataId = cartstore_params.data_id > 0 ? cartstore_params.data_id : 0;
                    string NetIp = string.IsNullOrWhiteSpace(cartstore_params.net_ip) ? "" : cartstore_params.net_ip;
                    string CartRemark = string.IsNullOrWhiteSpace(cartstore_params.cart_remark) ? "" : cartstore_params.cart_remark;
                    int CartId = cartstore_params.cart_id > 0 ? cartstore_params.cart_id : 0;
                    int ItemId = cartstore_params.item_id > 0 ? cartstore_params.item_id : 0;
                    decimal CartPrice = cartstore_params.cart_price > 0 ? cartstore_params.cart_price : 0;
                    int CartQTY = cartstore_params.cart_qty > 0 ? cartstore_params.cart_qty : 0;
                    decimal CartMRPrice = cartstore_params.cart_mrprice > 0 ? cartstore_params.cart_mrprice : 0;
                    decimal CartRPrice = cartstore_params.cart_rprice > 0 ? cartstore_params.cart_rprice : 0;
                    string CartDPrice_str = "";
                    if (string.IsNullOrWhiteSpace(cartstore_params.cart_dprice.ToString()))
                    {
                        CartDPrice_str = "0";
                    }
                    else
                    {
                        CartDPrice_str = cartstore_params.cart_dprice.ToString().Replace("₹", "").Trim();
                    }
                    Decimal.TryParse(CartDPrice_str, out decimal CartDPrice);

                    int CartColorCommonID = cartstore_params.product_color_mst_id > 0 ? cartstore_params.product_color_mst_id : 0;
                    int CartConfCommonID = cartstore_params.product_size_mst_id > 0 ? cartstore_params.product_size_mst_id : 0;
                    string CartItemInfoselect = string.IsNullOrWhiteSpace(cartstore_params.product_item_remarks) ? "" : cartstore_params.product_item_remarks;
                    string CatrItemCustomStatus = string.IsNullOrWhiteSpace(cartstore_params.product_item_custom_remarks_status) ? "" : cartstore_params.product_item_custom_remarks_status;
                    string CartItemInfoselectIDS = string.IsNullOrWhiteSpace(cartstore_params.product_item_remarks_ids) ? "" : cartstore_params.product_item_remarks_ids;

                    decimal ExtraGold = cartstore_params.extra_gold > 0 ? cartstore_params.extra_gold : 0;
                    decimal ExtraGoldPrice = cartstore_params.extra_gold_price > 0 ? cartstore_params.extra_gold_price : 0;
                    int ItemAproxDayCommonID = cartstore_params.ItemAproxDayCommonID > 0 ? cartstore_params.ItemAproxDayCommonID : 0;
                    string devicetype = string.IsNullOrWhiteSpace(cartstore_params.devicetype) ? "" : cartstore_params.devicetype;
                    string devicename = string.IsNullOrWhiteSpace(cartstore_params.devicename) ? "" : cartstore_params.devicename;
                    string appversion = string.IsNullOrWhiteSpace(cartstore_params.appversion) ? "" : cartstore_params.appversion;

                    CommonHelpers objHelpers = new CommonHelpers();
                    string
                        ItemPlainGold = "",
                        MstType = "",
                        ItemOdSfx = "",
                        resmessage = "";
                    int
                        ItemCtgCommonID = 0,
                        ItemGenderCommonID = 0,
                        ItemBrandCommonID = 0,
                        OpenCartID = 0;
                    decimal
                        ItemMetalWt = 0,
                        ItemGrossWt = 0,
                        CartItemMetalWt = 0;
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.GET_ITEMDETAILS_BYITEMID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@ItemId", ItemId);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ItemPlainGold = dataReader["ItemPlainGold"] as string ?? string.Empty;
                                    MstType = dataReader["MstType"] as string ?? string.Empty;
                                    ItemOdSfx = dataReader["ItemOdSfx"] as string ?? string.Empty;
                                    ItemCtgCommonID = dataReader["ItemCtgCommonID"] as int? ?? 0;
                                    ItemBrandCommonID = dataReader["ItemBrandCommonID"] as int? ?? 0;
                                    ItemGrossWt = dataReader["ItemGrossWt"] as decimal? ?? 0;
                                    ItemMetalWt = dataReader["ItemMetalWt"] as decimal? ?? 0;
                                    ItemGenderCommonID = dataReader["ItemGenderCommonID"] as int? ?? 0;
                                    OpenCartID = dataReader["OpenCartID"] as int? ?? 0;
                                }
                            }
                        }
                    }

                    cmdQuery = DBCommands.CARTSTORE;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        // ItemLivePriceCalculation
                        IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

                        // ItemLivePriceCalculation_NEW
                        IList<CartItemPriceDetailListing> cartItemPriceDetailList = new List<CartItemPriceDetailListing>();

                        string
                            designkt = "",
                            making_per_gram = "";
                        decimal
                            Gold_price = 0,
                            total_goldvalue = 0,
                            finalGoldValue = 0,
                            total_labour = 0,
                            gst_price = 0,
                            item_price = 0,
                            total_price = 0;

                        if (ItemPlainGold == "Y")
                        {
                            if (MstType == "F")
                            {
                                CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                cartitempricedetaillistparams.DataID = DataId;
                                cartitempricedetaillistparams.ItemID = ItemId;
                                cartitempricedetaillistparams.SizeID = CartConfCommonID;
                                cartitempricedetaillistparams.CategoryID = ItemCtgCommonID;
                                cartitempricedetaillistparams.ItemBrandCommonID = ItemBrandCommonID;
                                cartitempricedetaillistparams.ItemGrossWt = ItemGrossWt;
                                cartitempricedetaillistparams.ItemMetalWt = ItemMetalWt;
                                cartitempricedetaillistparams.ItemGenderCommonID = ItemGenderCommonID;
                                cartitempricedetaillistparams.IsWeightCalcRequired = 1;

                                cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);
                                if (cartItemPriceDetailList.Any())
                                {
                                    CartItemMetalWt = cartItemPriceDetailList[0].gold_wt;
                                }

                                if (OpenCartID > 0)
                                {
                                    cartitempricedetaillistparams.DataID = DataId;
                                    cartitempricedetaillistparams.ItemID = ItemId;
                                    cartitempricedetaillistparams.IsWeightCalcRequired = 0;
                                    cartItemPriceDetailList_gold = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                    if (cartItemPriceDetailList_gold.Any())
                                    {
                                        designkt = cartItemPriceDetailList_gold[0].ItemOdSfx;
                                        Gold_price = cartItemPriceDetailList_gold[0].pure_gold;
                                        total_goldvalue = cartItemPriceDetailList_gold[0].gold_ktprice;
                                        finalGoldValue = cartItemPriceDetailList_gold[0].gold_price;
                                        making_per_gram = cartItemPriceDetailList_gold[0].labour.Replace(" / gm", "");
                                        total_labour = cartItemPriceDetailList_gold[0].labour_price;
                                        gst_price = cartItemPriceDetailList_gold[0].GST;
                                        item_price = cartItemPriceDetailList_gold[0].item_price;
                                        total_price = cartItemPriceDetailList_gold[0].total_price;
                                    }
                                }
                                else
                                {
                                    if (cartItemPriceDetailList.Any())
                                    {
                                        designkt = cartItemPriceDetailList[0].ItemOdSfx;
                                        Gold_price = cartItemPriceDetailList[0].pure_gold;
                                        total_goldvalue = cartItemPriceDetailList[0].gold_ktprice;
                                        finalGoldValue = cartItemPriceDetailList[0].gold_price;
                                        making_per_gram = cartItemPriceDetailList[0].labour;
                                        total_labour = cartItemPriceDetailList[0].labour_price;
                                        gst_price = cartItemPriceDetailList_gold[0].GST;
                                        item_price = cartItemPriceDetailList_gold[0].item_price;
                                        total_price = cartItemPriceDetailList_gold[0].total_price;
                                    }
                                }
                            }
                            else
                            {
                                designkt = ItemOdSfx;
                                CartItemMetalWt = ItemMetalWt;
                            }
                        }

                        Decimal.TryParse(making_per_gram, out decimal making_per_gram_decimal);

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@NetIp", NetIp);
                        cmd.Parameters.AddWithValue("@CartRemark", CartRemark);
                        cmd.Parameters.AddWithValue("@CartId", CartId);
                        cmd.Parameters.AddWithValue("@ItemId", ItemId);
                        cmd.Parameters.AddWithValue("@CartPrice", CartPrice);
                        cmd.Parameters.AddWithValue("@CartQTY", CartQTY);
                        cmd.Parameters.AddWithValue("@CartMRPrice", CartMRPrice);
                        cmd.Parameters.AddWithValue("@CartRPrice", CartRPrice);
                        cmd.Parameters.AddWithValue("@CartDPrice", CartDPrice);
                        cmd.Parameters.AddWithValue("@CartColorCommonID", CartColorCommonID);
                        cmd.Parameters.AddWithValue("@CartConfCommonID", CartConfCommonID);
                        cmd.Parameters.AddWithValue("@CartItemInfoselect", CartItemInfoselect);
                        cmd.Parameters.AddWithValue("@CartItemInfoselectIDS", CartItemInfoselectIDS);
                        cmd.Parameters.AddWithValue("@CatrItemCustomStatus", CatrItemCustomStatus);
                        cmd.Parameters.AddWithValue("@ExtraGold", ExtraGold);
                        cmd.Parameters.AddWithValue("@ExtraGoldPrice", ExtraGoldPrice);
                        cmd.Parameters.AddWithValue("@ItemAproxDayCommonID", ItemAproxDayCommonID);
                        cmd.Parameters.AddWithValue("@devicetype", devicetype);
                        cmd.Parameters.AddWithValue("@devicename", devicename);
                        cmd.Parameters.AddWithValue("@appversion", appversion);
                        cmd.Parameters.AddWithValue("@SourceType", "APP");
                        cmd.Parameters.AddWithValue("@designkt", designkt);
                        cmd.Parameters.AddWithValue("@Gold_price", Gold_price);
                        cmd.Parameters.AddWithValue("@total_goldvalue", total_goldvalue);
                        cmd.Parameters.AddWithValue("@making_per_gram", making_per_gram_decimal);
                        cmd.Parameters.AddWithValue("@finalGoldValue", finalGoldValue);
                        cmd.Parameters.AddWithValue("@total_labour", total_labour);
                        cmd.Parameters.AddWithValue("@item_price", item_price);
                        cmd.Parameters.AddWithValue("@gst_price", gst_price);
                        cmd.Parameters.AddWithValue("@total_price", total_price);
                        cmd.Parameters.AddWithValue("@CartItemMetalWt", CartItemMetalWt);

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

        public int GoldDynamicPriceCart(GoldDynamicPriceCartParams golddynamicpricecartparams)
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

        public async Task<ResponseDetails> cartCheckOutAllotNew(CartCheckoutAllotNewParams cartcheckoutallotnew_params)
        {
            var responseDetails = new ResponseDetails();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int CartId = cartcheckoutallotnew_params.cart_id > 0 ? cartcheckoutallotnew_params.cart_id : 0;
                    int DataId = cartcheckoutallotnew_params.data_id > 0 ? cartcheckoutallotnew_params.data_id : 0;
                    int cart_order_data_id = cartcheckoutallotnew_params.cart_order_data_id > 0 ? cartcheckoutallotnew_params.cart_order_data_id : 0;
                    int cart_billing_data_id = cartcheckoutallotnew_params.cart_billing_data_id > 0 ? cartcheckoutallotnew_params.cart_billing_data_id : 0;
                    int cart_order_login_type_id = cartcheckoutallotnew_params.cart_order_login_type_id > 0 ? cartcheckoutallotnew_params.cart_order_login_type_id : 0;
                    int cart_billing_login_type_id = cartcheckoutallotnew_params.cart_billing_login_type_id > 0 ? cartcheckoutallotnew_params.cart_billing_login_type_id : 0;
                    string CartRemarks = string.IsNullOrWhiteSpace(cartcheckoutallotnew_params.cart_remarks) ? "" : cartcheckoutallotnew_params.cart_remarks;
                    DateTime cart_delivery_date = cartcheckoutallotnew_params.cart_delivery_date;

                    string cart_string = string.IsNullOrWhiteSpace(cartcheckoutallotnew_params.cart_string) ? "" : cartcheckoutallotnew_params.cart_string;
                    var cartArray = cart_string.Split(',')
                          .Where(item => !string.IsNullOrWhiteSpace(item))
                          .ToArray();
                    cart_string = string.Join(",", cartArray);

                    var ordertypeid = int.TryParse(cartcheckoutallotnew_params.ordertypeid.ToString(), out int parsedValue) ? parsedValue : 2498;
                    string devicetype = string.IsNullOrWhiteSpace(cartcheckoutallotnew_params.devicetype) ? "" : cartcheckoutallotnew_params.devicetype;
                    string devicename = string.IsNullOrWhiteSpace(cartcheckoutallotnew_params.devicename) ? "" : cartcheckoutallotnew_params.devicename;
                    string appversion = string.IsNullOrWhiteSpace(cartcheckoutallotnew_params.appversion) ? "" : cartcheckoutallotnew_params.appversion;

                    CommonHelpers objHelpers = new CommonHelpers();
                    string
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400,
                        NewCartId = 0;

                    // Checks items are from solitaire collection or not,
                    // If so then checks total of those collection's items is grether than or equal to 10,00,000
                    IList<CheckItemIsSolitaireComboListing> checkitemissolitairecomboList = new List<CheckItemIsSolitaireComboListing>();
                    CheckItemIsSolitaireComboParams checkitemissolitairecomboparams = new CheckItemIsSolitaireComboParams();
                    checkitemissolitairecomboparams.cart_id = CartId;
                    checkitemissolitairecomboparams.cart_billing_data_id = cart_billing_data_id;
                    checkitemissolitairecomboparams.ordertypeid = ordertypeid;
                    checkitemissolitairecomboparams.implodeCartMstItemIds = cart_string;
                    checkitemissolitairecomboList = objHelpers.CheckItemIsSolitaireCombo(checkitemissolitairecomboparams);

                    CheckItemIsSolitaireComboListing isSolitaireCombo = new CheckItemIsSolitaireComboListing();
                    if (checkitemissolitairecomboList.Count > 0)
                    {
                        isSolitaireCombo = checkitemissolitairecomboList[0];
                    }

                    bool is_valid = isSolitaireCombo.is_valid == 0 ? true : false;
                    // Check if the combo is invalid
                    if (!is_valid)
                    {
                        // If both minimum amount and cart price are 0
                        if (isSolitaireCombo.min_amount == 0 && isSolitaireCombo.cart_price == 0)
                        {
                            resmessage = "Cart is empty, Please add items into cart and then checkout.";
                        }
                        else
                        {
                            resmessage = string.Format("Your cart price ({0:N0}) should be minimum {1:N0}.", isSolitaireCombo.cart_price, isSolitaireCombo.min_amount);
                        }
                        // return Ok(new { success = false, message = msg, status_code = 200 });
                        responseDetails.success = false;
                        responseDetails.message = resmessage;
                        responseDetails.status = "200";
                        return responseDetails;
                    }

                    string cmdQuery = DBCommands.CARTCHECKOUTALLOTNEW;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@CartId", CartId);
                        cmd.Parameters.AddWithValue("@cart_order_data_id", cart_order_data_id);
                        cmd.Parameters.AddWithValue("@cart_billing_data_id", cart_billing_data_id);
                        cmd.Parameters.AddWithValue("@cart_order_login_type_id", cart_order_login_type_id);
                        cmd.Parameters.AddWithValue("@cart_billing_login_type_id", cart_billing_login_type_id);
                        cmd.Parameters.AddWithValue("@CartRemark", CartRemarks);
                        cmd.Parameters.AddWithValue("@cart_delivery_date", cart_delivery_date);
                        cmd.Parameters.AddWithValue("@cart_string", cart_string);
                        cmd.Parameters.AddWithValue("@ordertypeid", ordertypeid);
                        cmd.Parameters.AddWithValue("@devicetype", devicetype);
                        cmd.Parameters.AddWithValue("@devicename", devicename);
                        cmd.Parameters.AddWithValue("@appversion", appversion);
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
                                        NewCartId = firstRow["NewCartId"] as int? ?? 0;

                                        if (ds.Tables.Count > 1 && NewCartId > 0)
                                        {
                                            int
                                                tmpItemID = 0,
                                                tmpCartItemID = 0,
                                                tmpCartMstID = 0,
                                                tmpCartQty = 0;

                                            for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                                            {
                                                tmpItemID = ds.Tables[1].Rows[j]["ItemID"] as int? ?? 0;
                                                tmpCartItemID = ds.Tables[1].Rows[j]["CartItemID"] as int? ?? 0;
                                                tmpCartMstID = ds.Tables[1].Rows[j]["CartMstID"] as int? ?? 0;
                                                tmpCartQty = ds.Tables[1].Rows[j]["CartQty"] as int? ?? 0;

                                                IList<CartItemPriceDetailListing> cartItemPriceDetailList_gold = new List<CartItemPriceDetailListing>();

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

                                                CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                                cartitempricedetaillistparams.DataID = DataId;
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
                                                golddynamicpricecartparams.DataId = DataId;
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

        public static async Task<string> SendHttpRequest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throws an exception if status code is not 2xx
                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> SendSoapRequest(string url, string soapXml)
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

        public int SaveDiamondBookServiceResponse(CartCheckoutNoAllotNewParamsPart2 cartcheckoutnoallotnew_params)
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

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTNEW_SAVEDIABOOKRESPONSE;
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

        public int SaveCartMstItemPrices(CARTCHECKOUTNOALLOTNEW_UPDATE_CARTMSTITEM_PRICESParams cartcheckouotallotnew_update_cartmstitem_prices_params)
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

        public int SaveSolitaireStatus(CartCheckoutNoAllotWebSaveSolitaireStatusParams cartcheckoutnoallotweb_save_solitairestatus_params)
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

        public int UpdateCartMst(CartCheckoutNoAllotWebUpdateCartMstParams cartcheckoutnoallotweb_update_cartmst_params)
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

        public int SaveCartStatusMst(CartCheckoutNoAllotWebSaveCartStatusMstParams cartcheckoutnoallotweb_save_cartstatusmst_params)
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

        public int DeleteAndReassignCart(CartCheckoutNoAllotWebDeleteAndReassignCartParams cartcheckoutnoallotweb_deletandreassigncart_params)
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

        public int DeleteCartMst(CartCheckoutNoAllotWebDeleteCartMstParams cartcheckoutnoallotweb_deletcartmst_params)
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

        public class NoAllotResponse
        {
            public int status { get; set; }
            public int status_code { get; set; }
            public string message { get; set; }
        }

        public NoAllotResponse SaveCartCheckoutNoAllotNew_CartMstNew(CartCheckoutNoAllotNewSaveNewCartMstParams cartcheckoutnoallotnew_savenew_cartmst_params)
        {
            var objNoAllotResponse = new NoAllotResponse();
            objNoAllotResponse.status = 0;
            objNoAllotResponse.status_code = 400;
            objNoAllotResponse.message = "";

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = cartcheckoutnoallotnew_savenew_cartmst_params.DataID;
                    int CartID = cartcheckoutnoallotnew_savenew_cartmst_params.CartID;
                    int OrderTypeID = cartcheckoutnoallotnew_savenew_cartmst_params.OrderTypeID;
                    int cart_order_login_type_id = cartcheckoutnoallotnew_savenew_cartmst_params.cart_order_login_type_id;
                    int cart_billing_login_type_id = cartcheckoutnoallotnew_savenew_cartmst_params.cart_billing_login_type_id;
                    int cart_order_data_id = cartcheckoutnoallotnew_savenew_cartmst_params.cart_order_data_id;
                    int cart_billing_data_id = cartcheckoutnoallotnew_savenew_cartmst_params.cart_billing_data_id;
                    string cart_remarks = cartcheckoutnoallotnew_savenew_cartmst_params.cart_remarks;
                    string cart_delivery_date = cartcheckoutnoallotnew_savenew_cartmst_params.cart_delivery_date;
                    string itemall = cartcheckoutnoallotnew_savenew_cartmst_params.itemall;
                    string SourceType = cartcheckoutnoallotnew_savenew_cartmst_params.SourceType;
                    string devicetype = cartcheckoutnoallotnew_savenew_cartmst_params.devicetype;
                    string devicename = cartcheckoutnoallotnew_savenew_cartmst_params.devicename;
                    string appversion = cartcheckoutnoallotnew_savenew_cartmst_params.appversion;
                    string APP_ENV = cartcheckoutnoallotnew_savenew_cartmst_params.APP_ENV;

                    int oroseven_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.oroseven_cnt;
                    string orosevenString = cartcheckoutnoallotnew_savenew_cartmst_params.orosevenString;
                    int orofifty_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.orofifty_cnt;
                    string orofiftyString = cartcheckoutnoallotnew_savenew_cartmst_params.orofiftyString;
                    int orohours_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.orohours_cnt;
                    string orohoursString = cartcheckoutnoallotnew_savenew_cartmst_params.orohoursString;
                    int orotwentyone_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.orotwentyone_cnt;
                    string orotwentyoneString = cartcheckoutnoallotnew_savenew_cartmst_params.orotwentyoneString;
                    int orofive_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.orofive_cnt;
                    string orofiveString = cartcheckoutnoallotnew_savenew_cartmst_params.orofiveString;

                    int kisnaseven_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kisnaseven_cnt;
                    string kisnasevenString = cartcheckoutnoallotnew_savenew_cartmst_params.kisnasevenString;
                    int kisnafifty_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kisnafifty_cnt;
                    string kisnafiftyString = cartcheckoutnoallotnew_savenew_cartmst_params.kisnafiftyString;
                    int kisnahours_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kisnahours_cnt;
                    string kisnahoursString = cartcheckoutnoallotnew_savenew_cartmst_params.kisnahoursString;
                    int kisnatwentyone_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kisnatwentyone_cnt;
                    string kisnatwentyoneString = cartcheckoutnoallotnew_savenew_cartmst_params.kisnatwentyoneString;
                    int kisnafive_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kisnafive_cnt;
                    string kisnafiveString = cartcheckoutnoallotnew_savenew_cartmst_params.kisnafiveString;

                    int kgseven_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kgseven_cnt;
                    string kgsevenString = cartcheckoutnoallotnew_savenew_cartmst_params.kgsevenString;
                    int kgfifty_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kgfifty_cnt;
                    string kgfiftyString = cartcheckoutnoallotnew_savenew_cartmst_params.kgfiftyString;
                    int kghours_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kghours_cnt;
                    string kghoursString = cartcheckoutnoallotnew_savenew_cartmst_params.kghoursString;
                    int kgtwentyone_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kgtwentyone_cnt;
                    string kgtwentyoneString = cartcheckoutnoallotnew_savenew_cartmst_params.kgtwentyoneString;
                    int kgfive_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.kgfive_cnt;
                    string kgfiveString = cartcheckoutnoallotnew_savenew_cartmst_params.kgfiveString;

                    int silverseven_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.silverseven_cnt;
                    string silversevenString = cartcheckoutnoallotnew_savenew_cartmst_params.silversevenString;
                    int silverfifty_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.silverfifty_cnt;
                    string silverfiftyString = cartcheckoutnoallotnew_savenew_cartmst_params.silverfiftyString;
                    int silverhours_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.silverhours_cnt;
                    string silverhoursString = cartcheckoutnoallotnew_savenew_cartmst_params.silverhoursString;
                    int silvertwentyone_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.silvertwentyone_cnt;
                    string silvertwentyoneString = cartcheckoutnoallotnew_savenew_cartmst_params.silvertwentyoneString;
                    int silverfive_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.silverfive_cnt;
                    string silverfiveString = cartcheckoutnoallotnew_savenew_cartmst_params.silverfiveString;

                    int illumineseven_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.illumineseven_cnt;
                    string illuminesevenString = cartcheckoutnoallotnew_savenew_cartmst_params.illuminesevenString;
                    int illuminefifty_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.illuminefifty_cnt;
                    string illuminefiftyString = cartcheckoutnoallotnew_savenew_cartmst_params.illuminefiftyString;
                    int illuminehours_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.illuminehours_cnt;
                    string illuminehoursString = cartcheckoutnoallotnew_savenew_cartmst_params.illuminehoursString;
                    int illuminetwentyone_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.illuminetwentyone_cnt;
                    string illuminetwentyoneString = cartcheckoutnoallotnew_savenew_cartmst_params.illuminetwentyoneString;
                    int illuminefive_cnt = cartcheckoutnoallotnew_savenew_cartmst_params.illuminefive_cnt;
                    string illuminefiveString = cartcheckoutnoallotnew_savenew_cartmst_params.illuminefiveString;

                    dbConnection.Open();

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTNEW_SAVECARTMST;
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
                        cmd.Parameters.AddWithValue("@itemall", itemall);
                        cmd.Parameters.AddWithValue("@devicetype", devicetype);
                        cmd.Parameters.AddWithValue("@devicename", devicename);
                        cmd.Parameters.AddWithValue("@appversion", appversion);
                        cmd.Parameters.AddWithValue("@SourceType", SourceType);
                        cmd.Parameters.AddWithValue("@APP_ENV", APP_ENV);

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

                        cmd.Parameters.AddWithValue("@illumineseven_cnt", illumineseven_cnt);
                        cmd.Parameters.AddWithValue("@illuminesevenString", illuminesevenString);
                        cmd.Parameters.AddWithValue("@illuminefifty_cnt", illuminefifty_cnt);
                        cmd.Parameters.AddWithValue("@illuminefiftyString", illuminefiftyString);
                        cmd.Parameters.AddWithValue("@illuminehours_cnt", illuminehours_cnt);
                        cmd.Parameters.AddWithValue("@illuminehoursString", illuminehoursString);
                        cmd.Parameters.AddWithValue("@illuminetwentyone_cnt", illuminetwentyone_cnt);
                        cmd.Parameters.AddWithValue("@illuminetwentyoneString", illuminetwentyoneString);
                        cmd.Parameters.AddWithValue("@illuminefive_cnt", illuminefive_cnt);
                        cmd.Parameters.AddWithValue("@illuminefiveString", illuminefiveString);

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
                                        // Helper
                                        CommonHelpers objHelpers = new CommonHelpers();

                                        // Error lists
                                        List<string> error = new List<string>();
                                        List<bool> errStatus = new List<bool>();

                                        int resstatus = ds.Tables[0].Rows[0]["resstatus"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["resstatus"]) : 0;
                                        int returntype = ds.Tables[0].Rows[0]["returntype"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["returntype"]) : 0;
                                        int resstatuscode = ds.Tables[0].Rows[0]["resstatuscode"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["resstatuscode"]) : 0;
                                        string resmessage = ds.Tables[0].Rows[0]["resmessage"] != DBNull.Value ? Convert.ToString(ds.Tables[0].Rows[0]["resmessage"]) : string.Empty;

                                        if (resstatus == 1)
                                        {
                                            // $ordertypeid IN [6596, 2499, 20397]
                                            if (returntype == 0)
                                            {
                                                if (ds.Tables.Count > 1)
                                                {
                                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                                    {
                                                        var errDetailRow = ds.Tables[1].Rows[i];
                                                        int errstusVal = errDetailRow["errstus"] != DBNull.Value ? Convert.ToInt32(errDetailRow["errstus"]) : 0;
                                                        string errmsgVal = errDetailRow["errstus"] != DBNull.Value ? Convert.ToString(errDetailRow["errmsg"]) : string.Empty;

                                                        error.Add(errmsgVal);
                                                        errStatus.Add(errstusVal == 1 ? true : false);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // $ordertypeid NOT IN [6596, 2499, 20397]
                                                if (ds.Tables.Count > 1)
                                                {
                                                    int tmpCartID = 0, totalrows = 0;

                                                    string orderCheckoutTypeMsg = "";
                                                    string orderCheckoutDataMsg = "";
                                                    string newcartno = "";

                                                    // START - foreach ($cartidarray as $key => $cartid)
                                                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                                    {
                                                        var tmprow = ds.Tables[1].Rows[i];
                                                        tmpCartID = tmprow["CartID"] as int? ?? 0;
                                                        totalrows = tmprow["TotalRows"] as int? ?? 0;

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
                                                                tmpItemSoliterSts = "",
                                                                tmpItemIllumine = "";

                                                            if (ds.Tables.Count > 3)
                                                            {
                                                                var filteredRows = ds.Tables[3].AsEnumerable().Where(x => x.Field<int>("CartID") == tmpCartID);
                                                                DataTable dt = filteredRows.Any()
                                                                    ? filteredRows.CopyToDataTable()
                                                                    : ds.Tables[3].Clone();

                                                                for (int j = 0; j < dt.Rows.Count; j++)
                                                                {
                                                                    var tbl3Row = dt.Rows[j];

                                                                    tmpItemID = tbl3Row["ItemID"] != DBNull.Value ? Convert.ToInt32(tbl3Row["ItemID"]) : 0;
                                                                    tmpCartItemID = tbl3Row["CartItemID"] != DBNull.Value ? Convert.ToInt32(tbl3Row["CartItemID"]) : 0;
                                                                    tmpItemCtgCommonID = tbl3Row["ItemCtgCommonID"] != DBNull.Value ? Convert.ToInt32(tbl3Row["ItemCtgCommonID"]) : 0;
                                                                    tmpCartMstID = tbl3Row["CartMstID"] != DBNull.Value ? Convert.ToInt32(tbl3Row["CartMstID"]) : 0;
                                                                    tmpCartQty = tbl3Row["CartQty"] != DBNull.Value ? Convert.ToInt32(tbl3Row["CartQty"]) : 0;

                                                                    tmpItemPlainGold = tbl3Row["ItemPlainGold"] as string ?? string.Empty;
                                                                    tmpItemSoliterSts = tbl3Row["ItemSoliterSts"] as string ?? string.Empty;
                                                                    tmpItemIllumine = tbl3Row["item_illumine"] as string ?? string.Empty;

                                                                    if ((tmpItemSoliterSts == "Y" || tmpItemIllumine == "Y") && (tmpItemCtgCommonID == 4 || tmpItemCtgCommonID == 21 || tmpItemCtgCommonID == 22 || tmpItemCtgCommonID == 25))
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
                                                                    var filteredRowsNew = dt.AsEnumerable().Where(x => (x.Field<string>("ItemSoliterSts") == "Y" || x.Field<string>("item_illumine") == "Y") && new[] { 4, 21, 22, 25 }.Contains(x.Field<int>("ItemCtgCommonID")));

                                                                    DataTable dtNew = filteredRowsNew.Any()
                                                                        ? filteredRowsNew.CopyToDataTable()
                                                                        : new DataTable();

                                                                    for (int j = 0; j < dtNew.Rows.Count; j++)
                                                                    {
                                                                        var tbl3RowDetails = dtNew.Rows[j];

                                                                        CartSoliStkNoData = tbl3RowDetails["CartSoliStkNoData"] as string ?? string.Empty;
                                                                        tmpCartItemID = tbl3RowDetails["CartItemID"] as int? ?? 0;
                                                                        tmpItemID = tbl3RowDetails["ItemID"] as int? ?? 0;

                                                                        // API URL
                                                                        string apiUrl = $"https://service.hk.co/StockApprove?user={_customerID}&pkts={CartSoliStkNoData}&remark={tmpCartItemID}";

                                                                        // Send HTTP Request
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
                                                                    // END FOR LOOP - dtNew.Rows.Count
                                                                }
                                                                // ELSE PART
                                                            }

                                                            if (!errStatus.Contains(false))
                                                            {
                                                                // UPDATE T_CART_MST
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
                                                                cartcheckoutnoallotweb_update_cartmst_params.goldcnt = 0;
                                                                cartcheckoutnoallotweb_update_cartmst_params.goldvalue = 0;
                                                                cartcheckoutnoallotweb_update_cartmst_params.goldpremium = 0;

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
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (returntype == 0)
                                            {
                                                objNoAllotResponse.status = resstatus;
                                                objNoAllotResponse.status_code = resstatuscode;
                                                objNoAllotResponse.message = resmessage;
                                                return objNoAllotResponse;
                                            }
                                        }

                                        CartCheckoutNoAllotWebSaveCartStatusMstParams cartcheckoutnoallotweb_save_cartstatusmst_params = new CartCheckoutNoAllotWebSaveCartStatusMstParams();
                                        if (returntype != 0)
                                        {
                                            cartcheckoutnoallotweb_save_cartstatusmst_params.DataID = DataID;
                                            cartcheckoutnoallotweb_save_cartstatusmst_params.CartID = CartID;
                                            cartcheckoutnoallotweb_save_cartstatusmst_params.Stage = "Cart Process End-14";
                                            cartcheckoutnoallotweb_save_cartstatusmst_params.SourceType = SourceType;
                                            SaveCartStatusMst(cartcheckoutnoallotweb_save_cartstatusmst_params);
                                        }

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
                                        objNoAllotResponse.status = 0;
                                        objNoAllotResponse.status_code = 201;
                                        objNoAllotResponse.message = $"error: {ex.Message}";
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

        public async Task<ResponseDetails> CartCheckoutNoAllotNew(CartCheckoutNoAllotNewParams cartcheckoutnoallotnew_params)
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
                    string APP_ENV = "staging";

                    int CartId = cartcheckoutnoallotnew_params.cart_id > 0 ? cartcheckoutnoallotnew_params.cart_id : 0;
                    int DataId = cartcheckoutnoallotnew_params.data_id > 0 ? cartcheckoutnoallotnew_params.data_id : 0;

                    int cart_order_data_id = cartcheckoutnoallotnew_params.cart_order_data_id > 0 ? cartcheckoutnoallotnew_params.cart_order_data_id : 0;
                    int cart_billing_data_id = cartcheckoutnoallotnew_params.cart_billing_data_id > 0 ? cartcheckoutnoallotnew_params.cart_billing_data_id : 0;
                    int cart_order_login_type_id = cartcheckoutnoallotnew_params.cart_order_login_type_id > 0 ? cartcheckoutnoallotnew_params.cart_order_login_type_id : 0;
                    int cart_billing_login_type_id = cartcheckoutnoallotnew_params.cart_billing_login_type_id > 0 ? cartcheckoutnoallotnew_params.cart_billing_login_type_id : 0;
                    string cart_remarks = string.IsNullOrWhiteSpace(cartcheckoutnoallotnew_params.cart_remarks) ? "" : cartcheckoutnoallotnew_params.cart_remarks;

                    string cart_delivery_date = "";
                    DateTime parsed_cart_delivery_date;
                    bool isValidCartDeliveryDate = DateTime.TryParseExact(cartcheckoutnoallotnew_params.cart_delivery_date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsed_cart_delivery_date);
                    if (isValidCartDeliveryDate)
                    {
                        cart_delivery_date = parsed_cart_delivery_date.ToString("yyyy-MM-dd");
                    }

                    string itemall = string.IsNullOrWhiteSpace(cartcheckoutnoallotnew_params.itemall) ? "" : cartcheckoutnoallotnew_params.itemall;
                    // int ordertypeid = cartcheckoutnoallotnew_params.ordertypeid;
                    var ordertypeid = int.TryParse(cartcheckoutnoallotnew_params.ordertypeid.ToString(), out int parsedValue) ? parsedValue : 2498;

                    string devicetype = string.IsNullOrWhiteSpace(cartcheckoutnoallotnew_params.devicetype) ? "" : cartcheckoutnoallotnew_params.devicetype;
                    string devicename = string.IsNullOrWhiteSpace(cartcheckoutnoallotnew_params.devicename) ? "" : cartcheckoutnoallotnew_params.devicename;
                    string appversion = string.IsNullOrWhiteSpace(cartcheckoutnoallotnew_params.appversion) ? "" : cartcheckoutnoallotnew_params.appversion;

                    CommonHelpers objHelpers = new CommonHelpers();
                    brand_data datas = cartcheckoutnoallotnew_params.data;

                    List<string> kisnaFifty = new List<string>();
                    List<string> kisnaHours = new List<string>();
                    List<string> kisnaSeven = new List<string>();
                    List<string> kisnaTwentyone = new List<string>();
                    List<string> kisnaFive = new List<string>();

                    // Check if 'kisna_data' exists and is not empty
                    if (datas != null && datas.kisna_data != null)
                    {
                        var kisnaData = datas.kisna_data;
                        kisnaFifty = kisnaData.fifteen_day;
                        kisnaHours = kisnaData.hours;
                        kisnaSeven = kisnaData.seven_day;
                        kisnaTwentyone = kisnaData.twentyone_day;
                        kisnaFive = kisnaData.five_day;
                    }

                    List<string> oroFifteen = new List<string>();
                    //List<string> oroFifty = new List<string>();
                    List<string> oroHours = new List<string>();
                    List<string> oroSeven = new List<string>();
                    List<string> oroTwentyOne = new List<string>();
                    List<string> oroFive = new List<string>();

                    // Check if 'oro_data' exists and is not empty
                    int oro_data_cnt = 0;
                    if (datas != null && datas.oro_data != null)
                    {
                        var oroData = datas.oro_data;
                        oro_data_cnt = datas.oro_data.fifteen_day.Count + datas.oro_data.hours.Count + datas.oro_data.seven_day.Count + datas.oro_data.twentyone_day.Count + datas.oro_data.five_day.Count;
                        oroFifteen = oroData.fifteen_day;
                        oroHours = oroData.hours;
                        oroSeven = oroData.seven_day;
                        oroTwentyOne = oroData.twentyone_day;
                        oroFive = oroData.five_day;
                    }

                    List<string> kgFifty = new List<string>();
                    List<string> kgHours = new List<string>();
                    List<string> kgSeven = new List<string>();
                    List<string> kgTwentyOne = new List<string>();
                    List<string> kgFive = new List<string>();

                    // Check if 'kisna_gold_data' exists and is not empty
                    if (datas != null && datas.kisna_gold_data != null)
                    {
                        var kgData = datas.kisna_gold_data;
                        kgFifty = kgData.fifteen_day;
                        kgHours = kgData.hours;
                        kgSeven = kgData.seven_day;
                        kgTwentyOne = kgData.twentyone_day;
                        kgFive = kgData.five_day;
                    }

                    //Silver Coin 999 Data
                    List<string> silverFifteen = new List<string>();
                    //List<string> silverFifty = new List<string>();
                    List<string> silverHours = new List<string>();
                    List<string> silverSeven = new List<string>();
                    List<string> silverTwentyOne = new List<string>();
                    List<string> silverFive = new List<string>();

                    // Check if 'silver_data' exists and is not empty
                    if (datas != null && datas.silver_data != null)
                    {
                        var silverData = datas.silver_data;
                        silverFifteen = silverData.fifteen_day;
                        silverHours = silverData.hours;
                        silverSeven = silverData.seven_day;
                        silverTwentyOne = silverData.twentyone_day;
                        silverFive = silverData.five_day;
                    }

                    // Illumine Collection
                    List<string> illumineFifteen = new List<string>();
                    //List<string> illumineFifty = new List<string>();
                    List<string> illumineHours = new List<string>();
                    List<string> illumineSeven = new List<string>();
                    List<string> illumineTwentyOne = new List<string>();
                    List<string> illumineFive = new List<string>();

                    // Check if 'illumine_data' exists and is not empty
                    if (datas != null && datas.illumine_data != null)
                    {
                        var illumineData = datas.illumine_data;
                        illumineFifteen = illumineData.fifteen_day;
                        illumineHours = illumineData.hours;
                        illumineSeven = illumineData.seven_day;
                        illumineTwentyOne = illumineData.twentyone_day;
                        illumineFive = illumineData.five_day;
                    }

                    var cartMstItemIds = new List<string>();
                    cartMstItemIds.AddRange(kisnaFifty);
                    cartMstItemIds.AddRange(kisnaHours);
                    cartMstItemIds.AddRange(kisnaSeven);
                    cartMstItemIds.AddRange(kisnaTwentyone);
                    cartMstItemIds.AddRange(kisnaFive);

                    // cartMstItemIds.AddRange(oroFifty);
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

                    // cartMstItemIds.AddRange(silverFifty);
                    cartMstItemIds.AddRange(silverFifteen);
                    cartMstItemIds.AddRange(silverHours);
                    cartMstItemIds.AddRange(silverSeven);
                    cartMstItemIds.AddRange(silverTwentyOne);
                    cartMstItemIds.AddRange(silverFive);

                    // cartMstItemIds.AddRange(illumineFifty);
                    cartMstItemIds.AddRange(illumineFifteen);
                    cartMstItemIds.AddRange(illumineHours);
                    cartMstItemIds.AddRange(illumineSeven);
                    cartMstItemIds.AddRange(illumineTwentyOne);
                    cartMstItemIds.AddRange(illumineFive);

                    var filteredCartMstItemIds = cartMstItemIds.Where(id => !string.IsNullOrEmpty(id)).ToList();
                    string implodeCartMstItemIds = string.Join(",", filteredCartMstItemIds);

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

                    bool is_valid = isSolitaireCombo.is_valid == 0 ? true : false;
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

                    is_valid = isKisnaPremium.is_valid == 0 ? true : false;
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

                    string cmdQuery = DBCommands.CARTCHECKOUTNOALLOTNEW;
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
                        cmd.Parameters.AddWithValue("@APP_ENV", "");

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
                                        resstatuscode = firstRow["resstatuscode"] as int? ?? 200;
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

                                            if (returntype == 1) { }
                                            else
                                            {
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
                                                        CartSoliStkNoData = dt.Rows[i]["CartSoliStkNoData"] != DBNull.Value ? Convert.ToString(dt.Rows[i]["CartSoliStkNoData"]) : string.Empty;
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
                                                tmpItemGenderCommonID = 0;
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
                                                tmpisCOOInserted = "",
                                                tmpDataItemInfo = "",
                                                tmpItemIsSRP = "",
                                                tmpItemIsMRP = "",
                                                tmpitem_illumine = "";

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

                                                tmpItemGrossWt = rowDetails["ItemGrossWt"] != DBNull.Value ? Convert.ToDecimal(rowDetails["ItemGrossWt"]) : 0;
                                                tmpItemMetalWt = rowDetails["ItemMetalWt"] != DBNull.Value ? Convert.ToDecimal(rowDetails["ItemMetalWt"]) : 0;

                                                tmpCartMRPrice = rowDetails["CartMRPrice"] != DBNull.Value ? Convert.ToDecimal(rowDetails["CartMRPrice"]) : 0;
                                                tmpCartPrice = rowDetails["CartPrice"] != DBNull.Value ? Convert.ToDecimal(rowDetails["CartPrice"]) : 0;
                                                tmpCartDPrice = rowDetails["CartDPrice"] != DBNull.Value ? Convert.ToDecimal(rowDetails["CartDPrice"]) : 0;
                                                tmpCartItemMetalWt = rowDetails["CartItemMetalWt"] != DBNull.Value ? Convert.ToDecimal(rowDetails["CartItemMetalWt"]) : 0;

                                                tmpItemPlainGold = rowDetails["ItemPlainGold"] != DBNull.Value ? Convert.ToString(rowDetails["ItemPlainGold"]) : string.Empty;
                                                tmpMstType = rowDetails["MstType"] != DBNull.Value ? Convert.ToString(rowDetails["MstType"]) : string.Empty;
                                                tmpisCOOInserted = rowDetails["isCOOInserted"] != DBNull.Value ? Convert.ToString(rowDetails["isCOOInserted"]) : string.Empty;
                                                tmpDataItemInfo = rowDetails["DataItemInfo"] != DBNull.Value ? Convert.ToString(rowDetails["DataItemInfo"]) : string.Empty;
                                                tmpItemIsSRP = rowDetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowDetails["ItemIsSRP"]) : string.Empty;
                                                tmpitem_illumine = rowDetails["item_illumine"] != DBNull.Value ? Convert.ToString(rowDetails["item_illumine"]) : string.Empty;

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

                                                // order typeid not in (6596, 2499, 20397)
                                                if (returntype == 1)
                                                {
                                                    if (tmpMstType == "F")
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

                                                        if (tmpItemIsMRP == "Y" && tmpitem_illumine == "N" && tmpisCOOInserted == "N")
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
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 3;
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
                                                            total_price = Math.Round(cartItemPriceDetailList_gold[0].total_price, 0);
                                                            dp_final_price = Math.Round(cartItemPriceDetailList_gold[0].dp_final_price, 0);
                                                            dp_maring_percent = cartItemPriceDetailList_gold[0].dp_maring_percent;
                                                            diamond_price = cartItemPriceDetailList_gold[0].diamond_price;
                                                            gold_wt = cartItemPriceDetailList_gold[0].gold_wt;
                                                            gold_price = cartItemPriceDetailList_gold[0].pure_gold;
                                                            total_goldvalue = cartItemPriceDetailList_gold[0].gold_ktprice;
                                                            finalGoldValue = cartItemPriceDetailList_gold[0].gold_price;
                                                            platinum_wt = cartItemPriceDetailList_gold[0].platinum_wt;
                                                            platinum = cartItemPriceDetailList_gold[0].platinum;
                                                            platinum_price = cartItemPriceDetailList_gold[0].platinum_price;
                                                            total_labour = cartItemPriceDetailList_gold[0].labour_price;
                                                        }

                                                        if (tmpItemPlainGold == "Y" && tmpisCOOInserted == "N")
                                                        {
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemID = tmpCartItemID;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.CartMRPrice = total_price;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.CartPrice = total_price;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.CartDPrice = dp_final_price;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.FranMarginCurDP = dp_maring_percent;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 2;
                                                            SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                        }
                                                        else
                                                        {
                                                            if (tmpDataItemInfo == "Y" && tmpItemIsSRP == "Y")
                                                            {
                                                                tmpItemIsMRP = "Y";
                                                            }
                                                            if (((tmpItemIsMRP == "Y" && tmpisCOOInserted == "N") || tmpItemIsSRP == "Y") && tmpitem_illumine == "N")
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
                                                                }

                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemID = tmpCartItemID;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartMRPrice = total_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartPrice = total_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartDPrice = dp_final_price;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FranMarginCurDP = dp_maring_percent;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 3;
                                                                SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);

                                                            }
                                                            else
                                                            {
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemID = tmpCartItemID;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartMRPrice = tmpCartMRPrice;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartPrice = tmpCartPrice;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartDPrice = tmpCartDPrice;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemMetalWt = tmpCartItemMetalWt;
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 4;
                                                                SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // order typeid in (6596, 2499, 20397)
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

                                                    // order typeid in (6596, 2499, 20397)
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

                                                    if (tmpMstType == "F" && tmpItemPlainGold == "Y" && tmpisCOOInserted == "N")
                                                    {
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
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 1;
                                                            SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                        }
                                                    }
                                                    else if (tmpItemPlainGold == "Y" && tmpisCOOInserted == "N")
                                                    {
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
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 2;
                                                            SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (tmpDataItemInfo == "Y" && tmpItemIsSRP == "Y")
                                                        {
                                                            tmpItemIsMRP = "Y";
                                                        }
                                                        if (((tmpItemIsMRP == "Y" && tmpisCOOInserted == "N") || tmpItemIsSRP == "Y") && tmpitem_illumine == "N")
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
                                                                cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 3;
                                                                SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemID = tmpCartItemID;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.CartMRPrice = tmpCartMRPrice;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.CartPrice = tmpCartPrice;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.CartDPrice = tmpCartDPrice;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.CartItemMetalWt = tmpCartItemMetalWt;
                                                            cartcheckouotallotnew_update_cartmstitem_prices_params.FlagValue = 4;
                                                            SaveCartMstItemPrices(cartcheckouotallotnew_update_cartmstitem_prices_params);
                                                        }
                                                    }
                                                }
                                            }

                                            CartCheckoutNoAllotNewSaveNewCartMstParams cartcheckoutnoallotnew_savenew_cartmst_params = new CartCheckoutNoAllotNewSaveNewCartMstParams();
                                            cartcheckoutnoallotnew_savenew_cartmst_params.DataID = DataId;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.CartID = CartId;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.OrderTypeID = ordertypeid;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.cart_order_login_type_id = cart_order_login_type_id;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.cart_billing_login_type_id = cart_billing_login_type_id;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.cart_order_data_id = cart_order_data_id;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.cart_billing_data_id = cart_billing_data_id;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.cart_remarks = cart_remarks;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.cart_delivery_date = cart_delivery_date;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.itemall = itemall;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.SourceType = "APP";
                                            cartcheckoutnoallotnew_savenew_cartmst_params.devicetype = "WEB";
                                            cartcheckoutnoallotnew_savenew_cartmst_params.devicename = "";
                                            cartcheckoutnoallotnew_savenew_cartmst_params.appversion = "";
                                            cartcheckoutnoallotnew_savenew_cartmst_params.APP_ENV = APP_ENV;

                                            cartcheckoutnoallotnew_savenew_cartmst_params.oroseven_cnt = oroSeven.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.orosevenString = string.Join(",", oroSeven);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.orofifty_cnt = oroFifteen.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.orofiftyString = string.Join(",", oroFive);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.orohours_cnt = oroHours.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.orohoursString = string.Join(",", oroHours);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.orotwentyone_cnt = oroTwentyOne.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.orotwentyoneString = string.Join(",", oroTwentyOne);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.orofive_cnt = oroFive.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.orofiveString = string.Join(",", oroFive);

                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnaseven_cnt = kisnaSeven.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnasevenString = string.Join(",", kisnaSeven);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnafifty_cnt = kisnaFifty.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnafiftyString = string.Join(",", kisnaFifty);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnahours_cnt = kisnaHours.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnahoursString = string.Join(",", kisnaHours);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnatwentyone_cnt = kisnaTwentyone.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnatwentyoneString = string.Join(",", kisnaTwentyone);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnafive_cnt = kisnaFive.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kisnafiveString = string.Join(",", kisnaFive);

                                            cartcheckoutnoallotnew_savenew_cartmst_params.kgseven_cnt = kgSeven.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kgsevenString = string.Join(",", kgSeven);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kgfifty_cnt = kgFifty.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kgfiftyString = string.Join(",", kgFifty);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kghours_cnt = kgHours.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kghoursString = string.Join(",", kgHours);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kgtwentyone_cnt = kgTwentyOne.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kgtwentyoneString = string.Join(",", kgTwentyOne);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kgfive_cnt = kgFive.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.kgfiveString = string.Join(",", kgFive);

                                            cartcheckoutnoallotnew_savenew_cartmst_params.silverseven_cnt = silverSeven.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.silversevenString = string.Join(",", silverSeven);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.silverfifty_cnt = silverFifteen.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.silverfiftyString = string.Join(",", silverFifteen);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.silverhours_cnt = silverHours.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.silverhoursString = string.Join(",", silverHours);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.silvertwentyone_cnt = silverTwentyOne.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.silvertwentyoneString = string.Join(",", silverTwentyOne);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.silverfive_cnt = silverFive.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.silverfiveString = string.Join(",", silverFive);

                                            cartcheckoutnoallotnew_savenew_cartmst_params.illumineseven_cnt = illumineSeven.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.illuminesevenString = string.Join(",", illumineSeven);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.illuminefifty_cnt = illumineFifteen.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.illuminefiftyString = string.Join(",", illumineFifteen);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.illuminehours_cnt = illumineHours.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.illuminehoursString = string.Join(",", illumineHours);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.illuminetwentyone_cnt = illumineTwentyOne.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.illuminetwentyoneString = string.Join(",", illumineTwentyOne);
                                            cartcheckoutnoallotnew_savenew_cartmst_params.illuminefive_cnt = illumineFive.Count;
                                            cartcheckoutnoallotnew_savenew_cartmst_params.illuminefiveString = string.Join(",", illumineFive);

                                            var resNoAllotResponse = new NoAllotResponse();
                                            resNoAllotResponse = SaveCartCheckoutNoAllotNew_CartMstNew(cartcheckoutnoallotnew_savenew_cartmst_params);
                                            resstatus = resNoAllotResponse.status;
                                            resstatuscode = resNoAllotResponse.status_code;
                                            resmessage = resNoAllotResponse.message;
                                        }
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

        public async Task<ResponseDetails> CartItemDelete(CartItemDeleteParams cartitemdelete_params)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cart_auto_id = cartitemdelete_params.cart_auto_id as string ?? string.Empty;

                    if (string.IsNullOrWhiteSpace(cart_auto_id))
                    {
                        responseDetails.success = true;
                        responseDetails.message = "Items not found in cart.";
                        responseDetails.status = "200";
                        return responseDetails;
                    }

                    string
                        resmessage = "";
                    int
                        resstatus = 0,
                        resstatuscode = 400;

                    string cmdQuery = DBCommands.CARTITEM_DELETE;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartAutoIDList", cart_auto_id);

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

        public async Task<ResponseDetails> CartUpdateItem(CartUpdateItemParams cartupdateitem_params)
        {
            var responseDetails = new ResponseDetails();
            try
            {
                int cart_auto_id = cartupdateitem_params.cart_auto_id > 0 ? cartupdateitem_params.cart_auto_id : 0;
                int cart_id = cartupdateitem_params.cart_id > 0 ? cartupdateitem_params.cart_id : 0;
                int item_id = cartupdateitem_params.item_id > 0 ? cartupdateitem_params.item_id : 0;

                if (cart_auto_id > 0 && cart_id > 0 && item_id > 0)
                {
                    int size_common_id = cartupdateitem_params.size_common_id > 0 ? cartupdateitem_params.size_common_id : 0;
                    int color_common_id = cartupdateitem_params.color_common_id > 0 ? cartupdateitem_params.color_common_id : 0;

                    if (size_common_id == 0 && color_common_id == 0)
                    {
                        responseDetails.success = false;
                        responseDetails.message = "Please select size or color to update.";
                        responseDetails.status = "200";
                        return responseDetails;
                    }
                    else
                    {
                        using (SqlConnection dbConnection = new SqlConnection(_connection))
                        {
                            dbConnection.Open();
                            string cmdQuery;

                            string resmessage = "";
                            int resstatus = 0, resstatuscode = 400;

                            decimal CartPrice = 0, CartMRPrice = 0, CartRPrice = 0, CartDPrice = 0, CartItemMetalWt = 0;
                            int FLAG_SOURCE = 0;

                            if (size_common_id > 0)
                            {
                                cmdQuery = DBCommands.GET_DETAILS_FOR_UPDATE_CARTITEMS;

                                using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                                {
                                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@CartID", cart_id);
                                    cmd.Parameters.AddWithValue("@ItemID", item_id);

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

                                                    string ItemPlainGold = firstRow["ItemPlainGold"] != DBNull.Value ? Convert.ToString(firstRow["ItemPlainGold"]) : string.Empty;
                                                    string ItemIsSRP = firstRow["ItemPlainGold"] != DBNull.Value ? Convert.ToString(firstRow["ItemIsSRP"]) : string.Empty;
                                                    string ItemOdSfx = firstRow["ItemOdSfx"] != DBNull.Value ? Convert.ToString(firstRow["ItemOdSfx"]) : string.Empty;

                                                    int ItemCtgCommonID = firstRow["ItemCtgCommonID"] != DBNull.Value ? Convert.ToInt32(firstRow["ItemCtgCommonID"]) : 0;
                                                    int ItemBrandCommonID = firstRow["ItemBrandCommonID"] != DBNull.Value ? Convert.ToInt32(firstRow["ItemBrandCommonID"]) : 0;

                                                    decimal ItemGrossWt = firstRow["ItemGrossWt"] != DBNull.Value ? Convert.ToDecimal(firstRow["ItemGrossWt"]) : 0;
                                                    decimal ItemMetalWt = firstRow["ItemMetalWt"] != DBNull.Value ? Convert.ToDecimal(firstRow["ItemMetalWt"]) : 0;

                                                    int ItemGenderCommonID = firstRow["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(firstRow["ItemGenderCommonID"]) : 0;
                                                    string MstType = firstRow["MstType"] != DBNull.Value ? Convert.ToString(firstRow["MstType"]) : string.Empty;
                                                    int CartDataID = firstRow["CartDataID"] != DBNull.Value ? Convert.ToInt32(firstRow["CartDataID"]) : 0;

                                                    if (MstType == "F" && ItemPlainGold == "Y" && ItemIsSRP == "Y")
                                                    {
                                                        FLAG_SOURCE = 3;
                                                        CartItemPriceDetailListingParams cartitempricedetaillistparams = new CartItemPriceDetailListingParams();
                                                        IList<CartItemPriceDetailListing> cartItemPriceDetailList = new List<CartItemPriceDetailListing>();
                                                        CommonHelpers objHelpers = new CommonHelpers();

                                                        cartitempricedetaillistparams.DataID = CartDataID;
                                                        cartitempricedetaillistparams.ItemID = item_id;
                                                        cartitempricedetaillistparams.SizeID = size_common_id;
                                                        cartitempricedetaillistparams.CategoryID = ItemCtgCommonID;
                                                        cartitempricedetaillistparams.ItemBrandCommonID = ItemBrandCommonID;
                                                        cartitempricedetaillistparams.ItemGrossWt = ItemGrossWt;
                                                        cartitempricedetaillistparams.ItemMetalWt = ItemMetalWt;
                                                        cartitempricedetaillistparams.ItemGenderCommonID = ItemGenderCommonID;
                                                        cartitempricedetaillistparams.IsWeightCalcRequired = 1;
                                                        cartItemPriceDetailList = objHelpers.GetCartItemPriceDetails(cartitempricedetaillistparams);

                                                        if (cartItemPriceDetailList.Any())
                                                        {
                                                            CartPrice = cartItemPriceDetailList[0].total_price;
                                                            CartDPrice = cartItemPriceDetailList[0].dp_final_price;
                                                            CartRPrice = cartItemPriceDetailList[0].total_price;
                                                            CartMRPrice = cartItemPriceDetailList[0].total_price;
                                                            CartItemMetalWt = cartItemPriceDetailList[0].gold_wt;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        FLAG_SOURCE = 2;
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
                            else
                            {
                                FLAG_SOURCE = 1;
                            }

                            cmdQuery = DBCommands.CART_UPDATE_ITEMS;
                            using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                            {
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@CartItemID", cart_auto_id);
                                cmd.Parameters.AddWithValue("@CartMstID", cart_id);
                                cmd.Parameters.AddWithValue("@CartItemMstID", item_id);
                                cmd.Parameters.AddWithValue("@CartColorCommonID", color_common_id);
                                cmd.Parameters.AddWithValue("@CartConfCommonID", size_common_id);
                                cmd.Parameters.AddWithValue("@CartPrice", CartPrice);
                                cmd.Parameters.AddWithValue("@CartDPrice", CartDPrice);
                                cmd.Parameters.AddWithValue("@CartRPrice", CartRPrice);
                                cmd.Parameters.AddWithValue("@CartMRPrice", CartMRPrice);
                                cmd.Parameters.AddWithValue("@CartItemMetalWt", CartItemMetalWt);
                                cmd.Parameters.AddWithValue("@FLAG", FLAG_SOURCE);

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
                                                var rowDetails = ds.Tables[0].Rows[0];
                                                resstatus = rowDetails["resstatus"] as int? ?? 0;
                                                resstatuscode = rowDetails["resstatuscode"] as int? ?? 0;
                                                resmessage = rowDetails["resmessage"] as string ?? string.Empty;
                                            }
                                            catch (Exception ex)
                                            {
                                                responseDetails.success = false;
                                                responseDetails.message = $"SQL error: {ex.Message}";
                                                responseDetails.status = "400";
                                                return responseDetails;
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
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "Item or cart not found.";
                    responseDetails.status = "200";
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

        public async Task<ResponseDetails> CartChildList(CartChildListParams cartchildlistchparams)
        {
            var response = new ResponseDetails();
            var cartChildList = new List<CartChildListing>();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    var cmdQuery = DBCommands.CARTCHILDLIST_NEW;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        var objHelpers = new CommonHelpers();

                        int data_id = cartchildlistchparams.data_id > 0 ? cartchildlistchparams.data_id : 0;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cartChildList.Add(new CartChildListing
                                {
                                    data_id = reader.GetSafeInt("data_id").ToString(),
                                    parent_type_id = reader.GetSafeInt("parent_type_id").ToString(),
                                    parent_type_code = reader.GetSafeString("parent_type_code"),
                                    parent_type_name = reader.GetSafeString("parent_type_name"),
                                    data_code = reader.GetSafeString("data_code"),
                                    data_shop_name = reader.GetSafeString("data_shop_name"),
                                    data_latitude = reader.GetSafeString("data_latitude"),
                                    data_longitude = reader.GetSafeString("data_longitude"),
                                    data_alt_contact_no = reader.GetSafeString("data_alt_contact_no"),
                                    data_alt_email = reader.GetSafeString("data_alt_email"),
                                    data_tel_no = reader.GetSafeString("data_tel_no"),
                                    data_gstno = reader.GetSafeString("data_gstno"),
                                    data_remarks = reader.GetSafeString("data_remarks"),
                                    data_name = reader.GetSafeString("data_name"),
                                    data_contact_no = reader.GetSafeString("data_contact_no"),
                                    data_email = reader.GetSafeString("data_email"),
                                    profile_image = reader.GetSafeString("profile_image"),
                                    login_type_id = reader.GetSafeInt("login_type_id").ToString(),
                                    area_id = reader.GetSafeInt("area_id").ToString(),
                                    img_id = reader.GetSafeInt("img_id").ToString(),
                                    address = reader.GetSafeString("address"),
                                    Latitude = reader.GetSafeString("Latitude"),
                                    Longitude = reader.GetSafeString("Longitude"),
                                    data_alt_name = reader.GetSafeString("data_alt_name"),
                                    CartID = reader.GetSafeInt("CartID").ToString(),
                                });
                            }
                        }
                    }
                }

                response.success = cartChildList.Any();
                response.message = cartChildList.Any() ? "child list data successfully.!" : "No data found";
                response.status = "200";
                response.data = cartChildList;
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

        public async Task<OrderListResponse> OrderList(OrderListParams orderlistparams)
        {
            try
            {
                var responsedata = new OrderListResponse
                {
                    success = true,
                    message = "Successfully",
                    current_page = 1,
                    last_page = 1,
                    total_items = 1,
                    data = new List<OrderListData>
                    {
                        new OrderListData
                        {
                            cart_id = "238859",
                            order_no = "15202505025161-1/1",
                            PONo = null,
                            cart_date = "02/05/2025 03:17 PM",
                            status = "P",
                            user_name = "Mr. TEST KEY RETAILER (KEY RETAILER)",
                            cart_creater_name = "Mr. TEST CHANNEL PARTNER",
                            orderColor = "#FF7505",
                            amount = "80800.0",
                            CartChkOutDt = "2025-05-02 15:17:51.000",
                            cart_total_qty = "2.0",
                            CartBillingDataID = "5268",
                            DeliveryStatus = "22 Days",
                            approxDays = new OrderListApproxDays
                            {
                                manufactureStartDate = "03-05-2025",
                                manufactureEndDate = "20-05-2025",
                                deliveryStartDate = "21-05-2025",
                                deliveryEndDate = "28-05-2025",
                                deliveryInDays = "22 Days"
                            },
                            gold_rate = "9428"
                        }
                    },
                    orderColorList = new List<OrderListColor>
                    {
                        new OrderListColor { ordertype_mst_id = "2498", ordertype_mst_code = "Regular Order", ordertype_mst_name = "Regular Order", ordertype_mst_color = "#FF7505" },
                        new OrderListColor { ordertype_mst_id = "2499", ordertype_mst_code = "SNMCC Line Sale", ordertype_mst_name = "SNMCC Line Sale", ordertype_mst_color = "#00ABC1" },
                        new OrderListColor { ordertype_mst_id = "2500", ordertype_mst_code = "SNMCC New / Selection Order", ordertype_mst_name = "SNMCC New / Selection Order", ordertype_mst_color = "#319BE0" },
                        new OrderListColor { ordertype_mst_id = "18310", ordertype_mst_code = "Consumer Order", ordertype_mst_name = "Consumer Order", ordertype_mst_color = "#B1764D" },
                        new OrderListColor { ordertype_mst_id = "18821", ordertype_mst_code = "Oro Kraft Videos Selection", ordertype_mst_name = "Oro Kraft Videos Selection", ordertype_mst_color = "#EF6D70" },
                        new OrderListColor { ordertype_mst_id = "20397", ordertype_mst_code = "Real Line Sale", ordertype_mst_name = "Real Line Sale", ordertype_mst_color = "#04C483" },
                        new OrderListColor { ordertype_mst_id = "20834", ordertype_mst_code = "Rare Solitaire Diamond Order - Distribution", ordertype_mst_name = "Rare Solitaire Diamond Order - Distribution", ordertype_mst_color = null },
                        new OrderListColor { ordertype_mst_id = "20929", ordertype_mst_code = "Kisna Orokraft Kisna", ordertype_mst_name = "Kisna Orokraft Kisna", ordertype_mst_color = null }
                    }
                };

                return responsedata;
            }
            catch (SqlException ex)
            {
                var response = new ResponseDetails();
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.status = "400";
                response.data = new List<OrderListData>();
                return (OrderListResponse)response.data;
            }
        }

        public async Task<ReturnResponse> CheckItemSizeRange(CheckItemSizeRangeRequest param)
        {
            var response = new ReturnResponse();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    await dbConnection.OpenAsync();

                    using (var cmd = new SqlCommand(DBCommands.CheckItemSizeRange, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_id", param.dataId);
                        cmd.Parameters.AddWithValue("@data_login_type", param.dataLoginType);
                        cmd.Parameters.AddWithValue("@category_id", param.categoryId);
                        cmd.Parameters.AddWithValue("@item_id", param.itemId);
                        cmd.Parameters.AddWithValue("@size_id", param.sizeId);

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
                }
            }
            catch (SqlException sqlEx)
            {
                response.success = "0";
                response.message = $"SQL error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                response.success = "0";
                response.message = $"Unexpected error: {ex.Message}";
            }
            return response;
        }

        public async Task<ReturnResponse> CheckoutVerifyOtp(CheckoutVerifyOtpRequest param)
        {
            var response = new ReturnResponse();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    await dbConnection.OpenAsync();

                    using (var cmd = new SqlCommand(DBCommands.CheckoutVerifyOtp, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_id", param.dataId);
                        cmd.Parameters.AddWithValue("@otp", param.otp);

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
                }
            }
            catch (SqlException sqlEx)
            {
                response.success = "0";
                response.message = $"SQL error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                response.success = "0";
                response.message = $"Unexpected error: {ex.Message}";
            }
            return response;
        }
    }
}