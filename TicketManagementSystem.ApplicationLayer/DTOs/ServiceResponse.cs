namespace TicketManagementSystem.ApplicationLayer.DTOs
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public List<T>? DataList { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }

        private ServiceResponse(T data)
        {
            Data = data;
            Success = true;
        }

        private ServiceResponse(List<T> dataList)
        {
            DataList = dataList;
            Success = true;
        }

        private ServiceResponse(string message)
        {
            Message = message;
            Success = false;
        }

        public static ServiceResponse<T> SuccessResponse(T data) => new(data);
        public static ServiceResponse<T> SuccessResponse(List<T> dataList) => new(dataList);
        public static ServiceResponse<T> FailureResponse(string message) => new(message);
    }
}
