using System.Text.Json;
using FileDb.Models;
namespace FileDb.Brokers.Storage;

public class JSONStorageBroker : IStorageBroker
{
    private string fileName;
    public JSONStorageBroker(string file="../../../Users.json")
    {
        this.fileName = file;
        EnsureFileExists();
    }
    
    public User InsertUser(User user)
    {
        string jsonText = File.ReadAllText(fileName);
        List<User> users = ReadAllUsers();
        users.Add(user);
        jsonText=JsonSerializer.Serialize(users);
        File.WriteAllText(fileName,jsonText);
        return user;
    }

    public User? GetUserById(int id)
        => ReadAllUsers().FirstOrDefault(e => e.Id == id);

    public List<User> ReadAllUsers()
    {
        string jsonText = File.ReadAllText(fileName);
        List<User> users = JsonSerializer.Deserialize<List<User>>(jsonText);
        return users;
    }

    public User UpdateUser(User user)
    {
        var users = ReadAllUsers();
        var updateUser = users.FirstOrDefault(e => e.Id == user.Id);
        updateUser.Name = user.Name;
        var jsonText=JsonSerializer.Serialize(users);
        File.WriteAllText(fileName,jsonText);
        return updateUser;
    }

    public User? DeleteUserById(int id)
    {
        var users = ReadAllUsers();
        var deteleteUser = users.FirstOrDefault(e => e.Id == id);
        users.Remove(deteleteUser);
        var jsonText=JsonSerializer.Serialize(users);
        File.WriteAllText(fileName,jsonText);
        return deteleteUser;
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