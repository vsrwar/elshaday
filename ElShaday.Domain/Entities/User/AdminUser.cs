namespace ElShaday.Domain.Entities.User;

public sealed class AdminUser : User
{
    public AdminUser(string email, string nickName, bool active)
        : base(email, nickName, active)
    {
        Profile = UserProfile.Admin;
    }
}