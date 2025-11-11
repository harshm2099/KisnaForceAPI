namespace NewAvatarWebApis.Core.Application.Wrappers
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {

        }
        // Success Response
        public ApiResponse(T data, int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
        }

        // Failed Response
        public ApiResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public T Data { get; set; }
    }
}
