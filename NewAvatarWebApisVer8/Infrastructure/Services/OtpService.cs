using NewAvatarWebApis.Infrastructure.Services.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace OtpApi.Services
{
    public class OtpService : IOtpService
    {
        private readonly IConfiguration _config;

        public OtpService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendOtpAsync(string mobileNumber, string message)
        {
            try
            {
                // Fetch Twilio credentials from appsettings.json
                var accountSid = _config["Twilio:AccountSid"];
                var authToken = _config["Twilio:AuthToken"];
                var fromNumber = _config["Twilio:FromNumber"];

                TwilioClient.Init(accountSid, authToken);

                var msg = await MessageResource.CreateAsync(
                    body: message,
                    from: new PhoneNumber(fromNumber),
                    to: new PhoneNumber(mobileNumber)
                );

                return msg.ErrorCode == null; // true if successfully sent
            }
            catch
            {
                return false;
            }
        }
    }
}
