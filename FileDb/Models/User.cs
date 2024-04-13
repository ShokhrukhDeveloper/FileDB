using System;

namespace FileDb.Models;

public class User
{
    public User()
     {
        
    }
    public User(int id, string name)
    {
        Name=name;
        Id = id;
    }
 public User(string name)
    {
        Name=name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public override string ToString()
    {
        return $"{Id}:{Name}{Environment.NewLine}";
    }
}