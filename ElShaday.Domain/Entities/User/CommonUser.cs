namespace ElShaday.Domain.Entities.User;

public sealed class CommonUser : User
{
    public CommonUser(string email, string nickName, bool active)
        : base(email, nickName, active)
    {
        Profile = UserProfile.Common;
    }
}