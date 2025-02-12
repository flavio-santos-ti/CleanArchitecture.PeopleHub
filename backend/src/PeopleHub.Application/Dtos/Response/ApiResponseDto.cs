using System.Text.Json.Serialization;

namespace PeopleHub.Application.Dtos.Response;

public class ApiResponseDto<T>
{
    public string ContextName { get; }
    public bool IsSuccess { get; }
    public string Message { get; }
    public int StatusCode { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Data { get; }

    public ApiResponseDto(string contextName, bool isSuccess, string message, int statusCode, T? data = default)
    {
        ContextName = contextName;
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode;
        Data = data;
    }

    public static ApiResponseDto<T> Response(string contextName, string message = "Success", int statusCode = 200, T? data = default)
    {
        return new ApiResponseDto<T>(contextName, true, message, statusCode, data);
    }
}
