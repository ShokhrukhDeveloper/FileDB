using System.Linq;
using FileDb.Brokers.Storage;

namespace FileDb.Services;

public class IdentityService : IIdentityService
{
    private IStorageBroker storageBroker;
    private static IIdentityService? identityService;
    private IdentityService(IStorageBroker fileStorageBroker)
    {
        storageBroker = fileStorageBroker;
    }

    public int GetNewId()
    {
        if (storageBroker.
            ReadAllUsers().Count()==0)
        {
            return 1;
        }
       return storageBroker.
                 ReadAllUsers().
                 Last().
                 Id+1;
    }

    

    public static IIdentityService GetInstance(IStorageBroker storageBroker)
        => identityService ??= new IdentityService(storageBroker);



}