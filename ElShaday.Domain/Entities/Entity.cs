namespace ElShaday.Domain.Entities;

public abstract class Entity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; } = null;
    public DateTime? DeletedAt { get; protected set; } = null;
}