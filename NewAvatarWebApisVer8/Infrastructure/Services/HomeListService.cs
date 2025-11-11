using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using System.Data;
using static NewAvatarWebApis.Core.Application.DTOs.HomeDetailList;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class HomeListService : IHomeListService
    {
        public string _connection = DBCommands.CONNECTION_STRING;
        public async Task<ResponseDetails> HomeScreenMaster(HomeScreenMasterParams homescreenmasterparams)
        {
            var response = new ResponseDetails();
            var homescreenmasterList = new List<HomeScreenMasterListing>();

            try
            {
                string loader_img = string.Empty;
                using (var dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    var cmdQuery = DBCommands.HOMESCREENMASTER;

                    using (var cmd = new SqlCommand(cmdQuery, dbConnection))
                    {
                        var objHelpers = new CommonHelpers();

                        int DataId = homescreenmasterparams.data_id > 0 ? homescreenmasterparams.data_id : 0;
                        string tmpType = string.IsNullOrWhiteSpace(homescreenmasterparams.type) ? "" : homescreenmasterparams.type;

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@DataID", DataId);
                        cmd.Parameters.AddWithValue("@Type", tmpType);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                loader_img = reader.GetSafeString("loader_img").ToString();

                                homescreenmasterList.Add(new HomeScreenMasterListing
                                {
                                    home_id = reader.GetSafeInt("home_id").ToString(),
                                    item_id = reader.GetSafeInt("item_id").ToString(),
                                    cat_menu_id = reader.GetSafeInt("cat_menu_id").ToString(),
                                    link_url = reader.GetSafeString("link_url"),
                                    HomeRef = reader.GetSafeString("HomeRef"),
                                    image_path = reader.GetSafeString("image_path"),
                                    home_mstname = reader.GetSafeString("home_mstname"),
                                    home_mstid = reader.GetSafeInt("home_mstid").ToString(),
                                    loader_img = loader_img,
                                });
                            }
                        }
                    }
                }

                response.success = homescreenmasterList.Any();
                response.message = homescreenmasterList.Any() ? "Successfully" : "No data found";
                response.status = "200";
                response.data = homescreenmasterList;
                response.loader_img = loader_img;
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

        public async Task<HomeDataListResponse> HomeList()
        {
            var response = new HomeDataListResponse();
            var homeDataList = new List<HomeDataItem>();

            try
            {
                string loader_img = "";
                using (var dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    using (var cmd = new SqlCommand(DBCommands.HOMEDATA_LIST, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                loader_img = reader.GetSafeString("loader_img");
                                homeDataList.Add(new HomeDataItem
                                {
                                    home_id = reader.GetSafeInt("home_id").ToString(),
                                    item_id = reader.GetSafeInt("item_id").ToString(),
                                    cat_menu_id = reader.GetSafeInt("cat_menu_id").ToString(),
                                    link_url = reader.GetSafeString("link_url"),
                                    HomeRef = reader.GetSafeString("HomeRef"),
                                    image_path = reader.GetSafeString("image_path"),
                                    home_mstname = reader.GetSafeString("home_mstname"),
                                    home_mstid = reader.GetSafeInt("home_mstid").ToString(),
                                    loader_img = loader_img
                                });
                            }
                        }
                    }
                }

                response = new HomeDataListResponse
                {
                    success = homeDataList.Any(),
                    message = homeDataList.Any() ? "Successfully" : "No data found",
                    loader_img = loader_img,
                    data = homeDataList
                };

                return response;
            }
            catch (SqlException ex)
            {
                response = new HomeDataListResponse
                {
                    success = false,
                    message = $"SQL error: {ex.Message}",
                    loader_img = "",
                    data = new List<HomeDataItem>()
                };
                return response;
            }
        }

        public async Task<HomeDataListResponse> HomeListNew(HomeListRequest param, [FromHeader]CommonHeader header)
        {
            var response = new HomeDataListResponse();
            var homeDataList = new List<HomeDataItem>();

            try
            {
                string loader_img = "";
                using (var dbConnection = new SqlConnection(_connection))
                {
                    dbConnection.Open();
                    using (var cmd = new SqlCommand(DBCommands.HOMELISTNEW, dbConnection))
                    {
                        string dataId = string.IsNullOrWhiteSpace(param.dataId) ? "" : param.dataId;
                        string dataLoginType = string.IsNullOrWhiteSpace(param.dataLoginType) ? "" : param.dataLoginType;

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 120;
                        cmd.Parameters.AddWithValue("@dataId", dataId);
                        cmd.Parameters.AddWithValue("@dataLoginType", dataLoginType);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                loader_img = reader.GetSafeString("loader_img");
                                homeDataList.Add(new HomeDataItem
                                {
                                    home_id = reader.GetSafeInt("home_id").ToString(),
                                    item_id = reader.GetSafeInt("item_id").ToString(),
                                    cat_menu_id = reader.GetSafeInt("cat_menu_id").ToString(),
                                    link_url = reader.GetSafeString("link_url"),
                                    HomeRef = reader.GetSafeString("HomeRef"),
                                    image_path = reader.GetSafeString("image_path"),
                                    home_mstname = reader.GetSafeString("home_mstname"),
                                    home_mstid = reader.GetSafeInt("home_mstid").ToString(),
                                    loader_img = loader_img
                                });
                            }
                        }
                    }
                }

                response = new HomeDataListResponse
                {
                    success = homeDataList.Any(),
                    message = homeDataList.Any() ? "Successfully" : "No data found",
                    loader_img = loader_img,
                    data = homeDataList
                };

                return response;
            }
            catch (SqlException ex)
            {
                response = new HomeDataListResponse
                {
                    success = false,
                    message = $"SQL error: {ex.Message}",
                    loader_img = "",
                    data = new List<HomeDataItem>()
                };
                return response;
            }
        }
    }
}
