namespace ElShaday.Domain.Entities.User;

public sealed class CommonUser : User
{
    public CommonUser(string email, string nickName, string password)
        : base(email, nickName, password)
    {
    }
    
    // EF Constructor
    private CommonUser() : base()
    {
    }

    public override void Delete()
    {
        base.Delete();
        Active = false;
    }
}