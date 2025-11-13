

using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Models;
using System.Data;

namespace NewAvatarWebApis.Common
{
    public class CommonHelpers
    {
        private readonly string _connection = DBCommands.CONNECTION_STRING;
        private readonly IConfiguration _configuration;

        //public CommonHelpers(IConfiguration? configuration)
        //{
        //    _connection = _configuration.GetConnectionString("KisnaDBConnection");
        //}

        public string GetString(SqlDataReader reader, string columnName)
        {
            return reader[columnName] == DBNull.Value ? string.Empty : reader.GetString(reader.GetOrdinal(columnName));
        }

        public decimal GetDecimal(SqlDataReader reader, string columnName, bool round = false)
        {
            if (reader[columnName] == DBNull.Value) return 0;
            decimal value = reader.GetDecimal(reader.GetOrdinal(columnName));
            return round ? Math.Round(value, 2) : value;
        }

        public string GetLabourString(SqlDataReader reader, string columnName)
        {
            var labour = GetString(reader, columnName);
            return labour.Replace("gm", string.Empty)
                         .Replace(" / ", string.Empty)
                         .Replace("Fix", string.Empty)
                         .Replace("%", string.Empty)
                         .Trim();
        }

        public string ProcessCommaSeparatedString(string input)
        {
            return string.Join(",", input
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList());
        }

        public bool TryParseDecimal(string input)
        {
            decimal result;
            return decimal.TryParse(input, out result);
        }

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public IList<CartItemPriceDetailListing> GetCartItemPriceDetails(CartItemPriceDetailListingParams cartitempricedetaillistparams)
        {
            IList<CartItemPriceDetailListing> cartItemPriceDetailList = new List<CartItemPriceDetailListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    string cmdQuery = DBCommands.CALCULATE_WEIGHT_PRICE_FROMSIZE;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DataID", cartitempricedetaillistparams.DataID);
                        cmd.Parameters.AddWithValue("@ItemID", cartitempricedetaillistparams.ItemID);
                        cmd.Parameters.AddWithValue("@SizeID", cartitempricedetaillistparams.SizeID);
                        cmd.Parameters.AddWithValue("@CategoryID", cartitempricedetaillistparams.CategoryID);
                        cmd.Parameters.AddWithValue("@ItemBrandCommonID", cartitempricedetaillistparams.ItemBrandCommonID);
                        cmd.Parameters.AddWithValue("@ItemGrossWt", cartitempricedetaillistparams.ItemGrossWt);
                        cmd.Parameters.AddWithValue("@ItemMetalWt", cartitempricedetaillistparams.ItemMetalWt);
                        cmd.Parameters.AddWithValue("@IsWeightCalcRequired", cartitempricedetaillistparams.IsWeightCalcRequired);

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    var cartItemPriceDetail = new CartItemPriceDetailListing
                                    {
                                        ItemOdSfx = objHelpers.GetString(dataReader, "Sfx"),
                                        design = objHelpers.GetString(dataReader, "Design"),
                                        design_kt = objHelpers.GetString(dataReader, "DesignKT"),
                                        pure_gold = objHelpers.GetDecimal(dataReader, "Gold_pur"),
                                        gold_wt = objHelpers.GetDecimal(dataReader, "gold_wt"),
                                        gold_ktprice = objHelpers.GetDecimal(dataReader, "gold_ktprice", true),
                                        gold_price = objHelpers.GetDecimal(dataReader, "gold_price", true),
                                        platinum = objHelpers.GetDecimal(dataReader, "platinum"),
                                        platinum_wt = objHelpers.GetDecimal(dataReader, "platinum_wt"),
                                        platinum_ktprice = objHelpers.GetDecimal(dataReader, "platinum_ktprice", true),
                                        platinum_price = objHelpers.GetDecimal(dataReader, "platinum_price", true),
                                        diamond_qty = objHelpers.GetDecimal(dataReader, "diamond_Qty"),
                                        diamond_wt = objHelpers.GetDecimal(dataReader, "diamond_wt"),
                                        diamond_price = objHelpers.GetDecimal(dataReader, "diamond_price", true),
                                        stone_wt = objHelpers.GetDecimal(dataReader, "stone_wt"),
                                        stone_qty = objHelpers.GetDecimal(dataReader, "stone_qty"),
                                        stone_price = objHelpers.GetDecimal(dataReader, "stone_price", true),
                                        metal_price = objHelpers.GetDecimal(dataReader, "metal_price", true),
                                        other_price = objHelpers.GetDecimal(dataReader, "other_price", true),
                                        labour = objHelpers.GetLabourString(dataReader, "Labour"),
                                        // labour_percentage = objHelpers.GetDecimal(dataReader, "Labour"),
                                        labour_percentage = dataReader["Labour"] as decimal? ?? 0,
                                        labour_price = objHelpers.GetDecimal(dataReader, "labour_price", true),
                                        item_price = objHelpers.GetDecimal(dataReader, "MRP_Taxable", true),
                                        gst_percent = objHelpers.GetString(dataReader, "GST_PER"),
                                        GST = objHelpers.GetDecimal(dataReader, "MRP_GST", true),
                                        total_price = objHelpers.GetDecimal(dataReader, "MRP_WithTax", true),
                                        dp_labour_Per = objHelpers.GetString(dataReader, "Labour_DP"),
                                        dp_labour_percentage = objHelpers.GetString(dataReader, "Labour_DP"),
                                        dp_labour_price = objHelpers.GetDecimal(dataReader, "DP_labour_price", true),
                                        dp_price = objHelpers.GetDecimal(dataReader, "Dp_Taxable", true),
                                        DP_GST = objHelpers.GetDecimal(dataReader, "DP_GST", true),
                                        dp_final_price = objHelpers.GetDecimal(dataReader, "DP_WithTax", true),
                                        dp_maring_percent = objHelpers.GetDecimal(dataReader, "DpMarginPer"),
                                        dp_is_labour = objHelpers.GetString(dataReader, "DPoOnLabour"),
                                        dp_gold_price = objHelpers.GetDecimal(dataReader, "DP_Gold_price"),
                                        dp_platinum_price = objHelpers.GetDecimal(dataReader, "DP_platinum_price"),
                                        dp_metal_price = objHelpers.GetDecimal(dataReader, "DP_metal_price"),
                                        dp_diamond_price = objHelpers.GetDecimal(dataReader, "DP_diamond_price"),
                                        dp_stone_price = objHelpers.GetDecimal(dataReader, "DP_stone_price")
                                    };

                                    cartItemPriceDetailList.Add(cartItemPriceDetail);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cartItemPriceDetailList;
        }

