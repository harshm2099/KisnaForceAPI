using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Core.Application.DTOs;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using System.Web.Http;

namespace NewAvatarWebApis.Infrastructure.Services
{
    public class ScreenSaverService : IScreenSaverService
    {
        public string _Connection = DBCommands.CONNECTION_STRING;

        public async Task<ScreenSaverVideoResponse> ScreenSaverVideoList(ScreenSaverVideoParams screensaveview_params)
        {
            var response = new ScreenSaverVideoResponse();
            try
            {
                response = new ScreenSaverVideoResponse
                {
                    success = true,
                    message = "Successfully",
                    status = "enable",
                    minute = "1",
                    data = new List<ScreenSaverVideo>
                    {
                        new ScreenSaverVideo
                        {
                            ScreenId = "122",
                            Name = "10-04-2024-Kisna Reel_04_03",
                            Selection = "2621",
                            ImagePath = "https://assets.kisna.com/public/uploads/ScreenSaver/ExPmX2TsGf_1744779702.mp4",
                            Remarks = "Shop In Shop",
                            ScreenType = "All",
                            FileType = "Video",
                            Screencounter = null,
                            select_status = "SELECTED"
                        },
                        new ScreenSaverVideo
                        {
                            ScreenId = "124",
                            Name = "12-04-Kisna Reel_07_03",
                            Selection = "2621",
                            ImagePath = "https://assets.kisna.com/public/uploads/ScreenSaver/bGrQOlkIEH_1744779770.mp4",
                            Remarks = "Shop In Shop",
                            ScreenType = "All",
                            FileType = "Video",
                            Screencounter = null,
                            select_status = "SELECTED"
                        },
                        new ScreenSaverVideo
                        {
                            ScreenId = "125",
                            Name = "11-04-Kisna Reel_01_03",
                            Selection = "2621",
                            ImagePath = "https://assets.kisna.com/public/uploads/ScreenSaver/inEG2C0VOd_1744782669.mp4",
                            Remarks = "Shop in Shop",
                            ScreenType = "All",
                            FileType = "Video",
                            Screencounter = null,
                            select_status = "SELECTED"
                        }
                    }
                };

                return response;
            }
            catch (SqlException ex)
            {
                response.success = false;
                response.message = $"SQL error: {ex.Message}";
                response.data = new List<ScreenSaverVideo>();
                return response;
            }
        }
    }
}
