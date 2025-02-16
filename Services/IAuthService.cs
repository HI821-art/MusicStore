namespace MusicStore.Services
{
    public interface IAuthService
    {
        bool Register(string username, string password, string email);
        bool Login(string username, string password);
        bool ChangePassword(string username, string oldPassword, string newPassword);
        bool DeleteUser(string username);
    }
}