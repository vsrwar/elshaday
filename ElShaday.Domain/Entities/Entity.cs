namespace ElShaday.Domain.Entities;

public abstract class Entity
{
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; protected set; } = null;
    public DateTime? DeletedAt { get; protected set; } = null;

    public virtual void Delete()
    {
        DeletedAt = DateTime.UtcNow;
    }

    public virtual void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}