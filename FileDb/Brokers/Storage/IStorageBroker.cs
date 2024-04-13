
using System.Collections.Generic;
using FileDb.Models;

namespace FileDb.Brokers.Storage;

public interface IStorageBroker
{
    User InsertUser(User user);
    User? GetUserById(int id);
    List<User> ReadAllUsers();
    User UpdateUser(User user);
    User? DeleteUserById(int id);
}