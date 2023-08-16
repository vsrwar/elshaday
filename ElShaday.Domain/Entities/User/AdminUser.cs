namespace ElShaday.Domain.Entities.User;

public sealed class AdminUser : User
{
    public AdminUser(string email, string nickName)
        : base(email, nickName)
    {
    }

    public override void Delete()
    {
        base.Delete();
        Active = false;
    }
}