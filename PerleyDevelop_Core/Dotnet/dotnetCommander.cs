using System.Collections.Generic;
using System.Diagnostics;
public class DotnetCommander{
    string new_project = "dotnet new";
    string build_project = "dotnet build";
    string restore_project = "dotnet restore";
    string run_project = "dotnet run";
    public static List<string> app_types = new List<string>(){
        "gtkapp",
        "console",
        "classlib",
        "webapp",
        "webapi"
    };

    public void CreateApp(string apptype){
        ExecuteBashCommand(new_project + " " + apptype);        
    }

    public void RestoreApp(){
        ExecuteBashCommand(restore_project);
    }

    public void BuildApp(){
        ExecuteBashCommand(build_project);
    }

    public void RunApp(){
        ExecuteBashCommand(run_project);
    }

    private void ExecuteBashCommand(string command)
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