using Microsoft.Data.SqlClient;
using Microsoft.Exchange.WebServices.Data;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using System.Data;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class TeamsService : ITeamsService
    {
        public string _connection = DBCommands.CONNECTION_STRING;
        //string teamsPath = Convert.ToString(ConfigurationManager.AppSettings["uploadteamsimages"]);

        private readonly IWebHostEnvironment _webHostEnvironment;

        public TeamsService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public ServiceResponse<IList<MasterTeams>> GetAllTeams(string dataid)
        {
            IList<MasterTeams> teams = new List<MasterTeams>();
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CRUD_TEAM;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", dataid);
                        SqlParameter TeamIdCreated = new SqlParameter("@TEAMIDCREATED", SqlDbType.BigInt);
                        TeamIdCreated.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(TeamIdCreated);
                        using (SqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    try
                                    {
                                        teams.Add(new MasterTeams
                                        {
                                            dataid = Convert.ToString(dataReader["dataid"]),
                                            dataname = Convert.ToString(dataReader["dataname"]),
                                            dob = Convert.ToString(dataReader["DataDOB"]),
                                            isagreement = Convert.ToString(dataReader["isAgreement"]),
                                            doa = Convert.ToString(dataReader["DataDOA"]),
                                            profileimage = Convert.ToString(dataReader["ImgActualName"]),
                                            profileimagepath = Convert.ToString(dataReader["ImgPath"]),
                                            desktopimage = Convert.ToString(dataReader["DataPlaceImgPath"]),
                                            title = Convert.ToString(dataReader["DataContactTitle"]),
                                            contactname = Convert.ToString(dataReader["DataContactName"]),
                                            contacttitle = Convert.ToString(dataReader["Datacontacttitle"]),
                                            telephoneno = Convert.ToString(dataReader["DataTelNo"]),
                                            email = Convert.ToString(dataReader["DataEmail"]),
                                            gstno = Convert.ToString(dataReader["DataGSTNo"]),
                                            alternatetitle = Convert.ToString(dataReader["DataAltContactTitle"]),
                                            alternatename = Convert.ToString(dataReader["DataAltContactName"]),
                                            alternateemail = Convert.ToString(dataReader["DataAltContactEmail"]),
                                            code = Convert.ToString(dataReader["DataCd"]),
                                            pincode = Convert.ToString(dataReader["DataAddrPinCode"]),
                                            altercontactno = Convert.ToString(dataReader["DataAltContactNo"]),
                                            alternatecontactname = Convert.ToString(dataReader["DataAltContactName"]),
                                            address1 = Convert.ToString(dataReader["DataAddr1"]),
                                            address2 = Convert.ToString(dataReader["DataAddr2"]),
                                            cityid = Convert.ToString(dataReader["DataAddrCityID"]),
                                            stateid = Convert.ToString(dataReader["DataAddrState"]),
                                            territoryid = Convert.ToString(dataReader["DataTerritoryID"]),
                                            zoneid = Convert.ToString(dataReader["DataZoneId"]),
                                            areaid = Convert.ToString(dataReader["DataAreaID"]),
                                            devicesecuritystatus = Convert.ToString(dataReader["DataDeviceSecurityStatus"]),
                                            referencename = Convert.ToString(dataReader["Reference"]),
                                            referenceid = Convert.ToString(dataReader["ReferenceDataID"]),
                                            isstoreperson = Convert.ToString(dataReader["IsStorePerson"]),
                                            outletcategory = Convert.ToString(dataReader["OutletCommonID"]),
                                            producttype = Convert.ToString(dataReader["ProductTypeCommonID"]),
                                            kyc = Convert.ToString(dataReader["IsKyc"]),
                                            remarks = Convert.ToString(dataReader["DataRemarks"]),
                                            defaultlanguage = Convert.ToString(dataReader["DataLanguegeCommonID"]),
                                            community = Convert.ToString(dataReader["DataCommunityCommonID"]),
                                            part = Convert.ToString(dataReader["DataPartCommonID"]),
                                            emrcustomercode = Convert.ToString(dataReader["DataEmrCustCd"]),
                                            joindate = Convert.ToString(dataReader["DataDOJ"]),
                                            ImageId = Convert.ToString(dataReader["ImgId"]),
                                            UpdatedBy = Convert.ToString(dataReader["UpdatedBy"]),
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
                if (teams.Count > 0)
                {
                    return new ServiceResponse<IList<MasterTeams>>
                    {
                        Success = true,
                        Data = teams
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
                return new ServiceResponse<IList<MasterTeams>>
                {
                    Success = false,
                    Message = sqlEx.Message
                };
            }
        }
  
        public ServiceResponse<bool> AddItems(MasterTeams team)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    // No need for "~" in the service
                    string virtualPathELog = "/UploadFilesTeams/errorlog.txt";
                    string physicalPathELog = Path.Combine(_webHostEnvironment.ContentRootPath, virtualPathELog);

                    string cmdQuery = DBCommands.CRUD_TEAM;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@dataname", team.dataname);
                        cmd.Parameters.AddWithValue("@dob", team.dob);
                        cmd.Parameters.AddWithValue("@isagreement", team.isagreement);
                        cmd.Parameters.AddWithValue("@doa", team.doa);
                        cmd.Parameters.AddWithValue("@profileimage", team.profileimage);
                        cmd.Parameters.AddWithValue("@desktopimage", team.desktopimage);
                        cmd.Parameters.AddWithValue("@title", team.title);
                        cmd.Parameters.AddWithValue("@contactname", team.contactname);
                        cmd.Parameters.AddWithValue("@contacttitle", team.contacttitle);
                        cmd.Parameters.AddWithValue("@telephoneno", team.telephoneno);
                        cmd.Parameters.AddWithValue("@email", team.email);
                        cmd.Parameters.AddWithValue("@gstno", team.gstno);
                        cmd.Parameters.AddWithValue("@alternatetitle", team.alternatetitle);
                        cmd.Parameters.AddWithValue("@alternatename", team.alternatename);
                        cmd.Parameters.AddWithValue("@alternateemail", team.alternateemail);
                        cmd.Parameters.AddWithValue("@code", team.code);
                        cmd.Parameters.AddWithValue("@pincode", team.pincode);
                        cmd.Parameters.AddWithValue("@altercontactno", team.altercontactno);
                        cmd.Parameters.AddWithValue("@alternatecontactname", team.alternatecontactname);
                        cmd.Parameters.AddWithValue("@address1", team.address1);
                        cmd.Parameters.AddWithValue("@address2", team.address2);
                        cmd.Parameters.AddWithValue("@cityid", team.cityid);
                        cmd.Parameters.AddWithValue("@stateid", team.stateid);
                        cmd.Parameters.AddWithValue("@territoryid", team.territoryid);
                        cmd.Parameters.AddWithValue("@zoneid", team.zoneid);
                        cmd.Parameters.AddWithValue("@areaid", team.areaid);
                        cmd.Parameters.AddWithValue("@devicesecuritystatus", team.devicesecuritystatus);
                        cmd.Parameters.AddWithValue("@referenceid", team.referenceid);
                        cmd.Parameters.AddWithValue("@referencename", team.referencename);
                        cmd.Parameters.AddWithValue("@isstoreperson", team.isstoreperson);
                        cmd.Parameters.AddWithValue("@outletcategory", team.outletcategory);
                        cmd.Parameters.AddWithValue("@producttype", team.producttype);
                        cmd.Parameters.AddWithValue("@kyc", team.kyc);
                        cmd.Parameters.AddWithValue("@remarks", team.remarks);
                        cmd.Parameters.AddWithValue("@defaultlanguage", team.defaultlanguage);
                        cmd.Parameters.AddWithValue("@community", team.community);
                        cmd.Parameters.AddWithValue("@part", team.part);
                        cmd.Parameters.AddWithValue("@emrcustomercode", team.emrcustomercode);
                        cmd.Parameters.AddWithValue("@anniversary", team.anniversary);
                        cmd.Parameters.AddWithValue("@joindate", team.joindate);
                        cmd.Parameters.AddWithValue("@InsertedBy", team.InsertedBy);
                        cmd.Parameters.AddWithValue("@Flag", 1);
                        SqlParameter TeamIdCreated = new SqlParameter("@TEAMIDCREATED", SqlDbType.BigInt);
                        TeamIdCreated.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(TeamIdCreated);

                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                        {
                            try
                            {
                                Int64 ImageId = Convert.ToInt64(TeamIdCreated.Value);

                                string extention = string.Empty;
                                if (!string.IsNullOrEmpty(team.profileimage) && team.profileimage.Contains("."))
                                {
                                    extention = team.profileimage.Split('.')[1];
                                    // No need for "~" in the service
                                    string virtualPath = "/UploadFilesTeams/" + ImageId + "." + extention;
                                    // 'HttpContext' does not contain a definition for 'Current' in newer version of .net
                                    //string physicalPath = HttpContext.Current.Server.MapPath(virtualPath);
                                    string physicalPath = Path.Combine(_webHostEnvironment.ContentRootPath, virtualPath);
                                    System.IO.File.WriteAllBytes(physicalPath, team.profileimagearray);
                                }

                                extention = string.Empty;
                                if (!string.IsNullOrEmpty(team.desktopimage) && team.desktopimage.Contains("."))
                                {
                                    extention = team.desktopimage.Split('.')[1];
                                    // No need for "~" in the service
                                    string virtualPathDesktop = "/UploadFilesTeams/Desk_" + ImageId + "." + extention;
                                    string physicalPathDesktop = Path.Combine(_webHostEnvironment.ContentRootPath, virtualPathDesktop);
                                    if (team.desktopimagearray != null && team.desktopimagearray.Length > 0)
                                        System.IO.File.WriteAllBytes(physicalPathDesktop, team.desktopimagearray);
                                }

                            }
                            catch (Exception ex)
                            {
                                File.AppendAllText(physicalPathELog, Environment.NewLine + "Exception : For profile Image " + team.profileimage + " : " + ex.Message);
                            }
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

        public ServiceResponse<bool> EditItems(MasterTeams team)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CRUD_TEAM;
                    dbConnection.Open();

                    // No need for "~" in the service
                    string virtualPathELog = "/UploadFilesTeams/errorlog.txt";
                    string physicalPathELog = Path.Combine(_webHostEnvironment.ContentRootPath, virtualPathELog);

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@DataId", team.dataid);
                        cmd.Parameters.AddWithValue("@dataname", team.dataname);
                        cmd.Parameters.AddWithValue("@dob", team.dob);
                        cmd.Parameters.AddWithValue("@isagreement", team.isagreement);
                        cmd.Parameters.AddWithValue("@doa", team.doa);
                        cmd.Parameters.AddWithValue("@profileimage", team.profileimage);
                        cmd.Parameters.AddWithValue("@desktopimage", team.desktopimage);
                        cmd.Parameters.AddWithValue("@title", team.title);
                        cmd.Parameters.AddWithValue("@contactname", team.contactname);
                        cmd.Parameters.AddWithValue("@contacttitle", team.contacttitle);
                        cmd.Parameters.AddWithValue("@telephoneno", team.telephoneno);
                        cmd.Parameters.AddWithValue("@email", team.email);
                        cmd.Parameters.AddWithValue("@gstno", team.gstno);
                        cmd.Parameters.AddWithValue("@alternatetitle", team.alternatetitle);
                        cmd.Parameters.AddWithValue("@alternatename", team.alternatename);
                        cmd.Parameters.AddWithValue("@alternateemail", team.alternateemail);
                        cmd.Parameters.AddWithValue("@code", team.code);
                        cmd.Parameters.AddWithValue("@altercontactno", team.altercontactno);
                        cmd.Parameters.AddWithValue("@alternatecontactname", team.alternatecontactname);
                        cmd.Parameters.AddWithValue("@address1", team.address1);
                        cmd.Parameters.AddWithValue("@address2", team.address2);
                        cmd.Parameters.AddWithValue("@cityid", team.cityid);
                        cmd.Parameters.AddWithValue("@stateid", team.stateid);
                        cmd.Parameters.AddWithValue("@territoryid", team.territoryid);
                        cmd.Parameters.AddWithValue("@zoneid", team.zoneid);
                        cmd.Parameters.AddWithValue("@areaid", team.areaid);
                        cmd.Parameters.AddWithValue("@devicesecuritystatus", team.devicesecuritystatus);
                        cmd.Parameters.AddWithValue("@referenceid", team.referenceid);
                        cmd.Parameters.AddWithValue("@isstoreperson", team.isstoreperson);
                        cmd.Parameters.AddWithValue("@outletcategory", team.outletcategory);
                        cmd.Parameters.AddWithValue("@producttype", team.producttype);
                        cmd.Parameters.AddWithValue("@kyc", team.kyc);
                        cmd.Parameters.AddWithValue("@remarks", team.remarks);
                        cmd.Parameters.AddWithValue("@defaultlanguage", team.defaultlanguage);
                        cmd.Parameters.AddWithValue("@community", team.community);
                        cmd.Parameters.AddWithValue("@part", team.part);
                        cmd.Parameters.AddWithValue("@emrcustomercode", team.emrcustomercode);
                        cmd.Parameters.AddWithValue("@anniversary", team.anniversary);
                        cmd.Parameters.AddWithValue("@joindate", team.joindate);
                        cmd.Parameters.AddWithValue("@imageidupdate", team.ImageId);
                        cmd.Parameters.AddWithValue("@updatedby", team.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 2);
                        SqlParameter TeamIdCreated = new SqlParameter("@TEAMIDCREATED", SqlDbType.BigInt);
                        TeamIdCreated.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(TeamIdCreated);

                        int id = cmd.ExecuteNonQuery();
                        if (id >= 1)
                        {
                            try
                            {
                                Int64 ImageId = Convert.ToInt64(TeamIdCreated.Value);

                                string extention = string.Empty;
                                if (!string.IsNullOrEmpty(team.profileimage) && team.profileimage.Contains("."))
                                {
                                    extention = team.profileimage.Split('.')[1];
                                    // No need for "~" in the service
                                    string virtualPath = "/UploadFilesTeams/" + ImageId + "." + extention;
                                    string physicalPath = Path.Combine(_webHostEnvironment.ContentRootPath, virtualPath);
                                    System.IO.File.WriteAllBytes(physicalPath, team.profileimagearray);
                                }

                                extention = string.Empty;
                                if (!string.IsNullOrEmpty(team.desktopimage) && team.desktopimage.Contains("."))
                                {
                                    extention = team.desktopimage.Split('.')[1];
                                    // No need for "~" in the service
                                    string virtualPathDesktop = "/UploadFilesTeams/Desk_" + ImageId + "." + extention;
                                    string physicalPathDesktop = Path.Combine(_webHostEnvironment.ContentRootPath, virtualPathDesktop);
                                    if (team.desktopimagearray != null && team.desktopimagearray.Length > 0)
                                        System.IO.File.WriteAllBytes(physicalPathDesktop, team.desktopimagearray);
                                }
                            }
                            catch (Exception ex)
                            {
                                File.AppendAllText(physicalPathELog, Environment.NewLine + "Exception : For profile Image " + team.profileimage + " : " + ex.Message);
                            }
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

        public ServiceResponse<bool> DisableTeams(MasterTeams team)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_connection))
                {
                    string cmdQuery = DBCommands.CRUD_TEAM;
                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UpdatedBy", team.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Flag", 3);
                        cmd.Parameters.AddWithValue("@DataId", team.dataid);
                        SqlParameter TeamIdCreated = new SqlParameter("@TEAMIDCREATED", SqlDbType.BigInt);
                        TeamIdCreated.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(TeamIdCreated);
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
