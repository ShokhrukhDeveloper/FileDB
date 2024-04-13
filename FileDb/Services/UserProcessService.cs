using FileDb.Models;

namespace FileDb.Services;

public class UserProcessService  : IUserProcessService
{
    private readonly IUserService userService;
    private readonly IIdentityService identityService;

    public UserProcessService(IUserService userService,
        IIdentityService identitiyService)
    {
        this.userService = userService;
        this.identityService = identitiyService;
    }

    public User CreateNewUser(User user)
    {
        user.Id = this.identityService.GetNewId();
        this.userService.AddUser(user);
        return user;
    }

    public void DisplayUsers() =>
        this.userService.GetAllUser();

    public User DeleteUser(User user)
    {
       return this.userService.DeleteUser(user);
    }

    public User UpdateUser(User user)
    {
      return  this.userService.UpdateUser(user);
    }
}