namespace TicketManagementSystem.ApplicationLayer.DTOs
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        public List<T>? DataList { get; set; }

        public bool Success { get; set; }

        public string? Message { get; set; }

        public int StatusCode { get; set; }

        private ServiceResponse(T data, int statusCode)
        {
            Data = data;
            Success = true;
            StatusCode = statusCode;
        }

        private ServiceResponse(List<T> dataList, int statusCode)
        {
            DataList = dataList;
            Success = true;
            StatusCode = statusCode;
        }

        private ServiceResponse(string message, int statusCode)
        {
            Message = message;
            Success = false;
            StatusCode = statusCode;
        }

        public ServiceResponse() { }

        public static ServiceResponse<T?> SuccessResponse(T data) => new(data, 200);
        public static ServiceResponse<T> SuccessResponse(List<T> dataList) => new(dataList, 200);
        public static ServiceResponse<T?> NotFoundResponse(string message) => new(message, 404);
        public static ServiceResponse<T> FailureResponse(string message) => new(message, 400);
    }
}
