using System;
using System.Collections;
using System.Collections.Generic;
using FileDb.Brokers.Logging;
using FileDb.Brokers.Storage;
using FileDb.Models;
using FileDb.Services;

namespace FileDb;

public class Program
{
    private static IUserProcessService userProcess;
    public static void Main(string[] args)
    {
        userProcess = InitializeService();
        bool running = true;
        do
        {
            int input = InputUser();
            if (input ==0)
            {
                running = false;
                Console.WriteLine("Program Exit 0");
                return;
            }
            var result = input
                switch
                {
                    1 => (Action)CreateUser,
                    2=> (Action) UpdateUser,
                    3=>(Action) DeleteUser,
                    4=>(Action) PrintAllUser,
                    _=> throw new ArgumentOutOfRangeException()
                };
            result.Invoke();
        } while (running);

    }

    private static void PrintAllUser()
    {
        var users = userProcess.GetAllUsers();
        Console.WriteLine("----------------Begins-of--User-list--------------------");
        foreach (var user in users)
        {
            Console.Write(user+"\n");
        }
        Console.WriteLine("------------------End-of--User-list--------------------");
    }

    private static void PrintMenu()
    {
        Console.WriteLine("0) dasturdan chiqish");
        Console.WriteLine("1) User qo'shish");
        Console.WriteLine("2) User o'gartirish");
        Console.WriteLine("3) User o'chirish");
        Console.WriteLine("4) Barcha Userni ekranga chiqarish");
    }
    private static void CreateUser()
    {
        Console.Write("user nomi: ");
        var name = Console.ReadLine();
        userProcess.CreateNewUser(
            new User()
            {
                Name = name
            }
        );
        Console.WriteLine("User muvoffaqiyyatli qo'shildi");
    }
    private static void UpdateUser()
    {
        Console.Write("Id raqami : ");
        var inputId = Console.ReadLine();
        Console.Write("Yangilash uchun Name: ");
        var inputName = Console.ReadLine();
        userProcess.UpdateUser(
            new User()
            {
                Id = Convert.ToInt32(inputId),
                Name = inputName
            }
        );
        Console.WriteLine("User muvoffaqiyyatli yangiladi");
    }
    private static void DeleteUser()
    {
        Console.Write("Id raqami : ");
        var inputId = Console.ReadLine();
       
        userProcess.DeleteUser(
            new User()
            {
                Id = Convert.ToInt32(inputId)
            }
        );
        Console.WriteLine("User muvoffaqiyyatli o'chirildi"); 
    }
    private static int InputUser()
    {
        PrintMenu();
        Console.Write("");
        return Convert.ToInt32(Console.ReadLine());
    }
    private static IUserProcessService InitializeService()
    {
                IStorageBroker storageBroker = new JSONStorageBroker();
                ILoggingBroker loggingBroker = new LoggingBroker();
                IUserService userService = new UserService(loggingBroker,storageBroker);
                IIdentityService identityService = IdentityService.GetInstance(storageBroker);
                return new UserProcessService(userService,identityService);
    }
}