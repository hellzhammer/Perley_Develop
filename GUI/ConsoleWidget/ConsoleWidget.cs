using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace GtkNamespace
{
    public enum ConsoleType {
        Package, 
        IDE,
        Debug
    }
    public class ConsoleWidget : TextView
    {
        public ConsoleType type { get; private set; }
        public ConsoleWidget() : this(new Builder("ConsoleWidget.glade")) {}
        
        private ConsoleWidget(Builder builder) : base(builder.GetObject("ConsoleWidget").Handle)
        {
            builder.Autoconnect(this);

            this.Editable = false;
        }

        public void ClearConsole(){
            this.Buffer.Text = "";
        }

        public void DisplayConsoleMessage(string title, string description, DateTime timeStamp){
            this.Buffer.Text += "\n";
            this.Buffer.Text += title + "\n";
            this.Buffer.Text += "\t Description: " + description + "\n";
            this.Buffer.Text += "\t Time:" + timeStamp + "\n";
        }
    }
}
