namespace Domain.Entities.Common;

public abstract class BaseEntity
{
    public Guid Id { get; private init; } = Guid.CreateVersion7();
    
    public DateTime Created { get; private init; } = DateTime.UtcNow;
    
    public DateTime? Updated { get; set; }
}