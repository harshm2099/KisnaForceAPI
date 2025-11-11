namespace NewAvatarWebApis.Core.Application.Responses
{
    // You MUST add the <T> placeholder to the class definition
    public class ServiceResponse<T>
    {
        public bool? Success { get; set; }
        public string? Message { get; set; }
        public string? status { get; set; }

        // The T placeholder allows this property to be of any type
        public T Data { get; set; }
    }
}
