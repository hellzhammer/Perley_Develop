using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
public class App_Path{
    public static string projectPath = "";
    static string pluginPath = "/Plugins/";
    static string pluginSubCore = "Core";
    static string pluginSubGUI = "CoreGui";

    public static List<string> CorePlugins = new List<string>();
    public static List<string> CoreGuiPlugins = new List<string>();

    public static string GetGuiPath(){
        return Environment.CurrentDirectory + pluginPath + pluginSubGUI;
    }

    public static string GetCorePath(){
        return Environment.CurrentDirectory + pluginPath + pluginSubCore;
    }
}