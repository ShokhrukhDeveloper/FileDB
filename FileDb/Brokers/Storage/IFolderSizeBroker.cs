namespace FileDb.Brokers.Storage;

public interface IFolderSizeBroker
{
    long GetFolderSize(string folderPath);
}