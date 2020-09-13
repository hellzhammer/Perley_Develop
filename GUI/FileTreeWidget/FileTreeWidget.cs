using System;
using System.IO;

using Gtk;

using Perley_Develop_Core_lib.App_Components;
using Perley_Develop_Core_lib.PerleyDevelop_Core.FileSystem.Models;
using Perley_Develop_Core_lib.PerleyDevelop_Core.FileSystem.Interfaces;
using Perley_Develop_Core_lib.FileSystem;

namespace Perley_Develop_IDE.GUI
{
    public class FileTreeWidget : TreeView
    {
        string titleBar { get; set; }
        int colMinW { get; set; }
        int colMaxW { get; set; }
        public FileTreeWidget(string title, int minWidth, int maxWidth) : this(new Builder("FileTreeWidget.glade")) 
        {
            this.titleBar = title;
            this.colMinW = minWidth;
            this.colMaxW = maxWidth;
        }

        private FileTreeWidget(Builder builder) : base(builder.GetObject("FileTreeWidget").Handle)
        {
            builder.Autoconnect(this);
            InitGUI();
            TreeStore tree = buildTopLayer();
            Build(tree);
        }

        private void InitGUI(){
            this.Expand = false;
            
            TreeViewColumn projectColumn = new TreeViewColumn();
            projectColumn.MinWidth = colMinW;
            projectColumn.MaxWidth = colMaxW;
            projectColumn.Title = titleBar;
            CellRendererText projectNameCell = new CellRendererText();
            projectColumn.PackStart(projectNameCell, true);
            this.AppendColumn(projectColumn);
            projectColumn.AddAttribute(projectNameCell, "text", 0);
        }

        private void Build(TreeStore fileListStore){
            this.Model = fileListStore;

            this.RowActivated += (sender, args) =>
            {
                Console.WriteLine("Selection made: ");
                Gtk.TreeIter iter;
                fileListStore.GetIter(out iter, args.Path);
                var _file = fileListStore.GetValue(iter, 0);
                Console.WriteLine(_file.ToString());

                if (Directory.Exists(_file.ToString()))
                {
                    if (!this.Model.IterHasChild(iter))
                    {
                        string[] new_dirs = DirectoryManager.GetDirectorySubdirectories(_file.ToString());
                        string[] new_files = DirectoryManager.GetDirectoryFiles(_file.ToString());

                        foreach (var folder in new_dirs)
                        {
                            fileListStore.AppendValues(iter, folder.Substring(folder.LastIndexOf("/")));

                        }
                        foreach (var file in new_files)
                        {
                            fileListStore.AppendValues(iter, new FileInfo(file).Name);
                        }
                    }
                }
            };

            this.ShowAll();
        }

        private TreeStore buildTopLayer()
        {
            
            TreeStore fileListStore = new TreeStore(typeof(string));
            TreeIter iter = new TreeIter();
            foreach (IFileSystemItem item in Session.CurrentSession.ProjectDirectory.subPaths)
            {
                if (item == item as PerleyDev_Directory)
                {
                    var Item = item as PerleyDev_Directory;
                    iter = fileListStore.AppendValues(item.Name.Substring(item.Name.LastIndexOf("/")));
                    foreach (var e in Item.subPaths)
                    {
                        if(e == e as PerleyDev_File){
                            fileListStore.AppendValues(iter, e.Name);
                        }
                        else{
                            fileListStore.AppendValues(iter, e.Name.Substring(e.Name.LastIndexOf("/")));
                        }
                    }
                }
            }

            foreach (IFileSystemItem item in Session.CurrentSession.ProjectDirectory.subPaths)
            {
                if (item == item as PerleyDev_File)
                {
                    var Item = item as PerleyDev_File;
                    fileListStore.AppendValues(Item.Name);
                }
            }

            return fileListStore;
        }
    }
}
