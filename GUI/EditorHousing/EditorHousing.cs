using System.Collections;
using System.Drawing;
using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Perley_Develop_IDE.GUI
{
    public class EditorHousing : Notebook
    {
        public Gtk.Label CaptionLbl { get; set; }
        public Gtk.ToolButton CloseBtn { get; set; }

        public EditorHousing() : this(new Builder("EditorHousing.glade")) { }

        private EditorHousing(Builder builder) : base(builder.GetObject("EditorHousing").Handle)
        {
            builder.Autoconnect(this);
            ViewBuilder();
        }
        private void ViewBuilder()
        {
            (Widget i1, Widget i2) housing = BuildHousing();
            this.AppendPage(housing.i1, housing.i2);
            this.ShowAll();
        }
        public void AddEditor(){
            (Widget i1, Widget i2) housing = BuildHousing();
            this.AppendPage(housing.i1, housing.i2);
            this.ShowAll();
        }
        private (Widget i1, Widget i2) BuildHousing(){
            //todo fix this shit
            //text editor instance
            TextEditorView te = new TextEditorView();

            //build the tab label widget
            HBox box = BuildTabLabel();

            //this.InsertPage(te, box, 0);
            //todo remove below
            //add to notebook
            //this.AppendPage(te, box);
            //todo end remove
            return (te, box);
        }

        private HBox BuildTabLabel(){
            //build the tab label widget
            HBox box = new HBox(false, 0);
            CaptionLbl = new Gtk.Label("something");
            var img = new Image(Environment.CurrentDirectory + "/x.png");
            CloseBtn = new Gtk.ToolButton(img, "x");
            CloseBtn.Clicked += (sender, args) => {
                Console.WriteLine("I am fucking alive!");
            };
            //pack it up
            box.PackStart(CaptionLbl, false, false, 0);
            box.Add(CloseBtn);
            box.ShowAll();
            return box;
        }
    }
}
