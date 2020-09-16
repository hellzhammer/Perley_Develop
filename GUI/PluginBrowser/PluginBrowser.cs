using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Perley_Develop_IDE.GUI.PluginBrowser
{
    public class PluginBrowser : Window
    {
        [UI] private Box box = null;
        public PluginBrowser() : this(new Builder("PluginBrowser.glade")) { }

        private PluginBrowser(Builder builder) : base(builder.GetObject("PluginBrowser").Handle)
        {
            builder.Autoconnect(this);
            ViewBuilder();
        }

        void ViewBuilder()
        {
            this.SetSizeRequest(500, 200);

            Gtk.TreeView tree = new Gtk.TreeView();
            this.box.Add(tree);

            Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn();
            artistColumn.Title = "Plugin";

            Gtk.CellRendererText artistNameCell = new Gtk.CellRendererText();

            artistColumn.PackStart(artistNameCell, true);

            Gtk.TreeViewColumn songColumn = new Gtk.TreeViewColumn();
            songColumn.Title = "Active";

            Gtk.CellRendererToggle songTitleCell = new Gtk.CellRendererToggle();
            songColumn.PackStart(songTitleCell, false);
            tree.AppendColumn(artistColumn);
            tree.AppendColumn(songColumn);

            artistColumn.AddAttribute(artistNameCell, "text", 0);
            //songColumn.AddAttribute(songTitleCell, "toggle", 1);

            Gtk.TreeStore musicListStore = new Gtk.TreeStore(typeof(string), typeof(bool));
            bool test1 = true;
            Gtk.TreeIter iter = musicListStore.AppendValues("Dance", test1);
            //musicListStore.AppendValues(iter, "file1", false);
            bool test2 = false;
            iter = musicListStore.AppendValues("Hip-hop", test2);
            //musicListStore.AppendValues(iter, "file2", true);

            tree.Model = musicListStore;

            this.box.ShowAll();
        }
    }
}
