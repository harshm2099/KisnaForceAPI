using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class ItemsService : IItemsService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        public ServiceResponse<IList<MasterItems>> GetAllItems()
        {
            IList<MasterItems> items = new List<MasterItems>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    try
                                    {
                                        items.Add(new MasterItems
                                        {
                                            ItemID = Convert.ToInt32(dataReader["ItemID"]),
                                            ItemCode = Convert.ToString(dataReader["ItemCd"]),
                                            ItemName = Convert.ToString(dataReader["ItemName"]),
                                            ItemDesc = Convert.ToString(dataReader["ItemDesc"]),
                                            ItemValidSts = Convert.ToString(dataReader["ItemValidSts"]),
                                            ItemImgID = dataReader["ItemImgID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemImgID"]) : 0,
                                            ItemSKU = Convert.ToString(dataReader["ItemSKU"]),
                                            ItemMRP = dataReader["ItemMRP"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemMRP"]) : 0,
                                            ItemDiscount = dataReader["ItemDiscount"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemDiscount"]) : 0,
                                            ItemMargin = dataReader["ItemMargin"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemMargin"]) : 0,
                                            ItemRPrice = dataReader["ItemRPrice"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemRPrice"]) : 0,
                                            ItemMargin1 = dataReader["ItemMargin1"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemMargin1"]) : 0,
                                            ItemDPrice = dataReader["ItemDPrice"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemDPrice"]) : 0,
                                            ItemUOMID = dataReader["ItemUOMID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemUOMID"]) : 0,
                                            ItemGST = dataReader["ItemGST"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemGST"]) : 0,
                                            ItemPPTag = Convert.ToString(dataReader["ItemPPTag"]),
                                            ItemUsrID = Convert.ToString(dataReader["ItemUsrID"]),
                                            //ItemEntDt = Convert.ToDateTime(dataReader["ItemEntDt"]),
                                            //ItemCngDt = Convert.ToDateTime(dataReader["ItemCngDt"]),
                                            ItemEntDt = Convert.ToString(dataReader["ItemEntDt"]),
                                            ItemCngDt = Convert.ToString(dataReader["ItemCngDt"]),
                                            ItemOdDmCd = Convert.ToString(dataReader["ItemOdDmCd"]),
                                            ItemOdSfx = Convert.ToString(dataReader["ItemOdSfx"]),
                                            ItemOdDmSz = Convert.ToString(dataReader["ItemOdDmSz"]),
                                            ItemOdKt = Convert.ToString(dataReader["ItemOdKt"]),
                                            ItemOdDmCol = Convert.ToString(dataReader["ItemOdDmCol"]),
                                            ItemOdIdNo = dataReader["ItemOdIdNo"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemOdIdNo"]) : 0,
                                            ItemMainSts = Convert.ToString(dataReader["ItemMainSts"]),
                                            ItemStarNo = dataReader["ItemStarNo"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemStarNo"]) : 0,
                                            ItemMetalCommonID = dataReader["ItemMetalCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemMetalCommonID"]) : 0,
                                            ItemMetalWt = dataReader["ItemMetalWt"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemMetalWt"]) : 0,
                                            ItemStoneCommonID = dataReader["ItemStoneCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemStoneCommonID"]) : 0,
                                            ItemStoneWt = dataReader["ItemStoneWt"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemStoneWt"]) : 0,
                                            ItemStoneQty = dataReader["ItemStoneQty"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemStoneQty"]) : 0,
                                            ItemStoneQltyCommonID = dataReader["ItemStoneQltyCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemStoneQltyCommonID"]) : 0,
                                            ItemStoneColorCommonID = dataReader["ItemStoneColorCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemStoneColorCommonID"]) : 0,
                                            ItemStoneShapeCommonID = dataReader["ItemStoneShapeCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemStoneShapeCommonID"]) : 0,
                                            ItemBrandCommonID = dataReader["ItemBrandCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemBrandCommonID"]) : 0,
                                            ItemGenderCommonID = dataReader["ItemGenderCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemGenderCommonID"]) : 0,
                                            ItemDesignerCommonID = dataReader["ItemDesignerCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemDesignerCommonID"]) : 0,
                                            ItemCadDesignerCommonID = dataReader["ItemCadDesignerCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemCadDesignerCommonID"]) : 0,
                                            ItemGrossWt = dataReader["ItemGrossWt"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemGrossWt"]) : 0,
                                            ItemOtherWt = dataReader["ItemOtherWt"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemOtherWt"]) : 0,
                                            ItemCtgCommonID = dataReader["ItemCtgCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemCtgCommonID"]) : 0,
                                            ItemValidStsRemarks = Convert.ToString(dataReader["ItemValidStsRemarks"]),
                                            ItemNosePinScrewSts = Convert.ToString(dataReader["ItemNosePinScrewSts"]),
                                            ItemSoliterSts = Convert.ToString(dataReader["ItemSoliterSts"]),
                                            ItemPlainGold = Convert.ToString(dataReader["ItemPlainGold"]),
                                            ItemGoldLabourper = dataReader["ItemGoldLabourper"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemGoldLabourper"]) : 0,
                                            ItemAproxDay = Convert.ToString(dataReader["ItemAproxDay"]),
                                            ItemAproxDayCommonID = dataReader["ItemAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemAproxDayCommonID"]) : 0,
                                            ItemDisLabourPer = dataReader["ItemDisLabourPer"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemDisLabourPer"]) : 0,
                                            ItemStatusRemark = Convert.ToString(dataReader["ItemStatusRemark"]),
                                            ItemStatusUsrID = dataReader["ItemStatusUsrID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemStatusUsrID"]) : 0,
                                            //ItemStatusEntDt = Convert.ToDateTime(dataReader["ItemStatusEntDt"]),
                                            ItemStatusEntDt = Convert.ToString(dataReader["ItemStatusEntDt"]),
                                            ItemDiscontinueDate = Convert.ToString(dataReader["ItemDiscontinueDate"]),
                                            ItemDiscontinueRemark = Convert.ToString(dataReader["ItemDiscontinueRemark"]),
                                            ItemSubCollCommonID = dataReader["ItemSubCollCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemSubCollCommonID"]) : 0,
                                            ItemFranchiseSts = Convert.ToString(dataReader["ItemFranchiseSts"]),
                                            ItemIsMRP = Convert.ToString(dataReader["ItemIsMRP"]),
                                            ItemFixLabourSts = Convert.ToString(dataReader["ItemFixLabourSts"]),
                                            ItemFixLabourValue = dataReader["ItemFixLabourValue"] != DBNull.Value ? Convert.ToDecimal(dataReader["ItemFixLabourValue"]) : 0,
                                            ItemSubCtgCommonID = dataReader["ItemSubCtgCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemSubCtgCommonID"]) : 0,
                                            ItemSubSubCtgCommonID = dataReader["ItemSubSubCtgCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemSubSubCtgCommonID"]) : 0,
                                            ItemHeight = dataReader["ItemHeight"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemHeight"]) : 0,
                                            ItemWidth = dataReader["ItemWidth"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemWidth"]) : 0,
                                            ItemDAproxDay = Convert.ToString(dataReader["ItemDAproxDay"]),
                                            ItemDAproxDayCommonID = dataReader["ItemDAproxDayCommonID"] != DBNull.Value ? Convert.ToInt32(dataReader["ItemDAproxDayCommonID"]) : 0,
                                            ItemIsSRP = Convert.ToString(dataReader["ItemIsSRP"]),
                                            ItemSizeAvailable = Convert.ToString(dataReader["ItemSizeAvailable"]),
                                            InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                            InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                            UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                            UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                            IsActive = dataReader["IsActive"] != DBNull.Value ? Convert.ToBoolean(dataReader["IsActive"]) : true,
                                        });

                                    }
                                    catch (Exception ex)
                                    {

                                        throw;
                                    }
                                }
                            }
                        }
                    }
                }
                if (items.Count > 0)
                {
                    return new ServiceResponse<IList<MasterItems>>
                    {
                        Success = true,
                        Message = "Items retrieved successfully.",
                        Data = items
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterItems>>
                    {
                        Success = true,
                        Message = "No items found.",
                        Data = items // 'items' is the empty list in this case
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterItems>>
                {
                    Success = false,
                    Message = sqlEx.Message,
                    Data = null
                };
            }
        }

        public async Task<CommonResponse> AddItems(MasterItems Items)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", Items.ItemCode);
                        cmd.Parameters.AddWithValue("@MstName", Items.ItemName);
                        cmd.Parameters.AddWithValue("@MstDesc", Items.ItemDesc);
                        cmd.Parameters.AddWithValue("@ItemValidSts", Items.ItemValidSts);
                        cmd.Parameters.AddWithValue("@ItemImgID", Items.ItemImgID);
                        cmd.Parameters.AddWithValue("@ItemSKU", Items.ItemSKU);
                        cmd.Parameters.AddWithValue("@ItemMRP", Items.ItemMRP);
                        cmd.Parameters.AddWithValue("@ItemDiscount", Items.ItemDiscount);
                        cmd.Parameters.AddWithValue("@ItemMargin", Items.ItemMargin);
                        cmd.Parameters.AddWithValue("@ItemRPrice", Items.ItemRPrice);
                        cmd.Parameters.AddWithValue("@ItemMargin1", Items.ItemMargin1);
                        cmd.Parameters.AddWithValue("@ItemDPrice", Items.ItemDPrice);
                        cmd.Parameters.AddWithValue("@ItemUOMID", Items.ItemUOMID);
                        cmd.Parameters.AddWithValue("@ItemGST", Items.ItemGST);
                        cmd.Parameters.AddWithValue("@ItemPPTag", Items.ItemPPTag);
                        cmd.Parameters.AddWithValue("@ItemUsrID", Items.ItemUsrID);
                        cmd.Parameters.AddWithValue("@ItemEntDt", Items.ItemEntDt);
                        cmd.Parameters.AddWithValue("@ItemCngDt", Items.ItemCngDt);
                        cmd.Parameters.AddWithValue("@ItemOdDmCd", Items.ItemOdDmCd);
                        cmd.Parameters.AddWithValue("@ItemOdSfx", Items.ItemOdSfx);
                        cmd.Parameters.AddWithValue("@ItemOdDmSz", Items.ItemOdDmSz);
                        cmd.Parameters.AddWithValue("@ItemOdKt", Items.ItemOdKt);
                        cmd.Parameters.AddWithValue("@ItemOdDmCol", Items.ItemOdDmCol);
                        cmd.Parameters.AddWithValue("@ItemOdIdNo", Items.ItemOdIdNo);
                        cmd.Parameters.AddWithValue("@ItemMainSts", Items.ItemMainSts);
                        cmd.Parameters.AddWithValue("@ItemStarNo", Items.ItemStarNo);
                        cmd.Parameters.AddWithValue("@ItemMetalCommonID", Items.ItemMetalCommonID);
                        cmd.Parameters.AddWithValue("@ItemMetalWt", Items.ItemMetalWt);
                        cmd.Parameters.AddWithValue("@ItemStoneCommonID", Items.ItemStoneCommonID);
                        cmd.Parameters.AddWithValue("@ItemStoneWt", Items.ItemStoneWt);
                        cmd.Parameters.AddWithValue("@ItemStoneQty", Items.ItemStoneQty);
                        cmd.Parameters.AddWithValue("@ItemStoneQltyCommonID", Items.ItemStoneQltyCommonID);
                        cmd.Parameters.AddWithValue("@ItemStoneColorCommonID", Items.ItemStoneColorCommonID);
                        cmd.Parameters.AddWithValue("@ItemStoneShapeCommonID", Items.ItemStoneShapeCommonID);
                        cmd.Parameters.AddWithValue("@ItemBrandCommonID", Items.ItemBrandCommonID);
                        cmd.Parameters.AddWithValue("@ItemGenderCommonID", Items.ItemGenderCommonID);
                        cmd.Parameters.AddWithValue("@ItemDesignerCommonID", Items.ItemDesignerCommonID);
                        cmd.Parameters.AddWithValue("@ItemCadDesignerCommonID", Items.ItemCadDesignerCommonID);
                        cmd.Parameters.AddWithValue("@ItemGrossWt", Items.ItemGrossWt);
                        cmd.Parameters.AddWithValue("@ItemOtherWt", Items.ItemOtherWt);
                        cmd.Parameters.AddWithValue("@ItemCtgCommonID", Items.ItemCtgCommonID);
                        cmd.Parameters.AddWithValue("@ItemValidStsRemarks", Items.ItemValidStsRemarks);
                        cmd.Parameters.AddWithValue("@ItemNosePinScrewSts", Items.ItemNosePinScrewSts);
                        cmd.Parameters.AddWithValue("@ItemSoliterSts", Items.ItemSoliterSts);
                        cmd.Parameters.AddWithValue("@ItemPlainGold", Items.ItemPlainGold);
                        cmd.Parameters.AddWithValue("@ItemGoldLabourper", Items.ItemGoldLabourper);
                        cmd.Parameters.AddWithValue("@ItemAproxDay", Items.ItemAproxDay);
                        cmd.Parameters.AddWithValue("@ItemAproxDayCommonID", Items.ItemAproxDayCommonID);
                        cmd.Parameters.AddWithValue("@ItemDisLabourPer", Items.ItemDisLabourPer);
                        cmd.Parameters.AddWithValue("@ItemStatusRemark", Items.ItemStatusRemark);
                        cmd.Parameters.AddWithValue("@ItemStatusUsrID", Items.ItemStatusUsrID);
                        cmd.Parameters.AddWithValue("@ItemStatusEntDt", Items.ItemStatusEntDt);
                        cmd.Parameters.AddWithValue("@ItemDiscontinueDate", Items.ItemDiscontinueDate);
                        cmd.Parameters.AddWithValue("@ItemDiscontinueRemark", Items.ItemDiscontinueRemark);
                        cmd.Parameters.AddWithValue("@ItemSubCollCommonID", Items.ItemSubCollCommonID);
                        cmd.Parameters.AddWithValue("@ItemFranchiseSts", Items.ItemFranchiseSts);
                        cmd.Parameters.AddWithValue("@ItemIsMRP", Items.ItemIsMRP);
                        cmd.Parameters.AddWithValue("@ItemFixLabourSts", Items.ItemFixLabourSts);
                        cmd.Parameters.AddWithValue("@ItemFixLabourValue", Items.ItemFixLabourValue);
                        cmd.Parameters.AddWithValue("@ItemSubCtgCommonID", Items.ItemSubCtgCommonID);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgCommonID", Items.ItemSubSubCtgCommonID);
                        cmd.Parameters.AddWithValue("@ItemHeight", Items.ItemHeight);
                        cmd.Parameters.AddWithValue("@ItemWidth", Items.ItemWidth);
                        cmd.Parameters.AddWithValue("@ItemDAproxDay", Items.ItemDAproxDay);
                        cmd.Parameters.AddWithValue("@ItemDAproxDayCommonID", Items.ItemDAproxDayCommonID);
                        cmd.Parameters.AddWithValue("@ItemIsSRP", Items.ItemIsSRP);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", Items.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);


                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                            return new CommonResponse
                            {
                                status = "200",
                                status_code = "200",
                                success = true
                            };
                        else
                            return new CommonResponse
                            {
                                status = "400",
                                status_code = "400",
                                success = false,
                                message = "Something went wrong. Please check the data",
                            };

                    }
                }

            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    status_code = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}",
                };
            }
        }

        public async Task<CommonResponse> EditItem(MasterItems Item)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", Item.ItemCode);
                        cmd.Parameters.AddWithValue("@MstName", Item.ItemName);
                        cmd.Parameters.AddWithValue("@MstDesc", Item.ItemDesc);
                        cmd.Parameters.AddWithValue("@ItemValidSts", Item.ItemValidSts);
                        cmd.Parameters.AddWithValue("@ItemImgID", Item.ItemImgID);
                        cmd.Parameters.AddWithValue("@ItemSKU", Item.ItemSKU);
                        cmd.Parameters.AddWithValue("@ItemMRP", Item.ItemMRP);
                        cmd.Parameters.AddWithValue("@ItemDiscount", Item.ItemDiscount);
                        cmd.Parameters.AddWithValue("@ItemMargin", Item.ItemMargin);
                        cmd.Parameters.AddWithValue("@ItemRPrice", Item.ItemRPrice);
                        cmd.Parameters.AddWithValue("@ItemMargin1", Item.ItemMargin1);
                        cmd.Parameters.AddWithValue("@ItemDPrice", Item.ItemDPrice);
                        cmd.Parameters.AddWithValue("@ItemUOMID", Item.ItemUOMID);
                        cmd.Parameters.AddWithValue("@ItemGST", Item.ItemGST);
                        cmd.Parameters.AddWithValue("@ItemPPTag", Item.ItemPPTag);
                        cmd.Parameters.AddWithValue("@ItemUsrID", Item.ItemUsrID);
                        cmd.Parameters.AddWithValue("@ItemEntDt", Item.ItemEntDt);
                        cmd.Parameters.AddWithValue("@ItemCngDt", Item.ItemCngDt);
                        cmd.Parameters.AddWithValue("@ItemOdDmCd", Item.ItemOdDmCd);
                        cmd.Parameters.AddWithValue("@ItemOdSfx", Item.ItemOdSfx);
                        cmd.Parameters.AddWithValue("@ItemOdDmSz", Item.ItemOdDmSz);
                        cmd.Parameters.AddWithValue("@ItemOdKt", Item.ItemOdKt);
                        cmd.Parameters.AddWithValue("@ItemOdDmCol", Item.ItemOdDmCol);
                        cmd.Parameters.AddWithValue("@ItemOdIdNo", Item.ItemOdIdNo);
                        cmd.Parameters.AddWithValue("@ItemMainSts", Item.ItemMainSts);
                        cmd.Parameters.AddWithValue("@ItemStarNo", Item.ItemStarNo);
                        cmd.Parameters.AddWithValue("@ItemMetalCommonID", Item.ItemMetalCommonID);
                        cmd.Parameters.AddWithValue("@ItemMetalWt", Item.ItemMetalWt);
                        cmd.Parameters.AddWithValue("@ItemStoneCommonID", Item.ItemStoneCommonID);
                        cmd.Parameters.AddWithValue("@ItemStoneWt", Item.ItemStoneWt);
                        cmd.Parameters.AddWithValue("@ItemStoneQty", Item.ItemStoneQty);
                        cmd.Parameters.AddWithValue("@ItemStoneQltyCommonID", Item.ItemStoneQltyCommonID);
                        cmd.Parameters.AddWithValue("@ItemStoneColorCommonID", Item.ItemStoneColorCommonID);
                        cmd.Parameters.AddWithValue("@ItemStoneShapeCommonID", Item.ItemStoneShapeCommonID);
                        cmd.Parameters.AddWithValue("@ItemBrandCommonID", Item.ItemBrandCommonID);
                        cmd.Parameters.AddWithValue("@ItemGenderCommonID", Item.ItemGenderCommonID);
                        cmd.Parameters.AddWithValue("@ItemDesignerCommonID", Item.ItemDesignerCommonID);
                        cmd.Parameters.AddWithValue("@ItemCadDesignerCommonID", Item.ItemCadDesignerCommonID);
                        cmd.Parameters.AddWithValue("@ItemGrossWt", Item.ItemGrossWt);
                        cmd.Parameters.AddWithValue("@ItemOtherWt", Item.ItemOtherWt);
                        cmd.Parameters.AddWithValue("@ItemCtgCommonID", Item.ItemCtgCommonID);
                        cmd.Parameters.AddWithValue("@ItemValidStsRemarks", Item.ItemValidStsRemarks);
                        cmd.Parameters.AddWithValue("@ItemNosePinScrewSts", Item.ItemNosePinScrewSts);
                        cmd.Parameters.AddWithValue("@ItemSoliterSts", Item.ItemSoliterSts);
                        cmd.Parameters.AddWithValue("@ItemPlainGold", Item.ItemPlainGold);
                        cmd.Parameters.AddWithValue("@ItemGoldLabourper", Item.ItemGoldLabourper);
                        cmd.Parameters.AddWithValue("@ItemAproxDay", Item.ItemAproxDay);
                        cmd.Parameters.AddWithValue("@ItemAproxDayCommonID", Item.ItemAproxDayCommonID);
                        cmd.Parameters.AddWithValue("@ItemDisLabourPer", Item.ItemDisLabourPer);
                        cmd.Parameters.AddWithValue("@ItemStatusRemark", Item.ItemStatusRemark);
                        cmd.Parameters.AddWithValue("@ItemStatusUsrID", Item.ItemStatusUsrID);
                        cmd.Parameters.AddWithValue("@ItemStatusEntDt", Item.ItemStatusEntDt);
                        cmd.Parameters.AddWithValue("@ItemDiscontinueDate", Item.ItemDiscontinueDate);
                        cmd.Parameters.AddWithValue("@ItemDiscontinueRemark", Item.ItemDiscontinueRemark);
                        cmd.Parameters.AddWithValue("@ItemSubCollCommonID", Item.ItemSubCollCommonID);
                        cmd.Parameters.AddWithValue("@ItemFranchiseSts", Item.ItemFranchiseSts);
                        cmd.Parameters.AddWithValue("@ItemIsMRP", Item.ItemIsMRP);
                        cmd.Parameters.AddWithValue("@ItemFixLabourSts", Item.ItemFixLabourSts);
                        cmd.Parameters.AddWithValue("@ItemFixLabourValue", Item.ItemFixLabourValue);
                        cmd.Parameters.AddWithValue("@ItemSubCtgCommonID", Item.ItemSubCtgCommonID);
                        cmd.Parameters.AddWithValue("@ItemSubSubCtgCommonID", Item.ItemSubSubCtgCommonID);
                        cmd.Parameters.AddWithValue("@ItemHeight", Item.ItemHeight);
                        cmd.Parameters.AddWithValue("@ItemWidth", Item.ItemWidth);
                        cmd.Parameters.AddWithValue("@ItemDAproxDay", Item.ItemDAproxDay);
                        cmd.Parameters.AddWithValue("@ItemDAproxDayCommonID", Item.ItemDAproxDayCommonID);
                        cmd.Parameters.AddWithValue("@ItemIsSRP", Item.ItemIsSRP);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@UpdatedBy", Item.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", Item.ItemID);
                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                            return new CommonResponse
                            {
                                status = "200",
                                status_code = "200",
                                success = true
                            };
                        else
                            return new CommonResponse
                            {
                                status = "400",
                                status_code = "400",
                                success = false,
                                message = "Something went wrong. Please check the data",
                            };

                    }
                }

            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    status_code = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}",
                };
            }
        }

        public async Task<CommonResponse> DisableItem(MasterItems item)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", item.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", item.ItemID);
                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                            return new CommonResponse
                            {
                                status = "200",
                                status_code = "200",
                                success = true
                            };
                        else
                            return new CommonResponse
                            {
                                status = "400",
                                status_code = "400",
                                success = false,
                                message = "Something went wrong. Please check the data",
                            };

                    }
                }

            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    status_code = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}",
                };
            }
        }

        public ServiceResponse<IList<ItemCount>> GetAllItemsCount()
        {
            IList<ItemCount> items = new List<ItemCount>();
            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 4);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    try
                                    {
                                        items.Add(new ItemCount
                                        {
                                            ItemCountcnt = Convert.ToInt32(dataReader["ITEMCOUNT"]),
                                        });

                                    }
                                    catch (Exception ex)
                                    {

                                        throw;
                                    }

                                }
                            }
                        }
                    }
                }
                if (items.Count > 0)
                {
                    return new ServiceResponse<IList<ItemCount>>
                    {
                        Success = true,
                        Message = "Items retrieved successfully.",
                        Data = items
                    };
                }
                else
                {
                    return new ServiceResponse<IList<ItemCount>>
                    {
                        Success = true,
                        Message = "No items found.",
                        Data = items // 'items' is the empty list in this case
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<ItemCount>>
                {
                    Success = false,
                    Message = sqlEx.Message,
                    Data = null
                };
            }
        }
    }
}
