namespace Domain;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? SuspendAt { get; set; }
    public string? SuspensionReason { get;  set; }
    protected bool IsActive { get; set; }
}