using System.Collections.Generic;
using FileDb.Models;

namespace FileDb.Services;

public interface IUserProcessService
{
    public User CreateNewUser(User user);
    public List<User> GetAllUsers();
    public User DeleteUser(User user);
    public User UpdateUser(User user);

}