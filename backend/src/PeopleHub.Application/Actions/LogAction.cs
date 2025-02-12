namespace PeopleHub.Application.Actions;

public sealed class LogAction
{
    public string Value { get; }

    private LogAction(string value) => Value = value;

    public static readonly LogAction CREATE = new("CREATE");
    public static readonly LogAction CREATE_UPLOAD = new("CREATE_UPLOAD");
    public static readonly LogAction CREATE_LOGIN = new("CREATE_LOGIN");
    public static readonly LogAction DELETE = new("DELETE");
    public static readonly LogAction ERROR = new("ERROR");
    public static readonly LogAction NOT_FOUND = new("NOT_FOUND");
    public static readonly LogAction UPDATE = new("UPDATE");
    public static readonly LogAction LOGIN_SUCCESS = new("LOGIN_SUCCESS");
    public static readonly LogAction VALIDATION_ERROR = new("VALIDATION_ERROR");
    public static readonly LogAction READ = new("READ");

    public override string ToString() => Value;
}
