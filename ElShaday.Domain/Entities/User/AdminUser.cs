namespace ElShaday.Domain.Entities.User;

public sealed class AdminUser : User
{
    public AdminUser(string email, string nickName, string password)
        : base(email, nickName, password)
    {
    }

    // EF Constructor
    private AdminUser() : base()
    {
    }

    public override void Delete()
    {
        base.Delete();
        Active = false;
    }
}