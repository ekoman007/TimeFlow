namespace TimeFlow.Application.Responses
{
    public class GeneralResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
    }

    public class GeneralResponse<T> : GeneralResponse
    {
        public T Result { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }

    
}
