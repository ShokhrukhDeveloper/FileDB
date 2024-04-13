using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileDb.Models;

namespace FileDb.Brokers.Storage;

public class FileStorageBroker : IStorageBroker
{
    private string fileName;

    public FileStorageBroker(string fileName = "../../../Users.txt")
    {
        this.fileName = fileName;
        EnsureFileExists();
    }

    public User? DeleteUserById(int id)
    {
        List<User?> users=File.ReadAllLines(fileName).
            Select(ToModel).
            ToList()!;
        var user = users.FirstOrDefault(e => e.Id == id);
        users.Remove(user);
        File.WriteAllLines(fileName,users.Select(e=>e.ToString()));
        return user;
    }

    public List<User> ReadAllUsers()
        =>  File.ReadAllLines(fileName).
            Select(ToModel).
            ToList();
    

    public User InsertUser(User user)
    {
        File.AppendAllText(fileName, user.ToString());
        return user;
    }

    public User? GetUserById(int id) =>
            ReadAllUsers().
            FirstOrDefault(e => e.Id == id);
    
    public User UpdateUser(User user)
    {
        List<User> users = ReadAllUsers();

        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].Id == user.Id)
            {
                users[i] = user;
                break;
            }
        }
        File.WriteAllLines(fileName, users.Select(e=>e.ToString()));
        return user;
    }
    private User ToModel(string text)
    {
        string[] data= text.Split(':');
        return new User(data[1]);
    }
    private void EnsureFileExists()
    {
        bool fileExists = File.Exists(fileName);
        if (fileExists is false)
        {
            File.Create(fileName).Close();
        }
    }
}