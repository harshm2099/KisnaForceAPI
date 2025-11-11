using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using System.Data;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services
{
        public class CustomerMarginService : ICustomerMarginService
        {
            public string _connection = DBCommands.CONNECTION_STRING;

            public ServiceResponse<ResponseDetails> GetFranchiseList()
            {
                IList<FranchiseData> franchiseList = new List<FranchiseData>();
                var responseDetails = new ResponseDetails();
                int total_items = 0;

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.GET_FRANCHISELIST;
                        dbConnection.Open();

                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            using (SqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    {
                                        int DataID = dataReader["DataID"] as int? ?? 0;
                                        string DataName = dataReader["DataName"] as string ?? string.Empty;
                                        total_items = dataReader["total_items"] as int? ?? 0;

                                        franchiseList.Add(new FranchiseData
                                        {
                                            DataID = DataID,
                                            DataName = DataName,
                                        });
                                    }
                                }
                            }
                        }
                    }

                    if (franchiseList.Any())
                    {
                        responseDetails.success = true;
                        responseDetails.message = "Successfully";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = franchiseList;
                    }
                    else
                    {
                        responseDetails.success = false;
                        responseDetails.message = "No data found";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = new List<FranchiseData>();
                    }
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };

            }
                catch (SqlException sqlEx)
                {
                    responseDetails.success = false;
                    responseDetails.message = $"SQL error: {sqlEx.Message}";
                    responseDetails.status = "400";
                    responseDetails.total_items = "0";
                    responseDetails.data = new List<FranchiseData>();
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
            }

            public ServiceResponse<ResponseDetails> GetCustomerMarginData(int dataid)
            {
                IList<CustomerMarginData> customermargindataList = new List<CustomerMarginData>();
                var responseDetails = new ResponseDetails();
                int total_items = 0;

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.GET_CUSTOMERMARGINDATA_DATAID;
                        dbConnection.Open();

                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DataID", dataid);

                            using (SqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    {
                                        int MarginId = dataReader["MarginId"] as int? ?? 0;
                                        int MarginBrandID = dataReader["MarginBrandID"] as int? ?? 0;
                                        string MarginBrandName = dataReader["MarginBrandName"] as string ?? string.Empty;
                                        decimal MarginCurrentDP = dataReader["MarginCurrentDP"] as decimal? ?? 0;
                                        decimal MarginNewDP = dataReader["MarginNewDP"] as decimal? ?? 0;
                                        string OnLabour = dataReader["OnLabour"] as string ?? string.Empty;
                                        decimal Gold = dataReader["Gold"] as decimal? ?? 0;
                                        decimal GoldNew = dataReader["GoldNew"] as decimal? ?? 0;
                                        decimal Platinum = dataReader["Platinum"] as decimal? ?? 0;
                                        decimal PlatinumNew = dataReader["PlatinumNew"] as decimal? ?? 0;
                                        decimal Other = dataReader["Other"] as decimal? ?? 0;
                                        decimal OtherNew = dataReader["OtherNew"] as decimal? ?? 0;
                                        decimal Diamond = dataReader["Diamond"] as decimal? ?? 0;
                                        decimal DiamondNew = dataReader["DiamondNew"] as decimal? ?? 0;
                                        decimal Color = dataReader["Color"] as decimal? ?? 0;
                                        decimal ColorNew = dataReader["ColorNew"] as decimal? ?? 0;
                                        decimal Labour = dataReader["Labour"] as decimal? ?? 0;
                                        decimal LabourNew = dataReader["LabourNew"] as decimal? ?? 0;
                                        decimal Silver = dataReader["Silver"] as decimal? ?? 0;
                                        decimal SilverNew = dataReader["SilverNew"] as decimal? ?? 0;
                                        decimal SNMCCPer = dataReader["SNMCCPer"] as decimal? ?? 0;
                                        decimal SNMCCPerNew = dataReader["SNMCCPerNew"] as decimal? ?? 0;

                                        total_items = dataReader["total_items"] as int? ?? 0;

                                        customermargindataList.Add(new CustomerMarginData
                                        {
                                            MarginId = MarginId,
                                            MarginBrandID = MarginBrandID,
                                            MarginBrandName = MarginBrandName,
                                            MarginCurrentDP = MarginCurrentDP,
                                            MarginNewDP = MarginNewDP,
                                            OnLabour = OnLabour,
                                            Gold = Gold,
                                            GoldNew = GoldNew,
                                            Platinum = Platinum,
                                            PlatinumNew = PlatinumNew,
                                            Other = Other,
                                            OtherNew = OtherNew,
                                            Diamond = Diamond,
                                            DiamondNew = DiamondNew,
                                            Color = Color,
                                            ColorNew = ColorNew,
                                            Labour = Labour,
                                            LabourNew = LabourNew,
                                            Silver = Silver,
                                            SilverNew = SilverNew,
                                            SNMCCPer = SNMCCPer,
                                            SNMCCPerNew = SNMCCPerNew,
                                        });
                                    }
                                }
                            }
                        }
                    }

                    if (customermargindataList.Any())
                    {
                        responseDetails.success = true;
                        responseDetails.message = "Successfully";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = customermargindataList;
                    }
                    else
                    {
                        responseDetails.success = false;
                        responseDetails.message = "No data found";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = new List<CustomerMarginData>();
                    }
                //return Ok(responseDetails);
                return new ServiceResponse<ResponseDetails>
                {
                    Data = responseDetails
                };
            }
                catch (SqlException sqlEx)
                {
                    responseDetails.success = false;
                    responseDetails.message = $"SQL error: {sqlEx.Message}";
                    responseDetails.status = "400";
                    responseDetails.total_items = "0";
                    responseDetails.data = new List<CustomerMarginData>();
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
            }

            public ServiceResponse<ResponseDetails> CustomerMargin_AddUpdate(CustomerMarginAddUpdateParams customermargincrud_params)
            {
                var responseDetails = new ResponseDetails();
                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        int MarginId = customermargincrud_params.MarginId > 0 ? customermargincrud_params.MarginId : 0;
                        int DataId = customermargincrud_params.LoginDataId > 0 ? customermargincrud_params.LoginDataId : 0;
                        int MarginDataID = customermargincrud_params.MarginDataID > 0 ? customermargincrud_params.MarginDataID : 0;
                        int MarginBrandID = customermargincrud_params.MarginBrandID > 0 ? customermargincrud_params.MarginBrandID : 0;

                        decimal GoldNew = customermargincrud_params.GoldNew > 0 ? customermargincrud_params.GoldNew : 0;
                        decimal PlatinumNew = customermargincrud_params.PlatinumNew > 0 ? customermargincrud_params.PlatinumNew : 0;
                        decimal OtherNew = customermargincrud_params.OtherNew > 0 ? customermargincrud_params.OtherNew : 0;
                        decimal DiamondNew = customermargincrud_params.DiamondNew > 0 ? customermargincrud_params.DiamondNew : 0;
                        decimal ColorNew = customermargincrud_params.ColorNew > 0 ? customermargincrud_params.ColorNew : 0;
                        decimal LabourNew = customermargincrud_params.LabourNew > 0 ? customermargincrud_params.LabourNew : 0;
                        decimal SilverNew = customermargincrud_params.SilverNew > 0 ? customermargincrud_params.SilverNew : 0;
                        decimal SnmccPerNew = customermargincrud_params.SnmccPerNew > 0 ? customermargincrud_params.SnmccPerNew : 0;

                        string OnLabour = string.IsNullOrWhiteSpace(customermargincrud_params.OnLabour) ? "N" : customermargincrud_params.OnLabour;

                        int
                            resstatus = 0,
                            resstatuscode = 400,
                            updated_id = 0;
                        string resmessage = "";

                        string cmdQuery = "";
                        dbConnection.Open();

                        cmdQuery = DBCommands.CUSTOMERMARGIN_ADDUPDATE;
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MarginId", MarginId);
                            cmd.Parameters.AddWithValue("@DataID", DataId);
                            cmd.Parameters.AddWithValue("@MarginDataID", MarginDataID);
                            cmd.Parameters.AddWithValue("@MarginBrandID", MarginBrandID);
                            cmd.Parameters.AddWithValue("@goldNew", GoldNew);
                            cmd.Parameters.AddWithValue("@platinumNew", PlatinumNew);
                            cmd.Parameters.AddWithValue("@otherNew", OtherNew);
                            cmd.Parameters.AddWithValue("@diamondNew", DiamondNew);
                            cmd.Parameters.AddWithValue("@cStoneNew", ColorNew);
                            cmd.Parameters.AddWithValue("@labourNew", LabourNew);
                            cmd.Parameters.AddWithValue("@SilverNew", SilverNew);
                            cmd.Parameters.AddWithValue("@SnmccPerNew", SnmccPerNew);
                            cmd.Parameters.AddWithValue("@OnLabour", OnLabour);

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
                                            updated_id = firstRow["UpdatedId"] as int? ?? 0;
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
                        //return Ok(responseDetails);
                        return new ServiceResponse<ResponseDetails>
                        {
                            Data = responseDetails
                        };
                }
                }
                catch (SqlException sqlEx)
                {
                    responseDetails.success = false;
                    responseDetails.message = $"SQL error: {sqlEx.Message}";
                    responseDetails.status = "400";
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
            }

            public ServiceResponse<ResponseDetails> GetUserTypesForCustomerMarginDist()
            {
                IList<MasterUserType> userTypeList = new List<MasterUserType>();
                var responseDetails = new ResponseDetails();
                int total_items = 0;

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.GETUSERTYPES_FORCUSTOMERMARGINS;
                        dbConnection.Open();

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            using (SqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    {
                                        int UserTypeID = dataReader["UserTypeID"] as int? ?? 0;
                                        string UTCode = dataReader["UTCode"] as string ?? string.Empty;
                                        string UTName = dataReader["UTName"] as string ?? string.Empty;
                                        total_items = dataReader["total_items"] as int? ?? 0;

                                        userTypeList.Add(new MasterUserType
                                        {
                                            UserTypeID = UserTypeID,
                                            UTCode = UTCode,
                                            UTName = UTName,
                                        });
                                    }
                                }
                            }
                        }
                    }

                    if (userTypeList.Any())
                    {
                        responseDetails.success = true;
                        responseDetails.message = "Successfully";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = userTypeList;
                    }
                    else
                    {
                        responseDetails.success = false;
                        responseDetails.message = "No data found";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = new List<MasterUserType>();
                    }
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
                catch (SqlException sqlEx)
                {
                    responseDetails.success = false;
                    responseDetails.message = $"SQL error: {sqlEx.Message}";
                    responseDetails.status = "400";
                    responseDetails.total_items = "0";
                    responseDetails.data = new List<MasterUserType>();
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
            }

            public ServiceResponse<ResponseDetails> GetPPTagsForCustomerMarginDist()
            {
                IList<MasterPPTag> pptagList = new List<MasterPPTag>();
                var responseDetails = new ResponseDetails();
                int total_items = 0;

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.GETPPTAGS_FORCUSTOMERMARGINS;
                        dbConnection.Open();

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            using (SqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    {
                                        int PPTagID = dataReader["PPTagID"] as int? ?? 0;
                                        string PPTag = dataReader["PPTag"] as string ?? string.Empty;
                                        string Description = dataReader["Description"] as string ?? string.Empty;
                                        total_items = dataReader["total_items"] as int? ?? 0;

                                        pptagList.Add(new MasterPPTag
                                        {
                                            PPTagID = PPTagID,
                                            PPTag = PPTag,
                                            Description = Description,
                                        });
                                    }
                                }
                            }
                        }
                    }

                    if (pptagList.Any())
                    {
                        responseDetails.success = true;
                        responseDetails.message = "Successfully";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = pptagList;
                    }
                    else
                    {
                        responseDetails.success = false;
                        responseDetails.message = "No data found";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = new List<MasterPPTag>();
                    }
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
                catch (SqlException sqlEx)
                {
                    responseDetails.success = false;
                    responseDetails.message = $"SQL error: {sqlEx.Message}";
                    responseDetails.status = "400";
                    responseDetails.total_items = "0";
                    responseDetails.data = new List<MasterPPTag>();
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
            }

            public ServiceResponse<ResponseDetails> GetIsDisplayNewMarginForCustomerMarginDist(int current_admin_dataid)
            {
                IList<IsDisplayNewMarginListing> isdisplaynewmarginList = new List<IsDisplayNewMarginListing>();
                var responseDetails = new ResponseDetails();

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.GET_ISDISPLAYNEWMARGIN_FORCUSTOMERMARGINS;
                        dbConnection.Open();

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@current_admin_dataid", current_admin_dataid);

                            using (SqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    {
                                        int is_display_new_margin = dataReader["is_display_new_margin"] as int? ?? 0;
                                        isdisplaynewmarginList.Add(new IsDisplayNewMarginListing
                                        {
                                            is_display_new_margin = is_display_new_margin,
                                        });
                                    }
                                }
                            }
                        }
                    }

                    if (isdisplaynewmarginList.Any())
                    {
                        responseDetails.success = true;
                        responseDetails.message = "Successfully";
                        responseDetails.status = "200";
                        responseDetails.data = isdisplaynewmarginList;
                    }
                    else
                    {
                        responseDetails.success = false;
                        responseDetails.message = "No data found";
                        responseDetails.status = "200";
                        responseDetails.data = new List<IsDisplayNewMarginListing>();
                    }
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
                catch (SqlException sqlEx)
                {
                    responseDetails.success = false;
                    responseDetails.message = $"SQL error: {sqlEx.Message}";
                    responseDetails.status = "400";
                    responseDetails.total_items = "0";
                    responseDetails.data = new List<IsDisplayNewMarginListing>();
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
            }

            public ServiceResponse<ResponseDetails> GetUsersFromUserType(int usertypeid)
            {
                IList<User> userList = new List<User>();
                var responseDetails = new ResponseDetails();
                int total_items = 0;

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.GETUSERSFROMUSERTYPE;
                        dbConnection.Open();

                        CommonHelpers objHelpers = new CommonHelpers();
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@UsertypeID", usertypeid);

                            using (SqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    {
                                        int dataid = dataReader["DataID"] as int? ?? 0;
                                        string dataname = dataReader["DataName"] as string ?? string.Empty;
                                        total_items = dataReader["total_items"] as int? ?? 0;

                                        userList.Add(new User
                                        {
                                            Id = dataid.ToString(),
                                            Name = dataname,
                                        });
                                    }
                                }
                            }
                        }
                    }

                    if (userList.Any())
                    {
                        responseDetails.success = true;
                        responseDetails.message = "Successfully";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = userList;
                    }
                    else
                    {
                        responseDetails.success = false;
                        responseDetails.message = "No data found";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = new List<User>();
                    }
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
                catch (SqlException sqlEx)
                {
                    responseDetails.success = false;
                    responseDetails.message = $"SQL error: {sqlEx.Message}";
                    responseDetails.status = "400";
                    responseDetails.total_items = "0";
                    responseDetails.data = new List<User>();
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
            }

            public ServiceResponse<ResponseDetails> GetCustomerMarginDistData(int dataid)
            {
                IList<CustomerMarginDistData> customermargindistdataList = new List<CustomerMarginDistData>();
                var responseDetails = new ResponseDetails();
                int total_items = 0;

                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        string cmdQuery = DBCommands.GET_CUSTOMERMARGINDISTDATA_DATAID;
                        dbConnection.Open();

                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@DataID", dataid);

                            using (SqlDataReader dataReader = cmd.ExecuteReader())
                            {
                                if (dataReader.HasRows)
                                {
                                    while (dataReader.Read())
                                    {
                                        int MarginId = dataReader["MarginId"] as int? ?? 0;
                                        int MarginPPTagID = dataReader["MarginPPTagID"] as int? ?? 0;
                                        string MarginPPTag = dataReader["MarginPPTag"] as string ?? string.Empty;
                                        decimal MarginCurrentDP = dataReader["MarginCurrentDP"] as decimal? ?? 0;
                                        decimal MarginNewDP = dataReader["MarginNewDP"] as decimal? ?? 0;
                                        string OnLabour = dataReader["OnLabour"] as string ?? string.Empty;
                                        decimal Gold = dataReader["Gold"] as decimal? ?? 0;
                                        decimal GoldNew = dataReader["GoldNew"] as decimal? ?? 0;
                                        decimal Platinum = dataReader["Platinum"] as decimal? ?? 0;
                                        decimal PlatinumNew = dataReader["PlatinumNew"] as decimal? ?? 0;
                                        decimal Other = dataReader["Other"] as decimal? ?? 0;
                                        decimal OtherNew = dataReader["OtherNew"] as decimal? ?? 0;
                                        decimal Diamond = dataReader["Diamond"] as decimal? ?? 0;
                                        decimal DiamondNew = dataReader["DiamondNew"] as decimal? ?? 0;
                                        decimal Color = dataReader["Color"] as decimal? ?? 0;
                                        decimal ColorNew = dataReader["ColorNew"] as decimal? ?? 0;
                                        decimal Labour = dataReader["Labour"] as decimal? ?? 0;
                                        decimal LabourNew = dataReader["LabourNew"] as decimal? ?? 0;
                                        decimal Silver = dataReader["Silver"] as decimal? ?? 0;
                                        decimal SilverNew = dataReader["SilverNew"] as decimal? ?? 0;
                                        decimal SNMCCPer = dataReader["SNMCCPer"] as decimal? ?? 0;
                                        decimal SNMCCPerNew = dataReader["SNMCCPerNew"] as decimal? ?? 0;

                                        total_items = dataReader["total_items"] as int? ?? 0;

                                        customermargindistdataList.Add(new CustomerMarginDistData
                                        {
                                            MarginId = MarginId,
                                            MarginPPTagID = MarginPPTagID,
                                            MarginPPTag = MarginPPTag,
                                            MarginCurrentDP = MarginCurrentDP,
                                            MarginNewDP = MarginNewDP,
                                            OnLabour = OnLabour,
                                            Gold = Gold,
                                            GoldNew = GoldNew,
                                            Platinum = Platinum,
                                            PlatinumNew = PlatinumNew,
                                            Other = Other,
                                            OtherNew = OtherNew,
                                            Diamond = Diamond,
                                            DiamondNew = DiamondNew,
                                            Color = Color,
                                            ColorNew = ColorNew,
                                            Labour = Labour,
                                            LabourNew = LabourNew,
                                            Silver = Silver,
                                            SilverNew = SilverNew,
                                            SNMCCPer = SNMCCPer,
                                            SNMCCPerNew = SNMCCPerNew,
                                        });
                                    }
                                }
                            }
                        }
                    }

                    if (customermargindistdataList.Any())
                    {
                        responseDetails.success = true;
                        responseDetails.message = "Successfully";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = customermargindistdataList;
                    }
                    else
                    {
                        responseDetails.success = false;
                        responseDetails.message = "No data found";
                        responseDetails.status = "200";
                        responseDetails.total_items = total_items.ToString();
                        responseDetails.data = new List<CustomerMarginData>();
                    }
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
                catch (SqlException sqlEx)
                {
                    responseDetails.success = false;
                    responseDetails.message = $"SQL error: {sqlEx.Message}";
                    responseDetails.status = "400";
                    responseDetails.total_items = "0";
                    responseDetails.data = new List<CustomerMarginData>();
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
            }

            public ServiceResponse<ResponseDetails> CustomerMargindist_AddUpdate(CustomerMarginDistAddUpdateParams customermargindistcrud_params)
            {
                var responseDetails = new ResponseDetails();
                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_connection))
                    {
                        int MarginId = customermargindistcrud_params.MarginId > 0 ? customermargindistcrud_params.MarginId : 0;
                        int DataId = customermargindistcrud_params.LoginDataId > 0 ? customermargindistcrud_params.LoginDataId : 0;
                        int MarginDataID = customermargindistcrud_params.MarginDataID > 0 ? customermargindistcrud_params.MarginDataID : 0;
                        int MarginPPTagID = customermargindistcrud_params.MarginPPTagID > 0 ? customermargindistcrud_params.MarginPPTagID : 0;

                        decimal GoldNew = customermargindistcrud_params.GoldNew > 0 ? customermargindistcrud_params.GoldNew : 0;
                        decimal PlatinumNew = customermargindistcrud_params.PlatinumNew > 0 ? customermargindistcrud_params.PlatinumNew : 0;
                        decimal OtherNew = customermargindistcrud_params.OtherNew > 0 ? customermargindistcrud_params.OtherNew : 0;
                        decimal DiamondNew = customermargindistcrud_params.DiamondNew > 0 ? customermargindistcrud_params.DiamondNew : 0;
                        decimal ColorNew = customermargindistcrud_params.ColorNew > 0 ? customermargindistcrud_params.ColorNew : 0;
                        decimal LabourNew = customermargindistcrud_params.LabourNew > 0 ? customermargindistcrud_params.LabourNew : 0;
                        decimal SilverNew = customermargindistcrud_params.SilverNew > 0 ? customermargindistcrud_params.SilverNew : 0;
                        decimal SnmccPerNew = customermargindistcrud_params.SnmccPerNew > 0 ? customermargindistcrud_params.SnmccPerNew : 0;

                        string OnLabour = string.IsNullOrWhiteSpace(customermargindistcrud_params.OnLabour) ? "N" : customermargindistcrud_params.OnLabour;

                        int
                            resstatus = 0,
                            resstatuscode = 400,
                            updated_id = 0;
                        string resmessage = "";

                        string cmdQuery = "";
                        dbConnection.Open();

                        cmdQuery = DBCommands.CUSTOMERMARGINDIST_ADDUPDATE;
                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MarginId", MarginId);
                            cmd.Parameters.AddWithValue("@DataId", DataId);
                            cmd.Parameters.AddWithValue("@MarginDataID", MarginDataID);
                            cmd.Parameters.AddWithValue("@MarginPPTagID", MarginPPTagID);
                            cmd.Parameters.AddWithValue("@goldNew", GoldNew);
                            cmd.Parameters.AddWithValue("@platinumNew", PlatinumNew);
                            cmd.Parameters.AddWithValue("@otherNew", OtherNew);
                            cmd.Parameters.AddWithValue("@diamondNew", DiamondNew);
                            cmd.Parameters.AddWithValue("@cStoneNew", ColorNew);
                            cmd.Parameters.AddWithValue("@labourNew", LabourNew);
                            cmd.Parameters.AddWithValue("@SilverNew", SilverNew);
                            cmd.Parameters.AddWithValue("@SnmccPerNew", SnmccPerNew);
                            cmd.Parameters.AddWithValue("@OnLabour", OnLabour);

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
                                            updated_id = firstRow["UpdatedId"] as int? ?? 0;
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
                        //return Ok(responseDetails);
                        return new ServiceResponse<ResponseDetails>
                        {
                            Data = responseDetails
                        };
                }
                }
                catch (SqlException sqlEx)
                {
                    responseDetails.success = false;
                    responseDetails.message = $"SQL error: {sqlEx.Message}";
                    responseDetails.status = "400";
                    //return Ok(responseDetails);
                    return new ServiceResponse<ResponseDetails>
                    {
                        Data = responseDetails
                    };
            }
            }
        }
    }
