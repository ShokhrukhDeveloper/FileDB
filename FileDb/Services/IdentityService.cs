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
        if (!storageBroker.
            ReadAllUsers().Any())
        {
            return 1;
        }
       return storageBroker.
                 ReadAllUsers().
                 Last().
                 Id++;
    }

    

    public static IIdentityService GetInstance(IStorageBroker storageBroker)
        => identityService ??= new IdentityService(storageBroker);



}