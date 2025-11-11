using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using Newtonsoft.Json;
using System.Data;
using System.Text.RegularExpressions;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class MasterService : IMasterService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        //ZONES//
        public ServiceResponse<IList<MasterZone>> GetAllZones()
        {
            IList<MasterZone> zones = new List<MasterZone>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ZONE_MST;
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
                                    zones.Add(new MasterZone
                                    {
                                        ZoneID = Convert.ToInt32(dataReader["ZoneID"]),
                                        ZoneCode = Convert.ToString(dataReader["ZoneCode"]),
                                        ZoneName = Convert.ToString(dataReader["ZoneName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (zones.Count > 0)
                {
                    return new ServiceResponse<IList<MasterZone>>
                    {
                        Success = true,
                        Data = zones
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterZone>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterZone>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddZones(MasterZone zone)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ZONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", zone.ZoneCode);
                        cmd.Parameters.AddWithValue("@MstName", zone.ZoneName);
                        cmd.Parameters.AddWithValue("@MstDesc", zone.Description);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", zone.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditZones(MasterZone zone)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ZONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", zone.ZoneCode);
                        cmd.Parameters.AddWithValue("@MstName", zone.ZoneName);
                        cmd.Parameters.AddWithValue("@MstDesc", zone.Description);
                        cmd.Parameters.AddWithValue("@UpdatedBy", zone.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", zone.ZoneID);
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

        public ServiceResponse<bool> DisableZones(MasterZone zone)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ZONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", zone.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", zone.ZoneID);
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

        public ServiceResponse<IList<MasterZone>> GetZoneByID(int id)
        {
            IList<MasterZone> zones = new List<MasterZone>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ZONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstID", id);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    zones.Add(new MasterZone
                                    {
                                        ZoneID = Convert.ToInt32(dataReader["ZoneID"]),
                                        ZoneCode = Convert.ToString(dataReader["ZoneCode"]),
                                        ZoneName = Convert.ToString(dataReader["ZoneName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (zones.Count > 0)
                {
                    return new ServiceResponse<IList<MasterZone>>
                    {
                        Success = true,
                        Data = zones
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterZone>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterZone>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        //BRANDS//
        public ServiceResponse<IList<MasterBrand>> GetAllBrands()
        {
            IList<MasterBrand> brands = new List<MasterBrand>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.BRAND_MST;
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
                                    brands.Add(new MasterBrand
                                    {
                                        BrandID = Convert.ToInt32(dataReader["BrandID"]),
                                        BrandName = Convert.ToString(dataReader["BrandName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (brands.Count > 0)
                {
                    return new ServiceResponse<IList<MasterBrand>>
                    {
                        Success = true,
                        Data = brands
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterBrand>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterBrand>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddBrands(MasterBrand brand)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.BRAND_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", brand.BrandName);
                        cmd.Parameters.AddWithValue("@MstDesc", brand.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", brand.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", brand.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditBrands(MasterBrand brand)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.BRAND_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", brand.BrandName);
                        cmd.Parameters.AddWithValue("@MstDesc", brand.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", brand.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", brand.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", brand.BrandID);

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

        public ServiceResponse<bool> DisableBrands(MasterBrand brand)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.BRAND_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", brand.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", brand.BrandID);
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

        public ServiceResponse<IList<MasterBrand>> GetBrandByID(int id)
        {
            IList<MasterBrand> brands = new List<MasterBrand>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.BRAND_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstID", id);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    brands.Add(new MasterBrand
                                    {
                                        BrandID = Convert.ToInt32(dataReader["BrandID"]),
                                        BrandName = Convert.ToString(dataReader["BrandName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (brands.Count > 0)
                {
                    return new ServiceResponse<IList<MasterBrand>>
                    {
                        Success = true,
                        Data = brands
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterBrand>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterBrand>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterBrand>> GetBrandByName(string name)
        {
            IList<MasterBrand> brands = new List<MasterBrand>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.BRAND_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", name);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    brands.Add(new MasterBrand
                                    {
                                        BrandID = Convert.ToInt32(dataReader["BrandID"]),
                                        BrandName = Convert.ToString(dataReader["BrandName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (brands.Count > 0)
                {
                    return new ServiceResponse<IList<MasterBrand>>
                    {
                        Success = true,
                        Data = brands
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterBrand>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterBrand>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        //CATEGORY//
        public ServiceResponse<IList<MasterCategory>> GetAllCategories()
        {
            IList<MasterCategory> categories = new List<MasterCategory>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CATEGORY_MST;
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
                                    categories.Add(new MasterCategory
                                    {
                                        CategoryID = Convert.ToInt32(dataReader["CategoryID"]),
                                        CategoryName = Convert.ToString(dataReader["CategoryName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (categories.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCategory>>
                    {
                        Success = true,
                        Data = categories
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCategory>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCategory>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddCategory(MasterCategory category)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CATEGORY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", category.CategoryName);
                        cmd.Parameters.AddWithValue("@MstDesc", category.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", category.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", category.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditCategory(MasterCategory category)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CATEGORY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", category.CategoryName);
                        cmd.Parameters.AddWithValue("@MstDesc", category.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", category.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@UpdatedBy", category.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", category.CategoryID);

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

        public ServiceResponse<bool> DisableCategory(MasterCategory category)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.CATEGORY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", category.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", category.CategoryID);
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

        public ServiceResponse<IList<MasterCategory>> GetCategoryByID(int id)
        {
            IList<MasterCategory> category = new List<MasterCategory>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CATEGORY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstID", id);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    category.Add(new MasterCategory
                                    {
                                        CategoryID = Convert.ToInt32(dataReader["CategoryID"]),
                                        CategoryName = Convert.ToString(dataReader["CategoryName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (category.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCategory>>
                    {
                        Success = true,
                        Data = category
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCategory>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCategory>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterCategory>> GetCategoryByName(string name)
        {
            IList<MasterCategory> category = new List<MasterCategory>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CATEGORY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", name);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    category.Add(new MasterCategory
                                    {
                                        CategoryID = Convert.ToInt32(dataReader["CategoryID"]),
                                        CategoryName = Convert.ToString(dataReader["CategoryName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (category.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCategory>>
                    {
                        Success = true,
                        Data = category
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCategory>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCategory>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        //REGION//
        public ServiceResponse<IList<MasterRegion>> GetAllRegions()
        {
            IList<MasterRegion> regions = new List<MasterRegion>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.REGION_MST;
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
                                    regions.Add(new MasterRegion
                                    {
                                        RegionID = Convert.ToInt32(dataReader["RegionID"]),
                                        ZoneID = Convert.ToInt32(dataReader["ZoneID"]),
                                        RegionCode = Convert.ToString(dataReader["RegionCode"]),
                                        RegionName = Convert.ToString(dataReader["RegionName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (regions.Count > 0)
                {
                    return new ServiceResponse<IList<MasterRegion>>
                    {
                        Success = true,
                        Data = regions
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterRegion>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterRegion>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterRegion>> GetAllRegionByZoneId(string zoneid)
        {
            IList<MasterRegion> regions = new List<MasterRegion>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.REGION_MST_BY_ZONEID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ZoneID", zoneid);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    regions.Add(new MasterRegion
                                    {
                                        RegionID = Convert.ToInt32(dataReader["RegionID"]),
                                        ZoneID = Convert.ToInt32(dataReader["ZoneID"]),
                                        RegionCode = Convert.ToString(dataReader["RegionCode"]),
                                        RegionName = Convert.ToString(dataReader["RegionName"]),
                                        Description = Convert.ToString(dataReader["Description"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (regions.Count > 0)
                {
                    return new ServiceResponse<IList<MasterRegion>>
                    {
                        Success = true,
                        Data = regions
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterRegion>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterRegion>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddRegion(MasterRegion region)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.REGION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ZoneID", region.ZoneID);
                        cmd.Parameters.AddWithValue("@MstCd", region.RegionCode);
                        cmd.Parameters.AddWithValue("@MstName", region.RegionName);
                        cmd.Parameters.AddWithValue("@MstDesc", region.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", region.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", region.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditRegion(MasterRegion region)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.REGION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ZoneID", region.ZoneID);
                        cmd.Parameters.AddWithValue("@MstCd", region.RegionCode);
                        cmd.Parameters.AddWithValue("@MstName", region.RegionName);
                        cmd.Parameters.AddWithValue("@MstDesc", region.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", region.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", region.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", region.RegionID);

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

        public ServiceResponse<bool> DisableRegion(MasterRegion region)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.REGION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", region.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", region.RegionID);
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

        public ServiceResponse<IList<MasterRegion>> GetRegionByID(int id)
        {
            IList<MasterRegion> regions = new List<MasterRegion>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.REGION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstID", id);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    regions.Add(new MasterRegion
                                    {
                                        RegionID = Convert.ToInt32(dataReader["RegionID"]),
                                        ZoneID = Convert.ToInt32(dataReader["ZoneID"]),
                                        RegionCode = Convert.ToString(dataReader["RegionCode"]),
                                        RegionName = Convert.ToString(dataReader["RegionName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (regions.Count > 0)

                {
                    return new ServiceResponse<IList<MasterRegion>>
                    {
                        Success = true,
                        Data = regions
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterRegion>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterRegion>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterRegion>> GetRegionByName(MasterRegion region)
        {
            IList<MasterRegion> regions = new List<MasterRegion>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.REGION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", region.RegionName);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    regions.Add(new MasterRegion
                                    {
                                        RegionID = Convert.ToInt32(dataReader["RegionID"]),
                                        ZoneID = Convert.ToInt32(dataReader["ZoneID"]),
                                        RegionCode = Convert.ToString(dataReader["RegionCode"]),
                                        RegionName = Convert.ToString(dataReader["RegionName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (regions.Count > 0)
                {
                    return new ServiceResponse<IList<MasterRegion>>
                    {
                        Success = true,
                        Data = regions
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterRegion>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterRegion>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        //STATE//
        public ServiceResponse<IList<MasterState>> GetAllStates()
        {
            IList<MasterState> states = new List<MasterState>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STATE_MST;
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
                                    states.Add(new MasterState
                                    {
                                        StateID = dataReader["StateID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["StateID"]),
                                        ZoneID = dataReader["ZoneID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ZoneID"]),
                                        StateCode = dataReader["StateCode"] == DBNull.Value ? "0" : Convert.ToString(dataReader["StateCode"]),
                                        StateName = dataReader["StateName"] == DBNull.Value ? "0" : Convert.ToString(dataReader["StateName"]),
                                        Description = dataReader["Description"] == DBNull.Value ? "0" : Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = dataReader["InsertedBy"] == DBNull.Value ? "0" : Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = dataReader["UpdatedBy"] == DBNull.Value ? "" : Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = dataReader["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (states.Count > 0)
                {
                    return new ServiceResponse<IList<MasterState>>
                    {
                        Success = true,
                        Data = states
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterState>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterState>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterState>> GetAllStatesByRegionId(string regionid)
        {
            IList<MasterState> states = new List<MasterState>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STATE_MST_BY_REGIONID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RegionId", regionid);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    states.Add(new MasterState
                                    {
                                        StateID = Convert.ToInt32(dataReader["StateID"]),
                                        RegionID = Convert.ToInt32(dataReader["RegionID"]),
                                        StateCode = Convert.ToString(dataReader["StateCode"]),
                                        StateName = Convert.ToString(dataReader["StateName"]),
                                        Description = Convert.ToString(dataReader["Description"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (states.Count > 0)
                {
                    return new ServiceResponse<IList<MasterState>>
                    {
                        Success = true,
                        Data = states
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterState>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterState>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterState>> GetAllStatesByzoneId(string zoneid)
        {
            IList<MasterState> states = new List<MasterState>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STATE_MST_BY_ZONEID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ZoneId", zoneid);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    states.Add(new MasterState
                                    {
                                        StateID = Convert.ToInt32(dataReader["StateID"]),
                                        ZoneID = Convert.ToInt32(dataReader["ZoneId"]),
                                        StateCode = Convert.ToString(dataReader["StateCode"]),
                                        StateName = Convert.ToString(dataReader["StateName"]),
                                        Description = Convert.ToString(dataReader["Description"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (states.Count > 0)
                {
                    return new ServiceResponse<IList<MasterState>>
                    {
                        Success = true,
                        Data = states
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterState>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterState>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddState(MasterState state)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STATE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ZoneID", state.ZoneID);
                        cmd.Parameters.AddWithValue("@MstCd", state.StateCode);
                        cmd.Parameters.AddWithValue("@MstName", state.StateName);
                        cmd.Parameters.AddWithValue("@MstDesc", state.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", state.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", state.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditState(MasterState state)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STATE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ZoneID", state.ZoneID);
                        cmd.Parameters.AddWithValue("@MstCd", state.StateCode);
                        cmd.Parameters.AddWithValue("@MstName", state.StateName);
                        cmd.Parameters.AddWithValue("@MstDesc", state.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", state.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", state.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", state.StateID);

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

        public ServiceResponse<bool> DisableState(MasterState state)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STATE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", state.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", state.StateID);
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

        //DISTRICT//
        public ServiceResponse<IList<MasterDistrict>> GetAllDistricts()
        {
            IList<MasterDistrict> districts = new List<MasterDistrict>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DISTRICT_MST;
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
                                    districts.Add(new MasterDistrict
                                    {
                                        DistrictID = Convert.ToInt32(dataReader["DistrictID"]),
                                        StateID = Convert.ToInt32(dataReader["StateID"]),
                                        DistrictCode = Convert.ToString(dataReader["DistrictCode"]),
                                        DistrictName = Convert.ToString(dataReader["DistrictName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (districts.Count > 0)
                {
                    return new ServiceResponse<IList<MasterDistrict>>
                    {
                        Success = true,
                        Data = districts
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterDistrict>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterDistrict>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterDistrict>> GetAllDistrictsByStateId(string stateid)
        {
            IList<MasterDistrict> districts = new List<MasterDistrict>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DISTRICT_MST_BY_STATEID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StateId", stateid);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    districts.Add(new MasterDistrict
                                    {
                                        DistrictID = Convert.ToInt32(dataReader["DistrictID"]),
                                        StateID = Convert.ToInt32(dataReader["StateID"]),
                                        DistrictCode = Convert.ToString(dataReader["DistrictCode"]),
                                        DistrictName = Convert.ToString(dataReader["DistrictName"]),
                                        Description = Convert.ToString(dataReader["Description"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (districts.Count > 0)
                {
                    return new ServiceResponse<IList<MasterDistrict>>
                    {
                        Success = true,
                        Data = districts
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterDistrict>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterDistrict>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterTerritory>> GetAllTerritorysByStateId(string stateid)
        {
            IList<MasterTerritory> territory = new List<MasterTerritory>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TERRITORY_MST_BY_STATEID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StateId", stateid);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    territory.Add(new MasterTerritory
                                    {
                                        MstId = Convert.ToInt32(dataReader["MstID"]),
                                        MstName = Convert.ToString(dataReader["MstName"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (territory.Count > 0)
                {
                    return new ServiceResponse<IList<MasterTerritory>>
                    {
                        Success = true,
                        Data = territory
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterTerritory>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterTerritory>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddDistrict(MasterDistrict district)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DISTRICT_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StateID", district.StateID);
                        cmd.Parameters.AddWithValue("@MstCd", district.DistrictCode);
                        cmd.Parameters.AddWithValue("@MstName", district.DistrictName);
                        cmd.Parameters.AddWithValue("@MstDesc", district.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", district.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", district.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditDistrict(MasterDistrict district)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DISTRICT_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StateID", district.StateID);
                        cmd.Parameters.AddWithValue("@MstCd", district.DistrictCode);
                        cmd.Parameters.AddWithValue("@MstName", district.DistrictName);
                        cmd.Parameters.AddWithValue("@MstDesc", district.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", district.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", district.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", district.DistrictID);

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

        public ServiceResponse<bool> DisableDistrict(MasterDistrict district)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DISTRICT_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", district.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", district.DistrictID);
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

        //TALUKA//
        public ServiceResponse<IList<MasterTaluka>> GetAllTalukas()
        {
            IList<MasterTaluka> talukas = new List<MasterTaluka>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TALUKA_MST;
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
                                    talukas.Add(new MasterTaluka
                                    {
                                        TalukaID = Convert.ToInt32(dataReader["TalukaID"]),
                                        DistrictID = Convert.ToInt32(dataReader["DistrictID"]),
                                        TalukaCode = Convert.ToString(dataReader["TalukaCode"]),
                                        TalukaName = Convert.ToString(dataReader["TalukaName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = (dataReader["InsertedBy"] == null || dataReader["InsertedBy"] == DBNull.Value) ? string.Empty : Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = (dataReader["InsertedOn"] == null || dataReader["InsertedOn"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = (dataReader["InsertedBy"] == null || dataReader["InsertedBy"] == DBNull.Value) ? string.Empty : Convert.ToString(dataReader["InsertedBy"]),
                                        UpdatedOn = (dataReader["UpdatedOn"] == null || dataReader["UpdatedOn"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (talukas.Count > 0)
                {
                    return new ServiceResponse<IList<MasterTaluka>>
                    {
                        Success = true,
                        Data = talukas
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterTaluka>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterTaluka>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterTaluka>> GetAllTalukasByDistrictId(string districtid)
        {
            IList<MasterTaluka> talukas = new List<MasterTaluka>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TALUKA_MST_BY_DISTRICTID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DistrictId", districtid);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    talukas.Add(new MasterTaluka
                                    {
                                        TalukaID = Convert.ToInt32(dataReader["TalukaID"]),
                                        DistrictID = Convert.ToInt32(dataReader["DistrictID"]),
                                        TalukaCode = Convert.ToString(dataReader["TalukaCode"]),
                                        TalukaName = Convert.ToString(dataReader["TalukaName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (talukas.Count > 0)
                {
                    return new ServiceResponse<IList<MasterTaluka>>
                    {
                        Success = true,
                        Data = talukas
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterTaluka>>
                    {
                        Success = true,
                        Message = "No items found."
                    };  
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterTaluka>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddTaluka(MasterTaluka taluka)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TALUKA_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DistrictID", taluka.DistrictID);
                        cmd.Parameters.AddWithValue("@MstCd", taluka.TalukaCode);
                        cmd.Parameters.AddWithValue("@MstName", taluka.TalukaName);
                        cmd.Parameters.AddWithValue("@MstDesc", taluka.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", taluka.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", taluka.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditTaluka(MasterTaluka taluka)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TALUKA_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DistrictID", taluka.DistrictID);
                        cmd.Parameters.AddWithValue("@MstCd", taluka.TalukaCode);
                        cmd.Parameters.AddWithValue("@MstName", taluka.TalukaName);
                        cmd.Parameters.AddWithValue("@MstDesc", taluka.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", taluka.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", taluka.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", taluka.TalukaID);

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

        public ServiceResponse<bool> DisableTaluka(MasterTaluka taluka)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.TALUKA_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", taluka.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", taluka.TalukaID);
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

        //CITY//
        public ServiceResponse<IList<MasterCity>> GetAllCities()
        {
            IList<MasterCity> cities = new List<MasterCity>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CITY_MST;
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
                                    cities.Add(new MasterCity
                                    {
                                        CityID = Convert.ToInt32(dataReader["CityID"]),
                                        DistrictID = dataReader["DistrictID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DistrictID"]),
                                        TerritoryId = dataReader["TerritoryId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["TerritoryId"]),
                                        CityCode = Convert.ToString(dataReader["CityCode"]),
                                        CityName = Convert.ToString(dataReader["CityName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCity>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCity>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCity>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterCity>> GetCityByID(int id)
        {
            IList<MasterCity> cities = new List<MasterCity>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstID", id);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    cities.Add(new MasterCity
                                    {
                                        CityID = Convert.ToInt32(dataReader["CityID"]),
                                        DistrictID = Convert.ToInt32(dataReader["DistrictID"]),
                                        DistrictName = dataReader["DistrictName"] == DBNull.Value ? "" : Convert.ToString(dataReader["DistrictName"]),
                                        TerritoryName = dataReader["MstCd"] == DBNull.Value ? "" : Convert.ToString(dataReader["MstCd"]),
                                        CityCode = Convert.ToString(dataReader["CityCode"]),
                                        CityName = Convert.ToString(dataReader["CityName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == null ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCity>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCity>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCity>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterCity>> GetAllCityByDistrictId(string districtid)
        {
            IList<MasterCity> cities = new List<MasterCity>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CITY_MST_BY_DISTRICTID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DistrictId", districtid);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    cities.Add(new MasterCity
                                    {
                                        CityID = Convert.ToInt32(dataReader["CityID"]),
                                        DistrictID = Convert.ToInt32(dataReader["DistrictID"]),
                                        CityCode = Convert.ToString(dataReader["CityCode"]),
                                        CityName = Convert.ToString(dataReader["CityName"]),
                                        Description = Convert.ToString(dataReader["Description"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCity>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCity>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCity>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterCity>> GetAllCityByterritoryId(string territoryid)
        {
            IList<MasterCity> cities = new List<MasterCity>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CITY_MST_BY_TERRITORYID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TerritoryId", territoryid);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    cities.Add(new MasterCity
                                    {
                                        CityID = Convert.ToInt32(dataReader["CityID"]),
                                        DistrictID = Convert.ToInt32(dataReader["DistrictID"]),
                                        CityCode = Convert.ToString(dataReader["CityCode"]),
                                        CityName = Convert.ToString(dataReader["CityName"]),
                                        Description = Convert.ToString(dataReader["Description"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCity>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCity>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCity>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddCity(MasterCity city)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DistrictID", city.DistrictID);
                        cmd.Parameters.AddWithValue("@TerritoryId", city.TerritoryId);
                        cmd.Parameters.AddWithValue("@MstCd", city.CityCode);
                        cmd.Parameters.AddWithValue("@MstName", city.CityName);
                        cmd.Parameters.AddWithValue("@MstDesc", city.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", city.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", city.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditCity(MasterCity city)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DistrictID", city.DistrictID);
                        cmd.Parameters.AddWithValue("@TerritoryId", city.TerritoryId);
                        cmd.Parameters.AddWithValue("@MstCd", city.CityCode);
                        cmd.Parameters.AddWithValue("@MstName", city.CityName);
                        cmd.Parameters.AddWithValue("@MstDesc", city.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", city.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", city.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", city.CityID);

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

        public ServiceResponse<bool> DisableCity(MasterCity city)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", city.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", city.CityID);
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

        //AREA//
        public ServiceResponse<IList<MasterArea>> GetAllAreas()
        {
            IList<MasterArea> cities = new List<MasterArea>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.AREA_MST;
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
                                    cities.Add(new MasterArea
                                    {
                                        AreaID = Convert.ToInt32(dataReader["AreaID"]),
                                        CityID = dataReader["CityID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["CityID"]),
                                        AreaCode = Convert.ToString(dataReader["AreaCode"]),
                                        AreaName = Convert.ToString(dataReader["AreaName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterArea>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterArea>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterArea>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<MasterArea>> GetAllAreasByCityId(string cityid)
        {
            IList<MasterArea> cities = new List<MasterArea>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.AREA_MST_BY_CITYID;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CityId", cityid);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    cities.Add(new MasterArea
                                    {
                                        AreaID = Convert.ToInt32(dataReader["AreaID"]),
                                        CityID = Convert.ToInt32(dataReader["CityID"]),
                                        AreaCode = Convert.ToString(dataReader["AreaCode"]),
                                        AreaName = Convert.ToString(dataReader["AreaName"]),
                                        Description = Convert.ToString(dataReader["Description"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterArea>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterArea>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterArea>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddArea(MasterArea area)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.AREA_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CityID", area.CityID);
                        cmd.Parameters.AddWithValue("@MstCd", area.AreaCode);
                        cmd.Parameters.AddWithValue("@MstName", area.AreaName);
                        cmd.Parameters.AddWithValue("@MstDesc", area.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", area.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", area.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditArea(MasterArea area)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.AREA_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CityID", area.CityID);
                        cmd.Parameters.AddWithValue("@MstCd", area.AreaCode);
                        cmd.Parameters.AddWithValue("@MstName", area.AreaName);
                        cmd.Parameters.AddWithValue("@MstDesc", area.Description);
                        cmd.Parameters.AddWithValue("@SortOrder", area.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", area.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", area.AreaID);

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

        public ServiceResponse<bool> DisableArea(MasterArea area)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.AREA_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", area.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", area.AreaID);
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

        //ANTIQUE//
        public ServiceResponse<IList<MasterAntique>> GetAllAntiques()
        {
            IList<MasterAntique> cities = new List<MasterAntique>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ANTIQUE_MST;
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
                                    cities.Add(new MasterAntique
                                    {
                                        PartID = Convert.ToInt32(dataReader["PartID"]),
                                        PartCode = Convert.ToString(dataReader["PartCode"]),
                                        PartName = Convert.ToString(dataReader["PartName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterAntique>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterAntique>>
                    {
                        Success = true,
                        Message = "No items found."
                    }; ;
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterAntique>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddAntique(MasterAntique antique)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ANTIQUE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", antique.PartCode);
                        cmd.Parameters.AddWithValue("@MstName", antique.PartName);
                        cmd.Parameters.AddWithValue("@MstDesc", antique.Description);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", antique.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditAntique(MasterAntique antique)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ANTIQUE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", antique.PartCode);
                        cmd.Parameters.AddWithValue("@MstName", antique.PartName);
                        cmd.Parameters.AddWithValue("@MstDesc", antique.Description);
                        cmd.Parameters.AddWithValue("@UpdatedBy", antique.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", antique.PartID);

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

        public ServiceResponse<bool> DisableAntique(MasterAntique antique)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ANTIQUE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", antique.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", antique.PartID);
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

        //COLOR//
        public ServiceResponse<IList<MasterColor>> GetAllColors()
        {
            IList<MasterColor> cities = new List<MasterColor>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLOR_MST;
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
                                    cities.Add(new MasterColor
                                    {
                                        ColorID = Convert.ToInt32(dataReader["ColorID"]),
                                        ColorCode = Convert.ToString(dataReader["ColorCode"]),
                                        ColorName = Convert.ToString(dataReader["ColorName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterColor>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterColor>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterColor>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddColor(MasterColor color)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLOR_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", color.ColorCode);
                        cmd.Parameters.AddWithValue("@MstName", color.ColorName);
                        cmd.Parameters.AddWithValue("@MstDesc", color.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", color.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", color.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditColor(MasterColor color)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLOR_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", color.ColorCode);
                        cmd.Parameters.AddWithValue("@MstName", color.ColorName);
                        cmd.Parameters.AddWithValue("@MstDesc", color.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", color.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", color.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", color.ColorID);

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

        public ServiceResponse<bool> DisableColor(MasterColor color)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLOR_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", color.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", color.ColorID);
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

        //CADDESIGNER//
        public ServiceResponse<IList<MasterCADDesigner>> GetAllDesigners()
        {
            IList<MasterCADDesigner> cities = new List<MasterCADDesigner>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CADDESIGNER_MST;
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
                                    cities.Add(new MasterCADDesigner
                                    {
                                        DsgID = Convert.ToInt32(dataReader["DsgID"]),
                                        DsgCode = Convert.ToString(dataReader["DsgCode"]),
                                        DsgName = Convert.ToString(dataReader["DsgName"]),
                                        Remarks = Convert.ToString(dataReader["Remarks"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCADDesigner>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCADDesigner>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCADDesigner>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddDesigner(MasterCADDesigner designer)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CADDESIGNER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", designer.DsgCode);
                        cmd.Parameters.AddWithValue("@MstName", designer.DsgName);
                        cmd.Parameters.AddWithValue("@MstDesc", designer.Remarks);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", designer.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditDesigner(MasterCADDesigner designer)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CADDESIGNER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", designer.DsgCode);
                        cmd.Parameters.AddWithValue("@MstName", designer.DsgName);
                        cmd.Parameters.AddWithValue("@MstDesc", designer.Remarks);
                        cmd.Parameters.AddWithValue("@UpdatedBy", designer.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", designer.DsgID);

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

        public ServiceResponse<bool> DisableDesigner(MasterCADDesigner designer)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CADDESIGNER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", designer.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", designer.DsgID);
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

        //CARTBIFURCATION//
        public ServiceResponse<IList<MasterCartBifurcation>> GetAllCartDays()
        {
            IList<MasterCartBifurcation> cities = new List<MasterCartBifurcation>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CARTBIFURCATION_MST;
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
                                    cities.Add(new MasterCartBifurcation
                                    {
                                        DaysID = Convert.ToInt32(dataReader["DaysID"]),
                                        DaysCode = Convert.ToString(dataReader["DaysCode"]),
                                        DaysName = Convert.ToString(dataReader["DaysName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (cities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCartBifurcation>>
                    {
                        Success = true,
                        Data = cities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCartBifurcation>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCartBifurcation>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddCartDays(MasterCartBifurcation cart)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CARTBIFURCATION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", cart.DaysCode);
                        cmd.Parameters.AddWithValue("@MstName", cart.DaysName);
                        cmd.Parameters.AddWithValue("@MstDesc", cart.Description);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", cart.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditCartDays(MasterCartBifurcation cart)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CARTBIFURCATION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", cart.DaysCode);
                        cmd.Parameters.AddWithValue("@MstName", cart.DaysName);
                        cmd.Parameters.AddWithValue("@MstDesc", cart.Description);
                        cmd.Parameters.AddWithValue("@UpdatedBy", cart.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", cart.DaysID);

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

        public ServiceResponse<bool> DisableCartDays(MasterCartBifurcation cart)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CARTBIFURCATION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", cart.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", cart.DaysID);
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

        //USERTYPES//
        public ServiceResponse<IList<MasterUserType>> GetAllUserTypes()
        {
            IList<MasterUserType> userTypes = new List<MasterUserType>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USERTYPE_MST;
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
                                    userTypes.Add(new MasterUserType
                                    {
                                        UserTypeID = Convert.ToInt32(dataReader["UserTypeID"]),
                                        UTCode = Convert.ToString(dataReader["UTCode"]),
                                        UTName = Convert.ToString(dataReader["UTName"]),
                                        Remarks = Convert.ToString(dataReader["Remarks"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        UTChar = Convert.ToChar(dataReader["UTChar"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (userTypes.Count > 0)
                {
                    return new ServiceResponse<IList<MasterUserType>>
                    {
                        Success = true,
                        Data = userTypes
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterUserType>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterUserType>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddUserType(MasterUserType userType)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USERTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", userType.UTCode);
                        cmd.Parameters.AddWithValue("@MstName", userType.UTName);
                        cmd.Parameters.AddWithValue("@MstDesc", userType.Remarks);
                        cmd.Parameters.AddWithValue("@MstSortBy", userType.SortOrder);
                        cmd.Parameters.AddWithValue("@UTChar", userType.UTChar);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", userType.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditUserType(MasterUserType userType)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USERTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", userType.UTCode);
                        cmd.Parameters.AddWithValue("@MstName", userType.UTName);
                        cmd.Parameters.AddWithValue("@MstDesc", userType.Remarks);
                        cmd.Parameters.AddWithValue("@MstSortBy", userType.SortOrder);
                        cmd.Parameters.AddWithValue("@UTChar", userType.UTChar);
                        cmd.Parameters.AddWithValue("@UpdatedBy", userType.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", userType.UserTypeID);

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

        public ServiceResponse<bool> DisableUserType(MasterUserType userType)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USERTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", userType.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", userType.UserTypeID);
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

        //COLLECTIONS//
        public ServiceResponse<IList<MasterCollection>> GetAllCollections()
        {
            IList<MasterCollection> collections = new List<MasterCollection>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLLECTIONS_MST;
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
                                    collections.Add(new MasterCollection
                                    {
                                        CollectionID = Convert.ToInt32(dataReader["CollectionID"]),
                                        CollectionCode = Convert.ToString(dataReader["CollectionCode"]),
                                        CollectionName = Convert.ToString(dataReader["CollectionName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (collections.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCollection>>
                    {
                        Success = true,
                        Data = collections
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCollection>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCollection>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddCollection(MasterCollection collection)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLLECTIONS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", collection.CollectionCode);
                        cmd.Parameters.AddWithValue("@MstName", collection.CollectionName);
                        cmd.Parameters.AddWithValue("@MstDesc", collection.Description);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", collection.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        if (dt.Rows != null && dt.Rows.Count > 0)
                        {
                            int id = Convert.ToInt32(dt.Rows[0]["InsertedId"]);
                            //return id;
                            return new ServiceResponse<bool> { Success = true, Message = "Success", Data = true };
                        }
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

        public ServiceResponse<bool> EditCollection(MasterCollection collection)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLLECTIONS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", collection.CollectionCode);
                        cmd.Parameters.AddWithValue("@MstName", collection.CollectionName);
                        cmd.Parameters.AddWithValue("@MstDesc", collection.Description);
                        cmd.Parameters.AddWithValue("@UpdatedBy", collection.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", collection.CollectionID);

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

        public ServiceResponse<bool> DisableCollection(MasterCollection collection)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLLECTIONS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", collection.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", collection.CollectionID);
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

        //COURIER//
        public ServiceResponse<IList<MasterCourier>> GetAllCourierNames()
        {
            IList<MasterCourier> couriers = new List<MasterCourier>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COURIER_MST;
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
                                    couriers.Add(new MasterCourier
                                    {
                                        CourierID = Convert.ToInt32(dataReader["CourierID"]),
                                        CourierName = Convert.ToString(dataReader["CourierName"]),
                                        Remarks = Convert.ToString(dataReader["Remarks"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (couriers.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCourier>>
                    {
                        Success = true,
                        Data = couriers
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCourier>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCourier>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddCourier(MasterCourier courier)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COURIER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", courier.CourierName);
                        cmd.Parameters.AddWithValue("@MstDesc", courier.Remarks);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", courier.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditCourier(MasterCourier courier)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COURIER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", courier.CourierName);
                        cmd.Parameters.AddWithValue("@MstDesc", courier.Remarks);
                        cmd.Parameters.AddWithValue("@UpdatedBy", courier.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", courier.CourierID);

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

        public ServiceResponse<bool> DisableCourier(MasterCourier courier)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COURIER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", courier.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", courier.CourierID);
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

        //CUSTOMER CARE//
        public ServiceResponse<IList<MasterCustomerCare>> GetAllCustomerCares()
        {
            IList<MasterCustomerCare> cares = new List<MasterCustomerCare>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CUSTOMERCARE_MST;
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
                                    cares.Add(new MasterCustomerCare
                                    {
                                        CcID = Convert.ToInt32(dataReader["CcID"]),
                                        CCName = Convert.ToString(dataReader["CCName"]),
                                        CCEmail = Convert.ToString(dataReader["CCEmail"]),
                                        CCMobile = Convert.ToString(dataReader["CCMobile"]),
                                        Remarks = Convert.ToString(dataReader["Remarks"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (cares.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCustomerCare>>
                    {
                        Success = true,
                        Data = cares
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCustomerCare>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCustomerCare>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddCare(MasterCustomerCare care)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CUSTOMERCARE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", care.CCName);
                        cmd.Parameters.AddWithValue("@MasterEmail", care.CCEmail);
                        cmd.Parameters.AddWithValue("@MstMobile", care.CCMobile);
                        cmd.Parameters.AddWithValue("@MstDesc", care.Remarks);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", care.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditCare(MasterCustomerCare care)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CUSTOMERCARE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", care.CCName);
                        cmd.Parameters.AddWithValue("@MasterEmail", care.CCEmail);
                        cmd.Parameters.AddWithValue("@MstMobile", care.CCMobile);
                        cmd.Parameters.AddWithValue("@MstDesc", care.Remarks);
                        cmd.Parameters.AddWithValue("@UpdatedBy", care.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", care.CcID);

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

        public ServiceResponse<bool> DisableCare(MasterCustomerCare care)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CUSTOMERCARE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", care.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", care.CcID);
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

        //DESIGNER//
        public ServiceResponse<IList<MasterItemDesigner>> GetAllItemDesigners()
        {
            IList<MasterItemDesigner> designers = new List<MasterItemDesigner>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DESIGNER_MST;
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
                                    designers.Add(new MasterItemDesigner
                                    {
                                        DsgID = Convert.ToInt32(dataReader["DsgID"]),
                                        DsgName = Convert.ToString(dataReader["DsgName"]),
                                        Remarks = Convert.ToString(dataReader["Remarks"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (designers.Count > 0)
                {
                    return new ServiceResponse<IList<MasterItemDesigner>>
                    {
                        Success = true,
                        Data = designers
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterItemDesigner>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterItemDesigner>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddItemDesigner(MasterItemDesigner designer)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DESIGNER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", designer.DsgName);
                        cmd.Parameters.AddWithValue("@MstDesc", designer.Remarks);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", designer.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditItemDesigner(MasterItemDesigner designer)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DESIGNER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", designer.DsgName);
                        cmd.Parameters.AddWithValue("@MstDesc", designer.Remarks);
                        cmd.Parameters.AddWithValue("@UpdatedBy", designer.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", designer.DsgID);

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

        public ServiceResponse<bool> DisableItemDesigner(MasterItemDesigner designer)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DESIGNER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", designer.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", designer.DsgID);
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

        //STONES//
        public ServiceResponse<IList<MasterStone>> GetAllStones()
        {
            IList<MasterStone> stones = new List<MasterStone>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONE_MST;
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
                                    stones.Add(new MasterStone
                                    {
                                        StoneID = Convert.ToInt32(dataReader["StoneID"]),
                                        StoneName = Convert.ToString(dataReader["StoneName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (stones.Count > 0)
                {
                    return new ServiceResponse<IList<MasterStone>>
                    {
                        Success = true,
                        Data = stones
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterStone>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterStone>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddStone(MasterStone stone)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", stone.StoneName);
                        cmd.Parameters.AddWithValue("@MstDesc", stone.Description);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", stone.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditStone(MasterStone stone)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", stone.StoneName);
                        cmd.Parameters.AddWithValue("@MstDesc", stone.Description);
                        cmd.Parameters.AddWithValue("@UpdatedBy", stone.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", stone.StoneID);

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

        public ServiceResponse<bool> DisableStone(MasterStone stone)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", stone.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", stone.StoneID);
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

        //DESIGN STONES//
        public ServiceResponse<IList<MasterDesignStone>> GetAllDesignStones()
        {
            IList<MasterDesignStone> stones = new List<MasterDesignStone>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DESIGNSTONE_MST;
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
                                    stones.Add(new MasterDesignStone
                                    {
                                        StoneID = Convert.ToInt32(dataReader["StoneID"]),
                                        StoneCode = Convert.ToString(dataReader["StoneCode"]),
                                        StoneName = Convert.ToString(dataReader["StoneName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (stones.Count > 0)
                {
                    return new ServiceResponse<IList<MasterDesignStone>>
                    {
                        Success = true,
                        Data = stones
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterDesignStone>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterDesignStone>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddDesignStone(MasterDesignStone stone)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DESIGNSTONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", stone.StoneName);
                        cmd.Parameters.AddWithValue("@MstCd", stone.StoneCode);
                        cmd.Parameters.AddWithValue("@MstDesc", stone.Description);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", stone.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditDesignStone(MasterDesignStone stone)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DESIGNSTONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", stone.StoneCode);
                        cmd.Parameters.AddWithValue("@MstName", stone.StoneName);
                        cmd.Parameters.AddWithValue("@MstDesc", stone.Description);
                        cmd.Parameters.AddWithValue("@UpdatedBy", stone.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", stone.StoneID);

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

        public ServiceResponse<bool> DisableDesignStone(MasterDesignStone stone)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DESIGNSTONE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", stone.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", stone.StoneID);
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

        //DIAMOND QUALITY//
        public ServiceResponse<IList<MasterDiamondQuality>> GetAllDiamondqualities()
        {
            IList<MasterDiamondQuality> qualities = new List<MasterDiamondQuality>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DIAMONDQUALITY_MST;
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
                                    qualities.Add(new MasterDiamondQuality
                                    {
                                        DQualityID = Convert.ToInt32(dataReader["DQualityID"]),
                                        DQualityCode = Convert.ToString(dataReader["DQualityCode"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (qualities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterDiamondQuality>>
                    {
                        Success = true,
                        Data = qualities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterDiamondQuality>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterDiamondQuality>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddDiamondQuality(MasterDiamondQuality quality)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DIAMONDQUALITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", quality.DQualityCode);
                        cmd.Parameters.AddWithValue("@MstDesc", quality.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", quality.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", quality.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditDiamondQuality(MasterDiamondQuality quality)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DIAMONDQUALITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", quality.DQualityCode);
                        cmd.Parameters.AddWithValue("@MstSortBy", quality.SortOrder);
                        cmd.Parameters.AddWithValue("@MstDesc", quality.Description);
                        cmd.Parameters.AddWithValue("@UpdatedBy", quality.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", quality.DQualityID);

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

        public ServiceResponse<bool> DisableDiamondQuality(MasterDiamondQuality quality)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DIAMONDQUALITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", quality.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", quality.DQualityID);
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

        //DISTRIBUTOR TYPE//   
        public ServiceResponse<IList<MasterDistributorType>> GetAllDistributorTypes()
        {
            IList<MasterDistributorType> distributorTypes = new List<MasterDistributorType>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DISTRIBUTORTYPE_MST;
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
                                    distributorTypes.Add(new MasterDistributorType
                                    {
                                        DistTypeID = Convert.ToInt32(dataReader["DistTypeID"]),
                                        DistType = Convert.ToString(dataReader["DistType"]),
                                        Remarks = Convert.ToString(dataReader["Remarks"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (distributorTypes.Count > 0)
                {
                    return new ServiceResponse<IList<MasterDistributorType>>
                    {
                        Success = true,
                        Data = distributorTypes
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterDistributorType>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterDistributorType>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddDistributortype(MasterDistributorType distributorType)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DISTRIBUTORTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", distributorType.DistType);
                        cmd.Parameters.AddWithValue("@MstDesc", distributorType.Remarks);
                        cmd.Parameters.AddWithValue("@MstSortBy", distributorType.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", distributorType.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditDistributortype(MasterDistributorType distributorType)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DISTRIBUTORTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", distributorType.DistType);
                        cmd.Parameters.AddWithValue("@MstSortBy", distributorType.SortOrder);
                        cmd.Parameters.AddWithValue("@MstDesc", distributorType.Remarks);
                        cmd.Parameters.AddWithValue("@UpdatedBy", distributorType.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", distributorType.DistTypeID);

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

        public ServiceResponse<bool> DisableDistributortype(MasterDistributorType distributorType)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DISTRIBUTORTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", distributorType.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", distributorType.DistTypeID);
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

        //EMAIL//
        public ServiceResponse<IList<MasterEmail>> GetAllEmail()
        {
            IList<MasterEmail> emails = new List<MasterEmail>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAIL_MST;
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
                                    emails.Add(new MasterEmail
                                    {
                                        ID = Convert.ToInt32(dataReader["ID"]),
                                        Email = Convert.ToString(dataReader["Email"]),
                                        Mobile = Convert.ToString(dataReader["Mobile"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (emails.Count > 0)
                {
                    return new ServiceResponse<IList<MasterEmail>>
                    {
                        Success = true,
                        Data = emails
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterEmail>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterEmail>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddEmail(MasterEmail email)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAIL_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", email.Email);
                        cmd.Parameters.AddWithValue("@MstName", email.Description);
                        cmd.Parameters.AddWithValue("@MstDesc", email.Mobile);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", email.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditEmail(MasterEmail email)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAIL_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", email.Email);
                        cmd.Parameters.AddWithValue("@MstName", email.Description);
                        cmd.Parameters.AddWithValue("@MstDesc", email.Mobile);
                        cmd.Parameters.AddWithValue("@UpdatedBy", email.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", email.ID);

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

        public ServiceResponse<bool> DisableEmail(MasterEmail email)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAIL_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", email.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", email.ID);
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

        //EMAILIDLIST//
        public ServiceResponse<IList<MasterEmailIdList>> GetAllEmailList()
        {
            IList<MasterEmailIdList> emails = new List<MasterEmailIdList>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAILLIST_MST;
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
                                    emails.Add(new MasterEmailIdList
                                    {
                                        ID = Convert.ToInt32(dataReader["ID"]),
                                        Email = Convert.ToString(dataReader["Email"]),
                                        EmpName = Convert.ToString(dataReader["EmpName"]),
                                        Department = Convert.ToString(dataReader["Department"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (emails.Count > 0)
                {
                    return new ServiceResponse<IList<MasterEmailIdList>>
                    {
                        Success = true,
                        Data = emails
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterEmailIdList>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterEmailIdList>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddEmailIDToList(MasterEmailIdList email)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAILLIST_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", email.EmpName);
                        cmd.Parameters.AddWithValue("@MstName", email.Email);
                        cmd.Parameters.AddWithValue("@MstDesc", email.Department);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", email.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditEmailList(MasterEmailIdList email)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAILLIST_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", email.EmpName);
                        cmd.Parameters.AddWithValue("@MstName", email.Email);
                        cmd.Parameters.AddWithValue("@MstDesc", email.Department);
                        cmd.Parameters.AddWithValue("@UpdatedBy", email.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", email.ID);

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

        public ServiceResponse<bool> DisableEmaillist(MasterEmailIdList email)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAILLIST_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", email.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", email.ID);
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

        //EMAIL SUBJECT//
        public ServiceResponse<IList<MasterEmailSubject>> GetAllEmailSubjects()
        {
            IList<MasterEmailSubject> subjects = new List<MasterEmailSubject>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAILSUBJECT_MST;
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
                                    subjects.Add(new MasterEmailSubject
                                    {
                                        SubjectID = Convert.ToInt32(dataReader["SubjectID"]),
                                        SubjectName = Convert.ToString(dataReader["SubjectName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (subjects.Count > 0)
                {
                    return new ServiceResponse<IList<MasterEmailSubject>>
                    {
                        Success = true,
                        Data = subjects
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterEmailSubject>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterEmailSubject>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddEmailSubject(MasterEmailSubject subject)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAILSUBJECT_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", subject.SubjectName);
                        cmd.Parameters.AddWithValue("@MstDesc", subject.Description);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", subject.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditEmailSubject(MasterEmailSubject subject)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAILSUBJECT_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", subject.SubjectName);
                        cmd.Parameters.AddWithValue("@MstDesc", subject.Description);
                        cmd.Parameters.AddWithValue("@UpdatedBy", subject.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", subject.SubjectID);

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

        public ServiceResponse<bool> DisableEmailsubject(MasterEmailSubject subject)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.EMAILSUBJECT_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", subject.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", subject.SubjectID);
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

        //UOM//
        public ServiceResponse<IList<MasterUom>> GetAllUom()
        {
            IList<MasterUom> uoms = new List<MasterUom>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.UOM_MST;
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
                                    uoms.Add(new MasterUom
                                    {
                                        UomID = Convert.ToInt32(dataReader["UomID"]),
                                        Uom = Convert.ToString(dataReader["Uom"]),
                                        Remarks = Convert.ToString(dataReader["Remarks"]),
                                        SortOrder = dataReader["InsertedOn"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (uoms.Count > 0)
                {
                    return new ServiceResponse<IList<MasterUom>>
                    {
                        Success = true,
                        Data = uoms
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterUom>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterUom>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddUOM(MasterUom uom)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.UOM_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", uom.Uom);
                        cmd.Parameters.AddWithValue("@MstDesc", uom.Remarks);
                        cmd.Parameters.AddWithValue("@SortOrder", uom.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", uom.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditUOM(MasterUom uom)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.UOM_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", uom.Uom);
                        cmd.Parameters.AddWithValue("@MstDesc", uom.Remarks);
                        cmd.Parameters.AddWithValue("@SortOrder", uom.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", uom.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", uom.UomID);

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

        public ServiceResponse<bool> DisableUom(MasterUom uom)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.UOM_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", uom.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", uom.UomID);
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

        //METALS//
        public ServiceResponse<IList<MasterMetal>> GetAllMetals()
        {
            IList<MasterMetal> metals = new List<MasterMetal>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.METAL_MST;
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
                                    metals.Add(new MasterMetal
                                    {
                                        MetalID = Convert.ToInt32(dataReader["MetalID"]),
                                        MetalCode = Convert.ToString(dataReader["MetalCode"]),
                                        MetalName = Convert.ToString(dataReader["MetalName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (metals.Count > 0)
                {
                    return new ServiceResponse<IList<MasterMetal>>
                    {
                        Success = true,
                        Data = metals
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterMetal>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterMetal>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddMetal(MasterMetal metal)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.METAL_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", metal.MetalCode);
                        cmd.Parameters.AddWithValue("@MstName", metal.MetalName);
                        cmd.Parameters.AddWithValue("@MstDesc", metal.Description);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", metal.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditMetal(MasterMetal metal)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.METAL_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", metal.MetalName);
                        cmd.Parameters.AddWithValue("@MstCd", metal.MetalCode);
                        cmd.Parameters.AddWithValue("@MstDesc", metal.Description);
                        cmd.Parameters.AddWithValue("@UpdatedBy", metal.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", metal.MetalID);

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

        public ServiceResponse<bool> DisableMetal(MasterMetal metal)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.METAL_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", metal.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", metal.MetalID);
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

        //STONE COLORS//
        public ServiceResponse<IList<MasterStoneColor>> GetAllStoneColors()
        {
            IList<MasterStoneColor> colors = new List<MasterStoneColor>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONECOLOR_MST;
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
                                    colors.Add(new MasterStoneColor
                                    {
                                        SColorID = Convert.ToInt32(dataReader["SColorID"]),
                                        StoneColor = Convert.ToString(dataReader["StoneColor"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (colors.Count > 0)
                {
                    return new ServiceResponse<IList<MasterStoneColor>>
                    {
                        Success = true,
                        Data = colors
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterStoneColor>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterStoneColor>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddStoneColor(MasterStoneColor scolor)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONECOLOR_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", scolor.StoneColor);
                        cmd.Parameters.AddWithValue("@MstDesc", scolor.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", scolor.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", scolor.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditStoneColor(MasterStoneColor scolor)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONECOLOR_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", scolor.StoneColor);
                        cmd.Parameters.AddWithValue("@MstDesc", scolor.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", scolor.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", scolor.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", scolor.SColorID);

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

        public ServiceResponse<bool> DisableStoneColor(MasterStoneColor scolor)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONECOLOR_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", scolor.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", scolor.SColorID);
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

        //STONE SHAPES//
        public ServiceResponse<IList<MasterShapes>> GetAllStoneShapes()
        {
            IList<MasterShapes> shapes = new List<MasterShapes>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONESHAPE_MST;
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
                                    shapes.Add(new MasterShapes
                                    {
                                        ShapeID = Convert.ToInt32(dataReader["ShapeID"]),
                                        StoneShape = Convert.ToString(dataReader["StoneShape"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (shapes.Count > 0)
                {
                    return new ServiceResponse<IList<MasterShapes>>
                    {
                        Success = true,
                        Data = shapes
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterShapes>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterShapes>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddStoneShape(MasterShapes shape)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONESHAPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", shape.StoneShape);
                        cmd.Parameters.AddWithValue("@MstDesc", shape.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", shape.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", shape.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditStoneShape(MasterShapes shape)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONESHAPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", shape.StoneShape);
                        cmd.Parameters.AddWithValue("@MstDesc", shape.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", shape.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", shape.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", shape.ShapeID);

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

        public ServiceResponse<bool> DisableStoneShape(MasterShapes shape)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONESHAPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", shape.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", shape.ShapeID);
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

        //PPTAGS//
        public ServiceResponse<IList<MasterPPTag>> GetAllPPTags()
        {
            IList<MasterPPTag> tags = new List<MasterPPTag>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.PPTAG_MST;
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
                                    tags.Add(new MasterPPTag
                                    {
                                        PPTagID = Convert.ToInt32(dataReader["PPTagID"]),
                                        PPTag = Convert.ToString(dataReader["PPTag"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (tags.Count > 0)
                {
                    return new ServiceResponse<IList<MasterPPTag>>
                    {
                        Success = true,
                        Data = tags
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterPPTag>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterPPTag>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddPPTag(MasterPPTag pptag)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PPTAG_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", pptag.PPTag);
                        cmd.Parameters.AddWithValue("@MstDesc", pptag.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", pptag.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", pptag.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditPPTag(MasterPPTag pptag)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PPTAG_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", pptag.PPTag);
                        cmd.Parameters.AddWithValue("@MstDesc", pptag.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", pptag.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", pptag.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", pptag.PPTagID);

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

        public ServiceResponse<bool> DisablePPTag(MasterPPTag pptag)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PPTAG_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", pptag.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", pptag.PPTagID);
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

        //GENDER//
        public ServiceResponse<IList<MasterGender>> GetAllGenders()
        {
            IList<MasterGender> genders = new List<MasterGender>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GENDER_MST;
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
                                    genders.Add(new MasterGender
                                    {
                                        GenderID = Convert.ToInt32(dataReader["GenderID"]),
                                        Gender = Convert.ToString(dataReader["Gender"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (genders.Count > 0)
                {
                    return new ServiceResponse<IList<MasterGender>>
                    {
                        Success = true,
                        Data = genders
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterGender>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterGender>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddGender(MasterGender gender)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GENDER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", gender.Gender);
                        cmd.Parameters.AddWithValue("@MstDesc", gender.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", gender.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", gender.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditGender(MasterGender gender)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GENDER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", gender.Gender);
                        cmd.Parameters.AddWithValue("@MstDesc", gender.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", gender.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", gender.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", gender.GenderID);

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

        public ServiceResponse<bool> DisableGender(MasterGender gender)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GENDER_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", gender.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", gender.GenderID);
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

        //STAR COLORS//
        public ServiceResponse<IList<MasterStartColor>> GetAllItemStars()
        {
            IList<MasterStartColor> stars = new List<MasterStartColor>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STARCOLOR_MST;
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
                                    stars.Add(new MasterStartColor
                                    {
                                        StarColorID = Convert.ToInt32(dataReader["StarColorID"]),
                                        StarCode = Convert.ToString(dataReader["StarCode"]),
                                        StarName = Convert.ToString(dataReader["StarName"]),
                                        ColorCode = Convert.ToString(dataReader["ColorCode"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (stars.Count > 0)
                {
                    return new ServiceResponse<IList<MasterStartColor>>
                    {
                        Success = true,
                        Data = stars
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterStartColor>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterStartColor>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddStarColor(MasterStartColor scolor)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STARCOLOR_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", scolor.StarName); //StarCode
                        cmd.Parameters.AddWithValue("@MstCd", scolor.StarCode);
                        cmd.Parameters.AddWithValue("@MstDesc", scolor.ColorCode);
                        cmd.Parameters.AddWithValue("@MstSortBy", scolor.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", scolor.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditStarColor(MasterStartColor scolor)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STARCOLOR_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", scolor.StarName);
                        cmd.Parameters.AddWithValue("@MstCd", scolor.StarCode);
                        cmd.Parameters.AddWithValue("@MstDesc", scolor.ColorCode);
                        cmd.Parameters.AddWithValue("@MstSortBy", scolor.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", scolor.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", scolor.StarColorID);

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

        public ServiceResponse<bool> DisableStarColor(MasterStartColor scolor)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STARCOLOR_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", scolor.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", scolor.StarColorID);
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

        //STONE QUALITY//
        public ServiceResponse<IList<MasterStoneQuality>> GetStoneQuality()
        {
            IList<MasterStoneQuality> qualities = new List<MasterStoneQuality>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONEQUALITY_MST;
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
                                    qualities.Add(new MasterStoneQuality
                                    {
                                        StoneQualityID = Convert.ToInt32(dataReader["StoneQualityID"]),
                                        StoneQuality = Convert.ToString(dataReader["StoneQuality"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (qualities.Count > 0)
                {
                    return new ServiceResponse<IList<MasterStoneQuality>>
                    {
                        Success = true,
                        Data = qualities
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterStoneQuality>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterStoneQuality>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddStoneQuality(MasterStoneQuality squality)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONEQUALITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", squality.StoneQuality);
                        cmd.Parameters.AddWithValue("@MstDesc", squality.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", squality.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", squality.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditStoneQuality(MasterStoneQuality squality)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONEQUALITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", squality.StoneQuality);
                        cmd.Parameters.AddWithValue("@MstDesc", squality.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", squality.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", squality.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", squality.StoneQualityID);

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

        public ServiceResponse<bool> DisableStoneQuality(MasterStoneQuality squality)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.STONEQUALITY_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", squality.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", squality.StoneQualityID);
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

        //ITEM DAYS//
        public ServiceResponse<IList<MasterDays>> GetItemDays()
        {
            IList<MasterDays> days = new List<MasterDays>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMDAYS_MST;
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
                                    days.Add(new MasterDays
                                    {
                                        DaysID = Convert.ToInt32(dataReader["DaysID"]),
                                        DaysName = Convert.ToString(dataReader["DaysName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (days.Count > 0)
                {
                    return new ServiceResponse<IList<MasterDays>>
                    {
                        Success = true,
                        Data = days
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterDays>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterDays>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddItemDays(MasterDays days)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMDAYS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", days.DaysName);
                        cmd.Parameters.AddWithValue("@MstDesc", days.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", days.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", days.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditItemDay(MasterDays days)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMDAYS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", days.DaysName);
                        cmd.Parameters.AddWithValue("@MstDesc", days.Description);
                        cmd.Parameters.AddWithValue("@MstSortBy", days.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", days.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", days.DaysID);

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

        public ServiceResponse<bool> DisableItemDay(MasterDays days)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMDAYS_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", days.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", days.DaysID);
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

        //SUB-CATEGORIES//
        public ServiceResponse<IList<MasterSubCategory>> GetSubCategories()
        {
            IList<MasterSubCategory> subCategories = new List<MasterSubCategory>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SUBCAT_MST;
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
                                    subCategories.Add(new MasterSubCategory
                                    {
                                        SubCategoryID = Convert.ToInt32(dataReader["SubCategoryID"]),
                                        SubCategoryCode = Convert.ToString(dataReader["SubCategoryCode"]),
                                        SubCategoryName = Convert.ToString(dataReader["SubCategoryName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (subCategories.Count > 0)
                {
                    return new ServiceResponse<IList<MasterSubCategory>>
                    {
                        Success = true,
                        Data = subCategories
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterSubCategory>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterSubCategory>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddSubCategory(MasterSubCategory scat)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SUBCAT_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", scat.SubCategoryCode);
                        cmd.Parameters.AddWithValue("@MstName", scat.SubCategoryName);
                        cmd.Parameters.AddWithValue("@MstDesc", scat.Description);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", scat.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditSubCategory(MasterSubCategory scat)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SUBCAT_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", scat.SubCategoryName);
                        cmd.Parameters.AddWithValue("@MstDesc", scat.Description);
                        cmd.Parameters.AddWithValue("@MstCd", scat.SubCategoryCode);
                        cmd.Parameters.AddWithValue("@UpdatedBy", scat.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", scat.SubCategoryID);

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

        public ServiceResponse<bool> DisableSubCategory(MasterSubCategory scat)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SUBCAT_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", scat.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", scat.SubCategoryID);
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

        //TITLES//
        public ServiceResponse<IList<MasterNameTitle>> GetTitles()
        {
            IList<MasterNameTitle> titles = new List<MasterNameTitle>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.NAMETITLE_MST;
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
                                    titles.Add(new MasterNameTitle
                                    {
                                        TitleID = Convert.ToInt32(dataReader["TitleID"]),
                                        Title = Convert.ToString(dataReader["Title"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (titles.Count > 0)
                {
                    return new ServiceResponse<IList<MasterNameTitle>>
                    {
                        Success = true,
                        Data = titles
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterNameTitle>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterNameTitle>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddTitle(MasterNameTitle title)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.NAMETITLE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", title.Title);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", title.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditTitle(MasterNameTitle title)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.NAMETITLE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", title.Title);
                        cmd.Parameters.AddWithValue("@UpdatedBy", title.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", title.TitleID);

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

        public ServiceResponse<bool> DisableTitle(MasterNameTitle title)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.NAMETITLE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", title.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", title.TitleID);
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

        //PARTS//
        public ServiceResponse<IList<MasterPart>> GetParts()
        {
            IList<MasterPart> parts = new List<MasterPart>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PART_MST;
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
                                    parts.Add(new MasterPart
                                    {
                                        PartID = Convert.ToInt32(dataReader["PartID"]),
                                        PartName = Convert.ToString(dataReader["PartName"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (parts.Count > 0)
                {
                    return new ServiceResponse<IList<MasterPart>>
                    {
                        Success = true,
                        Data = parts
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterPart>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterPart>>  { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddPart(MasterPart part)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PART_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", part.PartName);
                        cmd.Parameters.AddWithValue("@MstSortBy", part.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", part.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditPart(MasterPart part)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PART_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", part.PartName);
                        cmd.Parameters.AddWithValue("@MstSortBy", part.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", part.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", part.PartID);

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

        public ServiceResponse<bool> DisablePart(MasterPart part)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PART_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", part.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", part.PartID);
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

        //LANGUAGES//
        public ServiceResponse<IList<MasterLanguages>> GetLanguages()
        {
            IList<MasterLanguages> languages = new List<MasterLanguages>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.LANGUAGES_MST;
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
                                    languages.Add(new MasterLanguages
                                    {
                                        LanguageID = Convert.ToInt32(dataReader["LanguageID"]),
                                        LanguageName = Convert.ToString(dataReader["LanguageName"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (languages.Count > 0)
                {
                    return new ServiceResponse<IList<MasterLanguages>>
                    {
                        Success = true,
                        Data = languages
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterLanguages>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterLanguages>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddLanguage(MasterLanguages language)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.LANGUAGES_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", language.LanguageName);
                        cmd.Parameters.AddWithValue("@MstSortBy", language.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", language.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditLanguage(MasterLanguages language)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.LANGUAGES_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", language.LanguageName);
                        cmd.Parameters.AddWithValue("@MstSortBy", language.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", language.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", language.LanguageID);

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

        public ServiceResponse<bool> DisableLanguage(MasterLanguages language)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.LANGUAGES_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", language.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", language.LanguageID);
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

        //RELIGION//
        public ServiceResponse<IList<MasterReligion>> GetReligions()
        {
            IList<MasterReligion> religions = new List<MasterReligion>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.RELIGION_MST;
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
                                    religions.Add(new MasterReligion
                                    {
                                        ReligionID = Convert.ToInt32(dataReader["ReligionID"]),
                                        ReligionName = Convert.ToString(dataReader["ReligionName"]),
                                        SortOrder = dataReader["SortOrder"] == DBNull.Value ? Decimal.MinValue : Convert.ToDecimal(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (religions.Count > 0)
                {
                    return new ServiceResponse<IList<MasterReligion>>
                    {
                        Success = true,
                        Data = religions
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterReligion>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterReligion>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddReligion(MasterReligion religion)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.RELIGION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", religion.ReligionName);
                        cmd.Parameters.AddWithValue("@MstSortBy", religion.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", religion.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditReligion(MasterReligion religion)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.RELIGION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", religion.ReligionName);
                        cmd.Parameters.AddWithValue("@MstSortBy", religion.SortOrder);
                        cmd.Parameters.AddWithValue("@UpdatedBy", religion.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", religion.ReligionID);

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

        public ServiceResponse<bool> DisableReligion(MasterReligion religion)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.RELIGION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", religion.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", religion.ReligionID);
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



        //***************************************************************************//
        //PRODUCT TYPES//
        public ServiceResponse<IList<MasterProductType>> GetProductTypes()
        {
            IList<MasterProductType> types = new List<MasterProductType>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PRODUCTTYPE_MST;
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
                                    types.Add(new MasterProductType
                                    {
                                        PTypeID = Convert.ToInt32(dataReader["PTypeID"]),
                                        PTypeName = Convert.ToString(dataReader["PTypeName"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (types.Count > 0)
                {
                    return new ServiceResponse<IList<MasterProductType>>
                    {
                        Success = true,
                        Data = types
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterProductType>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterProductType>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddProductType(MasterProductType ptype)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PRODUCTTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", ptype.PTypeName);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", ptype.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditProductType(MasterProductType ptype)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PRODUCTTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstName", ptype.PTypeName);
                        cmd.Parameters.AddWithValue("@UpdatedBy", ptype.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@MstID", ptype.PTypeID);

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

        public ServiceResponse<bool> DisableProductType(MasterProductType ptype)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PRODUCTTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", ptype.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", ptype.PTypeID);
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

        //COMMON//
        public ServiceResponse<IList<MasterCommon>> GetCommonMasters()
        {
            IList<MasterCommon> commons = new List<MasterCommon>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COMMON_MST;
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
                                    commons.Add(new MasterCommon
                                    {
                                        MstID = Convert.ToInt32(dataReader["MstID"]),
                                        MstCd = Convert.ToString(dataReader["MstCd"]),
                                        MstName = Convert.ToString(dataReader["MstName"]),
                                        MstDesc = Convert.ToString(dataReader["MstDesc"]),
                                        MstValidSts = dataReader["MstValidSts"] == DBNull.Value ? char.MinValue : Convert.ToChar(dataReader["MstValidSts"]),
                                        MstFlagID = dataReader["MstFlagID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MstFlagID"]),
                                        MstImgID = dataReader["MstImgID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["MstImgID"]),
                                        MstSortBy = dataReader["MstSortBy"] == DBNull.Value ? Decimal.MinValue : Convert.ToDecimal(dataReader["MstSortBy"]),
                                        MstIconImgPath = Convert.ToString(dataReader["MstIconImgPath"]),
                                        MstTyp = dataReader["MstTyp"] == DBNull.Value ? char.MinValue : Convert.ToChar(dataReader["MstTyp"]),
                                        SyncFlg = dataReader["SyncFlg"] == DBNull.Value ? char.MinValue : Convert.ToChar(dataReader["SyncFlg"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = dataReader["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (commons.Count > 0)
                {
                    return new ServiceResponse<IList<MasterCommon>>
                    {
                        Success = true,
                        Data = commons
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterCommon>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterCommon>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddCommon(MasterCommon common)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COMMON_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstID", common.MstID);
                        cmd.Parameters.AddWithValue("@MstCd", common.MstCd);
                        cmd.Parameters.AddWithValue("@MstName", common.MstName);
                        cmd.Parameters.AddWithValue("@MstDesc", common.MstDesc);
                        cmd.Parameters.AddWithValue("@MstValidSts", common.MstValidSts);
                        cmd.Parameters.AddWithValue("@MstFlagID", common.MstFlagID);
                        cmd.Parameters.AddWithValue("@MstImgID", common.MstImgID);
                        cmd.Parameters.AddWithValue("@MstSortBy", common.MstSortBy);
                        cmd.Parameters.AddWithValue("@MstIconImgPath", common.MstIconImgPath);
                        cmd.Parameters.AddWithValue("@MstTyp", common.MstTyp);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", common.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditCommon(MasterCommon common)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COMMON_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstID", common.MstID);
                        cmd.Parameters.AddWithValue("@MstCd", common.MstCd);
                        cmd.Parameters.AddWithValue("@MstName", common.MstName);
                        cmd.Parameters.AddWithValue("@MstDesc", common.MstDesc);
                        cmd.Parameters.AddWithValue("@MstValidSts", common.MstValidSts);
                        cmd.Parameters.AddWithValue("@MstFlagID", common.MstFlagID);
                        cmd.Parameters.AddWithValue("@MstImgID", common.MstImgID);
                        cmd.Parameters.AddWithValue("@MstSortBy", common.MstSortBy);
                        cmd.Parameters.AddWithValue("@MstIconImgPath", common.MstIconImgPath);
                        cmd.Parameters.AddWithValue("@MstTyp", common.MstTyp);
                        cmd.Parameters.AddWithValue("@UpdatedBy", common.UpdatedBy);
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

        public ServiceResponse<bool> DisableCommon(MasterCommon common)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COMMON_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", common.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", common.MstID);
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

        //NOTIFICATIONS//
        public ServiceResponse<IList<T_NOTIFICATION_MST>> GetNotifications()
        {
            IList<T_NOTIFICATION_MST> notifcations = new List<T_NOTIFICATION_MST>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.NOTIFICATION_MST;
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
                                    notifcations.Add(new T_NOTIFICATION_MST
                                    {
                                        NotificationID = Convert.ToInt32(dataReader["NotificationID"]),
                                        NotificationFromDataID = dataReader["NotificationFromDataID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["NotificationFromDataID"]),
                                        NotificationImgIconPath = Convert.ToString(dataReader["NotificationImgIconPath"]),
                                        NotificationImgBigPath = Convert.ToString(dataReader["NotificationImgBigPath"]),
                                        NotificationTitle = Convert.ToString(dataReader["NotificationTitle"]),
                                        NotificationText = Convert.ToString(dataReader["NotificationText"]),
                                        NotificationSendScheduleOn = dataReader["NotificationSendScheduleOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["NotificationSendScheduleOn"]),
                                        NotificationStatus = Convert.ToChar(dataReader["NotificationStatus"]),
                                        NotificationSendOn = dataReader["NotificationSendOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["NotificationSendOn"]),
                                        NotificationValidSts = Convert.ToChar(dataReader["NotificationValidSts"]),
                                        NotificationEntDt = dataReader["NotificationEntDt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["NotificationEntDt"]),
                                        NotificationTypeCommonID = dataReader["NotificationTypeCommonID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["NotificationTypeCommonID"]),
                                        NotificationTypeParam = Convert.ToString(dataReader["NotificationTypeParam"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (notifcations.Count > 0)
                {
                    return new ServiceResponse<IList<T_NOTIFICATION_MST>>
                    {
                        Success = true,
                        Data = notifcations
                    };
                }
                else
                {
                    return new ServiceResponse<IList<T_NOTIFICATION_MST>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<T_NOTIFICATION_MST>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddNotification(T_NOTIFICATION_MST notification)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.NOTIFICATION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NotificationID", notification.NotificationID);
                        cmd.Parameters.AddWithValue("@NotificationEntDt", notification.NotificationEntDt);
                        cmd.Parameters.AddWithValue("@NotificationFileTypeCommonID", notification.NotificationFileTypeCommonID);
                        cmd.Parameters.AddWithValue("@NotificationFromDataID", notification.NotificationFromDataID);
                        cmd.Parameters.AddWithValue("@NotificationImgBigPath", notification.NotificationImgBigPath);
                        cmd.Parameters.AddWithValue("@NotificationImgIconPath", notification.NotificationImgIconPath);
                        cmd.Parameters.AddWithValue("@NotificationSendOn", notification.NotificationSendOn);
                        cmd.Parameters.AddWithValue("@NotificationSendScheduleOn", notification.NotificationSendScheduleOn);
                        cmd.Parameters.AddWithValue("@NotificationStatus", notification.NotificationStatus);
                        cmd.Parameters.AddWithValue("@NotificationText", notification.NotificationText);
                        cmd.Parameters.AddWithValue("@NotificationTitle", notification.NotificationTitle);
                        cmd.Parameters.AddWithValue("@NotificationTypeCommonID", notification.NotificationTypeCommonID);
                        cmd.Parameters.AddWithValue("@NotificationTypeParam", notification.NotificationTypeParam);
                        cmd.Parameters.AddWithValue("@NotificationValidSts", notification.NotificationValidSts);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditNotification(T_NOTIFICATION_MST notification)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.NOTIFICATION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NotificationID", notification.NotificationID);
                        cmd.Parameters.AddWithValue("@NotificationEntDt", notification.NotificationEntDt);
                        cmd.Parameters.AddWithValue("@NotificationFileTypeCommonID", notification.NotificationFileTypeCommonID);
                        cmd.Parameters.AddWithValue("@NotificationFromDataID", notification.NotificationFromDataID);
                        cmd.Parameters.AddWithValue("@NotificationImgBigPath", notification.NotificationImgBigPath);
                        cmd.Parameters.AddWithValue("@NotificationImgIconPath", notification.NotificationImgIconPath);
                        cmd.Parameters.AddWithValue("@NotificationSendOn", notification.NotificationSendOn);
                        cmd.Parameters.AddWithValue("@NotificationSendScheduleOn", notification.NotificationSendScheduleOn);
                        cmd.Parameters.AddWithValue("@NotificationStatus", notification.NotificationStatus);
                        cmd.Parameters.AddWithValue("@NotificationText", notification.NotificationText);
                        cmd.Parameters.AddWithValue("@NotificationTitle", notification.NotificationTitle);
                        cmd.Parameters.AddWithValue("@NotificationTypeCommonID", notification.NotificationTypeCommonID);
                        cmd.Parameters.AddWithValue("@NotificationTypeParam", notification.NotificationTypeParam);
                        cmd.Parameters.AddWithValue("@NotificationValidSts", notification.NotificationValidSts);
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

        public ServiceResponse<bool> DisableNotification(T_NOTIFICATION_MST notification)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.PRODUCTTYPE_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", notification.NotificationFromDataID);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@MstID", notification.NotificationID);
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

        //SPLASH SCREEN//
        public ServiceResponse<IList<T_APP_SPLASH_MST>> GetSplashScreens()
        {
            IList<T_APP_SPLASH_MST> splash = new List<T_APP_SPLASH_MST>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SPLASH_MST;
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
                                    splash.Add(new T_APP_SPLASH_MST
                                    {
                                        AppSplashID = Convert.ToInt32(dataReader["AppSplashID"]),
                                        AppSplashImgPath = Convert.ToString(dataReader["AppSplashImgPath"]),
                                        AppSplashDay = dataReader["AppSplashDay"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["AppSplashDay"]),
                                        AppSplashActive = Convert.ToChar(dataReader["AppSplashActive"]),
                                        AppSplashUsrId = dataReader["AppSplashUsrId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["AppSplashUsrId"]),
                                        //AppSplashEntDt = dataReader["AppSplashEntDt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["AppSplashEntDt"]),
                                        //AppSplashCngDt = dataReader["AppSplashCngDt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["AppSplashCngDt"])
                                        AppSplashEntDt = dataReader["InsertedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        AppSplashCngDt = dataReader["UpdatedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (splash.Count > 0)
                {
                    return new ServiceResponse<IList<T_APP_SPLASH_MST>>
                    {
                        Success = true,
                        Data = splash
                    };
                }
                else
                {
                    return new ServiceResponse<IList<T_APP_SPLASH_MST>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<T_APP_SPLASH_MST>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddSplashScreen(T_APP_SPLASH_MST splash)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SPLASH_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AppSplashActive", splash.AppSplashActive);
                        cmd.Parameters.AddWithValue("@AppSplashImgPath", splash.AppSplashImgPath);
                        cmd.Parameters.AddWithValue("@AppSplashDay", splash.AppSplashDay);
                        cmd.Parameters.AddWithValue("@InsertedBy", splash.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditSplashScreen(T_APP_SPLASH_MST splash)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SPLASH_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AppSplashActive", splash.AppSplashActive);
                        cmd.Parameters.AddWithValue("@AppSplashImgPath", splash.AppSplashImgPath);
                        cmd.Parameters.AddWithValue("@AppSplashDay", splash.AppSplashDay);
                        cmd.Parameters.AddWithValue("@AppSplashDayCommonID", splash.AppSplashDayCommonID);
                        cmd.Parameters.AddWithValue("@AppSplashUsrId", splash.AppSplashUsrId);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@AppSplashID", splash.AppSplashID);

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

        public ServiceResponse<bool> DisableSplashScreen(T_APP_SPLASH_MST splash)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SPLASH_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", splash.AppSplashUsrId);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@AppSplashID", splash.AppSplashID);
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

        //SOLITAIRE PARAMETERS//

        public ServiceResponse<IList<T_DIA_COMMON_MST>> GetSolitaireParameters()
        {
            IList<T_DIA_COMMON_MST> parameters = new List<T_DIA_COMMON_MST>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SolitaireParameters_MST;
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
                                    parameters.Add(new T_DIA_COMMON_MST
                                    {
                                        FieldID = Convert.ToInt32(dataReader["FieldID"]),
                                        Field = Convert.ToString(dataReader["Field"]),
                                        Value = dataReader["Value"] == DBNull.Value ? decimal.MinValue : Convert.ToDecimal(dataReader["Value"]),
                                        ValidSts = dataReader["ValidSts"] == DBNull.Value ? char.MinValue : Convert.ToChar(dataReader["ValidSts"]),
                                        EntDt = dataReader["EntDt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["EntDt"]),
                                        UserId = dataReader["UserId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["UserId"]),
                                        ChgDt = dataReader["ChgDt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["ChgDt"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (parameters.Count > 0)
                {
                    return new ServiceResponse<IList<T_DIA_COMMON_MST>>
                    {
                        Success = true,
                        Data = parameters
                    };
                }
                else
                {
                    return new ServiceResponse<IList<T_DIA_COMMON_MST>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<T_DIA_COMMON_MST>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddSolitairepParameter(T_DIA_COMMON_MST SOLITAIREPARAM)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SolitaireParameters_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@FieldID", SOLITAIREPARAM.FieldID);
                        cmd.Parameters.AddWithValue("@Field", SOLITAIREPARAM.Field);
                        cmd.Parameters.AddWithValue("@Value", SOLITAIREPARAM.Value);
                        cmd.Parameters.AddWithValue("@ValidSts", SOLITAIREPARAM.ValidSts);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", SOLITAIREPARAM.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> Editsolitaireparameter(T_DIA_COMMON_MST SOLITAIREPARAM)
        {

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.SolitaireParameters_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Field", SOLITAIREPARAM.Field);
                        cmd.Parameters.AddWithValue("@Value", SOLITAIREPARAM.Value);
                        cmd.Parameters.AddWithValue("@ValidSts", SOLITAIREPARAM.ValidSts);
                        cmd.Parameters.AddWithValue("@UpdatedBy", SOLITAIREPARAM.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        cmd.Parameters.AddWithValue("@FieldID", SOLITAIREPARAM.FieldID);

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

        public ServiceResponse<bool> Disablesolitaireparameter(T_DIA_COMMON_MST solitaireparameter)
        {

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SolitaireParameters_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", solitaireparameter.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@FieldID", solitaireparameter.FieldID);
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

        //SUBMITMAPPING//
        public ServiceResponse<bool> AddItemsMapping(ItemMapping ItemsMap)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ITEMS_MAPPING;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ItemIds", ItemsMap.ItemId); 
                        cmd.Parameters.AddWithValue("@UserIds", ItemsMap.UserId); 

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

        public ServiceResponse<bool> AddCollectionUserMapping(CollectionUserMapping CollectionUserMap)
        {

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.COLLECTIONS_USER_MAPPING;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CollectionIds", CollectionUserMap.CollectionId); // "100883,100884"
                        cmd.Parameters.AddWithValue("@UserIds", CollectionUserMap.UserId); //"1000"
                        //cmd.Parameters.AddWithValue("@InsertedBy", region.InsertedBy);
                        //   cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<IList<Users>> GetItemsMappingUsers(string ZoneID, string StateID, string TerritoryID, string CityID, string AreaID, string UsertypeID)
        {
            IList<Users> Users = new List<Users>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GET_USERS_ITEMS_MAPPING;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ZONEID", ZoneID); //ItemsMap.ZoneID // "100883,100884"
                        cmd.Parameters.AddWithValue("@STATEID", StateID); //"1000"
                        //cmd.Parameters.AddWithValue("@DISTRICTID", ItemsMap.DistrictID);
                        cmd.Parameters.AddWithValue("@TERRITORYID", TerritoryID);
                        cmd.Parameters.AddWithValue("@CITYID", CityID);
                        cmd.Parameters.AddWithValue("@AREAID", AreaID);
                        cmd.Parameters.AddWithValue("@USERTYPEID", UsertypeID);
                        //cmd.Parameters.AddWithValue("@TALUKAID", ItemsMap.UserId);
                        //cmd.Parameters.AddWithValue("@REGIONID", ItemsMap.UserId);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    Users.Add(new Users
                                    {
                                        UserId = Convert.ToInt32(dataReader["DataUserId"]),
                                        UserName = Convert.ToString(dataReader["NAME"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (Users.Count > 0)
                {
                    return new ServiceResponse<IList<Users>>
                    {
                        Success = true,
                        Data = Users
                    };
                }
                else
                {
                    return new ServiceResponse<IList<Users>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<Users>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<IList<MasterOutletCategory>> GetOutLetCategory()
        {
            IList<MasterOutletCategory> outletcategory = new List<MasterOutletCategory>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.OutletCategory;
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
                                    outletcategory.Add(new MasterOutletCategory
                                    {
                                        COID = Convert.ToString(dataReader["COID"]),
                                        COCD = Convert.ToString(dataReader["COCD"]),
                                        CONAME = Convert.ToString(dataReader["CONAME"]),
                                        CODESC = Convert.ToString(dataReader["CODESC"]),
                                        MSTVALIDSTS = Convert.ToString(dataReader["MSTVALIDSTS"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (outletcategory.Count > 0)
                {
                    return new ServiceResponse<IList<MasterOutletCategory>>
                    {
                        Success = true,
                        Data = outletcategory
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterOutletCategory>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterOutletCategory>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<IList<MasterTerritory>> GetAllTerritory(Int32 MstId)
        {
            IList<MasterTerritory> objterritory = new List<MasterTerritory>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CRUD_Territory;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MSTID", MstId);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    objterritory.Add(new MasterTerritory
                                    {
                                        MstId = Convert.ToInt32(dataReader["MstId"]),
                                        MstCd = Convert.ToString(dataReader["MstCd"]),
                                        MstName = Convert.ToString(dataReader["MstName"]),
                                        MstDesc = Convert.ToString(dataReader["MstDesc"]),
                                        StateId = Convert.ToString(dataReader["StateId"]),
                                        StateName = Convert.ToString(dataReader["StateName"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (objterritory.Count > 0)
                {
                    return new ServiceResponse<IList<MasterTerritory>>
                    {
                        Success = true,
                        Data = objterritory
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterTerritory>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterTerritory>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<IList<User>> GetAllUsers()
        {
            IList<User> users = new List<User>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USERS;
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
                                    users.Add(new User
                                    {
                                        Id = Convert.ToString(dataReader["ID"]),
                                        Name = Convert.ToString(dataReader["NAME"])

                                    });
                                }
                            }
                        }
                    }
                }
                if (users.Count > 0)
                {
                    return new ServiceResponse<IList<User>>
                    {
                        Success = true,
                        Data = users
                    };
                }
                else
                {
                    return new ServiceResponse<IList<User>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<User>> { Success = false, Message = sqlEx.Message };
            }
        }

        //IMAGEVIEW//
        public ServiceResponse<IList<ImageView>>  GetAllImageView()
        {
            IList<ImageView> imageview = new List<ImageView>();

            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CRUD_ImageView;
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
                                    imageview.Add(new ImageView
                                    {
                                        ImageViewID = Convert.ToInt32(dataReader["ImageViewID"]),
                                        ImageViewCode = Convert.ToString(dataReader["ImageViewCode"]),
                                        ViewName = Convert.ToString(dataReader["ViewName"]),
                                        Description = Convert.ToString(dataReader["Description"]),
                                        SortOrder = Convert.ToString(dataReader["SortOrder"]),
                                        InsertedBy = Convert.ToString(dataReader["InsertedBy"]),
                                        InsertedOn = dataReader["InsertedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["InsertedOn"]),
                                        UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
                                        UpdatedOn = dataReader["UpdatedOn"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["UpdatedOn"]),
                                        IsActive = Convert.ToBoolean(dataReader["IsActive"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (imageview.Count > 0)
                {
                    return new ServiceResponse<IList<ImageView>>
                    {
                        Success = true,
                        Data = imageview
                    };
                }
                else
                {
                    return new ServiceResponse<IList<ImageView>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<ImageView>> { Success = false, Message = sqlEx.Message };
            }
        }

        //TERRITORY//
        public ServiceResponse<bool> AddTerritory(MasterTerritory territory)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CRUD_Territory;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstCd", territory.MstCd);
                        cmd.Parameters.AddWithValue("@MstName", territory.MstName);
                        cmd.Parameters.AddWithValue("@MstDesc", territory.MstDesc);
                        cmd.Parameters.AddWithValue("@StateId", territory.StateId);
                        cmd.Parameters.AddWithValue("@MstSortBy", territory.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@InsertedBy", territory.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<bool> EditTerritory(MasterTerritory territory)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CRUD_Territory;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstID", territory.MstId);
                        cmd.Parameters.AddWithValue("@MstCd", territory.MstCd);
                        cmd.Parameters.AddWithValue("@MstName", territory.MstName);
                        cmd.Parameters.AddWithValue("@MstDesc", territory.MstDesc);
                        cmd.Parameters.AddWithValue("@StateId", territory.StateId);
                        cmd.Parameters.AddWithValue("@MstSortBy", territory.SortOrder);
                        cmd.Parameters.AddWithValue("@IsActive", true);
                        cmd.Parameters.AddWithValue("@UpdatedBy", territory.UpdatedBy);
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

        public ServiceResponse<bool> DisableTerritory(MasterTerritory territory)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CRUD_Territory;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MstID", territory.MstId);
                        cmd.Parameters.AddWithValue("@UpdatedBy", territory.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);

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

        //MenuAPIs//
        public ServiceResponse<IList<MenuMaster>> GetMenu()
        {
            IList<MenuMaster> Menu = new List<MenuMaster>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.MENU;
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
                                    Menu.Add(new MenuMaster
                                    {
                                        Id = Convert.ToInt32(dataReader["Id"]),
                                        MenuName = Convert.ToString(dataReader["MenuName"]),
                                        ParentId = Convert.ToInt32(dataReader["ParentId"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (Menu.Count > 0)
                {
                    return new ServiceResponse<IList<MenuMaster>>
                    {
                        Success = true,
                        Data = Menu
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MenuMaster>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MenuMaster>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<IList<MenuMaster>> GetMenuMasterOld()
        {
            try
            {
                IList<MenuMaster> MenuMaster = new List<MenuMaster>();

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.MENUMASTER;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 1);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    MenuMaster.Add(new MenuMaster
                                    {
                                        Id = dataReader["Id"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Id"]),
                                        MenuName = dataReader["MenuName"] == DBNull.Value ? "" : Convert.ToString(dataReader["MenuName"]),
                                        PageURL = dataReader["PageURL"] == DBNull.Value ? "" : Convert.ToString(dataReader["PageURL"])

                                    });
                                }
                            }
                        }

                    }
                }
                if (MenuMaster.Count >= 1)
                    return new ServiceResponse<IList<MenuMaster>>
                    {
                        Success = true,
                        Data = MenuMaster
                    };
                else
                    return new ServiceResponse<IList<MenuMaster>>
                    {
                        Success = true,
                        Message = "No items found."
                    };

            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MenuMaster>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<IList<MenuMaster>> GetMenuMaster()
        {
            try
            {
                IList<MenuMaster> MenuMaster = new List<MenuMaster>();

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.MENUMASTER;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 1);
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            da.SelectCommand = cmd;
                            da.Fill(dt);
                            MenuMaster mnuMaster = new MenuMaster();
                            mnuMaster.MenuName = Convert.ToString(dt.Rows[0][0]);
                        }
                    }
                }
                if (MenuMaster.Count >= 1)
                    return new ServiceResponse<IList<MenuMaster>>
                    {
                        Success = true,
                        Data = MenuMaster
                    };
                else
                    return new ServiceResponse<IList<MenuMaster>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MenuMaster>> { Success = false, Message = sqlEx.Message };
            }
        }

        //public ServiceResponse<IList<MenuMaster>> GetMenuMasterData(int dataid)
        //{
        //    try
        //    {
        //        IList<MenuMaster> MenuMaster = new List<MenuMaster>();

        //        using (SqlConnection dbConnection = new SqlConnection(_connection))
        //        {
        //            string cmdQuery = DBCommands.MENUMASTER;
        //            dbConnection.Open();

        //            using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
        //            {
        //                cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@DataId", dataid);
        //                cmd.Parameters.AddWithValue("@Flag", 1);
        //                using (SqlDataAdapter da = new SqlDataAdapter())
        //                {
        //                    DataTable dt = new DataTable();
        //                    da.SelectCommand = cmd;
        //                    da.Fill(dt);

        //                    string rawJsonFromDb = Convert.ToString(dt.Rows[0][0]);
        //                    string cleanedJson = Regex.Unescape(rawJsonFromDb);
        //                    var menuItems = JsonConvert.DeserializeObject<List<MenuItem>>(cleanedJson);
        //                    return new ServiceResponse<IList<MenuMaster>>
        //                    {
        //                        Success = true,
        //                        Data = (IList<MenuMaster>)menuItems
        //                    };
        //                }

        //            }
        //        }
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        return new ServiceResponse<IList<MenuMaster>> { Success = false, Message = sqlEx.Message };
        //    }
        //}

        public ServiceResponse<IList<MenuItem>> GetMenuMasterData(int dataid)
        {
            try
            {
                IList<MenuItem> menuItems = new List<MenuItem>();

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.MENUMASTER;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", dataid);
                        cmd.Parameters.AddWithValue("@Flag", 1);

                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            da.SelectCommand = cmd;
                            da.Fill(dt);

                            if (dt.Rows.Count == 0 || dt.Rows[0][0] == DBNull.Value)
                            {
                                return new ServiceResponse<IList<MenuItem>> { Success = true, Data = new List<MenuItem>(), Message = "No menu data found." };
                            }

                            string rawJsonFromDb = Convert.ToString(dt.Rows[0][0]);

                            string cleanedJson = Regex.Unescape(rawJsonFromDb);

                            var deserializedItems = JsonConvert.DeserializeObject<List<MenuItem>>(cleanedJson);

                            return new ServiceResponse<IList<MenuItem>>
                            {
                                Success = true,
                                Data = deserializedItems
                            };
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MenuItem>> { Success = false, Message = sqlEx.Message };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<IList<MenuItem>> { Success = false, Message = $"Deserialization Error: {ex.Message}" };
            }
        }

        public ServiceResponse<bool> AddUserMenuPermission(UserMenuPermission UserMenuPermission)
        {

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USER_MENU_PERMISSION;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", UserMenuPermission.DataId);
                        cmd.Parameters.AddWithValue("@MenuId", UserMenuPermission.MenuId);
                        cmd.Parameters.AddWithValue("@InsertedBy", UserMenuPermission.UsrId);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<IList<UserMenuPermission>> GetUserMenuPermission(int dataid)
        {
            try
            {
                IList<UserMenuPermission> userMenuPermission = new List<UserMenuPermission>();

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USER_MENU_PERMISSION;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", dataid);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    userMenuPermission.Add(new UserMenuPermission
                                    {
                                        DataName = dataReader["DataName"] == DBNull.Value ? "" : Convert.ToString(dataReader["DataName"]),
                                        DataId = dataReader["DataID"] == DBNull.Value ? "" : Convert.ToString(dataReader["DataID"]),
                                        MenuName = dataReader["MenuName"] == DBNull.Value ? "" : Convert.ToString(dataReader["MenuName"]),
                                        Ids = dataReader["Ids"] == DBNull.Value ? "" : Convert.ToString(dataReader["Ids"]),
                                        strParentIds = dataReader["ParentIds"] == DBNull.Value ? "" : Convert.ToString(dataReader["ParentIds"])
                                    });
                                }
                            }
                        }

                    }
                }

                if (userMenuPermission.Count >= 1)
                    return new ServiceResponse<IList<UserMenuPermission>>
                    {
                        Success = true,
                        Data = userMenuPermission
                    };
                else
                    return new ServiceResponse<IList<UserMenuPermission>>
                    {
                        Success = true,
                        Message = "No items found."
                    };

            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<UserMenuPermission>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<IList<UserMenuPermissionCRUD>> GetUserMenuPermissionCRUD(int menupermissionid)
        {
            try
            {
                IList<UserMenuPermissionCRUD> userMenuPermissionCRUD = new List<UserMenuPermissionCRUD>();

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USER_MENU_PERMISSION_CRUD;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserMenuPermissionID", menupermissionid);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    userMenuPermissionCRUD.Add(new UserMenuPermissionCRUD
                                    {
                                        UserMenuPermissionID = dataReader["UserMenuPermissionID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["UserMenuPermissionID"]),
                                        DataName = dataReader["DataName"] == DBNull.Value ? "" : Convert.ToString(dataReader["DataName"]),
                                        DataCode = dataReader["DataCd"] == DBNull.Value ? "" : Convert.ToString(dataReader["DataCd"]),
                                        MenuName = dataReader["MenuName"] == DBNull.Value ? "" : Convert.ToString(dataReader["MenuName"]),
                                        IsRead = dataReader["IsRead"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsRead"]),
                                        IsWrite = dataReader["IsWrite"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsWrite"]),
                                        IsEdit = dataReader["IsEdit"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsEdit"]),
                                        IsDelete = dataReader["IsDelete"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsDelete"]),
                                        DataId = dataReader["DataId"] == DBNull.Value ? "0" : Convert.ToString(dataReader["DataId"]),

                                    });
                                }
                            }
                        }

                    }
                }


                if (userMenuPermissionCRUD.Count >= 1)
                    return new ServiceResponse<IList<UserMenuPermissionCRUD>>
                    {
                        Success = true,
                        Data = userMenuPermissionCRUD
                    };
                else
                    return new ServiceResponse<IList<UserMenuPermissionCRUD>>
                    {
                        Success = true,
                        Message = "No items found."
                    };

            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<UserMenuPermissionCRUD>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<bool> AddUserMenuPermissionCRUD(UserMenuPermissionCRUD UserMenuPermissionCRUD)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USER_MENU_PERMISSION_CRUD;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserMenuPermissionID", UserMenuPermissionCRUD.UserMenuPermissionID);
                        cmd.Parameters.AddWithValue("@DataId", UserMenuPermissionCRUD.DataId);
                        cmd.Parameters.AddWithValue("@IsRead", UserMenuPermissionCRUD.IsRead);
                        cmd.Parameters.AddWithValue("@IsWrite", UserMenuPermissionCRUD.IsWrite);
                        cmd.Parameters.AddWithValue("@IsEdit", UserMenuPermissionCRUD.IsEdit);
                        cmd.Parameters.AddWithValue("@IsDelete", UserMenuPermissionCRUD.IsDelete);
                        cmd.Parameters.AddWithValue("@InsertedBy", UserMenuPermissionCRUD.UsrId);
                        cmd.Parameters.AddWithValue("@Flag", 1);

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

        public ServiceResponse<IList<UserMenuPermissionCRUD>> GetUserMenuPermissionCRUDByDataId(int dataid, int menumasterid)
        {
            try
            {
                IList<UserMenuPermissionCRUD> userMenuPermissionCRUD = new List<UserMenuPermissionCRUD>();

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.USER_MENU_PERMISSION_CRUD;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserMenuPermissionID", 0);
                        cmd.Parameters.AddWithValue("@DataId", dataid);
                        cmd.Parameters.AddWithValue("@MenuMasterId", menumasterid);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    userMenuPermissionCRUD.Add(new UserMenuPermissionCRUD
                                    {
                                        UserMenuPermissionID = dataReader["UserMenuPermissionID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["UserMenuPermissionID"]),
                                        DataName = dataReader["UserMenuPermissionID"] == DBNull.Value ? "" : Convert.ToString(dataReader["DataName"]),
                                        DataCode = dataReader["DataCd"] == DBNull.Value ? "" : Convert.ToString(dataReader["DataCd"]),
                                        MenuName = dataReader["MenuName"] == DBNull.Value ? "" : Convert.ToString(dataReader["MenuName"]),
                                        IsRead = dataReader["IsRead"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsRead"]),
                                        IsWrite = dataReader["IsWrite"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsWrite"]),
                                        IsEdit = dataReader["IsEdit"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsEdit"]),
                                        IsDelete = dataReader["IsDelete"] == DBNull.Value ? false : Convert.ToBoolean(dataReader["IsDelete"]),
                                        DataId = dataReader["DataId"] == DBNull.Value ? "" : Convert.ToString(dataReader["DataId"]),
                                    });
                                }
                            }
                        }

                    }
                }


                if (userMenuPermissionCRUD.Count >= 1)
                    return new ServiceResponse<IList<UserMenuPermissionCRUD>>
                    {
                        Success = true,
                        Data = userMenuPermissionCRUD
                    };
                else
                    return new ServiceResponse<IList<UserMenuPermissionCRUD>>
                    {
                        Success = true,
                        Message = "No items found."
                    };

            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<UserMenuPermissionCRUD>> { Success = false, Message = sqlEx.Message };
            }
        }

        public ServiceResponse<IList<MasterTeams>> GetTeamUser(int Flag)
        {
            IList<MasterTeams> users = new List<MasterTeams>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.GETTEAMUSERS;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        //cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@Flag", Flag);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    users.Add(new MasterTeams
                                    {
                                        dataid = Convert.ToString(dataReader["DataId"]),
                                        dataname = Convert.ToString(dataReader["DataName"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (users.Count > 0)
                {
                    return new ServiceResponse<IList<MasterTeams>>
                    {
                        Success = true,
                        Data = users
                    };
                }
                else
                {
                    return new ServiceResponse<IList<MasterTeams>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<MasterTeams>> { Success = false, Message = sqlEx.Message };
            }
        }
    }
}
