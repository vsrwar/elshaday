namespace ElShaday.Domain.Entities.User;

public abstract class User : Entity
{
    public string Email { get; private set; }
    public string NickName { get; private set; }
    public bool Active { get; protected set; }

    protected User(string email, string nickName)
    {
        Email = email;
        NickName = nickName;
        Active = true;
    }

    public virtual void Activete() => Active = true;
    public virtual void Deactivate() => Active = false;
}