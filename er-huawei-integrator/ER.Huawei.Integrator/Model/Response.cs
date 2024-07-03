namespace ER.Huawei.Integrator.Cons.Application.Model
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public Response()
        {
            Success = true;
            Message = string.Empty;
            Data = default;
        }

        public Response(T data)
        {
            Success = true;
            Message = string.Empty;
            Data = data;
        }

        public Response(string message)
        {
            Success = false;
            Message = message;
            Data = default;
        }

        public Response(string message, T data)
        {
            Success = false;
            Message = message;
            Data = data;
        }
    }
}
