namespace NewAvatarWebApis.Infrastructure.Services.Interfaces
{
    public interface IOtpService
    {
        public Task<bool> SendOtpAsync(string mobileNumber, string message);
    }
}
