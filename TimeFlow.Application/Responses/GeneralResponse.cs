namespace TimeFlow.Application.Responses
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public T Result { get; set; }
    } 

    
}
