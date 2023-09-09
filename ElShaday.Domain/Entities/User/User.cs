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
    
    public bool VerifyPassword(string password) => Password.Compare(password);

    public void Update(string? email = null, string? nickName = null, Role? role = null, bool? active = null)
    {
        Email = email ?? Email;
        NickName = nickName ?? NickName;
        Role = role ?? Role;
        Active = active ?? Active;
        base.Update();
    }

    public void Activate() => Active = true;
    public void Deactivate() => Active = false;

    public void ChangePassword(string password)
    {
        Password = new Password(password);
    }

    public override void Delete()
    {
        base.Delete();
        Deactivate();
    }
}