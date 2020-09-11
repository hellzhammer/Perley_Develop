using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.ComponentModel;
using System;
using System.Web;
using System.Linq;
using System.IO;
using System.Reflection;

using Perley_Develop_Core_lib.App_Components.Models;
using Perley_Develop_IDE.Perley_Dev_System.Plugin;
using PerleyDev_Plugin_lib.Interfaces;
using PerleyDev_Plugin_lib.Models;

using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Perley_Develop_IDE.Perley_Dev_System.Plugin
{
    public class PerleyDevPluginLoader
    {
        public void LoadPlugins(string[] args)
        {
            try
            {
                if (args.Length == 1 && args[0] == "/d")
                {
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadLine();
                }

                // Load commands from plugins.
                string[] pluginPaths = new string[]
                {
                    // Paths to plugins to load.
                    Environment.CurrentDirectory + "/Plugins/PerleyDev_T/PerleyDev_T.dll"
                };

                IEnumerable<ExtensionBase> extensions = pluginPaths.SelectMany(pluginPath =>
                {
                    Assembly pluginAssembly = LoadPlugin(pluginPath);
                    return CreateCommands(pluginAssembly);
                }).ToList();

                if (args.Length == 0)
                {
                    Console.WriteLine("Extensions: ");
                    // Output the loaded commands.
                    foreach (ExtensionBase extension in extensions)
                    {
                        if (extension == null)
                        {
                            Console.WriteLine("Sorry Can't Load This File");
                            return;
                        }

                        bool i = extension.Init();
                        Console.WriteLine($"{extension.Name}\t - {extension.Description}");
                    }
                }
                else
                {
                    foreach (string extName in args)
                    {
                        Console.WriteLine($"-- {extName} --");

                        // Execute the command with the name passed as an argument.
                        ExtensionBase extension = extensions.FirstOrDefault(c => c.Name == extName);
                        if (extension == null)
                        {
                            Console.WriteLine("Sorry Can't Load This File.");
                            return;
                        }

                        bool i = extension.Init();

                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public IEnumerable<ExtensionBase> LoadSinglePlugin(string extPath){

            Assembly extAssembly = LoadPlugin(extPath);  
            foreach (Type type in extAssembly.GetTypes())
            {
                if (typeof(ExtensionBase).IsAssignableFrom(type))
                {
                    ExtensionBase result = Activator.CreateInstance(type) as ExtensionBase;
                    if (result != null)
                    {
                        yield return result;
                    }
                }
            }
        }

        private Assembly LoadPlugin(string relativePath)
        {
            // Navigate up to the solution root
            string root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(Program).Assembly.Location)))))));

            string pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));
            Console.WriteLine($"Loading Extensions from: {pluginLocation}");
            PluginContext loadContext = new PluginContext(pluginLocation);
            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        private IEnumerable<ExtensionBase> CreateCommands(Assembly assembly)
        {
            int count = 0;

            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(ExtensionBase).IsAssignableFrom(type))
                {
                    ExtensionBase result = Activator.CreateInstance(type) as ExtensionBase;
                    if (result != null)
                    {
                        count++;
                        yield return result;
                    }
                }
            }

            if (count == 0)
            {
                string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
                throw new ApplicationException(
                    $"Can't find any type which implements IExtension in {assembly} from {assembly.Location}.\n" +
                    $"Available types: {availableTypes}");
            }
        }
    }
}