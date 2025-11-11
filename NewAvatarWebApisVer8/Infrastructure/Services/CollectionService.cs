using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Core.Domain.Common.NewAvatarWebApis.Models;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using Newtonsoft.Json;
using System.Data;
using System.Web.Http;
using Xunit.Abstractions;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly string _Connection = DBCommands.CONNECTION_STRING;
        private readonly IConfiguration _configuration;

        public ServiceResponse<bool> AddCollectionMapping(CollectionItemMapping CollectionMap)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    string cmdQuery = DBCommands.COLLECTION_MAPPING;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", CollectionMap.ItemCode);
                        cmd.Parameters.AddWithValue("@ItemOdDmCd", CollectionMap.ItemOdDmCd);
                        cmd.Parameters.AddWithValue("@ItemOdSfx", CollectionMap.ItemOdSfx);
                        cmd.Parameters.AddWithValue("@UserIds", CollectionMap.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);
                        cmd.Parameters.AddWithValue("@CollectionId", CollectionMap.CollectionId);

                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                            return new ServiceResponse<bool> { Success = true, Message = "Success", Data = true };
                        else
                            return new ServiceResponse<bool> { Success = false, Message = "Something went wrong. Please check the data", Data = false };

                    }
                }

            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<bool> { Success = false, Message = sqlEx.Message, Data = false };
            }
        }

        public ServiceResponse<IList<MasterItemsrequest>> GetCollectionItemsById(string collectionid)
        {
            IList<MasterItemsrequest> items = new List<MasterItemsrequest>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))

                {
                    string cmdQuery = DBCommands.GET_COLLECTION_BY_ID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CollectionId", collectionid);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    items.Add(new MasterItemsrequest
                                    {
                                        ItemID = Convert.ToInt32(dataReader["ItemID"]),
                                        ItemCode = Convert.ToString(dataReader["ItemCd"]),
                                        ItemName = Convert.ToString(dataReader["ItemName"]),
                                        ItemOdDmCd = Convert.ToString(dataReader["ItemOdDmCd"]),
                                        ItemOdSfx = Convert.ToString(dataReader["ItemOdSfx"]),
                                        ItemDesc = Convert.ToString(dataReader["ItemDesc"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (items.Count > 0)
                {
                    return new ServiceResponse<IList<MasterItemsrequest>>
                    {
                        Success = true,
                        Message = "Items retrieved successfully.",
                        Data = items
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterItemsrequest>>
                    {
                        Success = true,
                        Message = "No items found for this collection.",
                        Data = items // 'items' is the empty list in this case
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterItemsrequest>>
                {
                    Success = false,
                    Message = sqlEx.Message,
                    Data = null
                };
            }
        }

        public ServiceResponse<bool> EditCollectionMapping(ClsEditCollectionItemMapping CollectionMap)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    string cmdQuery = DBCommands.COLLECTION_MAPPING;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemCode", CollectionMap.ItemCode); // "100883,100884"
                        cmd.Parameters.AddWithValue("@ItemOdDmCd", CollectionMap.ItemOdDmCd); //"1000"
                        cmd.Parameters.AddWithValue("@ItemOdSfx", CollectionMap.ItemOdSfx);
                        cmd.Parameters.AddWithValue("@UserIds", CollectionMap.UpdatedBy);
                        cmd.Parameters.AddWithValue("@CollectionId", CollectionMap.CollectionId); // "100883,100884"
                        cmd.Parameters.AddWithValue("@Flag", 2);

                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                            return new ServiceResponse<bool> { Success = true, Message = "Success", Data = true };
                        else
                            return new ServiceResponse<bool> { Success = false, Message = "Something went wrong. Please check the data", Data = false };

                    }
                }

            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<bool> { Success = false, Message = sqlEx.Message, Data = false };
            }
        }

        public ServiceResponse<IList<CustomCollectionResponse>> GetCustomCollectionList(CustomCollectionList param, [FromHeader] CommonHeader header)
        {
            IList<CustomCollectionResponse> items = new List<CustomCollectionResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))

                {
                    string cmdQuery = DBCommands.GETCUSTOMCOLLECTION;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string data_id = param.dataId as string ?? string.Empty;
                        string data_login_type = param.dataLoginType as string ?? string.Empty;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    items.Add(new CustomCollectionResponse
                                    {
                                        CollectionId = dataReader["collection_id"] != DBNull.Value ? Convert.ToString(dataReader["collection_id"]) : null,
                                        CollectionName = dataReader["collection_name"] != DBNull.Value ? Convert.ToString(dataReader["collection_name"]) : null,
                                        CollectionImage = dataReader["collection_image"] != DBNull.Value ? Convert.ToString(dataReader["collection_image"]) : null,
                                    });
                                }
                            }
                        }
                    }
                }
                if (items.Count > 0)
                {
                    return new ServiceResponse<IList<CustomCollectionResponse>>
                    {
                        Success = true,
                        Message = "Items retrieved successfully.",
                        Data = items
                    };
                }
                else
                {
                    return new ServiceResponse<IList<CustomCollectionResponse>>
                    {
                        Success = true,
                        Message = "No items found for this collection.",
                        Data = items
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<CustomCollectionResponse>>
                {
                    Success = false,
                    Message = sqlEx.Message,
                    Data = null
                };
            }
        }

        public ServiceResponse<IList<CustomCollectionCategoriesResposne>> GetCustomCollectionCategoryList(CollectionCategoryRequest param, [FromHeader] CommonHeader header)
        {
            IList<CustomCollectionCategoriesResposne> items = new List<CustomCollectionCategoriesResposne>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))

                {
                    string cmdQuery = DBCommands.GETCUSTOMCOLLECTIONCATEGORIES;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string data_id = param.dataId as string ?? string.Empty;
                        string data_login_type = param.dataLoginType as string ?? string.Empty;
                        string collection_id = param.collectionId as string ?? string.Empty;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@collection_id", collection_id);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    items.Add(new CustomCollectionCategoriesResposne
                                    {
                                        categoryId = dataReader["category_id"] != DBNull.Value ? Convert.ToString(dataReader["category_id"]) : null,
                                        categoryName = dataReader["category_name"] != DBNull.Value ? Convert.ToString(dataReader["category_name"]) : null,
                                        imagePath = dataReader["image_path"] != DBNull.Value ? Convert.ToString(dataReader["image_path"]) : null,
                                        mstSortBy = dataReader["MstSortBy"] != DBNull.Value ? Convert.ToString(dataReader["MstSortBy"]) : null,
                                    });
                                }
                            }
                        }
                    }
                }
                if (items.Count > 0)
                {
                    return new ServiceResponse<IList<CustomCollectionCategoriesResposne>>
                    {
                        Success = true,
                        Message = "Items retrieved successfully.",
                        Data = items
                    };
                }
                else
                {
                    return new ServiceResponse<IList<CustomCollectionCategoriesResposne>>
                    {
                        Success = true,
                        Message = "No items found for this collection.",
                        Data = items
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<CustomCollectionCategoriesResposne>>
                {
                    Success = false,
                    Message = sqlEx.Message,
                    Data = null
                };
            }
        }

        public async Task<ResponseDetails> CustomCollectionFilter(CustomCollectionFilter param, [FromHeader] CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<CustomCollectionFilterResponse> filterList = new List<CustomCollectionFilterResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    string cmdQuery = DBCommands.CustomCollectionFilter;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string data_id = string.IsNullOrWhiteSpace(param.dataId) ? "" : param.dataId;
                        string data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? "" : param.dataLoginType;
                        string collection_id = string.IsNullOrWhiteSpace(param.collectionId) ? "" : param.collectionId;
                        string category_id = string.IsNullOrWhiteSpace(param.categoryId) ? "" : param.categoryId;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@CategoryID", category_id);
                        cmd.Parameters.AddWithValue("@CollectionID", collection_id);
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
                                            string filterGroup = rowdetails["FilterGroup"] != DBNull.Value ? Convert.ToString(rowdetails["FilterGroup"]) : string.Empty;
                                            string name = rowdetails["Name"] != DBNull.Value ? Convert.ToString(rowdetails["Name"]) : string.Empty;
                                            string id = rowdetails["ID"] != DBNull.Value ? Convert.ToString(rowdetails["ID"]) : string.Empty;
                                            string count = rowdetails["Count"] != DBNull.Value ? Convert.ToString(rowdetails["Count"]) : string.Empty;
                                            string minValue = rowdetails["MinVal"] != DBNull.Value ? Convert.ToString(rowdetails["MinVal"]) : string.Empty;
                                            string maxValue = rowdetails["MaxVal"] != DBNull.Value ? Convert.ToString(rowdetails["MaxVal"]) : string.Empty;

                                            filterList.Add(new CustomCollectionFilterResponse
                                            {
                                                filterGroup = filterGroup,
                                                name = name,
                                                id = id,
                                                count = count,
                                                minValue = minValue,
                                                maxValue = maxValue
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
                if (filterList.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data1 = filterList;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data1 = new List<CustomCollectionFilterResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data1 = new List<CustomCollectionFilterResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CustomCollectionSubCategoryList(CustomCollectionFilter param, [FromHeader] CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<CustomCollectionSubCategoryListResponse> solitaireSortBy = new List<CustomCollectionSubCategoryListResponse>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    string cmdQuery = DBCommands.CustomCollectionSubCategoryList;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string data_id = string.IsNullOrWhiteSpace(param.dataId) ? "" : param.dataId;
                        string data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? "" : param.dataLoginType;
                        string collection_id = string.IsNullOrWhiteSpace(param.collectionId) ? "" : param.collectionId;
                        string category_id = string.IsNullOrWhiteSpace(param.categoryId) ? "" : param.categoryId;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type_id", data_login_type);
                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@collection_id", collection_id);
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
                                            string itemSubCategiryId = rowdetails["item_sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_sub_category_id"]) : string.Empty;
                                            string itemSubCategiryName = rowdetails["item_sub_category_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_sub_category_name"]) : string.Empty;
                                            string itemSubCategiryCount = rowdetails["item_sub_category_count"] != DBNull.Value ? Convert.ToString(rowdetails["item_sub_category_count"]) : string.Empty;

                                            solitaireSortBy.Add(new CustomCollectionSubCategoryListResponse
                                            {
                                                itemSubCategoryId = itemSubCategiryId,
                                                itemSubCategoryName = itemSubCategiryName,
                                                itemSubCategoryCount = itemSubCategiryCount,
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
                if (solitaireSortBy.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.data = solitaireSortBy;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CustomCollectionSubCategoryListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CustomCollectionSubCategoryListResponse>();
                return responseDetails;
            }
        }

        public async Task<ResponseDetails> CustomCollectionItemList(CustomCollectionItemListRequest param, [FromHeader] CommonHeader header)
        {
            var responseDetails = new ResponseDetails();
            IList<CustomCollectionItemListResponse> customCollectionlist = new List<CustomCollectionItemListResponse>();
            int? totalItems = null;
            int? lastPage = null;
            int? currentPage = null;
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    string cmdQuery = DBCommands.CustomCollectionItemList;
                    await dbConnection.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        string? data_id = string.IsNullOrWhiteSpace(param.dataId) ? null : param.dataId;
                        string? data_login_type = string.IsNullOrWhiteSpace(param.dataLoginType) ? null : param.dataLoginType;
                        string? collection_id = string.IsNullOrWhiteSpace(param.collectionId) ? null : param.collectionId;
                        string? category_id = string.IsNullOrWhiteSpace(param.categoryId) ? null : param.categoryId;
                        string? page = string.IsNullOrWhiteSpace(param.page) ? null : param.page;
                        string? limit = string.IsNullOrWhiteSpace(param.limit) ? null : param.limit;
                        

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        cmd.Parameters.AddWithValue("@data_id", data_id);
                        cmd.Parameters.AddWithValue("@data_login_type", data_login_type);
                        cmd.Parameters.AddWithValue("@collection_id", collection_id);
                        cmd.Parameters.AddWithValue("@category_id", category_id);
                        cmd.Parameters.AddWithValue("@page", page);
                        cmd.Parameters.AddWithValue("@default_limit_app_page", limit);
                            
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
                                            string itemId = rowdetails["item_id"] != DBNull.Value ? Convert.ToString(rowdetails["item_id"]) : string.Empty;
                                            string categoryId = rowdetails["category_id"] != DBNull.Value ? Convert.ToString(rowdetails["category_id"]) : string.Empty;
                                            string itemDescription = rowdetails["item_description"] != DBNull.Value ? Convert.ToString(rowdetails["item_description"]) : string.Empty;
                                            string itemCode = rowdetails["item_code"] != DBNull.Value ? Convert.ToString(rowdetails["item_code"]) : string.Empty;
                                            string itemName = rowdetails["item_name"] != DBNull.Value ? Convert.ToString(rowdetails["item_name"]) : string.Empty;
                                            string itemGenderCommonID = rowdetails["ItemGenderCommonID"] != DBNull.Value ? Convert.ToString(rowdetails["ItemGenderCommonID"]) : string.Empty;
                                            string itemNosePinScrewSts = rowdetails["ItemNosePinScrewSts"] != DBNull.Value ? Convert.ToString(rowdetails["ItemNosePinScrewSts"]) : string.Empty;
                                            string itemKt = rowdetails["item_kt"] != DBNull.Value ? Convert.ToString(rowdetails["item_kt"]) : string.Empty;
                                            string approxDeliveryDate = rowdetails["ApproxDeliveryDate"] != DBNull.Value ? Convert.ToString(rowdetails["ApproxDeliveryDate"]) : string.Empty;
                                            string itemBrandText = rowdetails["item_brand_text"] != DBNull.Value ? Convert.ToString(rowdetails["item_brand_text"]) : string.Empty;
                                            string itemIsSRP = rowdetails["ItemIsSRP"] != DBNull.Value ? Convert.ToString(rowdetails["ItemIsSRP"]) : string.Empty;
                                            string subCategoryId = rowdetails["sub_category_id"] != DBNull.Value ? Convert.ToString(rowdetails["sub_category_id"]) : string.Empty;
                                            string itemMrp = rowdetails["item_mrp"] != DBNull.Value ? Convert.ToString(rowdetails["item_mrp"]) : string.Empty;
                                            string priceFlag = rowdetails["priceflag"] != DBNull.Value ? Convert.ToString(rowdetails["priceflag"]) : string.Empty;
                                            string maxQtySold = rowdetails["max_qty_sold"] != DBNull.Value ? Convert.ToString(rowdetails["max_qty_sold"]) : string.Empty;
                                            string imagePath = rowdetails["image_path"] != DBNull.Value ? Convert.ToString(rowdetails["image_path"]) : string.Empty;
                                            string mostOrder = rowdetails["mostOrder"] != DBNull.Value ? Convert.ToString(rowdetails["mostOrder"]) : string.Empty;
                                            string plainGoldStatus = rowdetails["plaingold_status"] != DBNull.Value ? Convert.ToString(rowdetails["plaingold_status"]) : string.Empty;
                                            string itemPlainGold = rowdetails["ItemPlainGold"] != DBNull.Value ? Convert.ToString(rowdetails["ItemPlainGold"]) : string.Empty;
                                            string isStockFilter = rowdetails["isStockFilter"] != DBNull.Value ? Convert.ToString(rowdetails["isStockFilter"]) : string.Empty;
                                            string productTags = rowdetails["productTags"] != DBNull.Value ? Convert.ToString(rowdetails["productTags"]) : string.Empty;
                                            string isInFranchiseStore = rowdetails["isInFranchiseStore"] != DBNull.Value ? Convert.ToString(rowdetails["isInFranchiseStore"]) : string.Empty;
                                            totalItems = rowdetails["total_items"] as int? ?? (rowdetails["total_items"] != DBNull.Value ? Convert.ToInt32(rowdetails["total_items"]) : (int?)null);
                                            lastPage = rowdetails["last_page"] as int? ?? (rowdetails["last_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["last_page"]) : (int?)null);
                                            currentPage = rowdetails["current_page"] as int? ?? (rowdetails["current_page"] != DBNull.Value ? Convert.ToInt32(rowdetails["current_page"]) : (int?)null);

                                            customCollectionlist.Add(new CustomCollectionItemListResponse
                                            {
                                                ItemId = itemId,
                                                CategoryID = categoryId,
                                                ItemDescription = itemDescription,
                                                ItemCode = itemCode,
                                                ItemName = itemName,
                                                ItemGenderCommonId = itemGenderCommonID,
                                                ItemNosePinScrewSts = itemNosePinScrewSts,
                                                ItemKt = itemKt,
                                                ApproxDeliveryDate = approxDeliveryDate,
                                                ItemBrandText = itemBrandText,
                                                ItemIsSRP = itemIsSRP,
                                                SubCategoryID = subCategoryId,
                                                ItemMrp = itemMrp,
                                                PriceFlag = priceFlag,
                                                MaxQtySold = maxQtySold,
                                                ImagePath = imagePath,
                                                MostOrder = mostOrder,
                                                PlainGoldStatus = plainGoldStatus,
                                                ItemPlainGold = itemPlainGold,
                                                IsStockFilter = isStockFilter,
                                                ProductTags = productTags,
                                                IsInFranchiseStore = isInFranchiseStore
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
                if (customCollectionlist.Any())
                {
                    responseDetails.success = true;
                    responseDetails.message = "Successfully";
                    responseDetails.status = "200";
                    responseDetails.current_page = currentPage?.ToString();
                    responseDetails.last_page = lastPage?.ToString();
                    responseDetails.total_items = totalItems?.ToString();
                    responseDetails.data = customCollectionlist;
                }
                else
                {
                    responseDetails.success = false;
                    responseDetails.message = "No data found";
                    responseDetails.status = "200";
                    responseDetails.data = new List<CustomCollectionItemListResponse>();
                }
                return responseDetails;
            }
            catch (SqlException sqlEx)
            {
                responseDetails.success = false;
                responseDetails.message = $"SQL error: {sqlEx.Message}";
                responseDetails.status = "400";
                responseDetails.data = new List<CustomCollectionItemListResponse>();
                return responseDetails;
            }
        }
    }
}
