namespace PeopleHub.Domain.ValueObjects;

public class Phone
{
    public string Number { get; private set; }

    public Phone(string number)
    {
        Number = number;

        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrEmpty(Number) || Number.Length < 8)
            throw new ArgumentException("Invalid phone number.");
    }
}