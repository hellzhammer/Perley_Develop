using Gtk;
public class MainViewBuilder{
    public MenuBar BuildMenu(){
            MenuBar mb = new MenuBar();
            Menu filemenu = new Menu();
            MenuItem file = new MenuItem("File");
            file.Submenu = filemenu;
            MenuItem _new = new MenuItem("New File");
            filemenu.Append(_new);
            MenuItem open = new MenuItem("Open File");
            filemenu.Append(open);
            MenuItem openFolder = new MenuItem("Open Folder");
            filemenu.Append(openFolder);
            MenuItem close = new MenuItem("Close File");
            filemenu.Append(close);
            MenuItem closeFolder = new MenuItem("Close Folder");
            filemenu.Append(closeFolder);
            MenuItem exit = new MenuItem("Exit App");
            filemenu.Append(exit);
            mb.Append(file);

            Menu editmenu = new Menu();
            MenuItem emenu = new MenuItem("Edit");
            emenu.Submenu = editmenu;
            MenuItem prefs = new MenuItem("Preferences");
            editmenu.Append(prefs);
            mb.Append(emenu);
            return mb;
        }

        public VPaned BuildConsole(){
            VPaned pane = new VPaned();
              pane.Add1(new Label("Console"));
              pane.Add2(new TextView());
              pane.HeightRequest = 100;
              return pane;
        }
        public HBox BuildFooter(){
            HBox hbox = new HBox();
            hbox.PackStart(new Label("Progress:"), false, false, 10);
            var prog = new ProgressBar();
            hbox.PackStart(prog, false, false, 10);
            hbox.PackStart(new Label("Some output would go here."), false, false, 10);
            return hbox;
        }
        public HBox BuildTextEditor(){
            Notebook n = new Notebook();
              
              var te = new TextView();
              var errorTag = new TextTag ("error");
                errorTag.Foreground = "blue";
                errorTag.Weight = Pango.Weight.Bold;
                te.Buffer.TagTable.Add(errorTag);
              string text = "foo";

              // Insert text with tag.
              TextIter start = te.Buffer.EndIter;
              te.Buffer.InsertWithTags (ref start, text, errorTag);
              te.HeightRequest = 500;
              n.InsertPage(te, new Label("test"), 0);

              var tree = BuildFileTree();
              tree.WidthRequest = 100;
              HBox _hbox = new HBox();
              _hbox.PackStart(tree, false, true, 10);
              _hbox.Add(n);

              return _hbox;
        }

        private TreeView BuildFileTree(){
            Gtk.TreeView tree = new Gtk.TreeView ();
            Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
            artistColumn.Title = "File Tree";
            tree.AppendColumn (artistColumn);
            Gtk.ListStore musicListStore = new Gtk.ListStore (typeof (string), typeof (string));
            tree.Model = musicListStore;
            return tree;
        }
}