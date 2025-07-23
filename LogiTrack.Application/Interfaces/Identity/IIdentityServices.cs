namespace LogiTrack.Application.Interfaces.Identity;

public interface IIdentityServices
{
    public Task<bool> RegisterUser(string userName, string email, string password, string role = "User");

    public Task<(bool loginResult, string? userRole)> Login(string userName, string password);

    public Task<bool> CreateRole(string roleName);
}
