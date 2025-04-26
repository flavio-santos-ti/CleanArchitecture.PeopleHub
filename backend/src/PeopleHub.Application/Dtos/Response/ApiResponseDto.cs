using System.Text.Json.Serialization;

namespace PeopleHub.Application.Dtos.Response;

public class Response<T>
{
    public string ContextName { get; }
    public bool IsSuccess { get; }
    public string Message { get; }
    public int StatusCode { get; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public T? Data { get; }
}
