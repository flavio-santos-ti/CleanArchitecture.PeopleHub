namespace PeopleHub.Domain.ValueObjects;

public class Email
{
    public string Value { get; private set; }

    public Email(string value)
    {
        Value = value;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrEmpty(Value) || !Value.Contains("@"))
            throw new ArgumentException("Invalid email.");
    }
}