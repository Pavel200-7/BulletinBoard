using BulletinBoard.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace BulletinBoard.Hosts.UserAPI.Controllers;

public class UserController
{
    public UserManager<ApplicationUser> userManager;

    public bool XUI()
    {
        ApplicationUser r = new ApplicationUser();

    }
}
