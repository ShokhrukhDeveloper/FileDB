using System;
using System.Collections.Generic;
using FileDb.Brokers.Logging;
using FileDb.Brokers.Storage;
using FileDb.Models;

namespace FileDb.Services;

public class UserService : IUserService
{
    public UserService(ILoggingBroker loggingBroker, IStorageBroker storageBroker)
    {
        this.loggingBroker = loggingBroker;
        this.storageBroker = storageBroker;
    }
    private ILoggingBroker loggingBroker;
    private IStorageBroker storageBroker;
    public User AddUser(User user)
    {
        try
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                loggingBroker.LogError("User Error please Fix name");
                return user;
            }

            return storageBroker.InsertUser(user);
        }
        catch (Exception e)
        {
            loggingBroker.LogError(e);
            return new User();
        }

    }

    public User UpdateUser(User user)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(user.Name) || user.Id is 0)
            {
                loggingBroker.LogError("yangliashda xatolik uz beri Id 0 yoki Ism bo'sh qoldirigan");
                return new User();
            }

            var updatedUser = storageBroker.UpdateUser(user);
            loggingBroker.LogInformation($"User muvoffaqiyatli yangilandi [{user}]");
           return storageBroker.UpdateUser(user);
        }
        catch (Exception e)
        {
            loggingBroker.LogError(e);
            return  new User();
        }
    }

    public User DeleteUser(User user)
    {
        try
        {
          var deleteduser=  storageBroker.DeleteUserById(user.Id);
            if (deleteduser is null)
            {
                loggingBroker.LogError("O'chirishda xatolik yuz berdi ");
                return new User();
            }
            loggingBroker.LogInformation("Muvoffaqiyalti o'chirildi");
            return user;
        }
        catch (Exception e)
        {
          loggingBroker.LogError(e);
            return  new User();
        }
    }

    public User GetByUserId(int id)
    {
        try
        {
            var user = storageBroker.GetUserById(id);
            if (user is null)
            {
                loggingBroker.LogError("user topilmadi");
                return new User();
            }
            return user;
        }
        catch (Exception e)
        {
             loggingBroker.LogError(e);
             return new User();
        }
    }

    public List<User> GetAllUser()
    {
        return storageBroker.ReadAllUsers();
    }
}