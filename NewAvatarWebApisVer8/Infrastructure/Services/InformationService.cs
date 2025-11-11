using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class InformationService : IInformationService
    {
        public string _connection = DBCommands.CONNECTION_STRING;

        public ServiceResponse<IList<T_INFO_SRC_MST>> GetAllInformation()
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
                        Message = "Items retrieved successfully.",
                        Data = info
                    };
                }
                else
                {
                    return new ServiceResponse<IList<T_INFO_SRC_MST>>
                    {
                        Success = true,
                        Message = "No items found.",
                        Data = info // 'info' is the empty list in this case
                    };
                }
            }
            catch (SqlException sqlEx)
            {
                return new ServiceResponse<IList<T_INFO_SRC_MST>>
                {
                    Success = false,
                    Message = sqlEx.Message,
                    Data = null
                };
            }
        }

        public async Task<CommonResponse> AddInformation(T_INFO_SRC_MST info)
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
                        cmd.Parameters.AddWithValue("@InfoSrcCtg", info.InfoSrcCtg);
                        cmd.Parameters.AddWithValue("@InfoSrcTyp", info.InfoSrcTyp);
                        cmd.Parameters.AddWithValue("@InfoSrcFlag", info.InfoSrcFlag);
                        cmd.Parameters.AddWithValue("@InfoSrcDownloadSts", 'N');
                        cmd.Parameters.AddWithValue("@InfoTypeFlag", info.InfoTypeFlag);
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

        public async Task<CommonResponse> EditInformation(T_INFO_SRC_MST info)
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
                        cmd.Parameters.AddWithValue("@InfoSrcCtg", info.InfoSrcCtg);
                        cmd.Parameters.AddWithValue("@InfoSrcTyp", info.InfoSrcTyp);
                        cmd.Parameters.AddWithValue("@InfoSrcFlag", info.InfoSrcFlag);
                        cmd.Parameters.AddWithValue("@InfoSrcDownloadSts", info.InfoSrcDownloadSts);
                        cmd.Parameters.AddWithValue("@InfoTypeFlag", info.InfoTypeFlag);
                        cmd.Parameters.AddWithValue("@InfoSrcId", info.InfoSrcId);
                        cmd.Parameters.AddWithValue("@Flag", 2);

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

        public async Task<CommonResponse> DisableInformation(T_INFO_SRC_MST info)
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
    }
}
