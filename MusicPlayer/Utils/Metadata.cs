using Microsoft.Win32;
namespace MusicPlayer.Utils;

public class Metadata
{
    public static string absolutePath;
    private static string key = @"SOFTWARE\ChadPlayer";
    private static string valueName = "musicFolderPath";

    public static void SaveMusicFolderPath(string path)
    {
        absolutePath = path;
        RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(key);
        registryKey.SetValue(valueName, path);
    }
    
    public static string GetMusicFolderPath()
    {
        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(key);
        if (registryKey != null)
        {
            return registryKey.GetValue(valueName).ToString();
        }
        return string.Empty;  
    }
    
    public void UpdateMusicFolderPath(string newPath)
    {
        RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(key, writable: true);
    
        if (registryKey != null)
        {
            registryKey.SetValue(valueName, newPath);
            registryKey.Close();
        }
        else
        {
            registryKey = Registry.CurrentUser.CreateSubKey(key);
            registryKey.SetValue(valueName, newPath);
            registryKey.Close();
        }
    }
}