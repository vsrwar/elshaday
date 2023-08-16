using ElShaday.Domain.ValueObjects;

namespace ElShaday.Domain.Entities.User;

public sealed class User : Entity
{
    public string Email { get; private set; }
    public string NickName { get; private set; }
    public bool Active { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; set; }

    public User(string email, string nickName, string password)
    {
        Email = email;
        NickName = nickName;
        Active = true;
        Password = new Password(password);
    }

    // EF Constructor
    private User()
    {
    }

    public void Activete() => Active = true;
    public void Deactivate() => Active = false;
}