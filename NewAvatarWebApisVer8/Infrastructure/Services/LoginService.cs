using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewAvatarWebApis.Common;
using NewAvatarWebApis.Core.Application.Common;
using NewAvatarWebApis.Core.Application.Responses;
using NewAvatarWebApis.Core.Domain.Common.NewAvatarWebApis.Models;
using NewAvatarWebApis.Infrastructure.Data;
using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using NewAvatarWebApis.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;
using System.Net.Mail;
using Twilio.TwiML.Voice;
namespace NewAvatarWebApis.Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly string _Connection = DBCommands.CONNECTION_STRING;
        private readonly TokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly IOtpService _otpService;

        public LoginService(TokenService tokenService, IConfiguration config, IOtpService otpService)
        {
            _tokenService = tokenService;
            _configuration = config;
            _otpService = otpService;
        }

        public async Task<CommonResponse> Login(LoginParams request, CommonHeader header)
        {
            {
                try
                {
                    using (SqlConnection dbConnection = new SqlConnection(_Connection))
                    {
                        string
                            resmessage = "";
                        int
                            resstatus = 0,
                            resstatuscode = 400;

                        string email = request.email ?? string.Empty;
                        string password = request.password?? string.Empty;
                        string device_type = header?.devicetype ?? string.Empty;
                        string device_serial_no = request.device_serial_no ?? string.Empty;
                        string app_version = header?.appversion ?? string.Empty;
                        string device_name = header?.devicename ?? string.Empty;
                        string net_ip = request.net_ip ?? string.Empty;
                        string firebase_token = request.firebase_token ?? string.Empty;
                        string data_pin = request.data_pin ?? string.Empty;
                        string device_token = request.device_token ?? string.Empty;
                        string org_type = header?.orgtype ?? string.Empty;

                        string cmdQuery = DBCommands.LOGIN_CHECK;
                        dbConnection.Open();

                        using (SqlCommand cmd = new SqlCommand(cmdQuery, dbConnection))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@email", email);

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

                                            // Maintenance Error
                                            if (resstatus != 1)
                                            {
                                                return new CommonResponse
                                                {
                                                    success = false,
                                                    message = resmessage,
                                                    status_code = resstatuscode.ToString(),
                                                    status = resstatuscode.ToString()
                                                };
                                            }

                                            //Get DataIds Based on Email
                                            List<long> dataUserIds = new List<long>();
                                            if (ds.Tables.Count > 1)
                                            {
                                                Console.WriteLine($"Number of rows in the second table: {ds.Tables[1].Rows}");
                                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                                {

                                                    var rowDetails = ds.Tables[1].Rows[i];
                                                    long userid = rowDetails["id"] as long? ?? 0;
                                                    string hashPassword = rowDetails["password"] as string ?? string.Empty;

                                                    if (BCrypt.Net.BCrypt.Verify(password, hashPassword))
                                                    {
                                                        if (userid > 0)
                                                        {
                                                            dataUserIds.Add(userid);
                                                        }
                                                    }
                                                }
                                            }
                                            if (dataUserIds.Any())
                                            {
                                                dbConnection.Close();
                                                string cmdQry = DBCommands.LOGIN_DATA;
                                                using (SqlCommand cmdData = new SqlCommand(cmdQry, dbConnection))
                                                {
                                                    string dataIds = string.Join(",", dataUserIds);

                                                    cmdData.CommandType = System.Data.CommandType.StoredProcedure;

                                                    cmdData.Parameters.AddWithValue("@dataid", dataUserIds[0]);
                                                    cmdData.Parameters.AddWithValue("@dataid_count", dataUserIds.Count);
                                                    cmdData.Parameters.AddWithValue("@dataids", dataIds);
                                                    cmdData.Parameters.AddWithValue("@password", password);
                                                    cmdData.Parameters.AddWithValue("@device_type", device_type);
                                                    cmdData.Parameters.AddWithValue("@device_serial_no", device_serial_no);
                                                    cmdData.Parameters.AddWithValue("@app_version", app_version);
                                                    cmdData.Parameters.AddWithValue("@device_name", device_name);
                                                    cmdData.Parameters.AddWithValue("@net_ip", net_ip);
                                                    cmdData.Parameters.AddWithValue("@firebase_token", firebase_token);
                                                    cmdData.Parameters.AddWithValue("@data_pin", data_pin);
                                                    cmdData.Parameters.AddWithValue("@device_token", device_token);

                                                    using (SqlDataAdapter daData = new SqlDataAdapter(cmdData))
                                                    {
                                                        DataSet dsData = new DataSet();
                                                        daData.Fill(dsData);
                                                        if (dsData.Tables.Count > 0)
                                                        {
                                                            if (dsData.Tables[0].Rows.Count > 0)
                                                            {
                                                                var rowLoginData = dsData.Tables[0].Rows[0];
                                                                resstatus = rowLoginData["resstatus"] as int? ?? 0;
                                                                resstatuscode = rowLoginData["resstatuscode"] as int? ?? 0;
                                                                resmessage = rowLoginData["resmessage"] as string ?? string.Empty;

                                                                if (resstatus != 1)
                                                                {
                                                                    return new CommonResponse
                                                                    {
                                                                        status = resstatuscode.ToString(),
                                                                        status_code = resstatuscode.ToString(),
                                                                        success = false,
                                                                        message = resmessage,
                                                                        error = "Unauthorised",
                                                                        holdmsg = "Your service is temporary disable, Please contact to back office !",
                                                                    };
                                                                }

                                                                string changePasswordRequired = rowLoginData["changePasswordRequired"] as string ?? string.Empty;
                                                                string changePinRequired = rowLoginData["changePinRequired"] as string ?? string.Empty;
                                                                string loader_img = rowLoginData["loader_img"] as string ?? string.Empty;
                                                                string holdmsg = rowLoginData["holdmsg"] as string ?? string.Empty;
                                                                string timeid = rowLoginData["timeid"] as string ?? string.Empty;
                                                                int istoken = rowLoginData["istoken"] as int? ?? 0;

                                                                string newToken = string.Empty;
                                                                if (istoken == 1)
                                                                {
                                                                    newToken = _tokenService.GenerateJwtToken(dataUserIds[0].ToString(), email);
                                                                }

                                                                var userDataJson = new List<Dictionary<string, object>>();
                                                                var audioDataJson = new List<Dictionary<string, object>>();

                                                                if (dsData.Tables.Count > 1 && dsData.Tables[1].Rows.Count > 0)
                                                                {
                                                                    var table = dsData.Tables[1];
                                                                    foreach (DataRow row in table.Rows)
                                                                    {
                                                                        var dict = new Dictionary<string, object>();
                                                                        foreach (DataColumn col in table.Columns)
                                                                            dict[col.ColumnName] = row[col];
                                                                        userDataJson.Add(dict);
                                                                    }
                                                                }

                                                                if (dsData.Tables.Count > 2 && dsData.Tables[2].Rows.Count > 0)
                                                                {
                                                                    var table = dsData.Tables[2];
                                                                    foreach (DataRow row in table.Rows)
                                                                    {
                                                                        var dict = new Dictionary<string, object>();
                                                                        foreach (DataColumn col in table.Columns)
                                                                            dict[col.ColumnName] = row[col];
                                                                        audioDataJson.Add(dict);
                                                                    }
                                                                }

                                                                return new CommonResponse
                                                                {
                                                                    status = resstatuscode.ToString(),
                                                                    status_code = resstatuscode.ToString(),
                                                                    success = resstatus == 1 ? true : false,
                                                                    message = resmessage,
                                                                    changePasswordRequired = changePasswordRequired,
                                                                    changePinRequired = changePinRequired,
                                                                    token = newToken,
                                                                    loader_img = loader_img,
                                                                    data = userDataJson,
                                                                    audiodata = audioDataJson,
                                                                    holdmsg = holdmsg,
                                                                    timeid = timeid,
                                                                };
                                                            }
                                                        }
                                                    }
                                                }

                                            }
                                            else
                                            {
                                                return new CommonResponse
                                                {
                                                    status = "401",
                                                    status_code = "401",
                                                    success = false,
                                                    message = "Email or password invalid.",
                                                    error = "Unauthorised",
                                                    holdmsg = "Your service is temporary disable, Please contact to back office !",
                                                };
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                            }

                        }
                        return new CommonResponse
                        {
                            status = "401",
                            status_code = "401",
                            success = false,
                            message = "Email or password invalid.",
                            error = "Unauthorised",
                            holdmsg = "Your service is temporary disable, Please contact to back office !",
                        };
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
                        error = "Unauthorised",
                        holdmsg = "Your service is temporary disable, Please contact to back office !",
                    };
                }

            }
        }

        public async Task<CommonResponse> SendOtp(SendOtpParams request, CommonHeader header)
        {
            try
            {
                string mobile_no = string.IsNullOrWhiteSpace(request.mobile_no) ? string.Empty : request.mobile_no;
                string net_ip = string.IsNullOrWhiteSpace(request.net_ip) ? string.Empty : request.net_ip;
                string device_type = string.IsNullOrWhiteSpace(header?.devicetype) ? string.Empty : header.devicetype;

                if (string.IsNullOrEmpty(request.mobile_no))
                {
                    return new CommonResponse
                    {
                        status = "201",
                        success = false,
                        message = $"mobile no is required.",
                    };
                }

                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    using (var cmd = new SqlCommand(DBCommands.SENDOTP, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MobileNo", mobile_no);
                        cmd.Parameters.AddWithValue("@NetIP", net_ip);
                        cmd.Parameters.AddWithValue("@DeviceType", device_type);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string resstatus = reader.GetSafeString("resstatus");
                                string resstatuscode = reader.GetSafeString("resstatuscode");
                                string resmessage = reader.GetSafeString("resmessage");

                                if (resstatus != "1")
                                {
                                    return new CommonResponse
                                    {
                                        status = resstatuscode,
                                        success = resstatus == "1" ? true : false,
                                        message = resmessage,
                                    };
                                }

                                string smsmsg = reader.GetSafeString("smsmsg");
                                string otp = reader.GetSafeString("otp");

                                bool isSent = await _otpService.SendOtpAsync(mobile_no, smsmsg);

                                return new CommonResponse
                                {
                                    status = resstatuscode,
                                    success = resstatus == "1" ? true : false,
                                    message = resmessage,
                                    smsmsg = smsmsg,
                                    OTP = otp,
                                };
                            }
                        }
                    }
                }

                return new CommonResponse
                {
                    status = "401",
                    success = false,
                    message = "Mobile no is required.",
                };
            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}",
                };
            }
        }

        //public async Task<CommonResponse> SendEmailOtp(SendEmailOtpParams request, CommonHeader header)
        //{
        //    try
        //    {
        //        string email = string.IsNullOrWhiteSpace(request.email) ? string.Empty : request.email;
        //        string netIp = string.IsNullOrWhiteSpace(request.net_ip) ? string.Empty : request.net_ip;
        //        string deviceToken = string.IsNullOrWhiteSpace(request.device_token) ? string.Empty : request.device_token;
        //        string firebaseToken = string.IsNullOrWhiteSpace(request.firebase_token) ? string.Empty : request.firebase_token;
        //        string deviceType = string.IsNullOrWhiteSpace(request.devicetype) ? string.Empty : request.devicetype;
        //        string hdeviceType = string.IsNullOrWhiteSpace(header.devicetype) ? string.Empty : header.devicetype;
        //        string deviceName = string.IsNullOrWhiteSpace(header.devicename) ? string.Empty : header.devicename;
        //        string appVersion = string.IsNullOrWhiteSpace(header.appversion) ? string.Empty : header.appversion;
        //        string orgType = string.IsNullOrWhiteSpace(header.orgtype) ? string.Empty : header.orgtype;

        //        if (string.IsNullOrEmpty(request.email))
        //        {
        //            return new CommonResponse
        //            {
        //                status = "201",
        //                success = false,
        //                message = $"Email is required.",
        //            };
        //        }

        //        using (SqlConnection dbConnection = new SqlConnection(_Connection))
        //        {
        //            dbConnection.Open();
        //            using (var cmd = new SqlCommand(DBCommands.SENDEMAILOTP, dbConnection))
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.Parameters.AddWithValue("@Email", email);
        //                cmd.Parameters.AddWithValue("@NetIp", netIp);
        //                cmd.Parameters.AddWithValue("@DeviceToken", deviceToken);
        //                cmd.Parameters.AddWithValue("@FirebaseToken", firebaseToken);
        //                cmd.Parameters.AddWithValue("@DeviceType", deviceType);
        //                cmd.Parameters.AddWithValue("@HDeviceType", hdeviceType);
        //                cmd.Parameters.AddWithValue("@DeviceName", deviceName);
        //                cmd.Parameters.AddWithValue("@AppVersion", appVersion);
        //                cmd.Parameters.AddWithValue("@OrgType", orgType);

        //                using (var reader = cmd.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        string resstatus = reader.GetSafeString("resstatus");
        //                        string resstatuscode = reader.GetSafeString("resstatuscode");
        //                        string resmessage = reader.GetSafeString("resmessage");

        //                        if (resstatus != "1")
        //                        {
        //                            return new CommonResponse
        //                            {
        //                                status = resstatuscode,
        //                                success = resstatus == "1" ? true : false,
        //                                message = resmessage,
        //                            };
        //                        }

        //                        string otp = reader.GetSafeString("otp");

        //                        return new CommonResponse
        //                        {
        //                            status = resstatuscode,
        //                            success = resstatus == "1" ? true : false,
        //                            message = resmessage,
        //                            OTP = otp,
        //                        };
        //                    }
        //                }
        //            }
        //        }

        //        return new CommonResponse
        //        {
        //            status = "401",
        //            success = false,
        //            message = "Email is required.",
        //        };
        //    }
        //    catch (SqlException sqlEx)
        //    {
        //        return new CommonResponse
        //        {
        //            status = "400",
        //            success = false,
        //            message = $"SQL error: {sqlEx.Message}",
        //        };
        //    }
        //}
        public async Task<CommonResponse> SendEmailOtp(SendEmailOtpParams request, CommonHeader header)
        {
            try
            {
                string email = string.IsNullOrWhiteSpace(request.email) ? string.Empty : request.email;
                string netIp = string.IsNullOrWhiteSpace(request.net_ip) ? string.Empty : request.net_ip;
                string deviceToken = string.IsNullOrWhiteSpace(request.device_token) ? string.Empty : request.device_token;
                string firebaseToken = string.IsNullOrWhiteSpace(request.firebase_token) ? string.Empty : request.firebase_token;
                string deviceType = string.IsNullOrWhiteSpace(request.devicetype) ? string.Empty : request.devicetype;
                string hdeviceType = string.IsNullOrWhiteSpace(header?.devicetype) ? string.Empty : header?.devicetype;
                string deviceName = string.IsNullOrWhiteSpace(header?.devicename) ? string.Empty : header?.devicename;
                string appVersion = string.IsNullOrWhiteSpace(header?.appversion) ? string.Empty : header?.appversion;
                string orgType = string.IsNullOrWhiteSpace(header?.orgtype) ? string.Empty : header?.orgtype;

                if (string.IsNullOrEmpty(email))
                {
                    return new CommonResponse
                    {
                        status = "201",
                        success = false,
                        message = "Email is required.",
                    };
                }

                string otp = string.Empty;
                string resstatus = string.Empty;
                string resstatuscode = string.Empty;
                string resmessage = string.Empty;

                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    await dbConnection.OpenAsync();

                    using (var cmd = new SqlCommand(DBCommands.SENDEMAILOTP, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@NetIp", netIp);
                        cmd.Parameters.AddWithValue("@DeviceToken", deviceToken);
                        cmd.Parameters.AddWithValue("@FirebaseToken", firebaseToken);
                        cmd.Parameters.AddWithValue("@DeviceType", deviceType);
                        cmd.Parameters.AddWithValue("@HDeviceType", hdeviceType);
                        cmd.Parameters.AddWithValue("@DeviceName", deviceName);
                        cmd.Parameters.AddWithValue("@AppVersion", appVersion);
                        cmd.Parameters.AddWithValue("@OrgType", orgType);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                resstatus = reader.GetSafeString("resstatus");
                                resstatuscode = reader.GetSafeString("resstatuscode");
                                resmessage = reader.GetSafeString("resmessage");

                                if (resstatus != "1")
                                {
                                    return new CommonResponse
                                    {
                                        status = resstatuscode,
                                        success = false,
                                        message = resmessage
                                    };
                                }

                                otp = reader.GetSafeString("otp");
                            }
                        }
                    }
                }

                // Send OTP email via Mailtrap
                bool emailSent = await SendOtpEmailWithMailtrapAsync(email, otp);
                if (!emailSent)
                {
                    return new CommonResponse
                    {
                        status = "500",
                        success = false,
                        message = "Failed to send OTP email. Please try again."
                    };
                }

                return new CommonResponse
                {
                    status = resstatuscode,
                    success = resstatus == "1",
                    message = $"OTP sent successfully to {email}.",
                    OTP = otp
                };
            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}"
                };
            }
            catch (Exception ex)
            {
                return new CommonResponse
                {
                    status = "500",
                    success = false,
                    message = $"Unexpected error: {ex.Message}"
                };
            }
        }

        // Mailtrap email sender
        private async Task<bool> SendOtpEmailWithMailtrapAsync(string toEmail, string otp)
        {
            try
            {
                // Looking to send emails in production? Check out our Email API/SMTP product!
                using (var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525))
                {
                    client.Credentials = new NetworkCredential("d0915810d0b717", "62b1e1959aa61e");
                    client.EnableSsl = true;

                    var mail = new MailMessage
                    {
                        From = new MailAddress("from@example.com", "MyApp Support"),
                        Subject = "Your OTP Code",
                        Body = $"Hello,\n\nYour OTP code is: {otp}\n\nThis code is valid for 10 minutes.\n\nThanks,\nMyApp Team",
                        IsBodyHtml = false
                    };
                    mail.To.Add(toEmail);

                    await client.SendMailAsync(mail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mailtrap email error: {ex.Message}");
                return false;
            }
        }

        public async Task<CommonResponse> sendOtpme(SendOtpmeParams request)
        {
            try
            {
                string email = string.IsNullOrWhiteSpace(request.email) ? string.Empty : request.email;
                string mobile_no = string.IsNullOrWhiteSpace(request.mobile_no) ? string.Empty : request.mobile_no;
                string devicetype = string.IsNullOrWhiteSpace(request.device_type) ? string.Empty : request.device_type;

                if (string.IsNullOrEmpty(request.email) && string.IsNullOrEmpty(request.mobile_no))
                {
                    return new CommonResponse
                    {
                        status = "201",
                        success = false,
                        message = $"Please provide either mobile number or email.",
                    };
                }

                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    dbConnection.Open();
                    using (var cmd = new SqlCommand(DBCommands.SENDOTPME, dbConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MobileNo", mobile_no);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@DeviceType", devicetype);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string resstatus = reader.GetSafeString("resstatus");
                                string resstatuscode = reader.GetSafeString("resstatuscode");
                                string resmessage = reader.GetSafeString("resmessage");

                                if (resstatus != "1")
                                {
                                    return new CommonResponse
                                    {
                                        status = resstatuscode,
                                        success = resstatus == "1" ? true : false,
                                        message = resmessage,
                                    };
                                }

                                string otp = reader.GetSafeString("otp");

                                if (mobile_no.Length > 0)
                                {
                                    string smsmsg = reader.GetSafeString("smsmsg");

                                    return new CommonResponse
                                    {
                                        status = resstatuscode,
                                        success = resstatus == "1" ? true : false,
                                        message = resmessage,
                                        OTP = otp,
                                        smsmsg = smsmsg,
                                    };
                                }
                                else
                                {
                                    string emailmsg = reader.GetSafeString("emailmsg");
                                    string emailsubject = reader.GetSafeString("emailsubject");

                                    return new CommonResponse
                                    {
                                        status = resstatuscode,
                                        success = resstatus == "1" ? true : false,
                                        message = resmessage,
                                        OTP = otp,
                                        emailmsg = emailmsg,
                                        emailsubject = emailsubject,
                                    };
                                }

                            }
                        }
                    }
                }

                return new CommonResponse
                {
                    status = "401",
                    success = false,
                    message = "Mobile no is required.",
                };
            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}",
                };
            }
        }

        public async Task<CommonResponse> VerifyEmail(VerifyEmailParams request)
        {
            try
            {
                CommonHelpers objHelpers = new CommonHelpers();

                if (!string.IsNullOrEmpty(request.email_mobile) &&
                            !objHelpers.IsValidEmail(request.email_mobile) &&
                            !long.TryParse(request.email_mobile, out _))
                {
                    return new CommonResponse
                    {
                        status = "201",
                        status_code = "201",
                        success = false,
                        message = $"Email must be a valid email or Mobile must be a numeric value.",
                        error = "Unauthorised",
                    };
                }

                string email_mobile = string.IsNullOrWhiteSpace(request.email_mobile) ? string.Empty : request.email_mobile;

                int flagEmailMobile = 0;

                if (objHelpers.IsValidEmail(request.email_mobile))
                {
                    // Login via email
                    flagEmailMobile = 1;
                }
                else
                {
                    // Login via mobile number
                    flagEmailMobile = 2;
                }

                using (var conn = new SqlConnection(_Connection))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(DBCommands.VERIFY_EMAIL_MOBILE, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailMobile", email_mobile);
                        cmd.Parameters.AddWithValue("@Flag", flagEmailMobile);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string resstatus = reader.GetSafeString("resstatus");
                                string resstatuscode = reader.GetSafeString("resstatuscode");
                                string resmessage = reader.GetSafeString("resmessage");

                                return new CommonResponse
                                {
                                    status = resstatuscode,
                                    success = resstatus == "1" ? true : false,
                                    message = resmessage,
                                };
                            }
                        }
                    }
                }

                return new CommonResponse
                {
                    status = "400",
                    success = false,
                    message = "Your account does not exist or is inactive.",
                };
            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    status_code = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}",
                    error = "Unauthorised",
                };
            }
        }

        public async Task<CommonResponse> LoginWithMobileOTP(LoginParamsWithMobileOtp login_params, CommonHeader header)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    string
                        resmessage = "",
                        resstatus = "0",
                        resstatuscode = "400";

                    string contactNo = login_params.contactNo as string ?? string.Empty;
                    string otp = login_params.otp as string ?? string.Empty;
                    string device_type = header?.devicetype as string ?? string.Empty;
                    string device_serial_no = login_params.device_serial_no as string ?? string.Empty;
                    string app_version = header?.appversion as string ?? string.Empty;
                    string device_name = header?.devicename as string ?? string.Empty;
                    string net_ip = login_params.net_ip as string ?? string.Empty;
                    string firebase_token = login_params.firebase_token as string ?? string.Empty;
                    string data_pin = login_params.data_pin as string ?? string.Empty;
                    string device_token = login_params.device_token as string ?? string.Empty;
                    string org_type = header?.orgtype as string ?? string.Empty;

                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(DBCommands.LOGIN_WITHMOBILEOTP, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ContactNo", contactNo);
                        cmd.Parameters.AddWithValue("@OTP", otp);
                        cmd.Parameters.AddWithValue("@DeviceType", device_type);
                        cmd.Parameters.AddWithValue("@NetIP", net_ip);
                        cmd.Parameters.AddWithValue("@DeviceSerialNo", device_serial_no);
                        cmd.Parameters.AddWithValue("@DeviceToken", device_token);
                        cmd.Parameters.AddWithValue("@FirebaseToken", firebase_token);
                        cmd.Parameters.AddWithValue("@DataPin", data_pin);
                        cmd.Parameters.AddWithValue("@ApVersion", app_version);
                        cmd.Parameters.AddWithValue("@DeviceName", device_name);

                        using (SqlDataAdapter daData = new SqlDataAdapter(cmd))
                        {
                            DataSet dsData = new DataSet();
                            daData.Fill(dsData);

                            if (dsData.Tables.Count > 0)
                            {
                                if (dsData.Tables[0].Rows.Count > 0)
                                {
                                    var rowLoginData = dsData.Tables[0].Rows[0];
                                    //var rawScreenShotStatus = rowLoginData["screen_short_status"];

                                    //bool isScreenShotStatus = rawScreenShotStatus != DBNull.Value;

                                    //rowLoginData["screen_short_status"] = isScreenShotStatus;
                                    resstatus = rowLoginData.GetSafeString("resstatus");
                                    resstatuscode = rowLoginData.GetSafeString("resstatuscode");
                                    resmessage = rowLoginData.GetSafeString("resmessage");

                                    if (resstatus != "1")
                                    {
                                        return new CommonResponse
                                        {
                                            status = resstatuscode,
                                            status_code = resstatuscode,
                                            success = false,
                                            message = resmessage,
                                            error = "Unauthorised",
                                            holdmsg = "Your service is temporary disable, Please contact to back office !",
                                        };
                                    }

                                    int IsMultiLogin = rowLoginData.GetSafeInt("IsMultiLogin");
                                    string holdmsg = rowLoginData["holdmsg"] as string ?? string.Empty;
                                    string loader_img = dsData.Tables[1].Rows[0].GetSafeString("loader_img"); ;

                                    // Multi-Login
                                    if (IsMultiLogin == 1)
                                    {
                                        var multiUserDataJson = new List<Dictionary<string, object>>();

                                        if (dsData.Tables.Count > 1 && dsData.Tables[1].Rows.Count > 0)
                                        {
                                            var table = dsData.Tables[1];
                                            foreach (DataRow row in table.Rows)
                                            {
                                                var dict = new Dictionary<string, object>();
                                                foreach (DataColumn col in table.Columns)
                                                    dict[col.ColumnName] = row[col];
                                                multiUserDataJson.Add(dict);
                                            }
                                        }
             
                                        return new CommonResponse
                                        {
                                            status = resstatus,
                                            status_code = resstatus,
                                            success = resstatus == "1" ? true : false,
                                            message = resmessage,
                                            loader_img = loader_img,
                                            data = multiUserDataJson,
                                            holdmsg = holdmsg,
                                        };
                                    }

                                    // Single Login
                                    string timeid = rowLoginData.GetSafeString("timeid");
                                    int istoken = rowLoginData.GetSafeInt("istoken");

                                    string newToken = string.Empty;
                                    if (istoken == 1)
                                    {
                                        int data_id = dsData.Tables[1].Rows[0].GetSafeInt("data_id");
                                        newToken = _tokenService.GenerateJwtToken(data_id.ToString(), contactNo);
                                        System.Diagnostics.Debug.WriteLine("Login Token: " + newToken);
                                    }

                                    var userDataJson = new List<Dictionary<string, object>>();

                                    if (dsData.Tables.Count > 1 && dsData.Tables[1].Rows.Count > 0)
                                    {
                                        var table = dsData.Tables[1];
                                        foreach (DataRow row in table.Rows)
                                        {
                                            var dict = new Dictionary<string, object>();
                                            foreach (DataColumn col in table.Columns)
                                                dict[col.ColumnName] = row[col];
                                            userDataJson.Add(dict);
                                        }
                                    }

                                    return new CommonResponse
                                    {
                                        status = resstatuscode,
                                        status_code = resstatuscode,
                                        success = resstatus == "1" ? true : false,
                                        message = resmessage,
                                        token = newToken,
                                        loader_img = loader_img,
                                        data = userDataJson,
                                        holdmsg = holdmsg,
                                        timeid = timeid,
                                    };
                                }
                            }
                        }
                    }
                    return new CommonResponse
                    {
                        status = "401",
                        status_code = "401",
                        success = false,
                        message = "OTP is wrong please try again.",
                        error = "Unauthorised",
                        holdmsg = "Your service is temporary disable, Please contact to back office !",
                    };
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
                    error = "Unauthorised",
                    holdmsg = "Your service is temporary disable, Please contact to back office !",
                };
            }
        }

        public async Task<CommonResponse> LoginNewEMob(LoginNewEMobParams request)
        {
            try
            {
                CommonHelpers objHelpers = new CommonHelpers();
                if (string.IsNullOrWhiteSpace(request.password))
                {
                    return new CommonResponse
                    {
                        status = "201",
                        status_code = "201",
                        success = false,
                        message = $"Password is required.",
                        error = "Unauthorised",
                    };
                }

                if (!string.IsNullOrEmpty(request.email_mobile) &&
                            !objHelpers.IsValidEmail(request.email_mobile) &&
                            !long.TryParse(request.email_mobile, out _))
                {
                    return new CommonResponse
                    {
                        status = "201",
                        status_code = "201",
                        success = false,
                        message = $"Email must be a valid email or Mobile must be a numeric value.",
                        error = "Unauthorised",
                    };
                }

                string email_mobile = string.IsNullOrWhiteSpace(request.email_mobile) ? string.Empty : request.email_mobile;
                string password = string.IsNullOrWhiteSpace(request.password) ? string.Empty : request.password;
                string device_type = string.IsNullOrWhiteSpace(request.device_type) ? string.Empty : request.device_type;
                string device_serial_no = string.IsNullOrWhiteSpace(request.device_serial_no) ? string.Empty : request.device_serial_no;
                string app_version = string.IsNullOrWhiteSpace(request.app_version) ? string.Empty : request.app_version;
                string device_name = string.IsNullOrWhiteSpace(request.device_name) ? string.Empty : request.device_name;
                string net_ip = string.IsNullOrWhiteSpace(request.net_ip) ? string.Empty : request.net_ip;
                string firebase_token = string.IsNullOrWhiteSpace(request.firebase_token) ? string.Empty : request.firebase_token;
                string data_pin = string.IsNullOrWhiteSpace(request.data_pin) ? string.Empty : request.data_pin;
                string device_token = string.IsNullOrWhiteSpace(request.device_token) ? string.Empty : request.device_token;

                var matchingUserIds = new List<int>();

                using (var conn = new SqlConnection(_Connection))
                {
                    conn.Open();
                    int flagEmailMobile = 0;

                    if (objHelpers.IsValidEmail(request.email_mobile))
                    {
                        // Login via email
                        flagEmailMobile = 1;
                    }
                    else
                    {
                        // Login via mobile number
                        flagEmailMobile = 2;
                    }

                    using (var cmd = new SqlCommand(DBCommands.GETUSERBY_EMAIL_MOBILE, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmailMobile", email_mobile);
                        cmd.Parameters.AddWithValue("@Flag", flagEmailMobile);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string hashedPassword = reader.GetSafeString("password");
                                if (BCrypt.Net.BCrypt.Verify(request.password, hashedPassword))
                                {
                                    matchingUserIds.Add(reader.GetSafeInt("id"));
                                }
                            }
                        }
                    }
                }

                if (matchingUserIds.Any())
                {
                    using (var conn = new SqlConnection(_Connection))
                    {
                        string
                            resmessage = "",
                            resstatus = "0",
                            resstatuscode = "400";

                        using (SqlCommand cmdData = new SqlCommand(DBCommands.LOGIN_WITHEMAILMOBILE, conn))
                        {
                            string dataIds = string.Join(",", matchingUserIds);

                            cmdData.CommandType = System.Data.CommandType.StoredProcedure;

                            cmdData.Parameters.AddWithValue("@dataid", matchingUserIds[0]);
                            cmdData.Parameters.AddWithValue("@dataid_count", matchingUserIds.Count);
                            cmdData.Parameters.AddWithValue("@dataids", dataIds);
                            cmdData.Parameters.AddWithValue("@password", password);
                            cmdData.Parameters.AddWithValue("@device_type", device_type);
                            cmdData.Parameters.AddWithValue("@device_serial_no", device_serial_no);
                            cmdData.Parameters.AddWithValue("@app_version", app_version);
                            cmdData.Parameters.AddWithValue("@device_name", device_name);
                            cmdData.Parameters.AddWithValue("@net_ip", net_ip);
                            cmdData.Parameters.AddWithValue("@firebase_token", firebase_token);
                            cmdData.Parameters.AddWithValue("@data_pin", data_pin);
                            cmdData.Parameters.AddWithValue("@device_token", device_token);

                            using (SqlDataAdapter daData = new SqlDataAdapter(cmdData))
                            {
                                DataSet dsData = new DataSet();
                                daData.Fill(dsData);
                                if (dsData.Tables.Count > 0)
                                {
                                    if (dsData.Tables[0].Rows.Count > 0)
                                    {
                                        var rowLoginData = dsData.Tables[0].Rows[0];
                                        resstatus = rowLoginData.GetSafeString("resstatus");
                                        resstatuscode = rowLoginData.GetSafeString("resstatuscode");
                                        resmessage = rowLoginData.GetSafeString("resmessage");

                                        if (resstatus != "1")
                                        {
                                            return new CommonResponse
                                            {
                                                status = resstatus,
                                                status_code = resstatus,
                                                success = false,
                                                message = resmessage,
                                                error = "Unauthorised",
                                                holdmsg = "Your service is temporary disable, Please contact to back office !",
                                            };
                                        }

                                        string changePasswordRequired = rowLoginData.GetSafeString("changePasswordRequired");
                                        string changePinRequired = rowLoginData.GetSafeString("changePinRequired");
                                        string loader_img = rowLoginData.GetSafeString("loader_img");
                                        string holdmsg = rowLoginData.GetSafeString("holdmsg");
                                        string timeid = rowLoginData.GetSafeString("timeid");
                                        string istoken = rowLoginData.GetSafeString("istoken");

                                        string newToken = string.Empty;
                                        if (istoken == "1")
                                        {
                                            string user_email = dsData.Tables[1].Rows[0].GetSafeString("user_email");
                                            newToken = _tokenService.GenerateJwtToken(matchingUserIds[0].ToString(), user_email);
                                            System.Diagnostics.Debug.WriteLine("Login Token: " + newToken);
                                        }

                                        var userData = dsData.Tables[1];
                                        var audioData = dsData.Tables[2];
                                        string userDataJson = JsonConvert.SerializeObject(userData);
                                        string audioDataJson = JsonConvert.SerializeObject(audioData);

                                        return new CommonResponse
                                        {
                                            status = resstatus,
                                            status_code = resstatus,
                                            success = resstatus == "1" ? true : false,
                                            message = resmessage,
                                            changePasswordRequired = changePasswordRequired,
                                            changePinRequired = changePinRequired,
                                            token = newToken,
                                            loader_img = loader_img,
                                            data = userDataJson,
                                            audiodata = audioDataJson,
                                            holdmsg = holdmsg,
                                            timeid = timeid,
                                        };
                                    }
                                }
                            }
                        }
                    }
                }

                return new CommonResponse
                {
                    status = "401",
                    status_code = "401",
                    success = false,
                    message = "Email or password invalid.",
                    error = "Unauthorised",
                    holdmsg = "Your service is temporary disable, Please contact to back office !",
                };
            }
            catch (SqlException sqlEx)
            {
                return new CommonResponse
                {
                    status = "400",
                    status_code = "400",
                    success = false,
                    message = $"SQL error: {sqlEx.Message}",
                    error = "Unauthorised",
                };
            }
        }

        public async Task<CommonResponse> LoginWithEmailOTP(LoginParams login_params, CommonHeader header)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    string
                        resmessage = "",
                        resstatus = "0",
                        resstatuscode = "400";

                    string email = login_params.email as string ?? string.Empty;
                    string password = login_params.password as string ?? string.Empty;
                    string device_type = login_params.device_type as string ?? string.Empty;
                    string device_serial_no = login_params.device_serial_no as string ?? string.Empty;
                    string app_version = login_params.app_version as string ?? string.Empty;
                    string device_name = login_params.device_name as string ?? string.Empty;
                    string net_ip = login_params.net_ip as string ?? string.Empty;
                    string firebase_token = login_params.firebase_token as string ?? string.Empty;
                    string data_pin = login_params.data_pin as string ?? string.Empty;
                    string device_token = login_params.device_token as string ?? string.Empty;
                    string header_device_type = header?.devicetype as string ?? string.Empty;
                    string org_type = header?.orgtype as string ?? string.Empty;

                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(DBCommands.LOGIN_WITHEMAILOTP, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@OTP", password);
                        cmd.Parameters.AddWithValue("@DeviceType", header_device_type);
                        cmd.Parameters.AddWithValue("@NetIP", net_ip);
                        cmd.Parameters.AddWithValue("@DeviceSerialNo", device_serial_no);
                        cmd.Parameters.AddWithValue("@DeviceToken", device_token);
                        cmd.Parameters.AddWithValue("@FirebaseToken", firebase_token);
                        cmd.Parameters.AddWithValue("@DataPin", data_pin);
                        cmd.Parameters.AddWithValue("@ApVersion", app_version);
                        cmd.Parameters.AddWithValue("@DeviceName", device_name);

                        using (SqlDataAdapter daData = new SqlDataAdapter(cmd))
                        {
                            DataSet dsData = new DataSet();
                            daData.Fill(dsData);

                            if (dsData.Tables.Count > 0)
                            {
                                if (dsData.Tables[0].Rows.Count > 0)
                                {
                                    var rowLoginData = dsData.Tables[0].Rows[0];
                                    resstatus = rowLoginData.GetSafeString("resstatus");
                                    resstatuscode = rowLoginData.GetSafeString("resstatuscode");
                                    resmessage = rowLoginData.GetSafeString("resmessage");

                                    if (resstatus != "1")
                                    {
                                        return new CommonResponse
                                        {
                                            status = resstatuscode,
                                            status_code = resstatuscode,
                                            success = false,
                                            message = resmessage,
                                            error = "Unauthorised",
                                            holdmsg = "Your service is temporary disable, Please contact to back office !",
                                        };
                                    }

                                    string IsMultiLogin = rowLoginData.GetSafeString("IsMultiLogin");
                                    string holdmsg = rowLoginData["holdmsg"] as string ?? string.Empty;
                                    string loader_img = dsData.Tables[1].Rows[0].GetSafeString("loader_img"); ;

                                    // Multi-Login
                                    if (IsMultiLogin == "1")
                                    {
                                        //var multiUserData = dsData.Tables[1];
                                        //string multiUserDataJson = JsonConvert.SerializeObject(multiUserData);
                                        var multiUserDataJson = new List<Dictionary<string, object>>();

                                        if (dsData.Tables.Count > 1 && dsData.Tables[1].Rows.Count > 0)
                                        {
                                            var table = dsData.Tables[1];
                                            foreach (DataRow row in table.Rows)
                                            {
                                                var dict = new Dictionary<string, object>();
                                                foreach (DataColumn col in table.Columns)
                                                    dict[col.ColumnName] = row[col];
                                                multiUserDataJson.Add(dict);
                                            }
                                        }

                                        return new CommonResponse
                                        {
                                            status = resstatus,
                                            status_code = resstatus,
                                            success = resstatus == "1" ? true : false,
                                            message = resmessage,
                                            loader_img = loader_img,
                                            data = multiUserDataJson,
                                            holdmsg = holdmsg,
                                        };
                                    }

                                    // Single Login
                                    string timeid = rowLoginData.GetSafeString("timeid");
                                    int istoken = rowLoginData.GetSafeInt("istoken");

                                    string newToken = string.Empty;
                                    if (istoken == 1)
                                    {
                                        string data_id = dsData.Tables[1].Rows[0].GetSafeString("data_id");
                                        newToken = _tokenService.GenerateJwtToken(data_id.ToString(), email);
                                        System.Diagnostics.Debug.WriteLine("Login Token: " + newToken);
                                    }

                                    var userDataJson = new List<Dictionary<string, object>>();

                                    if (dsData.Tables.Count > 1 && dsData.Tables[1].Rows.Count > 0)
                                    {
                                        var table = dsData.Tables[1];
                                        foreach (DataRow row in table.Rows)
                                        {
                                            var dict = new Dictionary<string, object>();
                                            foreach (DataColumn col in table.Columns)
                                                dict[col.ColumnName] = row[col];
                                            userDataJson.Add(dict);
                                        }
                                    }

                                    return new CommonResponse
                                    {
                                        status = resstatuscode,
                                        status_code = resstatuscode,
                                        success = resstatus == "1" ? true : false,
                                        message = resmessage,
                                        token = newToken,
                                        loader_img = loader_img,
                                        data = userDataJson,
                                        holdmsg = holdmsg,
                                        timeid = timeid,
                                    };
                                }
                            }
                        }

                    }
                    return new CommonResponse
                    {
                        status = "401",
                        status_code = "401",
                        success = false,
                        message = "OTP is wrong please try again.",
                        error = "Unauthorised",
                        holdmsg = "Your service is temporary disable, Please contact to back office !",
                    };
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
                    error = "Unauthorised",
                    holdmsg = "Your service is temporary disable, Please contact to back office !",
                };
            }
        }

        public async Task<CommonResponse> LoginWithUserID(LoginWithUserIdParams login_params, CommonHeader header)
        {
            try
            {
                using (SqlConnection dbConnection = new SqlConnection(_Connection))
                {
                    string
                        resmessage = "",
                        resstatus = "0",
                        resstatuscode = "400";

                    string data_id = login_params.data_id as string ?? string.Empty;
                    string device_type = login_params.device_type as string ?? string.Empty;
                    string device_serial_no = login_params.device_serial_no as string ?? string.Empty;
                    string net_ip = login_params.net_ip as string ?? string.Empty;
                    string firebase_token = login_params.firebase_token as string ?? string.Empty;
                    string device_token = login_params.device_token as string ?? string.Empty;
                    string header_device_type = header?.devicetype as string ?? string.Empty;
                    string app_version = header?.appversion as string ?? string.Empty;
                    string device_name = header?.devicename as string ?? string.Empty;
                    string org_type = header?.orgtype as string ?? string.Empty;

                    dbConnection.Open();

                    using (SqlCommand cmd = new SqlCommand(DBCommands.LOGIN_WITHUSERID, dbConnection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DataID", data_id);
                        cmd.Parameters.AddWithValue("@DeviceType", header_device_type);
                        cmd.Parameters.AddWithValue("@NetIP", net_ip);
                        cmd.Parameters.AddWithValue("@DeviceSerialNo", device_serial_no);
                        cmd.Parameters.AddWithValue("@DeviceToken", device_token);
                        cmd.Parameters.AddWithValue("@FirebaseToken", firebase_token);
                        cmd.Parameters.AddWithValue("@ApVersion", app_version);
                        cmd.Parameters.AddWithValue("@DeviceName", device_name);

                        using (SqlDataAdapter daData = new SqlDataAdapter(cmd))
                        {
                            DataSet dsData = new DataSet();
                            daData.Fill(dsData);

                            if (dsData.Tables.Count > 0)
                            {
                                if (dsData.Tables[0].Rows.Count > 0)
                                {
                                    var rowLoginData = dsData.Tables[0].Rows[0];
                                    resstatus = rowLoginData.GetSafeString("resstatus");
                                    resstatuscode = rowLoginData.GetSafeString("resstatuscode");
                                    resmessage = rowLoginData.GetSafeString("resmessage");

                                    if (resstatus != "1")
                                    {
                                        return new CommonResponse
                                        {
                                            status = resstatuscode,
                                            status_code = resstatuscode,
                                            success = false,
                                            message = resmessage,
                                            error = "Unauthorised",
                                            holdmsg = "Your service is temporary disable, Please contact to back office !",
                                        };
                                    }

                                    string holdmsg = rowLoginData["holdmsg"] as string ?? string.Empty;
                                    string loader_img = dsData.Tables[1].Rows[0].GetSafeString("loader_img"); ;
                                    string timeid = rowLoginData.GetSafeString("timeid");
                                    string istoken = rowLoginData.GetSafeString("istoken");

                                    string newToken = string.Empty;
                                    if (istoken == "1")
                                    {
                                        string user_email = dsData.Tables[1].Rows[0].GetSafeString("user_email");
                                        newToken = _tokenService.GenerateJwtToken(data_id.ToString(), user_email);
                                        System.Diagnostics.Debug.WriteLine("Login Token: " + newToken);
                                    }

                                    var userDataJson = new List<Dictionary<string, object>>();
                                    var audioDataJson = new List<Dictionary<string, object>>();

                                    if (dsData.Tables.Count > 1 && dsData.Tables[1].Rows.Count > 0)
                                    {
                                        var table = dsData.Tables[1];
                                        foreach (DataRow row in table.Rows)
                                        {
                                            var dict = new Dictionary<string, object>();
                                            foreach (DataColumn col in table.Columns)
                                            {
                                                if (col.ColumnName == "screen_short_status")
                                                {
                                                    dict[col.ColumnName] = (Convert.ToInt32(row[col]) == 1);
                                                }
                                                else
                                                {
                                                    dict[col.ColumnName] = row[col];
                                                }
                                            }
                                            userDataJson.Add(dict);
                                        }
                                    }


                                    if (dsData.Tables.Count > 2 && dsData.Tables[2].Rows.Count > 0)
                                    {
                                        var table = dsData.Tables[2];
                                        foreach (DataRow row in table.Rows)
                                        {
                                            var dict = new Dictionary<string, object>();
                                            foreach (DataColumn col in table.Columns)
                                                dict[col.ColumnName] = row[col];
                                            audioDataJson.Add(dict);
                                        }
                                    }

                                    return new CommonResponse
                                    {
                                        status = resstatuscode,
                                        status_code = resstatuscode,
                                        success = resstatus == "1" ? true : false,
                                        message = resmessage,
                                        token = newToken,
                                        loader_img = loader_img,
                                        data = userDataJson,
                                        audiodata = audioDataJson,
                                        holdmsg = holdmsg,
                                        timeid = timeid,
                                    };
                                }
                            }
                        }

                    }
                    return new CommonResponse
                    {
                        status = "401",
                        status_code = "401",
                        success = false,
                        message = "OTP is wrong please try again.",
                        error = "Unauthorised",
                        holdmsg = "Your service is temporary disable, Please contact to back office !",
                    };
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
                    error = "Unauthorised",
                    holdmsg = "Your service is temporary disable, Please contact to back office !",
                };
            }

        }
    }
}
