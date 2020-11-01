namespace BlogApi.Features
{
    public class Response<T> 
    {
        public bool Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public Response(T data)
        {
            Status = true;
            Data = data;
            Message = "Succesfull.";
        }
        
        public Response(string message)
        {
            Status = false;
            Message = message;
        }
    }
}