        public IList<CartItemDPRPCALCListing> Get_DP_RP_Calculation(CartItemDPRPCALCListingParams cartitemDPRPCALClistparams)
        {
            var cartItemDPRPCALCList = new List<CartItemDPRPCALCListing>();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    using (var cmd = new SqlCommand(DBCommands.DP_RP_CALCULATION, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@DataID", SqlDbType.Int).Value = cartitemDPRPCALClistparams.DataID;
                        cmd.Parameters.Add("@MRP", SqlDbType.Int).Value = cartitemDPRPCALClistparams.MRP;

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                var D_Price = dataReader["D_Price"] != DBNull.Value ? Convert.ToDecimal(dataReader["D_Price"]) : 0;
                                var R_Price = dataReader["R_Price"] != DBNull.Value ? Convert.ToDecimal(dataReader["R_Price"]) : 0;
                                var D_M_Percentage = dataReader["D_M_Percentage"] != DBNull.Value ? Convert.ToDecimal(dataReader["D_M_Percentage"]) : 0;

                                cartItemDPRPCALCList.Add(new CartItemDPRPCALCListing
                                {
                                    D_Price = D_Price,
                                    R_Price = R_Price,
                                    D_M_Percentage = D_M_Percentage
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"SQL Error: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }

            return cartItemDPRPCALCList;
        }

        //public List<CartItem_sizeListing> GetCartItemsSizeList(CartItems_sizeListingParams cartitemsizelistparams)
        //{
        //    var cartItemSizeList = new List<CartItem_sizeListing>();
        //    try
        //    {
        //        using (var dbConnection = new SqlConnection(_connection))
        //        {
        //            string cmdQuery = DBCommands.CARTITEMS_SIZELIST;

        //            using (var cmd = new SqlCommand(cmdQuery, dbConnection))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //                cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = cartitemsizelistparams.ItemID;
        //                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = cartitemsizelistparams.CategoryID;
        //                cmd.Parameters.Add("@BranID", SqlDbType.Int).Value = cartitemsizelistparams.BranID;
        //                cmd.Parameters.Add("@ItemNosePinScrewSts", SqlDbType.Int).Value = cartitemsizelistparams.ItemNosePinScrewSts;
        //                cmd.Parameters.Add("@ItemGenderCommonID", SqlDbType.Int).Value = cartitemsizelistparams.ItemGenderCommonID;
        //                cmd.Parameters.Add("@pfield_name", SqlDbType.NVarChar).Value = cartitemsizelistparams.pfield_name;
        //                cmd.Parameters.Add("@pselectedSize", SqlDbType.Int).Value = cartitemsizelistparams.pselectedSize;
        //                cmd.Parameters.Add("@pdefault_size_name", SqlDbType.Int).Value = cartitemsizelistparams.pdefault_size_name;

        //                dbConnection.Open();

        //                using (var dataReader = cmd.ExecuteReader())
        //                {
        //                    while (dataReader.Read())
        //                    {
        //                        var cartItem = new CartItem_sizeListing
        //                        {
        //                            product_size_mst_id = dataReader["product_size_mst_id"] as int? ?? 0,
        //                            product_size_mst_code = dataReader["product_size_mst_code"] as string ?? string.Empty,
        //                            product_size_mst_name = dataReader["product_size_mst_name"] as string ?? string.Empty,
        //                            product_size_mst_desc = dataReader["product_size_mst_desc"] as string ?? string.Empty,
        //                            field_name = dataReader["field_name"] as string ?? string.Empty,
        //                            selectedSize = dataReader["selectedSize"] as int? ?? 0,
        //                            default_size_name = dataReader["default_size_name"] as int? ?? 0
        //                        };

        //                        cartItemSizeList.Add(cartItem);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return cartItemSizeList;
        //}

        public List<CartProduct_sizeListing> GetCartProductSizeList(CartProduct_sizeListingParams cartproductsizelistparams)
        {
            var cartProductSizeList = new List<CartProduct_sizeListing>();
            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CART_ITEM_PRODUCTSIZELIST;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = cartproductsizelistparams.ItemID;
                        cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = cartproductsizelistparams.CategoryID;
                        cmd.Parameters.Add("@BrandID", SqlDbType.Int).Value = cartproductsizelistparams.BranID;
                        cmd.Parameters.Add("@ItemNosePinScrewSts", SqlDbType.VarChar).Value = cartproductsizelistparams.ItemNosePinScrewSts;
                        cmd.Parameters.Add("@ItemGenderCommonID", SqlDbType.Int).Value = cartproductsizelistparams.ItemGenderCommonID;
                        cmd.Parameters.Add("@field_name", SqlDbType.VarChar).Value = cartproductsizelistparams.field_name;
                        cmd.Parameters.Add("@selectedSize", SqlDbType.VarChar).Value = cartproductsizelistparams.selectedSize;
                        cmd.Parameters.Add("@default_size_name", SqlDbType.Int).Value = cartproductsizelistparams.default_size_name;

                        dbConnection.Open();

                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                var cartItem = new CartProduct_sizeListing
                                {
                                    product_size_mst_id = dataReader["product_size_mst_id"] as int? ?? 0,
                                    product_size_mst_code = dataReader["product_size_mst_code"] as string ?? string.Empty,
                                    product_size_mst_name = dataReader["product_size_mst_name"] as string ?? string.Empty,
                                    product_size_mst_desc = dataReader["product_size_mst_desc"] as string ?? string.Empty,
                                    field_name = dataReader["field_name"] as string ?? string.Empty,
                                    selectedSize = dataReader["selectedSize"] as string ?? string.Empty,
                                    default_size_name = dataReader["default_size_name"] as int? ?? 0,
                                };
                                cartProductSizeList.Add(cartItem);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return cartProductSizeList;
        }

        public List<CartItem_colorListing> GetCartItemsColorList(CartItem_colorListingParams cartitemcolorlistparams)
        {
            var cartItemColorList = new List<CartItem_colorListing>();
            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CARTITEM_COLORLIST;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@item_id", SqlDbType.Int).Value = cartitemcolorlistparams.item_id;
                        cmd.Parameters.Add("@default_color_code", SqlDbType.NVarChar).Value = cartitemcolorlistparams.default_color_code;
                        cmd.Parameters.Add("@metalid", SqlDbType.Int).Value = cartitemcolorlistparams.metalid;

                        dbConnection.Open();

                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                var cartItem = new CartItem_colorListing
                                {
                                    product_color_mst_id = dataReader["product_color_mst_id"].ToString(),
                                    product_color_mst_code = dataReader["product_color_mst_code"] as string ?? string.Empty,
                                    product_color_mst_name = dataReader["product_color_mst_name"] as string ?? string.Empty,
                                    IsDefault = dataReader["IsDefault"].ToString()
                                };

                                cartItemColorList.Add(cartItem);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return cartItemColorList;
        }

        public List<CartItem_itemsColorSizeListing> GetCartItemsColorSizeList(CartItem_itemsColorSizeListingParams cartitemcolorsizelistparams)
        {
            var cartItemColorSizeList = new List<CartItem_itemsColorSizeListing>();
            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CARTITEM_COLORSIZELIST;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@itemid", SqlDbType.Int).Value = cartitemcolorsizelistparams.itemid;
                        cmd.Parameters.Add("@cart_auto_id", SqlDbType.Int).Value = cartitemcolorsizelistparams.cart_auto_id;
                        cmd.Parameters.Add("@CartID", SqlDbType.Int).Value = cartitemcolorsizelistparams.CartID;

                        dbConnection.Open();

                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                var cartItem = new CartItem_itemsColorSizeListing
                                {
                                    cart_item_detail_id = dataReader["cart_item_detail_id"] as int? ?? 0,
                                    cart_mst_id = dataReader["cart_mst_id"] as int? ?? 0,
                                    cart_item_id = dataReader["cart_item_id"] as int? ?? 0,
                                    cart_qty = dataReader["cart_qty"] as int? ?? 0,
                                    cart_color_id = dataReader["cart_color_id"] as int? ?? 0,
                                    cart_size_id = dataReader["cart_size_id"] as int? ?? 0,
                                    cart_item_remarks = dataReader["cart_item_remarks"] as string ?? string.Empty,
                                    cart_item_remarks_ids = dataReader["cart_item_remarks_ids"] as string ?? string.Empty,
                                    cart_item_custom_remarks = dataReader["cart_item_custom_remarks"] as string ?? string.Empty,
                                    cart_item_custom_remarks_ids = dataReader["cart_item_custom_remarks_ids"] as string ?? string.Empty,
                                    cart_item_custom_status = dataReader["cart_item_custom_status"] as int? ?? 0
                                };

                                cartItemColorSizeList.Add(cartItem);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return cartItemColorSizeList;
        }

        public List<Item_itemOrderInstructionListing> GetItemOrderInstructionList()
        {
            var itemOrderInstructionList = new List<Item_itemOrderInstructionListing>();
            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEM_ORDERINSTRUCTIONLIST;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        dbConnection.Open();

                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                int item_instruction_mst_id = dataReader["item_instruction_mst_id"] as int? ?? 0;
                                string item_instruction_mst_code = dataReader["item_instruction_mst_code"] as string ?? string.Empty;
                                string item_instruction_mst_name = dataReader["item_instruction_mst_name"] as string ?? string.Empty;

                                var cartItem = new Item_itemOrderInstructionListing
                                {
                                    item_instruction_mst_id = item_instruction_mst_id.ToString(),
                                    item_instruction_mst_code = item_instruction_mst_code,
                                    item_instruction_mst_name = item_instruction_mst_name,
                                };

                                itemOrderInstructionList.Add(cartItem);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return itemOrderInstructionList;
        }

        public List<Item_itemOrderCustomInstructionListing> GetItemOrderCustomInstructionList()
        {
            var itemOrderCustomInstructionList = new List<Item_itemOrderCustomInstructionListing>();
            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEM_ORDERCUSTOMINSTRUCTIONLIST;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        dbConnection.Open();

                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                int item_instruction_mst_id = dataReader["item_instruction_mst_id"] as int? ?? 0;
                                string item_instruction_mst_code = dataReader["item_instruction_mst_code"] as string ?? string.Empty;
                                string item_instruction_mst_name = dataReader["item_instruction_mst_name"] as string ?? string.Empty;

                                var cartItem = new Item_itemOrderCustomInstructionListing
                                {
                                    item_instruction_mst_id = item_instruction_mst_id.ToString(),
                                    item_instruction_mst_code = item_instruction_mst_code,
                                    item_instruction_mst_name = item_instruction_mst_name
                                };

                                itemOrderCustomInstructionList.Add(cartItem);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return itemOrderCustomInstructionList;
        }

        public List<Item_TagsListing> GetItemTagList(Item_TagsListingParams itemtaglistparams)
        {
            var itemTagList = new List<Item_TagsListing>();
            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEM_TAGS;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@item_id", itemtaglistparams.item_id);

                        dbConnection.Open();

                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                var tag_name = dataReader["tag_name"] as string ?? string.Empty;
                                var tag_color = dataReader["tag_color"] as string ?? string.Empty;

                                itemTagList.Add(new Item_TagsListing
                                {
                                    tag_name = tag_name,
                                    tag_color = tag_color
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return itemTagList;
        }

        public List<CartItem_item_images_colorListing> GetCartItemImageColorList(CartItem_item_images_colorListingParams cartitemimagecolorlistparams)
        {
            var cartItemImageColorList = new List<CartItem_item_images_colorListing>();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CARTITEM_IMAGECOLORS;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@itemid", cartitemimagecolorlistparams.itemid);
                        cmd.Parameters.AddWithValue("@master_common_id", cartitemimagecolorlistparams.master_common_id);

                        dbConnection.Open();
                        using (var dataReader = cmd.ExecuteReader())
                        {
                            var colorTable = new DataTable();
                            colorTable.Load(dataReader);

                            // Check if there are more than 1 tables (second table contains image details)
                            DataTable imageTable = null;
                            if (dataReader.NextResult()) // Move to the next result (table)
                            {
                                imageTable = new DataTable();
                                imageTable.Load(dataReader);
                            }

                            // Process the colors and their associated image details
                            foreach (DataRow colorRow in colorTable.Rows)
                            {
                                var color_id = colorRow["color_id"] as int? ?? 0;

                                // Filter image details for the current color_id
                                var colorImageDetails = imageTable?.AsEnumerable()
                                    .Where(row => row.Field<int>("color_id") == color_id)
                                    .Select(row => new CartItem_item_images_color_detailsListing
                                    {
                                        color_id = color_id,
                                        image_view_name = row["image_view_name"] as string ?? string.Empty,
                                        image_path = row["image_path"] as string ?? string.Empty
                                    }).ToList() ?? new List<CartItem_item_images_color_detailsListing>();

                                cartItemImageColorList.Add(new CartItem_item_images_colorListing
                                {
                                    color_id = color_id,
                                    color_image_details = colorImageDetails
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            return cartItemImageColorList;
        }

        public List<DateTime> GetHoliDaysList(DateTime startDate, DateTime endDate)
        {
            var holidayData = new List<DateTime>();

            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    using (var cmd = new SqlCommand(DBCommands.HOLIDAYSLIST_BYDATES, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StartDate", startDate);
                        cmd.Parameters.AddWithValue("@EndDate", endDate);

                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                if (dataReader["hDate"] != DBNull.Value)
                                {
                                    holidayData.Add(dataReader.GetDateTime(dataReader.GetOrdinal("hDate")));
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return holidayData;
        }

        //public List<Item_images_colorListing> GetItemImageColorList(Item_images_colorListingParams itemimagecolorlistparams)
        //{
        //    var itemImageColorList = new List<Item_images_colorListing>();

        //    try
        //    {
        //        using (var dbConnection = new SqlConnection(_connection))
        //        {
        //            string cmdQuery = DBCommands.ITEM_IMAGECOLORS;

        //            using (var cmd = new SqlCommand(cmdQuery, dbConnection))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@ItemID", itemimagecolorlistparams.itemid);

        //                dbConnection.Open();
        //                using (var dataReader = cmd.ExecuteReader())
        //                {
        //                    // First table with color information
        //                    var colorTable = new DataTable();
        //                    colorTable.Load(dataReader);

        //                    // Check if there are more than 1 tables (second table contains image details)
        //                    DataTable imageTable = null;
        //                    if (dataReader.NextResult()) // Move to the next result (table)
        //                    {
        //                        imageTable = new DataTable();
        //                        imageTable.Load(dataReader);
        //                    }

        //                    // Process the colors and their associated image details
        //                    foreach (DataRow colorRow in colorTable.Rows)
        //                    {
        //                        var color_id = colorRow["color_id"] as int? ?? 0;

        //                        // Filter image details for the current color_id
        //                        var colorImageDetails = imageTable?.AsEnumerable()
        //                            .Where(row => row.Field<int>("color_id") == color_id)
        //                            .Select(row => new Item_images_color_detailsListing
        //                            {
        //                                color_id = color_id,
        //                                image_view_name = row["image_view_name"] as string ?? string.Empty,
        //                                image_path = row["image_path"] as string ?? string.Empty
        //                            }).ToList() ?? new List<Item_images_color_detailsListing>();

        //                        // Add to the result list
        //                        itemImageColorList.Add(new Item_images_colorListing
        //                        {
        //                            color_id = color_id,
        //                            color_image_details = colorImageDetails
        //                        });
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Unexpected error: {ex.Message}");
        //    }

        //    return itemImageColorList;
        //}

        public IList<ItemCategoryMappingList> GetItemCategoryMappingList(int pItemGenderCommonID, string pItemNosePinScrewSts)
        {
            IList<ItemCategoryMappingList> itemCategoryMappingList = new List<ItemCategoryMappingList>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    string cmdQuery = DBCommands.ITEMCATEGORYMAPPINGS_LIST;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemGenderCommonID", pItemGenderCommonID);
                        cmd.Parameters.AddWithValue("@ItemNosePinScrewSts", pItemNosePinScrewSts);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    int size_common_id = dataReader["size_common_id"] as int? ?? 0;
                                    string field_name = dataReader["field_name"] as string ?? string.Empty;
                                    int default_size = dataReader["default_size"] as int? ?? 0;
                                    string exclude_sizes = dataReader["exclude_sizes"] as string ?? string.Empty;

                                    var itemCategoryMapping = new ItemCategoryMappingList
                                    {
                                        category_id = category_id.ToString(),
                                        size_common_id = size_common_id.ToString(),
                                        field_name = field_name,
                                        default_size = default_size,
                                        exclude_sizes = exclude_sizes
                                    };
                                    itemCategoryMappingList.Add(itemCategoryMapping);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return itemCategoryMappingList;
        }

        public IList<ItemCategoryMappingList> GetItemCategoryMappingKisnaPremiumList(int pItemGenderCommonID, string pItemNosePinScrewSts)
        {
            IList<ItemCategoryMappingList> itemCategoryMappingList = new List<ItemCategoryMappingList>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    string cmdQuery = DBCommands.ITEMCATEGORYMAPPINGS_KISNAPREMIUM_LIST;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemGenderCommonID", pItemGenderCommonID);
                        cmd.Parameters.AddWithValue("@ItemNosePinScrewSts", pItemNosePinScrewSts);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int category_id = dataReader["category_id"] as int? ?? 0;
                                    int size_common_id = dataReader["size_common_id"] as int? ?? 0;
                                    string field_name = dataReader["field_name"] as string ?? string.Empty;
                                    int default_size = dataReader["default_size"] as int? ?? 0;
                                    string exclude_sizes = dataReader["exclude_sizes"] as string ?? string.Empty;

                                    var itemCategoryMapping = new ItemCategoryMappingList
                                    {
                                        category_id = category_id.ToString(),
                                        size_common_id = size_common_id.ToString(),
                                        field_name = field_name,
                                        default_size = default_size,
                                        exclude_sizes = exclude_sizes
                                    };
                                    itemCategoryMappingList.Add(itemCategoryMapping);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return itemCategoryMappingList;
        }

        public IList<ItemSelectedSizeList> GetSelectedSizeByCollectionList(int pItemId, string pMstType)
        {
            IList<ItemSelectedSizeList> itemSelectedSize_List = new List<ItemSelectedSizeList>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    string cmdQuery = DBCommands.GETSELECTEDSIZE_BYCOLLECTIONS;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@item_id", pItemId);
                        cmd.Parameters.AddWithValue("@MstType", pMstType);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int selectedSize = dataReader["selectedSize"] != DBNull.Value ? Convert.ToInt32(dataReader["selectedSize"]) : 0;
                                    int flag_exclude_sizes = dataReader["flag_exclude_sizes"] != DBNull.Value ? Convert.ToInt32(dataReader["flag_exclude_sizes"]) : 0;
                                    string exclude_sizes = dataReader["exclude_sizes"] != DBNull.Value ? Convert.ToString(dataReader["exclude_sizes"]) : string.Empty;

                                    var itemSelectedSize = new ItemSelectedSizeList
                                    {
                                        selectedSize = selectedSize,
                                        flag_exclude_sizes = flag_exclude_sizes,
                                        exclude_sizes = exclude_sizes
                                    };
                                    itemSelectedSize_List.Add(itemSelectedSize);
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return itemSelectedSize_List;
        }

        public List<Item_sizeListing> GetItemsSizeList(Items_sizeListingParams itemsizelistparams)
        {
            var itemSizeList = new List<Item_sizeListing>();
            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEM_SIZELIST;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@ItemID", SqlDbType.Int).Value = itemsizelistparams.ItemID;
                        cmd.Parameters.Add("@CategoryCommonID", SqlDbType.Int).Value = itemsizelistparams.CategoryCommonID;
                        cmd.Parameters.Add("@ExcludeSizes", SqlDbType.NVarChar).Value = itemsizelistparams.ExcludeSizes;

                        dbConnection.Open();

                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                int product_size_mst_id = dataReader["product_size_mst_id"] as int? ?? 0;
                                string product_size_mst_code = dataReader["product_size_mst_code"] as string ?? string.Empty;
                                string product_size_mst_name = dataReader["product_size_mst_name"] as string ?? string.Empty;
                                string product_size_mst_desc = dataReader["product_size_mst_desc"] as string ?? string.Empty;

                                var item = new Item_sizeListing
                                {
                                    product_size_mst_id = product_size_mst_id.ToString(),
                                    product_size_mst_code = product_size_mst_code,
                                    product_size_mst_name = product_size_mst_name,
                                    product_size_mst_desc = product_size_mst_desc
                                };

                                itemSizeList.Add(item);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return itemSizeList;
        }

        public List<Item_ColorSizeListing> GetItemsColorSizeList(Item_ColorSizeListingParams itemcolorsizelistparams)
        {
            var itemColorSizeList = new List<Item_ColorSizeListing>();
            try
            {
                using (var dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEM_COLORSIZELIST;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@itemid", SqlDbType.Int).Value = itemcolorsizelistparams.itemid;
                        cmd.Parameters.Add("@CartID", SqlDbType.Int).Value = itemcolorsizelistparams.CartID;

                        dbConnection.Open();

                        using (var dataReader = cmd.ExecuteReader())
                        {
                            while (dataReader.Read())
                            {
                                int cart_item_detail_id = dataReader["cart_item_detail_id"] as int? ?? 0;
                                int cart_mst_id = dataReader["cart_mst_id"] as int? ?? 0;
                                int cart_item_id = dataReader["cart_item_id"] as int? ?? 0;
                                int cart_qty = dataReader["cart_qty"] as int? ?? 0;
                                int cart_color_id = dataReader["cart_color_id"] as int? ?? 0;
                                int cart_size_id = dataReader["cart_size_id"] as int? ?? 0;
                                string cart_item_remarks = dataReader["cart_item_remarks"] as string ?? string.Empty;
                                string cart_item_remarks_ids = dataReader["cart_item_remarks_ids"] as string ?? string.Empty;
                                string cart_item_custom_remarks = dataReader["cart_item_custom_remarks"] as string ?? string.Empty;
                                string cart_item_custom_remarks_ids = dataReader["cart_item_custom_remarks_ids"] as string ?? string.Empty;
                                int cart_item_custom_status = dataReader["cart_item_custom_status"] as int? ?? 0;

                                var itemDetails = new Item_ColorSizeListing
                                {
                                    cart_item_detail_id = cart_item_detail_id.ToString(),
                                    cart_mst_id = cart_mst_id.ToString(),
                                    cart_item_id = cart_item_id.ToString(),
                                    cart_qty = cart_qty.ToString(),
                                    cart_color_id = cart_color_id.ToString(),
                                    cart_size_id = cart_size_id.ToString(),
                                    cart_item_remarks = cart_item_remarks,
                                    cart_item_remarks_ids = cart_item_remarks_ids,
                                    cart_item_custom_remarks = cart_item_custom_remarks,
                                    cart_item_custom_remarks_ids = cart_item_custom_remarks_ids,
                                    cart_item_custom_status = cart_item_custom_status.ToString(),
                                };

                                itemColorSizeList.Add(itemDetails);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return itemColorSizeList;
        }

        public string GetIsInFranchiseStock(string pItemCode, string pMstType)
        {
            string isInFranchiseStore = "N";
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    string cmdQuery = DBCommands.ISINFRANCHISESTOCK_NEW;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", pItemCode);
                        cmd.Parameters.AddWithValue("@MstType", pMstType);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    string tmpIsInFranchiseStore = dataReader["isInFranchiseStore"] as string ?? string.Empty;
                                    if (!string.IsNullOrWhiteSpace(tmpIsInFranchiseStore))
                                    {
                                        isInFranchiseStore = tmpIsInFranchiseStore;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isInFranchiseStore;
        }

        public string GetIsInFranchiseStock_KisnaPremium(string pItemCode, string pMstType)
        {
            string isInFranchiseStore = "N";
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    string cmdQuery = DBCommands.ISINFRANCHISESTOCK_KISNAPREMIUM;
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", pItemCode);
                        cmd.Parameters.AddWithValue("@MstType", pMstType);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    string tmpIsInFranchiseStore = dataReader["isInFranchiseStore"] as string ?? string.Empty;
                                    if (!string.IsNullOrWhiteSpace(tmpIsInFranchiseStore))
                                    {
                                        isInFranchiseStore = tmpIsInFranchiseStore;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return isInFranchiseStore;
        }

        public IList<CheckItemIsSolitaireComboListing> CheckItemIsSolitaireCombo(CheckItemIsSolitaireComboParams checkitemissolitairecomboparams)
        {
            IList<CheckItemIsSolitaireComboListing> checkitemissolitairecomboList = new List<CheckItemIsSolitaireComboListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CHECKITEM_ISSOLITAIRECOMBO;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartID", checkitemissolitairecomboparams.cart_id);
                        cmd.Parameters.AddWithValue("@CartBillingDataID", checkitemissolitairecomboparams.cart_billing_data_id);
                        cmd.Parameters.AddWithValue("@ordertypeid", checkitemissolitairecomboparams.ordertypeid);
                        cmd.Parameters.AddWithValue("@implodeCartMstItemIds", checkitemissolitairecomboparams.implodeCartMstItemIds);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    int is_valid = dataReader["is_valid"] != DBNull.Value ? Convert.ToInt32(dataReader["is_valid"]) : 0;
                                    int min_amount = dataReader["min_amount"] != DBNull.Value ? Convert.ToInt32(dataReader["min_amount"]) : 0;
                                    decimal cart_price = dataReader["cart_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["cart_price"]) : 0;
                                    string collection_name = dataReader["collection_name"] as string ?? string.Empty;

                                    checkitemissolitairecomboList.Add(new CheckItemIsSolitaireComboListing
                                    {
                                        is_valid = is_valid,
                                        min_amount = min_amount,
                                        cart_price = cart_price,
                                        collection_name = collection_name,
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                checkitemissolitairecomboList = new List<CheckItemIsSolitaireComboListing>();
            }
            return checkitemissolitairecomboList;
        }

        public IList<CheckItemIsNewPremiumCollectionListing> CheckItem_IsNewPremiumCollection(CheckItemIsNewPremiumCollectionParams checkitemisnewpremiumcollectionparams)
        {
            IList<CheckItemIsNewPremiumCollectionListing> checkitemisnewpremiumcollectionList = new List<CheckItemIsNewPremiumCollectionListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CHECKITEM_ISNEWPREMIUMCOLLECTION;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CartID", checkitemisnewpremiumcollectionparams.cart_id);
                        cmd.Parameters.AddWithValue("@CartBillingDataID", checkitemisnewpremiumcollectionparams.cart_billing_data_id);
                        cmd.Parameters.AddWithValue("@ordertypeid", checkitemisnewpremiumcollectionparams.ordertypeid);
                        cmd.Parameters.AddWithValue("@implodeCartMstItemIds", checkitemisnewpremiumcollectionparams.implodeCartMstItemIds);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    /*
                                    int is_valid = dataReader["is_valid"] as int? ?? 0;
                                    int min_amount = dataReader["min_amount"] as int? ?? 0;
                                    decimal cart_price = dataReader["cart_price"] as decimal? ?? 0;
                                    string collection_name = dataReader["collection_name"] as string ?? string.Empty;
                                    */
                                    int is_valid = dataReader["is_valid"] != DBNull.Value ? Convert.ToInt32(dataReader["is_valid"]) : 0;
                                    int min_amount = dataReader["min_amount"] != DBNull.Value ? Convert.ToInt32(dataReader["min_amount"]) : 0;
                                    decimal cart_price = dataReader["cart_price"] != DBNull.Value ? Convert.ToDecimal(dataReader["cart_price"]) : 0;
                                    string collection_name = dataReader["collection_name"] as string ?? string.Empty;


                                    checkitemisnewpremiumcollectionList.Add(new CheckItemIsNewPremiumCollectionListing
                                    {
                                        is_valid = is_valid,
                                        min_amount = min_amount,
                                        cart_price = cart_price,
                                        collection_name = collection_name,
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                checkitemisnewpremiumcollectionList = new List<CheckItemIsNewPremiumCollectionListing>();
            }
            return checkitemisnewpremiumcollectionList;
        }

        public int SaveGoldCartItems(GoldCartInsertParams goldcartparams)
        {
            int resstatus = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    string cmdQuery = DBCommands.CHECKOUTBULKUPLOAD_NEW_INSERTGOLDCARTITEM;

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", goldcartparams.DataId);
                        cmd.Parameters.AddWithValue("@CartMstId", goldcartparams.CartMstId);
                        cmd.Parameters.AddWithValue("@CartItemId", goldcartparams.CartItemId);
                        cmd.Parameters.AddWithValue("@ItemId", goldcartparams.ItemId);
                        cmd.Parameters.AddWithValue("@design_kt", goldcartparams.design_kt);
                        cmd.Parameters.AddWithValue("@pure_gold", goldcartparams.pure_gold);
                        cmd.Parameters.AddWithValue("@gold_ktprice", goldcartparams.gold_ktprice);
                        cmd.Parameters.AddWithValue("@gold_price", goldcartparams.gold_price);
                        cmd.Parameters.AddWithValue("@labour_price", goldcartparams.labour_price);
                        cmd.Parameters.AddWithValue("@item_price", goldcartparams.item_price);
                        cmd.Parameters.AddWithValue("@gst_price", goldcartparams.gst_price);
                        cmd.Parameters.AddWithValue("@total_price", goldcartparams.total_price);
                        cmd.Parameters.AddWithValue("@CartQty", goldcartparams.CartQty);
                        cmd.Parameters.AddWithValue("@making_per_gram", goldcartparams.making_per_gram);
                        cmd.Parameters.AddWithValue("@ItemOdSfx", goldcartparams.ItemOdSfx);

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

        public int SaveLogValues(LogInsertParams logparams)
        {
            int resstatus = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    string cmdQuery = DBCommands.CHECKOUTBULKUPLOAD_NEW_INSERTLOG;

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", logparams.DataId);
                        cmd.Parameters.AddWithValue("@CartMstId", logparams.CartMstId);
                        cmd.Parameters.AddWithValue("@CartItemId", logparams.CartItemId);
                        cmd.Parameters.AddWithValue("@ItemId", logparams.ItemId);
                        cmd.Parameters.AddWithValue("@CartQty", logparams.CartQty);
                        cmd.Parameters.AddWithValue("@CartOldQty", logparams.CartOldQty);
                        cmd.Parameters.AddWithValue("@CartColorCommonID", logparams.CartColorCommonID);
                        cmd.Parameters.AddWithValue("@CartConfCommonID", logparams.CartConfCommonID);
                        cmd.Parameters.AddWithValue("@CartMRPrice", logparams.CartMRPrice);
                        cmd.Parameters.AddWithValue("@CartRPrice", logparams.CartRPrice);
                        cmd.Parameters.AddWithValue("@CartDPrice", logparams.CartDPrice);

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

        public int SaveTempTableLogValues(TempTableLogInsertParams temptablelogparams)
        {
            int resstatus = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CHECKOUTBULKUPLOAD_NEW_TEMPTABLELOG;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", temptablelogparams.DataId);
                        cmd.Parameters.AddWithValue("@OperationName", temptablelogparams.OperationName);

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

        public int ItemDynamicPriceCart(ItemDynamicPriceCartParams itemdynamicpricecart_params)
        {
            int resstatus = 0;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    int DataID = itemdynamicpricecart_params.DataID;
                    int ItemID = itemdynamicpricecart_params.ItemID;
                    int CartID = itemdynamicpricecart_params.CartID;
                    int CartItemID = itemdynamicpricecart_params.CartItemID;
                    decimal diamond_price = itemdynamicpricecart_params.diamond_price;
                    decimal gold_wt = itemdynamicpricecart_params.gold_wt;
                    decimal pure_gold = itemdynamicpricecart_params.pure_gold;
                    decimal gold_ktprice = itemdynamicpricecart_params.gold_ktprice;
                    decimal gold_price = itemdynamicpricecart_params.gold_price;
                    decimal platinum_wt = itemdynamicpricecart_params.platinum_wt;
                    decimal platinum = itemdynamicpricecart_params.platinum;
                    decimal platinum_price = itemdynamicpricecart_params.platinum_price;
                    decimal labour_price = itemdynamicpricecart_params.labour_price;

                    string cmdQuery = DBCommands.ITEMDYNAMICPRICECART;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataID", DataID);
                        cmd.Parameters.AddWithValue("@ItemID", ItemID);
                        cmd.Parameters.AddWithValue("@CartID", CartID);
                        cmd.Parameters.AddWithValue("@CartItemID", CartItemID);
                        cmd.Parameters.AddWithValue("@diamond_price", diamond_price);
                        cmd.Parameters.AddWithValue("@gold_wt", gold_wt);
                        cmd.Parameters.AddWithValue("@pure_gold", pure_gold);
                        cmd.Parameters.AddWithValue("@gold_ktprice", gold_ktprice);
                        cmd.Parameters.AddWithValue("@gold_price", gold_price);
                        cmd.Parameters.AddWithValue("@platinum_wt", platinum_wt);
                        cmd.Parameters.AddWithValue("@platinum", platinum);
                        cmd.Parameters.AddWithValue("@platinum_price", platinum_price);
                        cmd.Parameters.AddWithValue("@labour_price", labour_price);

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

        public async Task<List<CartItemWithIllumine>> GetSoliterDiamondAsync(int cart_id, string illumineCollection)
        {
            IList<CartItemWithIllumine> CartItemWithIllumineList = new List<CartItemWithIllumine>();

            try
            {
                string query = $@"
                 SELECT c.CartItemID,i.ItemID,i.ItemSoliterSts,i.ItemCtgCommonID,
                        CASE
                            WHEN EXISTS (
                                SELECT 1 
                                FROM T_STRU_COMMON_ITEM_MST WITH(NOLOCK)
                                WHERE StruItemID = i.ItemID
                                  AND StruItemCommonID IN ({illumineCollection})
                            ) THEN 'Y' 
                            ELSE 'N'
                        END AS item_illumine
                 FROM dbo.T_CART_MST_ITEM c WITH(NOLOCK)
                 INNER JOIN dbo.T_ITEM_MST i WITH(NOLOCK) ON c.CartItemMstID = i.ItemID
                 WHERE c.CartMstID = @cart_id";

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, dbConnection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cart_id", SqlDbType.Int) { Value = cart_id });

                        await using var reader = await cmd.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            CartItemWithIllumineList.Add(new CartItemWithIllumine
                            {
                                CartItemID = reader.GetInt32("CartItemID"),
                                ItemID = reader.GetInt32("ItemID"),
                                ItemSoliterSts = reader.GetString("ItemSoliterSts"),
                                ItemCtgCommonID = reader.GetInt32("ItemCtgCommonID"),
                                item_illumine = reader.GetString("item_illumine")
                            });
                        }

                        return (List<CartItemWithIllumine>)CartItemWithIllumineList;
                    }
                }
            }
            catch (Exception)
            {
                CartItemWithIllumineList = new List<CartItemWithIllumine>();
                throw;
            }
        }

        public async Task<List<SolitaireStock>> GetSolitaireStockAsync(int cart_id, int item_id, int cart_item_id)
        {
            IList<SolitaireStock> stockList = new List<SolitaireStock>();

            try
            {
                string query = @"
                SELECT ISNULL(isAvailableStk, 'N') AS isAvailableStk,CartSoliStkNo
                FROM dbo.T_CART_ITEM_SOLI_MST WITH(NOLOCK)
                WHERE CartMstId      = @cart_id
                AND ItemMstId      = @item_id
                AND CartItemMstId  = @cart_item_id";

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(query, dbConnection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@cart_id", SqlDbType.Int) { Value = cart_id });
                        cmd.Parameters.Add(new SqlParameter("@item_id", SqlDbType.Int) { Value = item_id });
                        cmd.Parameters.Add(new SqlParameter("@cart_item_id", SqlDbType.Int) { Value = cart_item_id });

                        await using var reader = await cmd.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            stockList.Add(new SolitaireStock
                            {
                                isAvailableStk = reader.GetString("isAvailableStk"),
                                CartSoliStkNo = reader.GetString("CartSoliStkNo")
                            });
                        }

                        return (List<SolitaireStock>)stockList;
                    }
                }
            }
            catch (Exception)
            {
                stockList = new List<SolitaireStock>();
                throw;
            }
        }

        public async Task<(string gold, string dollar)> GetGoldDollarValuesAsync()
        {
            const string sql = "SELECT Value FROM T_DIA_COMMON_MST WHERE ValidSts = 'Y' ORDER BY (CASE WHEN Value LIKE '%Gold%' THEN 0 ELSE 1 END)";

            await using var conn = new SqlConnection(_connection);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(sql, conn);

            await using var reader = await cmd.ExecuteReaderAsync();
            var values = new List<string>();
            while (await reader.ReadAsync())
                values.Add(reader.GetString(0));

            return (values.Count > 0 ? values[0] : "0", values.Count > 1 ? values[1] : "0");
        }

        public async Task<bool> IsStoneFreeAsync(string stkNo)
        {
            const string sql = @"
                    SELECT 1 
                    FROM T_SOLI_DIA_STK_AVAILABLE WITH(NOLOCK) 
                    WHERE DiaStkNO = @stkNo AND isBookded = 'N'";

            await using var conn = new SqlConnection(_connection);
            await conn.OpenAsync();

            await using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@stkNo", SqlDbType.VarChar) { Value = stkNo });

            var obj = await cmd.ExecuteScalarAsync();
            return obj != null;
        }

        public async Task BookAvailableStonesAsync(IEnumerable<string> packetNos)
        {
            if (!packetNos.Any()) return;

            var placeholders = string.Join(",", packetNos.Select((_, i) => $"@p{i}"));
            var sql = $"UPDATE T_SOLI_DIA_STK_AVAILABLE SET isBookded = 'Y' WHERE DiaStkNO IN ({placeholders})";

            await using var conn = new SqlConnection(_connection);
            await conn.OpenAsync();
            await using var cmd = new SqlCommand(sql, conn);

            int index = 0;
            foreach (var no in packetNos)
                cmd.Parameters.Add(new SqlParameter($"@p{index++}", SqlDbType.VarChar) { Value = no });

            await cmd.ExecuteNonQueryAsync();
        }
    }
}