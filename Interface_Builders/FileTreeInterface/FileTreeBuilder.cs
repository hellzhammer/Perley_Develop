using System.IO;
using System.Collections.Generic;
using System;

using Gtk;

using Perley_Develop_Core_lib.PerleyDevelop_Core.FileSystem.Models;
using Perley_Develop_Core_lib.PerleyDevelop_Core.FileSystem.Interfaces;
using Perley_Develop_Core_lib.App_Components;

namespace Perley_Develop_IDE.Interface_Builders.FileTreeInterface
{
    public class FileTreeBuilder
    {
        public static TreeView Builder(){
            // here we build the base gui
            TreeView tree = InitGUI();

            //start by building just the top layer again with the 
            //new logic addition.
            TreeStore fileListStore = buildTopLayer();
            
            // now i need to figure out how to go through every entry 
            // and display what is in each and every folder

            //complete tree and return..
            tree.Model = fileListStore;

            tree.RowActivated += (sender, args) =>
            {
                Console.WriteLine("Selection made: ");
                Gtk.TreeIter iter;
                fileListStore.GetIter(out iter, args.Path);

                var _file = fileListStore.GetValue(iter, 0);
                Console.WriteLine(_file.ToString());
            };

            tree.ShowAll();
            return tree;
        }

        private static TreeView InitGUI(){
            TreeView tree = new TreeView();
            tree.WidthRequest = 150;
            TreeViewColumn projectColumn = new TreeViewColumn();
            projectColumn.Title = "Project";
            CellRendererText projectNameCell = new CellRendererText();
            projectColumn.PackStart(projectNameCell, true);
            tree.AppendColumn(projectColumn);
            projectColumn.AddAttribute(projectNameCell, "text", 0);
            return tree;
        }

        private static TreeStore buildTopLayer(){
            TreeStore fileListStore = new TreeStore(typeof(string));
            TreeIter iter = new TreeIter();
            foreach (IFileSystemItem item in Session.CurrentSession.ProjectDirectory.subPaths)
            {
                if(item == item as PerleyDev_Directory){
                    var Item = item as PerleyDev_Directory;
                    iter = fileListStore.AppendValues(item.Name);
                    foreach (var e in Item.subPaths)
                    {
                        fileListStore.AppendValues(iter, e.Name);
                    }
                }
            }

            foreach (IFileSystemItem item in Session.CurrentSession.ProjectDirectory.subPaths)
            {
                if(item == item as PerleyDev_File){
                    var Item = item as PerleyDev_File;
                    fileListStore.AppendValues(Item.Name);
                }
            }

            return fileListStore;
        }
    }
}