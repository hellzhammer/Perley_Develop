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
        }

        private void InitGUI()
        {
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

        private void RecursiveBuilder(string path, TreeIter iter, TreeStore fileListStore)
        {
            if (Directory.Exists(path))
            {
                if (!this.Model.IterHasChild(iter))
                {
                    string[] new_dirs = DirectoryManager.GetDirectorySubdirectories(path);
                    string[] new_files = DirectoryManager.GetDirectoryFiles(path);

                    foreach (var folder in new_dirs)
                    {
                        PerleyDev_Directory pdir = new PerleyDev_Directory(folder);
                        var iter2 = fileListStore.AppendValues(iter, folder.Substring(folder.LastIndexOf("/")), pdir);
                        RecursiveBuilder(folder, iter2, fileListStore);
                    }
                    foreach (var file in new_files)
                    {
                        PerleyDev_File pfile = new PerleyDev_File(file);
                        fileListStore.AppendValues(iter, new FileInfo(file).Name, pfile);
                    }
                }
            }
        }

        private TreeStore buildTopLayer()
        {
            TreeStore fileListStore = new TreeStore(typeof(string), typeof(IFileSystemItem));
            TreeIter iter = new TreeIter();
            this.Model = fileListStore;
            foreach (IFileSystemItem item in Session.CurrentSession.ProjectDirectory.subPaths)
            {
                if (item == item as PerleyDev_Directory)
                {
                    var Item = item as PerleyDev_Directory;
                    iter = fileListStore.AppendValues(item.Name.Substring(item.Name.LastIndexOf("/")), Item);
                    RecursiveBuilder(Item.Path, iter, fileListStore);
                }
            }

            foreach (IFileSystemItem item in Session.CurrentSession.ProjectDirectory.subPaths)
            {
                if (item == item as PerleyDev_File)
                {
                    var Item = item as PerleyDev_File;
                    fileListStore.AppendValues(Item.Name, Item);
                }
            }

            this.ActivateOnSingleClick = true;
            this.Show();

            return fileListStore;
        }
    }
}
