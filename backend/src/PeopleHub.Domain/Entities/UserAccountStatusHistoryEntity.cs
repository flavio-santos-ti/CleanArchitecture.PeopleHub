namespace PeopleHub.Domain.Entities;

public class UserAccountStatusHistoryEntity
{
    public Guid Id { get; private set; }

    public Guid UserAccountId { get; private set; }
    public Guid ChangedBy { get; private set; }

    public bool OldStatus { get; private set; }
    public bool NewStatus { get; private set; }

    public DateTime ChangedAt { get; private set; }

    // Construtor sem parâmetros requerido pelo Entity Framework Core
    // Parameterless constructor required by Entity Framework Core
    private UserAccountStatusHistoryEntity()
    {
        Id = Guid.NewGuid();
        ChangedAt = DateTime.UtcNow;
    }

    // Construtor principal
    // Main constructor
    public UserAccountStatusHistoryEntity(
        Guid userAccountId,
        Guid changedBy,
        bool oldStatus,
        bool newStatus
    )
    {
        Id = Guid.NewGuid();
        UserAccountId = userAccountId;
        ChangedBy = changedBy;
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedAt = DateTime.UtcNow;

        Validate();
    }

    // Valida os dados obrigatórios
    // Validates required data
    private void Validate()
    {
        if (UserAccountId == Guid.Empty)
            throw new ArgumentException("UserAccountId is required.");
        if (ChangedBy == Guid.Empty)
            throw new ArgumentException("ChangedBy is required.");
    }
}
