using System.Collections.Generic;
using FileDb.Models;

namespace FileDb.Services;

public interface IUserService
{
    User AddUser(User user);
    User UpdateUser(User user);
    User DeleteUser(User user);
    User GetByUserId(int id);
    List<User> GetAllUser();
}