using System;
using Gtk;
using Perley_Develop_IDE.GUI;
public class MainViewBuilder
{
    public MenuBar BuildMenu(Window win)
    {
        MenuBar mb = new MenuBar();
        Menu filemenu = new Menu();
        MenuItem file = new MenuItem("File");
        file.Submenu = filemenu;
        /*
        MenuItem _new = new MenuItem("New File");
        filemenu.Append(_new);
        MenuItem open = new MenuItem("Open File");
        filemenu.Append(open);
        MenuItem close = new MenuItem("Close File");
        filemenu.Append(close);
        MenuItem closeFolder = new MenuItem("Close Folder");
        filemenu.Append(closeFolder);
        */
        /*
        todo there is some work that needs to be done before i can actually implement this.
        MenuItem openFolder = new MenuItem("Open Folder");
        openFolder.Activated += (sender, args) =>
        {
            string dir = OpenDirectory(win);
            string[] dirs = DirectoryManager.GetDirectorySubdirectories(dir);
            foreach (var folder in dirs)
            {
                Console.WriteLine(folder);
            }
        };

        filemenu.Append(openFolder);
        */

        MenuItem exit = new MenuItem("Exit App");
        exit.Activated += (sender, args) =>
        {
            Application.Quit();
        };
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
    public VPaned BuildConsole()
    {
        VPaned pane = new VPaned();
        pane.Add1(new Label("Console"));
        pane.Add2(new TextView());
        pane.HeightRequest = 100;
        return pane;
    }
    public HBox BuildFooter()
    {
        HBox hbox = new HBox();
        hbox.PackStart(new Label("Progress:"), false, false, 10);
        var prog = new ProgressBar();
        hbox.PackStart(prog, false, false, 10);
        hbox.PackStart(new Label("Some output would go here."), false, false, 10);
        return hbox;
    }
    public HPaned BuildTextEditor()
    {
        var tree = new FileTreeWidget("Hello Project", 75, 120);
        HPaned paned = new HPaned();
        ScrolledWindow scroll = new ScrolledWindow();
        scroll.Add(tree);
        paned.Pack1(scroll, false, false);

        ScrolledWindow scrollw = new ScrolledWindow();
        scrollw.HeightRequest = 550;
        scrollw.Add(new EditorHousing());
        
        paned.Add2(scrollw); // text editor
        return paned;
    }
    public string OpenDirectory(Window win)
    {
        string rtnval = string.Empty;
        Gtk.FileChooserDialog dirchooser =
        new Gtk.FileChooserDialog("Choose Directory To Open",
          win,
          FileChooserAction.SelectFolder,
          "Cancel", ResponseType.Cancel,
          "Open", ResponseType.Accept);

        if (dirchooser.Run() == (int)ResponseType.Accept)
        {
            Console.WriteLine(dirchooser.CurrentFolder);
            rtnval = dirchooser.CurrentFolder;
        }
        else
        {
            rtnval = null;
        }

        dirchooser.Dispose();
        return rtnval;
    }
}