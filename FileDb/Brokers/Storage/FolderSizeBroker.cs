namespace FileDb.Brokers.Storage;

public class FolderSizeBroker : IFolderSizeBroker
{
    public  long GetFolderSize(string folderPath)
    {
        long totalSize = 0;
        string[] files = Directory.GetFiles(folderPath);
        foreach (string file in files)
        {
            FileInfo fileInfo = new FileInfo(file);
            totalSize += fileInfo.Length;
        }
        string[] subfolders = Directory.GetDirectories(folderPath);
        foreach (string subfolder in subfolders)
        {
            totalSize += GetFolderSize(subfolder);
        }
        return totalSize;
    }
}