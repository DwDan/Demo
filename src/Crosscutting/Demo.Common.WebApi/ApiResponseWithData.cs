namespace Demo.Common.WebApi;

public class ApiResponseWithData<T> : ApiResponse
{
    public T? Data { get; set; }
}
