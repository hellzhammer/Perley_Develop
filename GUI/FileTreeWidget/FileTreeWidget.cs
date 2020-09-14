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

        private void Build(TreeStore fileListStore)
        {
            this.Model = fileListStore;
            this.ActivateOnSingleClick = true;
            this.RowExpanded += (sender, args) =>
            {
                Console.WriteLine("Expanding");
            };
            this.RowActivated += (sender, args) =>
            {
                Console.WriteLine("Selection made: ");
                Gtk.TreeIter iter;
                fileListStore.GetIter(out iter, args.Path);
                var _file = fileListStore.GetValue(iter, 0);
                IFileSystemItem selectedItem = fileListStore.GetValue(iter, 1) as IFileSystemItem;
                Console.WriteLine(selectedItem.Path);

                if (Directory.Exists(selectedItem.Path))
                {
                    if (!this.Model.IterHasChild(iter))
                    {
                        string[] new_dirs = DirectoryManager.GetDirectorySubdirectories(selectedItem.Path);
                        string[] new_files = DirectoryManager.GetDirectoryFiles(selectedItem.Path);

                        foreach (var folder in new_dirs)
                        {
                            PerleyDev_Directory pdir = new PerleyDev_Directory(folder);
                            var iter2 = fileListStore.AppendValues(iter, folder.Substring(folder.LastIndexOf("/")), pdir);
                            
                        }
                        foreach (var file in new_files)
                        {
                            PerleyDev_File pfile = new PerleyDev_File(file);
                            fileListStore.AppendValues(iter, new FileInfo(file).Name, pfile);
                        }
                    }
                }
            };

            this.ShowAll();
        }

        private TreeStore buildTopLayer()
        {
            TreeStore fileListStore = new TreeStore(typeof(string), typeof(IFileSystemItem));
            TreeIter iter = new TreeIter();
            foreach (IFileSystemItem item in Session.CurrentSession.ProjectDirectory.subPaths)
            {
                if (item == item as PerleyDev_Directory)
                {
                    var Item = item as PerleyDev_Directory;
                    iter = fileListStore.AppendValues(item.Name.Substring(item.Name.LastIndexOf("/")), Item);
                    foreach (var e in Item.subPaths)
                    {
                        if (e == e as PerleyDev_File)
                        {
                            var ee = e as PerleyDev_File;
                            fileListStore.AppendValues(iter, e.Name, ee);
                        }
                        else
                        {
                            var iter2 = fileListStore.AppendValues(iter, e.Path.Substring(e.Path.LastIndexOf("/")), Item);
                        }
                    }
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

            return fileListStore;
        }
    }
}
