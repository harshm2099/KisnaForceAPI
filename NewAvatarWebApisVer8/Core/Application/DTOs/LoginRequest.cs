namespace NewAvatarWebApis.Core.Domain.Common
{
    namespace NewAvatarWebApis.Models
    {
        public class LoginParams
        {
            public string email { get; set; }
            public string password { get; set; }
            public string data_pin { get; set; }
            public string device_type { get; set; }
            public string device_token { get; set; }
            public string device_serial_no { get; set; }
            public string app_version { get; set; }
            public string device_name { get; set; }
            public string net_ip { get; set; }
            public string firebase_token { get; set; }
        }

        public class LoginParamsWithMobileOtp
        {
            public string contactNo { get; set; }
            public string otp { get; set; }
            public string data_pin { get; set; }
            public string device_type { get; set; }
            public string device_token { get; set; }
            public string device_serial_no { get; set; }
            public string app_version { get; set; }
            public string device_name { get; set; }
            public string net_ip { get; set; }
            public string firebase_token { get; set; }
        }

        public class LoginWithUserIdParams
        {
            public string data_id { get; set; }
            public string device_type { get; set; }
            public string device_token { get; set; }
            public string device_serial_no { get; set; }
            public string app_version { get; set; }
            public string device_name { get; set; }
            public string net_ip { get; set; }
            public string firebase_token { get; set; }
        }

        public class LoginNewEMobParams
        {
            public string email_mobile { get; set; }
            public string password { get; set; }
            public string net_ip { get; set; }
            public string device_serial_no { get; set; }
            public string device_token { get; set; }
            public string device_type { get; set; } // I OR A
            public string firebase_token { get; set; }
            public string data_pin { get; set; }
            public string app_version { get; set; }
            public string device_name { get; set; }
        }

        public class SendOtpParams
        {
            public string mobile_no { get; set; }
            public string net_ip { get; set; }
        }

        public class SendOtpmeParams
        {
            public string email_mobile { get; set; }
            public string mobile_no { get; set; }
            public string email { get; set; }
            public string device_type { get; set; }
        }

        public class SendEmailOtpParams
        {
            public string email { get; set; }
            public string net_ip { get; set; }
            public string device_token { get; set; }
            public string firebase_token { get; set; }
            public string devicetype { get; set; }
        }

        public class VerifyEmailParams
        {
            public string email_mobile { get; set; }
        }

        public class ChangePasswordParams
        {
            public string data_id { get; set; }
            public string password { get; set; }
        }

        public class ChangePasswordResponse
        {
            public bool success { get; set; }
            public string message { get; set; }
        }

    }

}
