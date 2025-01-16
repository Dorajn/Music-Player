using System.IO;

public class Data
{
    public List<(string, List<string>)> FetchedData { get; private set; } = [];

    public Data(string directoryPath)
    {
        string[] subDirectoriePaths = Directory.GetDirectories(directoryPath);
        foreach (string subDirectoryPath in subDirectoriePaths)
        {
            DirectoryInfo subDirectoryInfo = new DirectoryInfo(subDirectoryPath);
            List<string> fileNames = subDirectoryInfo.GetFiles().Select(file => file.Name).ToList();
            FetchedData.Add((subDirectoryInfo.Name, fileNames));
        }
    }

    // For debuging
    public void Print()
    {
        foreach (var (subDirectory, subDirectoryFiles) in FetchedData)
        {
            Console.WriteLine(subDirectory);
            foreach (var subDirectoryFile in subDirectoryFiles)
            {
                Console.WriteLine("-- " + subDirectoryFile);
            }
        }
    }
}
