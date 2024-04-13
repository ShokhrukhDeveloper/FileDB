using FileDb.Models;

namespace FileDb.Services;

public interface IUserProcessService
{
    public User CreateNewUser(User user);
    public void DisplayUsers();
    public User DeleteUser(User user);
    public User UpdateUser(User user);

}