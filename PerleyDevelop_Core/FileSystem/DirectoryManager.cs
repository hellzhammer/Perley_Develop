using System.IO;
public class DirectoryManager{
    public static string[] GetDirectoryFiles(string dir){
        return Directory.GetFiles(dir);
    }

    public static string[] GetDirectorySubdirectories(string dir){
        return Directory.GetDirectories(dir);
    }   
}