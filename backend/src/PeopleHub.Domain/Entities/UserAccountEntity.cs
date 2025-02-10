namespace PeopleHub.Domain.Entities;

public class UserAccountEntity
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }

    public UserAccountEntity(string email, string passwordHash)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentException("Email cannot be empty.");
        if (string.IsNullOrEmpty(passwordHash))
            throw new ArgumentException("Password cannot be empty.");

        Email = email;
        PasswordHash = passwordHash;
    }

    public void UpdatePassword(string newPassword)
    {
        if (string.IsNullOrEmpty(newPassword))
            throw new ArgumentException("New password cannot be empty.");

        PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
    }
}
