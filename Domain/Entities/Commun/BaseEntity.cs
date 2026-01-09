namespace Domain.Entities.Commun;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? SuspendAt { get; set; }
    public string? SuspensionReason { get;  set; }
    public bool IsActive { get; set; }
}