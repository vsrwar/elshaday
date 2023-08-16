namespace ElShaday.Domain.Entities.User;

public sealed class CommonUser : User
{
    public CommonUser(string email, string nickName)
        : base(email, nickName)
    {
    }

    public override void Delete()
    {
        base.Delete();
        Active = false;
    }
}