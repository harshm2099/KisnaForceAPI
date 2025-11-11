using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class OthersService : IOthersService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        //PRODUCT VERIFICATION//
        public ServiceResponse<IList<T_PIECE_VERIFY_LOG>> VerifyProduct(string numType, string numValue)
        {
            IList<T_PIECE_VERIFY_LOG> zones = new List<T_PIECE_VERIFY_LOG>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.VerifyProduct;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        if (string.IsNullOrEmpty(numType) || numType == "0") { numType = ""; }
                        if (string.IsNullOrEmpty(numValue) || numValue == "0") { numValue = ""; }

                        cmd.Parameters.AddWithValue("@numType", numType);
                        cmd.Parameters.AddWithValue("@numValue", numValue);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    zones.Add(new T_PIECE_VERIFY_LOG
                                    {
                                        PVId = Convert.ToInt32(dataReader["PVId"]),
                                        DataID = Convert.ToInt32(dataReader["DataID"]),
                                        PVType = Convert.ToString(dataReader["PVType"]),
                                        PVNumber = Convert.ToString(dataReader["PVNumber"]),
                                        RequestType = Convert.ToString(dataReader["RequestType"]),
                                        Status = Convert.ToString(dataReader["Status"]),
                                        fileType = Convert.ToString(dataReader["fileType"]),
                                        Entdt = dataReader["Entdt"] == null ? DateTime.MinValue : Convert.ToDateTime(dataReader["Entdt"]),
                                        TicketNo = Convert.ToString(dataReader["TicketNo"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (zones.Count > 0)
                {
                    return new ServiceResponse<IList<T_PIECE_VERIFY_LOG>>
                    {
                        Success = true,
                        Data = zones
                    };
                }
                else
                {
                    return new ServiceResponse<IList<T_PIECE_VERIFY_LOG>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<T_PIECE_VERIFY_LOG>>
                {
                    Success = false,
                    Message = sqlEx.Message,
                };
            }
        }

        //SHOP LIST//
        public ServiceResponse<IList<T_NEW_ROUTE_USER_LIST>> GetShopList(string State, string FromDt, string ToDt)
        {
            IList<T_NEW_ROUTE_USER_LIST> shops = new List<T_NEW_ROUTE_USER_LIST>();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.Shoplist;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@state", State);
                        cmd.Parameters.AddWithValue("@fromdt", FromDt);
                        cmd.Parameters.AddWithValue("@todt", ToDt);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    shops.Add(new T_NEW_ROUTE_USER_LIST
                                    {
                                        A_I = Convert.ToInt32(dataReader["A_I"]),
                                        NewRoutId = dataReader["NewRoutId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["NewRoutId"]),
                                        DataID = dataReader["DataID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DataID"]),
                                        ShopName = dataReader["ShopName"] == DBNull.Value ? "0" : Convert.ToString(dataReader["ShopName"]),
                                        Contactnumber = dataReader["Contactnumber"] == DBNull.Value ? "0" : Convert.ToString(dataReader["Contactnumber"]),
                                        ContactPersonName = dataReader["ContactPersonName"] == DBNull.Value ? "0" : Convert.ToString(dataReader["ContactPersonName"]),
                                        Latitude = dataReader["Latitude"] == DBNull.Value ? "0" : Convert.ToString(dataReader["Latitude"]),
                                        Longitude = dataReader["Longitude"] == DBNull.Value ? "0" : Convert.ToString(dataReader["Longitude"]),
                                        Area = dataReader["Area"] == DBNull.Value ? "0" : Convert.ToString(dataReader["Area"]),
                                        City = dataReader["City"] == DBNull.Value ? "0" : Convert.ToString(dataReader["City"]),
                                        State = dataReader["State"] == DBNull.Value ? "0" : Convert.ToString(dataReader["State"]),
                                        EntDt = dataReader["Entdt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["Entdt"]),
                                        CngDt = dataReader["CngDt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["CngDt"]),
                                        AnniversaryDate = dataReader["AnniversaryDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["AnniversaryDate"]),
                                        DocumentImage = dataReader["DocumentImage"] == DBNull.Value ? "0" : Convert.ToString(dataReader["DocumentImage"]),
                                        EmailId = dataReader["EmailId"] == DBNull.Value ? "0" : Convert.ToString(dataReader["EmailId"]),
                                        AddressLine1 = dataReader["AddressLine1"] == DBNull.Value ? "0" : Convert.ToString(dataReader["AddressLine1"]),
                                        AddressLine2 = dataReader["AddressLine2"] == DBNull.Value ? "0" : Convert.ToString(dataReader["AddressLine2"]),
                                        BirthDate = dataReader["BirthDate"] == DBNull.Value ? "0" : Convert.ToString(dataReader["BirthDate"]),
                                        Remark = dataReader["Remark"] == DBNull.Value ? "0" : Convert.ToString(dataReader["Remark"]),
                                        CurrentBusiness = dataReader["CurrentBusiness"] == DBNull.Value ? "0" : Convert.ToString(dataReader["CurrentBusiness"]),
                                        OtherBusiness = dataReader["OtherBusiness"] == DBNull.Value ? "0" : Convert.ToString(dataReader["OtherBusiness"]),
                                        FirmName = dataReader["FirmName"] == DBNull.Value ? "0" : Convert.ToString(dataReader["FirmName"]),
                                        StoreLocation = dataReader["StoreLocation"] == DBNull.Value ? "0" : Convert.ToString(dataReader["StoreLocation"]),
                                        StoreSize = dataReader["StoreSize"] == DBNull.Value ? "0" : Convert.ToString(dataReader["StoreSize"]),
                                        InvestmentDiscussion = dataReader["InvestmentDiscussion"] == DBNull.Value ? "0" : Convert.ToString(dataReader["InvestmentDiscussion"]),
                                        NoofRetailers = dataReader["NoofRetailers"] == DBNull.Value ? "0" : Convert.ToString(dataReader["NoofRetailers"]),
                                        Amount = dataReader["Amount"] == DBNull.Value ? "0" : Convert.ToString(dataReader["Amount"]),
                                        ExistsRetailer = dataReader["ExistsRetailer"] == DBNull.Value ? "0" : Convert.ToString(dataReader["ExistsRetailer"]),
                                        InactiveKisnaRetailer = dataReader["InactiveKisnaRetailer"] == DBNull.Value ? "0" : Convert.ToString(dataReader["InactiveKisnaRetailer"]),
                                        LeadReference = dataReader["LeadReference"] == DBNull.Value ? "0" : Convert.ToString(dataReader["LeadReference"]),
                                        Comment = dataReader["Comment"] == DBNull.Value ? "0" : Convert.ToString(dataReader["Comment"]),
                                        TypeofBusiness = dataReader["TypeofBusiness"] == DBNull.Value ? "0" : Convert.ToString(dataReader["TypeofBusiness"]),
                                        TypeOtherBusiness = dataReader["TypeOtherBusiness"] == DBNull.Value ? "0" : Convert.ToString(dataReader["TypeOtherBusiness"]),
                                        BAId = dataReader["BAId"] == DBNull.Value ? "0" : Convert.ToString(dataReader["BAId"]),
                                        OutletCategory = dataReader["OutletCategory"] == DBNull.Value ? "0" : Convert.ToString(dataReader["OutletCategory"]),
                                        TargetValue = dataReader["TargetValue"] == DBNull.Value ? "0" : Convert.ToString(dataReader["TargetValue"]),
                                        MonthPlan = dataReader["MonthPlan"] == DBNull.Value ? "0" : Convert.ToString(dataReader["MonthPlan"]),
                                        DiamondSelling = dataReader["DiamondSelling"] == DBNull.Value ? "0" : Convert.ToString(dataReader["DiamondSelling"]),
                                        DataName = dataReader["DataName"] == DBNull.Value ? "0" : Convert.ToString(dataReader["DataName"]),
                                        UTName = dataReader["UTName"] == DBNull.Value ? "0" : Convert.ToString(dataReader["UTName"]),
                                        DataCd = dataReader["DataCd"] == DBNull.Value ? "0" : Convert.ToString(dataReader["DataCd"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (shops.Count > 0)
                {
                    return new ServiceResponse<IList<T_NEW_ROUTE_USER_LIST>>
                    {
                        Success = true,
                        Data = shops
                    };
                }
                else
                {
                    return new ServiceResponse<IList<T_NEW_ROUTE_USER_LIST>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<T_NEW_ROUTE_USER_LIST>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddShoplist(Shoplistitems Shoplistitems)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.Shoplistupload;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", Shoplistitems.bacode);
                        cmd.Parameters.AddWithValue("@ShopName", Shoplistitems.shopName);
                        cmd.Parameters.AddWithValue("@Latitude", Shoplistitems.latitude);
                        cmd.Parameters.AddWithValue("@Longitude", Shoplistitems.longitude);
                        cmd.Parameters.AddWithValue("@Area", Shoplistitems.area);
                        cmd.Parameters.AddWithValue("@City", Shoplistitems.city);
                        cmd.Parameters.AddWithValue("@State", Shoplistitems.state);
                        cmd.Parameters.AddWithValue("@Contactnumber", Shoplistitems.contactnumber);

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

        //GOLD PARAMETER //
        public ServiceResponse<IList<GoldParameter>> FetchGoldParameters(int paramID)
        {
            IList<GoldParameter> goldParameters = new List<GoldParameter>();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.GoldParams;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamID", paramID);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    goldParameters.Add(new GoldParameter
                                    {
                                        ParamID = dataReader["GoldPrameterId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["GoldPrameterId"]),
                                        GoldValue = dataReader["GoldParameterValue"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["GoldParameterValue"]),
                                        ParamName = dataReader["GoldParameterName"] == DBNull.Value ? "" : Convert.ToString(dataReader["GoldParameterName"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (goldParameters.Count > 0)
                {
                    return new ServiceResponse<IList<GoldParameter>>
                    {
                        Success = true,
                        Data = goldParameters
                    };
                }
                else
                {
                    return new ServiceResponse<IList<GoldParameter>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<GoldParameter>>
                {
                    Success = false,
                    Message = sqlEx.Message,
                };
            }
        }

        public ServiceResponse<bool> UpdateGoldValue(GoldParameter gold)
        {

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.GoldParams;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamID", gold.ParamID);
                        cmd.Parameters.AddWithValue("@GoldValue", gold.GoldValue);
                        cmd.Parameters.AddWithValue("@userID", gold.UserID);
                        cmd.Parameters.AddWithValue("@Flag", 1);
                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                            //return Ok();
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

        //FRANCHISE RAW MATERIAL - LABOUR //   
        public ServiceResponse<IList<RmPrice>> FetchRMValuesForFranchise(string rmType = "L")
        {
            IList<RmPrice> rmPrices = new List<RmPrice>();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.FranchisePrice;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmType", rmType);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    rmPrices.Add(new RmPrice
                                    {
                                        RmID = Convert.ToInt32(dataReader["RmId"]),
                                        RmType = Convert.ToString(dataReader["RmType"]),
                                        RmName = Convert.ToString(dataReader["RmName"]),
                                        FromSize = Convert.ToString(dataReader["RmFromSize"]),
                                        ToSize = Convert.ToString(dataReader["RmToSize"]),
                                        BasedOnWt = dataReader["BasOnWtFlg"] == DBNull.Value ? ' ' : Convert.ToChar(dataReader["BasOnWtFlg"]),
                                        RmValue = Convert.ToDecimal(dataReader["RmPrice"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (rmPrices.Count > 0)
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Data = rmPrices
                    };
                }
                else
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<RmPrice>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateFranchiseRmPrices(RmPrice rmprice)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchisePrice;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", rmprice.RmID);
                        cmd.Parameters.AddWithValue("@RmValue", rmprice.RmValue);
                        cmd.Parameters.AddWithValue("@basedOnWt", rmprice.BasedOnWt);
                        cmd.Parameters.AddWithValue("@userID", rmprice.UserID);
                        cmd.Parameters.AddWithValue("@Flag", 1);
                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                            return new ServiceResponse<bool> { Success = true, Message = "Success", Data = true, };
                        else
                            return new ServiceResponse<bool> { Success = false, Message = "Something went wrong. Please check the data", Data = false, };
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<bool> { Success = false, Message = sqlEx.Message, Data = false, };

            }
        }

        public ServiceResponse<IList<RmPrice>> FetchColorStonesForFranchise(string stone = "Bugguete")
        {
            IList<RmPrice> rmPrices = new List<RmPrice>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseCSPrice;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Stone", stone);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    rmPrices.Add(new RmPrice
                                    {
                                        RmID = Convert.ToInt32(dataReader["RmId"]),
                                        RmType = Convert.ToString(dataReader["RmType"]),
                                        RmName = Convert.ToString(dataReader["RmName"]),
                                        FromSize = Convert.ToString(dataReader["RmFromSize"]),
                                        ToSize = Convert.ToString(dataReader["RmToSize"]),
                                        BasedOnWt = dataReader["BasOnWtFlg"] == DBNull.Value ? ' ' : Convert.ToChar(dataReader["BasOnWtFlg"]),
                                        RmValue = Convert.ToDecimal(dataReader["RmPrice"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (rmPrices.Count > 0)
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Data = rmPrices
                    };
                }
                else
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<RmPrice>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateFranchiseColorStonePrices(RmPrice rmprice)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseExtra;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", rmprice.RmID);
                        cmd.Parameters.AddWithValue("@RmValue", rmprice.RmValue);
                        cmd.Parameters.AddWithValue("@basedOnWt", rmprice.BasedOnWt);
                        cmd.Parameters.AddWithValue("@userID", rmprice.UserID);
                        cmd.Parameters.AddWithValue("@Flag", 1);
                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                            return new ServiceResponse<bool> { Success = true, Message = "Success", Data = true, };
                        else
                            return new ServiceResponse<bool> { Success = false, Message = "Something went wrong. Please check the data", Data = false, };
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<bool> { Success = false, Message = sqlEx.Message, Data = false, };
            }
        }

        public ServiceResponse<IList<RmPrice>> FetchDiamondRatesForFranchise(string quality = "SI", string shape = "RND")
        {
            IList<RmPrice> rmPrices = new List<RmPrice>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseDiamond;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@diamondQuality", quality);
                        cmd.Parameters.AddWithValue("@diamondShape", shape);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    rmPrices.Add(new RmPrice
                                    {
                                        RmID = Convert.ToInt32(dataReader["RmId"]),
                                        RmType = Convert.ToString(dataReader["RmType"]),
                                        RmName = Convert.ToString(dataReader["RmName"]),
                                        shape = Convert.ToString(dataReader["RmShape"]),
                                        quality = Convert.ToString(dataReader["RmDiaQuality"]),
                                        BasedOnWt = dataReader["BasOnWtFlg"] == DBNull.Value ? ' ' : Convert.ToChar(dataReader["BasOnWtFlg"]),
                                        RmValue = Convert.ToDecimal(dataReader["RmPrice"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (rmPrices.Count > 0)
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Data = rmPrices
                    };
                }
                else
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<RmPrice>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateFranchiseDiamondPrices(RmPrice rmprice)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.FranchiseCSPrice;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", rmprice.RmID);
                        cmd.Parameters.AddWithValue("@RmValue", rmprice.RmValue);
                        cmd.Parameters.AddWithValue("@userID", rmprice.UserID);
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

        public ServiceResponse<IList<RmPrice>> FetchExtraMaterialRatesForFranchise()
        {
            IList<RmPrice> rmPrices = new List<RmPrice>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseExtra;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    rmPrices.Add(new RmPrice
                                    {
                                        RmID = Convert.ToInt32(dataReader["RmId"]),
                                        RmType = Convert.ToString(dataReader["RmType"]),
                                        RmName = Convert.ToString(dataReader["RmName"]),
                                        RmValue = Convert.ToDecimal(dataReader["RmPrice"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (rmPrices.Count > 0)
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Data = rmPrices
                    };
                }
                else
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<RmPrice>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateFranchiseExtraMaterialPrices(RmPrice rmprice)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseCSPrice;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", rmprice.RmID);
                        cmd.Parameters.AddWithValue("@RmValue", rmprice.RmValue);
                        cmd.Parameters.AddWithValue("@userID", rmprice.UserID);
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

        //DISTRIBUTOR RAW MATERIAL - LABOUR //
        public ServiceResponse<IList<RmPrice>> FetchRMValuesForDistributor(string rmType = "L", string ppTag = "Promo")
        {
            IList<RmPrice> rmPrices = new List<RmPrice>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorPrice;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RmType", rmType);
                        cmd.Parameters.AddWithValue("@Pptag", ppTag);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    rmPrices.Add(new RmPrice
                                    {
                                        RmID = Convert.ToInt32(dataReader["RmId"]),
                                        RmType = Convert.ToString(dataReader["RmType"]),
                                        RmName = Convert.ToString(dataReader["RmName"]),
                                        FromSize = Convert.ToString(dataReader["RmFromSize"]),
                                        ToSize = Convert.ToString(dataReader["RmToSize"]),
                                        BasedOnWt = dataReader["BasOnWtFlg"] == DBNull.Value ? ' ' : Convert.ToChar(dataReader["BasOnWtFlg"]),
                                        RmValue = Convert.ToDecimal(dataReader["RmPrice"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (rmPrices.Count > 0)
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Data = rmPrices
                    };
                }
                else
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<RmPrice>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateDistributorRmPrices(RmPrice rmprice)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorPrice;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", rmprice.RmID);
                        cmd.Parameters.AddWithValue("@RmValue", rmprice.RmValue);
                        cmd.Parameters.AddWithValue("@basedOnWt", rmprice.BasedOnWt);
                        cmd.Parameters.AddWithValue("@userID", rmprice.UserID);
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

        public ServiceResponse<IList<RmPrice>> FetchExtraMaterialRatesForDistributor(string pptag)
        {
            IList<RmPrice> rmPrices = new List<RmPrice>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorExtra;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pptag", pptag);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    rmPrices.Add(new RmPrice
                                    {
                                        RmID = Convert.ToInt32(dataReader["RmId"]),
                                        RmType = Convert.ToString(dataReader["RmType"]),
                                        RmName = Convert.ToString(dataReader["RmName"]),
                                        RmValue = Convert.ToDecimal(dataReader["RmPrice"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (rmPrices.Count > 0)
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Data = rmPrices
                    };
                }
                else
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<RmPrice>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateDistributorExtraMaterialPrices(RmPrice rmprice)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorExtra;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", rmprice.RmID);
                        cmd.Parameters.AddWithValue("@RmValue", rmprice.RmValue);
                        cmd.Parameters.AddWithValue("@userID", rmprice.UserID);
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

        public ServiceResponse<IList<RmPrice>> FetchColorStoneRatesForDistributor(string stone = "Bugguete", string pptag = "Premium")
        {
            IList<RmPrice> rmPrices = new List<RmPrice>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorCSPrice;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Stone", stone);
                        cmd.Parameters.AddWithValue("@pptag", pptag);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    rmPrices.Add(new RmPrice
                                    {
                                        RmID = Convert.ToInt32(dataReader["RmId"]),
                                        RmType = Convert.ToString(dataReader["RmType"]),
                                        RmName = Convert.ToString(dataReader["RmName"]),
                                        BasedOnWt = dataReader["BasOnWtFlg"] == DBNull.Value ? ' ' : Convert.ToChar(dataReader["BasOnWtFlg"]),
                                        RmValue = Convert.ToDecimal(dataReader["RmPrice"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (rmPrices.Count > 0)
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Data = rmPrices
                    };
                }
                else
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<RmPrice>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateDistributorColorStonePrices(RmPrice rmprice)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorCSPrice; //DistributorExtra
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", rmprice.RmID);
                        cmd.Parameters.AddWithValue("@RmValue", rmprice.RmValue);
                        cmd.Parameters.AddWithValue("@basedOnWt", rmprice.BasedOnWt);
                        cmd.Parameters.AddWithValue("@userID", rmprice.UserID);
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

        public ServiceResponse<IList<RmPrice>> FetchDiamondRatesForDistributor(string pptag = "Premium", string quality = "SI", string shape = "RND")
        {
            IList<RmPrice> rmPrices = new List<RmPrice>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorDiamond;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@pptag", pptag);
                        cmd.Parameters.AddWithValue("@diamondQuality", quality);
                        cmd.Parameters.AddWithValue("@diamondShape", shape);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    rmPrices.Add(new RmPrice
                                    {
                                        RmID = Convert.ToInt32(dataReader["RmId"]),
                                        RmType = Convert.ToString(dataReader["RmType"]),
                                        RmName = Convert.ToString(dataReader["RmName"]),
                                        RmValue = Convert.ToDecimal(dataReader["RmPrice"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (rmPrices.Count > 0)
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Data = rmPrices
                    };
                }
                else
                {
                    return new ServiceResponse<IList<RmPrice>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<RmPrice>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateDistributorDiamondPrices(RmPrice rmprice)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorDiamond;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", rmprice.RmID);
                        cmd.Parameters.AddWithValue("@RmValue", rmprice.RmValue);
                        cmd.Parameters.AddWithValue("@userID", rmprice.UserID);
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

        // GEOMETRIC REPORT //
        public ServiceResponse<IList<GeoData>> FetchGeometricReport()
        {
            IList<GeoData> geoDatas = new List<GeoData>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.Geometric;
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
                                    geoDatas.Add(new GeoData
                                    {
                                        SrNo = Convert.ToInt32(dataReader["A_I"]),
                                        State = Convert.ToString(dataReader["DataAddrState"]),
                                        count = Convert.ToInt32(dataReader["cnt"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (geoDatas.Count > 0)
                {
                    return new ServiceResponse<IList<GeoData>>
                    {
                        Success = true,
                        Data = geoDatas
                    };
                }
                else
                {
                    return new ServiceResponse<IList<GeoData>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<GeoData>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        //FRANCHISE - LABOUR ON COMPLEXITY //
        public ServiceResponse<IList<LOC>> FetchLOCValuesForFranchise(int rmUserID, int rmCatID, int rmComplexityID)
        {
            IList<LOC> locs = new List<LOC>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseLOC;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmCatID", rmCatID);
                        cmd.Parameters.AddWithValue("@RmComplexityID", rmComplexityID);
                        cmd.Parameters.AddWithValue("@RmUserID", rmUserID);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    locs.Add(new LOC
                                    {
                                        rmID = Convert.ToInt32(dataReader["RmId"]),
                                        labourPercentage = Convert.ToDecimal(dataReader["RmPrice"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (locs.Count > 0)
                {
                    return new ServiceResponse<IList<LOC>>
                    {
                        Success = true,
                        Data = locs
                    };
                }
                else
                {
                    return new ServiceResponse<IList<LOC>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<LOC>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateFranchiseLOC(LOC loc)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseLOC;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", loc.rmID);
                        cmd.Parameters.AddWithValue("@LabourPerc", loc.labourPercentage);
                        cmd.Parameters.AddWithValue("@userID", loc.UserID);
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

        //FRANCHISE - LABOUR ON COIN COMPLEXITY //
        public ServiceResponse<IList<LOCC>> FetchLOCCValuesForFranchise(int rmUserID, int rmComplexityID)
        {
            IList<LOCC> locc = new List<LOCC>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseLOCC;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmComplexityID", rmComplexityID);
                        cmd.Parameters.AddWithValue("@RmUserID", rmUserID);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    locc.Add(new LOCC
                                    {
                                        ID = dataReader["ID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ID"]),
                                        grams = dataReader["grams"] == DBNull.Value ? "0" : Convert.ToString(dataReader["grams"]),
                                        labour = dataReader["labourvalue"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["labourvalue"]),
                                        RmSubCatID = dataReader["ComplexityId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["ComplexityId"]),
                                        UserID = dataReader["DataId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DataId"])

                                    });
                                }
                            }
                        }
                    }
                }
                if (locc.Count > 0)
                {
                    return new ServiceResponse<IList<LOCC>>
                    {
                        Success = true,
                        Data = locc
                    };
                }
                else
                {
                    return new ServiceResponse<IList<LOCC>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<LOCC>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateFranchiseLOCC(LOCC loc)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.FranchiseLOCC;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", loc.ID);
                        cmd.Parameters.AddWithValue("@Labour", loc.labour); //@LabourPerc
                        cmd.Parameters.AddWithValue("@RmComplexityID", loc.RmSubCatID);
                        cmd.Parameters.AddWithValue("@RmUserID", loc.UserID);
                        cmd.Parameters.AddWithValue("@Flag", 2); //1
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

        //DISTRIBUTOR - LABOUR ON COMPLEXITY //
        public ServiceResponse<IList<LOC>> FetchLOCValuesForDistributor(int rmUserTypeID, int rmUserID, int rmComplexityID)
        {
            IList<LOC> locs = new List<LOC>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorLOC;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rmUserTypeID", rmUserTypeID);
                        cmd.Parameters.AddWithValue("@RmComplexityID", rmComplexityID);
                        cmd.Parameters.AddWithValue("@RmUserID", rmUserID);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    locs.Add(new LOC
                                    {
                                        rmID = Convert.ToInt32(dataReader["RmId"]),
                                        //labourPercentage = Convert.ToDecimal(dataReader["RmPrice"])
                                        labourPercentage = Convert.ToDecimal(dataReader["FinalLabourPercentage"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (locs.Count > 0)
                {
                    return new ServiceResponse<IList<LOC>>
                    {
                        Success = true,
                        Data = locs
                    };
                }
                else
                {
                    return new ServiceResponse<IList<LOC>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<LOC>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateDistributorLOC(LOC loc)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorLOC;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmComplexityID", loc.RmSubCatID);
                        cmd.Parameters.AddWithValue("@LabourPerc", loc.labourPercentage);
                        cmd.Parameters.AddWithValue("@userID", loc.UserID);
                        cmd.Parameters.AddWithValue("@rmUserTypeID", loc.RmUserTypeID);
                        cmd.Parameters.AddWithValue("@RmUserID", loc.RmUserID);
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

        //DISTRIBUTOR - LABOUR ON COIN COMPLEXITY //
        public ServiceResponse<IList<LOCC>> FetchLOCCValuesForDistributor(int rmUserID, int rmUserTypeID, int rmComplexityID)
        {
            IList<LOCC> locc = new List<LOCC>();

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))

                {
                    string cmdQuery = DBCommands.DistributorLOCC; //FranchiseLOCC
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmComplexityID", rmComplexityID);
                        cmd.Parameters.AddWithValue("@RmUserID", rmUserID);
                        cmd.Parameters.AddWithValue("@RmUserTypeID", rmUserTypeID);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    locc.Add(new LOCC
                                    {
                                        ID = Convert.ToInt32(dataReader["ID"]),
                                        grams = Convert.ToString(dataReader["grams"]),
                                        labour = Convert.ToDecimal(dataReader["labourvalue"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (locc.Count > 0)
                {
                    return new ServiceResponse<IList<LOCC>>
                    {
                        Success = true,
                        Data = locc
                    };
                }
                else
                {
                    return new ServiceResponse<IList<LOCC>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<LOCC>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateDistributorLOCC(LOCC loc)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DistributorLOCC;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@RmID", loc.ID);
                        cmd.Parameters.AddWithValue("@LabourPerc", loc.labour);
                        cmd.Parameters.AddWithValue("@RmComplexityID", loc.RmSubCatID);
                        cmd.Parameters.AddWithValue("@userID", loc.UserID);
                        cmd.Parameters.AddWithValue("@RmUserTypeID", loc.RmUserTypeID);
                        cmd.Parameters.AddWithValue("@RmUserID", loc.RmUserID);
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

        // FRANCHISE GOLD PARAMETER //
        public ServiceResponse<IList<GoldParameter>> FetchGoldParametersForFranchise(int paramID = 0)
        {
            IList<GoldParameter> goldParameters = new List<GoldParameter>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseGoldValues;
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamID", paramID);
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        IList<GoldParameter> gp = new List<GoldParameter>();
                        //  IList<T_INFO_SRC_MST> info = new List<T_INFO_SRC_MST>();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    goldParameters.Add(new GoldParameter
                                    {                                  
                                        ParamID = dataReader["GoldPrameterId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["GoldPrameterId"]),
                                        GoldParameterName = Convert.ToString(dataReader["GoldParameterName"]),
                                        GoldParameterFranchise = Convert.ToDecimal(dataReader["GoldParameterFranchise"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (goldParameters.Count > 0)
                {
                    return new ServiceResponse<IList<GoldParameter>>
                    {
                        Success = true,
                        Data = goldParameters
                    };
                }
                else
                {
                    return new ServiceResponse<IList<GoldParameter>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<GoldParameter>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<GoldParameter>> FetchGoldParametersForFranchiseDetails(int paramID = 0)
        {
            IList<GoldParameter> goldParameters = new List<GoldParameter>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseGoldValues;
                    dbConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamID", paramID);
                        cmd.Parameters.AddWithValue("@Flag", 2);

                        IList<GoldParameter> gp = new List<GoldParameter>();
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    if (paramID == 1)
                                    {
                                        goldParameters.Add(new GoldParameter
                                        {
                                            current_franchise_gold_value = dataReader["current_franchise_gold_value"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["current_franchise_gold_value"]),
                                            current_live_gold_value = dataReader["current_live_gold_value"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["current_live_gold_value"]),
                                            premium_franchise_gold_value = dataReader["premium_franchise_gold_value"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["premium_franchise_gold_value"]),
                                            totalfloat = dataReader["total"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["total"]),
                                        });
                                    }
                                    else
                                    {
                                        if (paramID == 8)
                                        {
                                            goldParameters.Add(new GoldParameter
                                            {
                                                GoldParameterValue = dataReader["GoldParameterValue"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["GoldParameterValue"]),
                                                GoldParameterFranchise = dataReader["GoldParameterFranchise"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["GoldParameterFranchise"]),
                                                GoldParameterFranchise1 = dataReader["GoldParameterFranchise1"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["GoldParameterFranchise1"]),
                                                totalfloat = dataReader["total"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["total"]),
                                            });
                                        }
                                        else if (paramID == 13)
                                        {
                                            goldParameters.Add(new GoldParameter
                                            {
                                                GoldParameterValue = dataReader["GoldParameterValue"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["GoldParameterValue"]),
                                                GoldParameterFranchiseFloat = dataReader["GoldParameterFranchise"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["GoldParameterFranchise"]),
                                                GoldParameterFranchise1 = dataReader["GoldParameterFranchise1"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["GoldParameterFranchise1"]),
                                                totalfloat = dataReader["total"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["total"]),
                                            });

                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                if (goldParameters.Count > 0)
                {
                    return new ServiceResponse<IList<GoldParameter>>
                    {
                        Success = true,
                        Data = goldParameters
                    };
                }
                else
                {
                    return new ServiceResponse<IList<GoldParameter>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<GoldParameter>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> UpdateGoldValueForFranchise(GoldParameter gold)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.FranchiseGoldValues;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ParamID", gold.ParamID);
                        if (gold.ParamID == 1 || gold.ParamID == 8 || gold.ParamID == 13)
                        {
                            cmd.Parameters.AddWithValue("@current_franchise_gold_value_", gold.current_franchise_gold_value);
                            cmd.Parameters.AddWithValue("@current_live_gold_value_", gold.current_live_gold_value);
                            cmd.Parameters.AddWithValue("@GoldParameterFranchise_", gold.GoldParameterFranchise);
                            cmd.Parameters.AddWithValue("@total", gold.total);
                        }

                        cmd.Parameters.AddWithValue("@userID", gold.UserID);
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

        // SURVEY //
        public ServiceResponse<IList<T_INFO_SRC_MST>> GetAllSurveys()
        {
            IList<T_INFO_SRC_MST> info = new List<T_INFO_SRC_MST>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.INFORMATION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    info.Add(new T_INFO_SRC_MST
                                    {
                                        InfoSrcId = Convert.ToInt32(dataReader["InfoSrcId"]),
                                        InfoSrcName = Convert.ToString(dataReader["InfoSrcName"]),
                                        InfoSrcUrl = Convert.ToString(dataReader["InfoSrcUrl"]),
                                        InfoSrcDesc = Convert.ToString(dataReader["InfoSrcDesc"]),
                                        InfoSrcStatus = Convert.ToChar(dataReader["InfoSrcStatus"]),
                                        InfoSrcCtg = dataReader["InfoSrcCtg"] == DBNull.Value ? "" : Convert.ToString(dataReader["InfoSrcCtg"]),
                                        InfoSrcTyp = dataReader["InfoSrcTyp"] == DBNull.Value ? "" : Convert.ToString(dataReader["InfoSrcTyp"]),
                                        InfoSrcFlag = dataReader["InfoSrcFlag"] == DBNull.Value ? ' ' : Convert.ToChar(dataReader["InfoSrcFlag"]),
                                        InfoSrcSortBy = dataReader["InfoSrcSortBy"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["InfoSrcSortBy"]),
                                        InfoSrcUsrId = dataReader["InfoSrcUsrId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["InfoSrcUsrId"]),
                                        InfoSrcEntDt = dataReader["InfoSrcEntDt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InfoSrcEntDt"]),
                                        InfoSrcCngDt = dataReader["InfoSrcCngDt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["InfoSrcCngDt"]),
                                        InfoSrcDownloadSts = dataReader["InfoSrcDownloadSts"] == DBNull.Value ? ' ' : Convert.ToChar(dataReader["InfoSrcDownloadSts"]),
                                        InfoTypeFlag = dataReader["InfoTypeFlag"] == DBNull.Value ? "" : Convert.ToString(dataReader["InfoTypeFlag"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (info.Count > 0)
                {
                    return new ServiceResponse<IList<T_INFO_SRC_MST>>
                    {
                        Success = true,
                        Data = info
                    };
                }
                else
                {
                    return new ServiceResponse<IList<T_INFO_SRC_MST>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<T_INFO_SRC_MST>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddInformation(T_INFO_SRC_MST info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.INFORMATION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@InfoSrcName", info.InfoSrcName);
                        cmd.Parameters.AddWithValue("@InfoSrcUrl", info.InfoSrcUrl);
                        cmd.Parameters.AddWithValue("@InfoSrcDesc", info.InfoSrcDesc);
                        cmd.Parameters.AddWithValue("@InfoSrcStatus", info.InfoSrcStatus);
                        cmd.Parameters.AddWithValue("@InfoSrcCtg", "Survey");
                        cmd.Parameters.AddWithValue("@InfoSrcTyp", "Survey");
                        cmd.Parameters.AddWithValue("@InfoSrcFlag", "W");
                        cmd.Parameters.AddWithValue("@InfoSrcDownloadSts", 'N');
                        cmd.Parameters.AddWithValue("@InfoTypeFlag", "Kisna");
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

        public ServiceResponse<bool> EditInformation(T_INFO_SRC_MST info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.INFORMATION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@InfoSrcName", info.InfoSrcName);
                        cmd.Parameters.AddWithValue("@InfoSrcUrl", info.InfoSrcUrl);
                        cmd.Parameters.AddWithValue("@InfoSrcDesc", info.InfoSrcDesc);
                        cmd.Parameters.AddWithValue("@InfoSrcStatus", info.InfoSrcStatus);
                        cmd.Parameters.AddWithValue("@InfoSrcCtg", "Survey");
                        cmd.Parameters.AddWithValue("@InfoSrcTyp", "Survey");
                        cmd.Parameters.AddWithValue("@InfoSrcFlag", "W");
                        cmd.Parameters.AddWithValue("@InfoSrcDownloadSts", 'N');
                        cmd.Parameters.AddWithValue("@InfoTypeFlag", "Kisna");
                        cmd.Parameters.AddWithValue("@InfoSrcId", info.InfoSrcId);
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

        public ServiceResponse<bool> DisableInformation(T_INFO_SRC_MST info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.INFORMATION_MST;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@InfoSrcId", info.InfoSrcId);
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

        // BANNER //
        public ServiceResponse<IList<Banner>> GetAllBanners()
        {
            IList<Banner> banners = new List<Banner>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.banner;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    banners.Add(new Banner
                                    {
                                        HomeID = dataReader["HomeID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["HomeID"]),
                                        HomeImgID = dataReader["HomeImgID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["HomeImgID"]),
                                        HomeItemID = dataReader["HomeItemID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["HomeItemID"]),
                                        HomeMenuID = dataReader["HomeMenuID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["HomeMenuID"]),
                                        HomeURL = Convert.ToString(dataReader["HomeURL"]),
                                        HomeValidSts = dataReader["HomeValidSts"] == DBNull.Value ? 'N' : Convert.ToChar(dataReader["HomeValidSts"]),
                                        HomeRemarks = Convert.ToString(dataReader["HomeRemarks"]),
                                        HomeSortBy = Convert.ToDecimal(dataReader["HomeSortBy"]),
                                        HomeRefCommonID = dataReader["HomeID"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["HomeRefCommonID"]),
                                        HomeImgPath = Convert.ToString(dataReader["HomeImgPath"]),
                                        SoliterStatus = Convert.ToString(dataReader["SoliterStatus"]),
                                        HomeRef = Convert.ToString(dataReader["HomeRef"]),
                                        HomeAppMenu = Convert.ToString(dataReader["HomeAppMenu"]),
                                        FilePath = Convert.ToString(dataReader["FilePath"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (banners.Count > 0)
                {
                    return new ServiceResponse<IList<Banner>>
                    {
                        Success = true,
                        Data = banners
                    };
                }
                else
                {
                    return new ServiceResponse<IList<Banner>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<Banner>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddBanner(Banner info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.banner;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HomeImgID", info.HomeImgID);
                        cmd.Parameters.AddWithValue("@HomeItemID", info.HomeItemID);
                        cmd.Parameters.AddWithValue("@HomeMenuID", info.HomeMenuID);
                        cmd.Parameters.AddWithValue("@HomeURL", info.HomeURL);
                        cmd.Parameters.AddWithValue("@HomeValidSts", info.HomeValidSts);
                        cmd.Parameters.AddWithValue("@HomeRemarks", info.HomeRemarks);
                        cmd.Parameters.AddWithValue("@HomeSortBy", info.HomeSortBy);
                        cmd.Parameters.AddWithValue("@HomeUsrID", info.UserID);
                        cmd.Parameters.AddWithValue("@HomeRefCommonID", info.HomeRefCommonID);
                        cmd.Parameters.AddWithValue("@HomeImgPath", info.HomeImgPath);
                        cmd.Parameters.AddWithValue("@SoliterStatus", info.SoliterStatus);
                        cmd.Parameters.AddWithValue("@HomeRef", info.HomeRef);
                        cmd.Parameters.AddWithValue("@HomeAppMenu", info.HomeAppMenu);
                        cmd.Parameters.AddWithValue("@FilePath", info.FilePath);
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
        public ServiceResponse<bool> EditBanner(Banner info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.banner;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HomeID", info.HomeID);
                        cmd.Parameters.AddWithValue("@HomeImgID", info.HomeImgID);
                        cmd.Parameters.AddWithValue("@HomeItemID", info.HomeItemID);
                        cmd.Parameters.AddWithValue("@HomeMenuID", info.HomeMenuID);
                        cmd.Parameters.AddWithValue("@HomeURL", info.HomeURL);
                        cmd.Parameters.AddWithValue("@HomeValidSts", info.HomeValidSts);
                        cmd.Parameters.AddWithValue("@HomeRemarks", info.HomeRemarks);
                        cmd.Parameters.AddWithValue("@HomeSortBy", info.HomeSortBy);
                        cmd.Parameters.AddWithValue("@HomeUsrID", info.UserID);
                        cmd.Parameters.AddWithValue("@HomeRefCommonID", info.HomeRefCommonID);
                        cmd.Parameters.AddWithValue("@HomeImgPath", info.HomeImgPath);
                        cmd.Parameters.AddWithValue("@SoliterStatus", info.SoliterStatus);
                        cmd.Parameters.AddWithValue("@HomeRef", info.HomeRef);
                        cmd.Parameters.AddWithValue("@HomeAppMenu", info.HomeAppMenu);
                        cmd.Parameters.AddWithValue("@FilePath", info.FilePath);
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

        public ServiceResponse<bool> DisableBanner(Banner info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.banner;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@HomeID", info.HomeID);
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

        // SCREEN SAVER  //
        public ServiceResponse<IList<ScreenSaver>> GetAllScreenSavers()
        {
            IList<ScreenSaver> savers = new List<ScreenSaver>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ScreenSaver;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    savers.Add(new ScreenSaver
                                    {
                                        ScreenId = Convert.ToInt32(dataReader["ScreenId"]),
                                        Name = Convert.ToString(dataReader["Name"]),
                                        Selection = Convert.ToString(dataReader["Selection"]),
                                        ImagePath = Convert.ToString(dataReader["ImagePath"]),
                                        Remarks = Convert.ToString(dataReader["Remarks"]),
                                        ScreenType = Convert.ToString(dataReader["ScreenType"]),
                                        ValidSts = Convert.ToChar(dataReader["ValidSts"]),
                                        Screencounter = dataReader["Screencounter"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["Screencounter"]),
                                        ScreensaverType = Convert.ToInt32(dataReader["ScreensaverType"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (savers.Count > 0)
                {
                    return new ServiceResponse<IList<ScreenSaver>>
                    {
                        Success = true,
                        Data = savers
                    };
                }
                else
                {
                    return new ServiceResponse<IList<ScreenSaver>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<ScreenSaver>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddScreenSaver(ScreenSaver saver)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ScreenSaver;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", saver.Name);
                        cmd.Parameters.AddWithValue("@Selection", saver.Selection);
                        cmd.Parameters.AddWithValue("@ImagePath", saver.ImagePath);
                        cmd.Parameters.AddWithValue("@Remarks", saver.Remarks);
                        cmd.Parameters.AddWithValue("@ScreenType", saver.ScreenType);
                        cmd.Parameters.AddWithValue("@ValidSts", saver.ValidSts);
                        cmd.Parameters.AddWithValue("@Screencounter", saver.Screencounter);
                        cmd.Parameters.AddWithValue("@ScreensaverType", saver.ScreensaverType);
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

        public ServiceResponse<bool> EditScreenSaver(ScreenSaver saver)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ScreenSaver;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ScreenID", saver.ScreenId);
                        cmd.Parameters.AddWithValue("@Name", saver.Name);
                        cmd.Parameters.AddWithValue("@Selection", saver.Selection);
                        cmd.Parameters.AddWithValue("@ImagePath", saver.ImagePath);
                        cmd.Parameters.AddWithValue("@Remarks", saver.Remarks);
                        cmd.Parameters.AddWithValue("@ScreenType", saver.ScreenType);
                        cmd.Parameters.AddWithValue("@ValidSts", saver.ValidSts);
                        cmd.Parameters.AddWithValue("@Screencounter", saver.Screencounter);
                        cmd.Parameters.AddWithValue("@ScreensaverType", saver.ScreensaverType);
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

        public ServiceResponse<bool> DisableScreenSaver(ScreenSaver saver)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.ScreenSaver;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ScreenID", saver.ScreenId);
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

        // NEW OUTLET  //
        public ServiceResponse<IList<NewOutlet>> GetAllNewOutlets()
        {
            IList<NewOutlet> outlets = new List<NewOutlet>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.outlet;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    outlets.Add(new NewOutlet
                                    {
                                        NewOutLetID = Convert.ToInt32(dataReader["NewOutLetID"]),
                                        ShopName = Convert.ToString(dataReader["ShopName"]),
                                        EmailId = Convert.ToString(dataReader["EmailId"]),
                                        ContactNo = Convert.ToString(dataReader["ContactNo"]),
                                        Image = Convert.ToString(dataReader["Image"]),
                                        City = Convert.ToInt32(dataReader["City"]),
                                        NewOutLetDate = dataReader["NewOutLetDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["NewOutLetDate"]),
                                        SrName = Convert.ToString(dataReader["SrName"]),
                                        Distributor = Convert.ToString(dataReader["Distributor"]),
                                        GstNo = Convert.ToString(dataReader["GstNo"]),
                                        Penetration_Details = Convert.ToString(dataReader["Penetration_Details"]),
                                        ContactPerson = Convert.ToString(dataReader["ContactPerson"]),
                                        //ContactNo = Convert.ToString(dataReader["ContactNo"]),
                                        Address = Convert.ToString(dataReader["Address"]),
                                        State = Convert.ToInt32(dataReader["State"]),
                                        PinCodeNo = Convert.ToInt32(dataReader["PinCodeNo"]),
                                        DOB = dataReader["DOB"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DOB"]),
                                        ADate = dataReader["ADate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["ADate"]),
                                        //JoinDate = Convert.ToDateTime(dataReader["JoinDate"]),
                                        JoinDate = dataReader["JoinDate"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["JoinDate"]),
                                        Territory = Convert.ToInt32(dataReader["Territory"]),
                                        SubTerritory = Convert.ToString(dataReader["SubTerritory"]),
                                        Area = Convert.ToInt32(dataReader["Area"]),
                                        DateOfDeal = dataReader["DateOfDeal"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["DateOfDeal"]),
                                        DataImageId = Convert.ToString(dataReader["DataImageId"]),
                                        DataDeviceSecurityStatus = Convert.ToChar(dataReader["DataDeviceSecurityStatus"]),
                                        DataOrgCommonID = Convert.ToInt32(dataReader["DataOrgCommonID"]),
                                        SelectType = Convert.ToInt32(dataReader["SelectType"]),
                                        SelectTitle = Convert.ToInt32(dataReader["SelectTitle"]),
                                        DataCd = Convert.ToString(dataReader["DataCd"]),
                                        DataName = Convert.ToString(dataReader["DataName"]),
                                        DataContactTitle = dataReader["DataContactTitle"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DataContactTitle"]),
                                        DisplayType = Convert.ToInt32(dataReader["DisplayType"]),
                                        Community = Convert.ToInt32(dataReader["Community"]),
                                        NoOfVisit = Convert.ToInt32(dataReader["NoOfVisit"]),
                                        WeeklyOff = Convert.ToInt32(dataReader["WeeklyOff"]),
                                        Reference1 = Convert.ToString(dataReader["Reference1"]),
                                        Reference2 = Convert.ToString(dataReader["Reference2"]),
                                        DistBillNo = Convert.ToString(dataReader["DistBillNo"]),
                                        InitialQty = Convert.ToInt32(dataReader["InitialQty"]),
                                        InitialAmount = Convert.ToInt32(dataReader["InitialAmount"]),
                                        CourierService = Convert.ToInt32(dataReader["CourierService"]),
                                        StyleNo = Convert.ToString(dataReader["StyleNo"]),
                                        DataLongitude = Convert.ToString(dataReader["DataLongitude"]),
                                        DataLatitude = Convert.ToString(dataReader["DataLatitude"]),
                                        EmrCustCd = Convert.ToString(dataReader["EmrCustCd"]),
                                        ValidSts = Convert.ToChar(dataReader["ValidSts"]),
                                        EditSts = Convert.ToChar(dataReader["EditSts"]),
                                        UsrId = Convert.ToInt32(dataReader["UsrId"]),
                                        EntDt = dataReader["EntDt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dataReader["EntDt"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (outlets.Count > 0)
                {
                    return new ServiceResponse<IList<NewOutlet>>
                    {
                        Success = true,
                        Data = outlets
                    };
                }
                else
                {
                    return new ServiceResponse<IList<NewOutlet>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<NewOutlet>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddNewOutlet(NewOutlet outlet)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.outlet;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NewOutLetDate", outlet.NewOutLetDate);
                        cmd.Parameters.AddWithValue("@SrName", outlet.SrName);
                        cmd.Parameters.AddWithValue("@Distributor", outlet.Distributor);
                        cmd.Parameters.AddWithValue("@ShopName", outlet.ShopName);
                        cmd.Parameters.AddWithValue("@GstNo", outlet.GstNo);
                        cmd.Parameters.AddWithValue("@Penetration_Details", outlet.Penetration_Details);
                        cmd.Parameters.AddWithValue("@ContactPerson", outlet.ContactPerson);
                        cmd.Parameters.AddWithValue("@ContactNo", outlet.ContactNo);
                        cmd.Parameters.AddWithValue("@EmailId", outlet.EmailId);
                        cmd.Parameters.AddWithValue("@Address", outlet.Address);
                        cmd.Parameters.AddWithValue("@State", outlet.State);
                        cmd.Parameters.AddWithValue("@City", outlet.City);
                        cmd.Parameters.AddWithValue("@PinCodeNo", outlet.PinCodeNo);
                        cmd.Parameters.AddWithValue("@DOB", outlet.DOB);
                        cmd.Parameters.AddWithValue("@ADate", outlet.ADate);
                        cmd.Parameters.AddWithValue("@JoinDate", outlet.JoinDate);
                        cmd.Parameters.AddWithValue("@Territory", outlet.Territory);
                        cmd.Parameters.AddWithValue("@SubTerritory", outlet.SubTerritory);
                        cmd.Parameters.AddWithValue("@Area", outlet.Area);
                        cmd.Parameters.AddWithValue("@DateOfDeal", outlet.DateOfDeal);
                        cmd.Parameters.AddWithValue("@DataImageId", outlet.DataImageId);
                        cmd.Parameters.AddWithValue("@DataDeviceSecurityStatus", outlet.DataDeviceSecurityStatus);
                        cmd.Parameters.AddWithValue("@DataOrgCommonID", outlet.DataOrgCommonID);
                        cmd.Parameters.AddWithValue("@SelectType", outlet.SelectType);
                        cmd.Parameters.AddWithValue("@SelectTitle", outlet.SelectTitle);
                        cmd.Parameters.AddWithValue("@DataCd", outlet.DataCd);
                        cmd.Parameters.AddWithValue("@DataName", outlet.DataName);
                        cmd.Parameters.AddWithValue("@DataContactTitle", outlet.DataContactTitle);
                        cmd.Parameters.AddWithValue("@DisplayType", outlet.DisplayType);
                        cmd.Parameters.AddWithValue("@Community", outlet.Community);
                        cmd.Parameters.AddWithValue("@NoOfVisit", outlet.NoOfVisit);
                        cmd.Parameters.AddWithValue("@WeeklyOff", outlet.WeeklyOff);
                        cmd.Parameters.AddWithValue("@Reference1", outlet.Reference1);
                        cmd.Parameters.AddWithValue("@Reference2", outlet.Reference2);
                        cmd.Parameters.AddWithValue("@DistBillNo", outlet.DistBillNo);
                        cmd.Parameters.AddWithValue("@InitialQty", outlet.InitialQty);
                        cmd.Parameters.AddWithValue("@InitialAmount", outlet.InitialAmount);
                        cmd.Parameters.AddWithValue("@CourierService", outlet.CourierService);
                        cmd.Parameters.AddWithValue("@StyleNo", outlet.StyleNo);
                        cmd.Parameters.AddWithValue("@Image", outlet.Image);
                        cmd.Parameters.AddWithValue("@DataLongitude", outlet.DataLongitude);
                        cmd.Parameters.AddWithValue("@DataLatitude", outlet.DataLatitude);
                        cmd.Parameters.AddWithValue("@EmrCustCd", outlet.EmrCustCd);
                        cmd.Parameters.AddWithValue("@ValidSts", outlet.ValidSts);
                        cmd.Parameters.AddWithValue("@UsrId", outlet.UsrId);
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

        public ServiceResponse<bool> EditNewOutlet(NewOutlet outlet)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.outlet;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NewOutLetID", outlet.NewOutLetID);
                        cmd.Parameters.AddWithValue("@NewOutLetDate", outlet.NewOutLetDate);
                        cmd.Parameters.AddWithValue("@SrName", outlet.SrName);
                        cmd.Parameters.AddWithValue("@Distributor", outlet.Distributor);
                        cmd.Parameters.AddWithValue("@ShopName", outlet.ShopName);
                        cmd.Parameters.AddWithValue("@GstNo", outlet.GstNo);
                        cmd.Parameters.AddWithValue("@Penetration_Details", outlet.Penetration_Details);
                        cmd.Parameters.AddWithValue("@ContactPerson", outlet.ContactPerson);
                        cmd.Parameters.AddWithValue("@ContactNo", outlet.ContactNo);
                        cmd.Parameters.AddWithValue("@EmailId", outlet.EmailId);
                        cmd.Parameters.AddWithValue("@Address", outlet.Address);
                        cmd.Parameters.AddWithValue("@State", outlet.State);
                        cmd.Parameters.AddWithValue("@City", outlet.City);
                        cmd.Parameters.AddWithValue("@PinCodeNo", outlet.PinCodeNo);
                        cmd.Parameters.AddWithValue("@DOB", outlet.DOB);
                        cmd.Parameters.AddWithValue("@ADate", outlet.ADate);
                        cmd.Parameters.AddWithValue("@JoinDate", outlet.JoinDate);
                        cmd.Parameters.AddWithValue("@Territory", outlet.Territory);
                        cmd.Parameters.AddWithValue("@SubTerritory", outlet.SubTerritory);
                        cmd.Parameters.AddWithValue("@Area", outlet.Area);
                        cmd.Parameters.AddWithValue("@DateOfDeal", outlet.DateOfDeal);
                        cmd.Parameters.AddWithValue("@DataImageId", outlet.DataImageId);
                        cmd.Parameters.AddWithValue("@DataDeviceSecurityStatus", outlet.DataDeviceSecurityStatus);
                        cmd.Parameters.AddWithValue("@DataOrgCommonID", outlet.DataOrgCommonID);
                        cmd.Parameters.AddWithValue("@SelectType", outlet.SelectType);
                        cmd.Parameters.AddWithValue("@SelectTitle", outlet.SelectTitle);
                        cmd.Parameters.AddWithValue("@DataCd", outlet.DataCd);
                        cmd.Parameters.AddWithValue("@DataName", outlet.DataName);
                        cmd.Parameters.AddWithValue("@DataContactTitle", outlet.DataContactTitle);
                        cmd.Parameters.AddWithValue("@DisplayType", outlet.DisplayType);
                        cmd.Parameters.AddWithValue("@Community", outlet.Community);
                        cmd.Parameters.AddWithValue("@NoOfVisit", outlet.NoOfVisit);
                        cmd.Parameters.AddWithValue("@WeeklyOff", outlet.WeeklyOff);
                        cmd.Parameters.AddWithValue("@Reference1", outlet.Reference1);
                        cmd.Parameters.AddWithValue("@Reference2", outlet.Reference2);
                        cmd.Parameters.AddWithValue("@DistBillNo", outlet.DistBillNo);
                        cmd.Parameters.AddWithValue("@InitialQty", outlet.InitialQty);
                        cmd.Parameters.AddWithValue("@InitialAmount", outlet.InitialAmount);
                        cmd.Parameters.AddWithValue("@CourierService", outlet.CourierService);
                        cmd.Parameters.AddWithValue("@StyleNo", outlet.StyleNo);
                        cmd.Parameters.AddWithValue("@Image", outlet.Image);
                        cmd.Parameters.AddWithValue("@DataLongitude", outlet.DataLongitude);
                        cmd.Parameters.AddWithValue("@DataLatitude", outlet.DataLatitude);
                        cmd.Parameters.AddWithValue("@EmrCustCd", outlet.EmrCustCd);
                        cmd.Parameters.AddWithValue("@ValidSts", outlet.ValidSts);
                        cmd.Parameters.AddWithValue("@UsrId", outlet.UsrId);
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

        public ServiceResponse<bool> DisableNewOutlet(NewOutlet outlet)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.outlet;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NewOutLetID", outlet.NewOutLetID);
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

        public ServiceResponse<IList<SoliterParameter>> GetAllSoliterParameter(int FieldId)
        {
            IList<SoliterParameter> SoliterParameter = new List<SoliterParameter>();
            try
            {

                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SoliterParameter;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 0);
                        cmd.Parameters.AddWithValue("@FieldId", FieldId);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    SoliterParameter.Add(new SoliterParameter
                                    {
                                        SrNo = dataReader["SrNo"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["SrNo"]),
                                        FieldId = dataReader["FieldId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["FieldId"]),
                                        FieldName = dataReader["Field"] == DBNull.Value ? "0" : Convert.ToString(dataReader["Field"]),
                                        FieldValue = dataReader["Value"] == DBNull.Value ? 0 : Convert.ToSingle(dataReader["Value"]),
                                        ValidSts = dataReader["ValidSts"] == DBNull.Value ? ' ' : Convert.ToChar(dataReader["ValidSts"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (SoliterParameter.Count > 0)
                {
                    return new ServiceResponse<IList<SoliterParameter>>
                    {
                        Success = true,
                        Data = SoliterParameter
                    };
                }
                else
                {
                    return new ServiceResponse<IList<SoliterParameter>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<SoliterParameter>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddSoliterParameter(SoliterParameter info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SoliterParameter;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FieldName", info.FieldName);
                        cmd.Parameters.AddWithValue("@FieldValue", info.FieldValue);
                        cmd.Parameters.AddWithValue("@ValidSts", info.ValidSts);
                        cmd.Parameters.AddWithValue("@InsertedBy", info.UserID);
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

        public ServiceResponse<bool> EditSoliterParameter(SoliterParameter info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SoliterParameter;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FieldId", info.FieldId);
                        cmd.Parameters.AddWithValue("@FieldName", info.FieldName);
                        cmd.Parameters.AddWithValue("@FieldValue", info.FieldValue);
                        cmd.Parameters.AddWithValue("@ValidSts", info.ValidSts);
                        cmd.Parameters.AddWithValue("@UpdatedBy", info.UserID);
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

        public ServiceResponse<bool> DisableSoliterParameter(SoliterParameter info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SoliterParameter;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FieldId", info.FieldId);
                        cmd.Parameters.AddWithValue("@UpdatedBy", info.UserID);
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

        public ServiceResponse<bool> DeleteSoliterParameter(SoliterParameter info)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.SoliterParameter;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FieldId", info.FieldId);
                        cmd.Parameters.AddWithValue("@UpdatedBy", info.UserID);
                        cmd.Parameters.AddWithValue("@Flag", 4);

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

        public ServiceResponse<IList<DataMaster>> GetUserData(int DataId)
        {
            IList<DataMaster> DataMaster = new List<DataMaster>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DataMaster;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Flag", 0);
                        cmd.Parameters.AddWithValue("@DataId", DataId);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    DataMaster.Add(new DataMaster
                                    {
                                        DataId = dataReader["DataId"] == DBNull.Value ? 0 : Convert.ToInt32(dataReader["DataId"]),
                                        DataName = dataReader["DataName"] == DBNull.Value ? " " : Convert.ToString(dataReader["DataName"]),
                                    });
                                }
                            }
                        }
                    }
                }
                if (DataMaster.Count > 0)
                {
                    return new ServiceResponse<IList<DataMaster>>
                    {
                        Success = true,
                        Data = DataMaster
                    };
                }
                else
                {
                    return new ServiceResponse<IList<DataMaster>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<DataMaster>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<DiscontinueItems>> GetItemPriceData(Int16 Source, int DataId, string ItemName) 
        {
            IList<DiscontinueItems> DiscontinueItems = new List<DiscontinueItems>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.DiscontnueItems;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", DataId);
                        cmd.Parameters.AddWithValue("@ItemName", ItemName);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    DiscontinueItems.Add(new DiscontinueItems
                                    {
                                        ItemName = dataReader["ItemName"] == DBNull.Value ? " " : Convert.ToString(dataReader["ItemName"]),
                                        ItemOdSfx = dataReader["ItemOdSfx"] == DBNull.Value ? " " : Convert.ToString(dataReader["ItemOdSfx"]),
                                        ItemMRP = dataReader["ItemMRP"] == DBNull.Value ? " " : Convert.ToString(dataReader["ItemMRP"]),
                                        ItemDPrice = dataReader["ItemDPrice"] == DBNull.Value ? 0 : Convert.ToDecimal(dataReader["ItemDPrice"]),
                                        ItemValidSts = dataReader["ItemValidSts"] == DBNull.Value ? ' ' : Convert.ToChar(dataReader["ItemValidSts"]),

                                    });
                                }
                            }
                        }
                    }
                }
                if (DiscontinueItems.Count > 0)
                {
                    return new ServiceResponse<IList<DiscontinueItems>>
                    {
                        Success = true,
                        Data = DiscontinueItems
                    };
                }
                else
                {
                    return new ServiceResponse<IList<DiscontinueItems>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<DiscontinueItems>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<IList<Ecatelog>> GetEcatelog()
        {
            IList<Ecatelog> eCatelog = new List<Ecatelog>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.Ecatelog;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Flag", 0);

                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    eCatelog.Add(new Ecatelog
                                    {
                                        AutoNumber = Convert.ToInt32(dataReader["AutoNumber"]),
                                        InfoSrcId = Convert.ToString(dataReader["InfoSrcId"]),
                                        InfoSrcName = Convert.ToString(dataReader["InfoSrcName"]),
                                        InfoSrcUrl = Convert.ToString(dataReader["InfoSrcUrl"]),
                                        InfoSrcDesc = Convert.ToString(dataReader["InfoSrcDesc"]),
                                        InfoSrcCtg = Convert.ToString(dataReader["InfoSrcCtg"]),
                                        InfoSrcTyp = Convert.ToString(dataReader["InfoSrcTyp"]),
                                        PDFUrl = Convert.ToString(dataReader["PDFUrl"])
                                    });
                                }
                            }
                        }
                    }
                }
                if (eCatelog.Count > 0)
                {
                    return new ServiceResponse<IList<Ecatelog>>
                    {
                        Success = true,
                        Data = eCatelog
                    };
                }
                else
                {
                    return new ServiceResponse<IList<Ecatelog>>
                    {
                        Success = true,
                        Message = "No items found."
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<Ecatelog>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }

        public ServiceResponse<bool> AddNewCatelog(Ecatelog ecatelog)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.Ecatelog;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@InfoSrcDesc", ecatelog.InfoSrcDesc);
                        cmd.Parameters.AddWithValue("@InfoSrcName", ecatelog.InfoSrcName);
                        cmd.Parameters.AddWithValue("@InfoSrcUrl", ecatelog.InfoSrcUrl);
                        cmd.Parameters.AddWithValue("@InfoSrcStatus", ecatelog.InfoSrcStatus);
                        cmd.Parameters.AddWithValue("@UsrId", ecatelog.UsrId);
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

        public ServiceResponse<bool> UpdateCatelog(Ecatelog ecatelog)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.Ecatelog;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@InfoSrcDesc", ecatelog.InfoSrcDesc);
                        cmd.Parameters.AddWithValue("@InfoSrcName", ecatelog.InfoSrcName);
                        cmd.Parameters.AddWithValue("@InfoSrcUrl", ecatelog.InfoSrcUrl);
                        cmd.Parameters.AddWithValue("@InfoSrcStatus", ecatelog.InfoSrcStatus);
                        cmd.Parameters.AddWithValue("@UsrId", ecatelog.UsrId);
                        cmd.Parameters.AddWithValue("@InfoSrcId", ecatelog.InfoSrcId);
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

        public ServiceResponse<bool> DeleteCatelog(Ecatelog ecatelog)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.Ecatelog;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UsrId", ecatelog.UsrId);
                        cmd.Parameters.AddWithValue("@InfoSrcId", ecatelog.InfoSrcId);
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
    }
}
