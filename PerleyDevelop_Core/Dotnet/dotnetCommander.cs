using System.Collections.Generic;
using System.Diagnostics;
public class DotnetCommander{
    public static string new_project = "dotnet new";
    public static string build_project = "dotnet build";
    public static string restore_project = "dotnet restore";
    public static List<string> app_types = new List<string>(){
        "gtkapp",
        "console",
        "classlib",
        "webapp",
        "webapi"
    };

    public static void ExecuteBashCommand(string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo() { 
                FileName = "/bin/bash", 
                Arguments = "-c \"" + command + "\"" 
                };
            
            Process proc = new Process() { 
                StartInfo = startInfo, 
                };
            
            proc.Start();
            proc.WaitForExit();
            proc.Dispose();
        }
}