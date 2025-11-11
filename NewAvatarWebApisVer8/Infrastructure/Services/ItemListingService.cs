using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class ItemListingService : IItemListingService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        public ServiceResponse<IList<ItemListing>> GetItemsListing(ItemListingParams item)
        {

            IList<ItemListingParams> items = new List<ItemListingParams>();
            IList<ItemListing> itemslisting = new List<ItemListing>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEM_LISTING;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", item.UserId);
                        cmd.Parameters.AddWithValue("@DataId", item.DataId);
                        cmd.Parameters.AddWithValue("@UserTypeId", item.UserTypeId);
                        cmd.Parameters.AddWithValue("@Mode", item.Mode);
                        cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                        cmd.Parameters.AddWithValue("@SortId", item.SortId);
                        cmd.Parameters.AddWithValue("@Variant", item.Variant);
                        cmd.Parameters.AddWithValue("@Item_Name", item.Item_Name);
                        cmd.Parameters.AddWithValue("@SubCategoryID", item.SubCategoryID);
                        cmd.Parameters.AddWithValue("@RingSize", item.RingSize);
                        cmd.Parameters.AddWithValue("@DsgKt", item.DsgKt);
                        cmd.Parameters.AddWithValue("@DsgColor", item.DsgColor);
                        cmd.Parameters.AddWithValue("@Price", item.price);
                        cmd.Parameters.AddWithValue("@Metalwt", item.metalwt);
                        cmd.Parameters.AddWithValue("@Diawt", item.diawt);
                        cmd.Parameters.AddWithValue("@Item_ID", item.Item_ID);
                        cmd.Parameters.AddWithValue("@Stock_Av", item.Stock_Av);
                        cmd.Parameters.AddWithValue("@Family_Av", item.Family_Av);
                        cmd.Parameters.AddWithValue("@Regular_Av", item.Regular_Av);
                        cmd.Parameters.AddWithValue("@Wearit", item.wearit);
                        cmd.Parameters.AddWithValue("@Tryon", item.tryon);
                        cmd.Parameters.AddWithValue("@Gender", item.gender);
                        cmd.Parameters.AddWithValue("@TagWiseFilter", item.TagWiseFilter);
                        cmd.Parameters.AddWithValue("@BrandWiseFilter", item.BrandWiseFilter);
                        cmd.Parameters.AddWithValue("@Delivery_days", item.delivery_days);
                        cmd.Parameters.AddWithValue("@ItemSubCtgIDs", item.ItemSubCtgIDs);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgIDs", item.ItemSubSubCtgIDs);
                        cmd.Parameters.AddWithValue("@SalesLocation", item.salesLocation);
                        cmd.Parameters.AddWithValue("@DesginTimeLine", item.desginTimeLine);


                        int id = cmd.ExecuteNonQuery();

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    itemslisting.Add(new ItemListing
                                    {
                                        item_id = Convert.ToInt32(dataReader["item_id"]),
                                        category_id = Convert.ToInt32(dataReader["category_id"]),
                                        item_code = Convert.ToString(dataReader["item_code"]),
                                        ItemAproxDay = Convert.ToString(dataReader["ItemAproxDay"]),
                                        item_name = Convert.ToString(dataReader["item_name"]),
                                        item_description = Convert.ToString(dataReader["item_description"]),
                                        item_mrp = Convert.ToDecimal(dataReader["item_mrp"]),
                                        item_price = Convert.ToDecimal(dataReader["item_price"]),
                                        dist_price = Convert.ToInt32(dataReader["dist_price"]),
                                        dsg_size = Convert.ToString(dataReader["dsg_size"]),
                                        dsg_kt = Convert.ToString(dataReader["dsg_kt"]),
                                        dsg_color = Convert.ToString(dataReader["dsg_color"]),
                                        item_kt = Convert.ToString(dataReader["item_kt"]),
                                        item_color = Convert.ToString(dataReader["item_color"]),
                                        item_metal = Convert.ToString(dataReader["item_metal"]),
                                        item_stone = Convert.ToString(dataReader["item_stone"]),
                                        item_stone_wt = Convert.ToDecimal(dataReader["item_stone_wt"]),
                                        item_stone_qty = Convert.ToInt32(dataReader["item_stone_qty"]),
                                        star_color = Convert.ToString(dataReader["star_color"]),
                                        ItemMetalCommonID = Convert.ToInt32(dataReader["ItemMetalCommonID"]),
                                        ItemNosePinScrewSts = Convert.ToString(dataReader["ItemNosePinScrewSts"]),
                                        ItemAproxDayCommonID = Convert.ToInt32(dataReader["ItemAproxDayCommonID"]),
                                        priceflag = Convert.ToString(dataReader["priceflag"]),
                                        ItemPlainGold = Convert.ToString(dataReader["ItemPlainGold"]),
                                        ItemSoliterSts = Convert.ToString(dataReader["ItemSoliterSts"]),
                                        ItemGenderCommonID = Convert.ToInt32(dataReader["ItemGenderCommonID"]),
                                        sub_category_id = Convert.ToInt32(dataReader["sub_category_id"]),
                                        max_qty_sold = Convert.ToString(dataReader["max_qty_sold"]),
                                        cart_img = Convert.ToString(dataReader["cart_img"]),
                                        image_path = Convert.ToString(dataReader["image_path"]),
                                    });
                                }
                            }
                        }

                    }
                }
                if (itemslisting.Count > 0)

                {
                    return new ServiceResponse<IList<ItemListing>>
                    {
                        Success = true,
                        Message = "Items retrieved successfully.",
                        Data = itemslisting
                    };
                }
                else
                {
                    return new ServiceResponse<IList<ItemListing>>
                    {
                        Success = true,
                        Message = "No items found.",
                        Data = itemslisting // 'itemslisting' is the empty list in this case
                    };
                }

            }
            catch (SqlException sqlEx)
            {

                return new ServiceResponse<IList<ItemListing>>
                {
                    Success = false,
                    Message = sqlEx.Message,
                    Data = null
                };
            }

        }
    }
}
