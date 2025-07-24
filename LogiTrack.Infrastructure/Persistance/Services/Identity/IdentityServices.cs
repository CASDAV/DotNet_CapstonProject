using LogiTrack.Application.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LogiTrack.Infrastructure.Persistance.Services.Identity;

internal class IdentityServices : IIdentityServices
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly ILogger<IdentityServices> _logger;

    public IdentityServices(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, ILogger<IdentityServices> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public async Task<bool> CreateRole(string roleName)
    {
        if (await _roleManager.RoleExistsAsync(roleName)) return false;

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

        return result.Succeeded;
    }

    public async Task<(bool loginResult, string? userRole)> Login(string userName, string password)
    {
        SignInResult result = await _signInManager.PasswordSignInAsync(userName, password, false, false);

        if (!result.Succeeded)
        {
            _logger.LogError($"User lgin failed");
            return (false, null);
        }

        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            _logger.LogError($"User lgin failed");
            return (false, null);
        }

        var roles = await _userManager.GetRolesAsync(user);

        _logger.LogInformation($"User {userName} succesfully login");

        return (result.Succeeded, roles.FirstOrDefault());
    }

    public async Task<bool> RegisterUser(string userName, string email, string password, string role = "User")
    {
        var user = new IdentityUser { UserName = userName, Email = email, };
        IdentityResult result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
            return result.Succeeded;
        }

        return result.Succeeded;

    }
}
