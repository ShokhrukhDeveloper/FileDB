using System.Collections.Generic;
using FileDb.Brokers.Storage;
using FileDb.Models;

namespace FileDb.Services;

public class UserProcessService  : IUserProcessService
{
    private readonly IFolderSizeBroker folderSizeBoker;
    private readonly IUserService userService;
    private readonly IIdentityService identityService;

    public UserProcessService(IUserService userService,
        IIdentityService identitiyService)
    {
        this.userService = userService;
        this.identityService = identitiyService;
        this.folderSizeBoker = new FolderSizeBroker();
    }

    public User CreateNewUser(User user)
    {
        user.Id = this.identityService.GetNewId();
        this.userService.AddUser(user);
        return user;
    }

    public List<User> GetAllUsers() =>
        this.userService.GetAllUser();

    public User DeleteUser(User user)
    {
       return this.userService.DeleteUser(user);
    }

    public User UpdateUser(User user)
    {
      return  this.userService.UpdateUser(user);
    }

    long GetFolderSize()
    {
      return  folderSizeBoker.GetFolderSize("../../../");
    }
}