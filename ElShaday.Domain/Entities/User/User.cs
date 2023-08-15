namespace ElShaday.Domain.Entities.User;

public abstract class User : Entity
{
    public string Email { get; private set; }
    public string NickName { get; private set; }
    public bool Active { get; private set; }
    public UserProfile Profile { get; protected set; }

    protected User(string email, string nickName, bool active)
    {
        Email = email;
        NickName = nickName;
        Active = active;
    }

    protected virtual void Activete() => Active = true;
    protected virtual void Deactivate() => Active = false;
}