using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace Perley_Develop_IDE.GUI
{
    public enum ConsoleType {
        Package, 
        IDE,
        Debug
    }
    public class ConsoleWidget : TextView
    {
        public ConsoleType type { get; private set; }
        public ConsoleWidget(ConsoleType _type) : this(new Builder("ConsoleWidget.glade")) {
            this.type = _type;
        }
        
        private ConsoleWidget(Builder builder) : base(builder.GetObject("ConsoleWidget").Handle)
        {
            builder.Autoconnect(this);

            this.Editable = false;
            this.HeightRequest = 150;
        }

        public void ClearConsole(){
            this.Buffer.Text = "";
        }

        public void DisplayConsoleMessage(string title, string description, DateTime timeStamp, string stackTrace = null){
            this.Buffer.Text += "\n";
            this.Buffer.Text += title + "\n";
            this.Buffer.Text += "\t Description: " + description + "\n";
            if(!string.IsNullOrEmpty(stackTrace)){
                this.Buffer.Text += "\t Stack Trace: " + stackTrace + "\n";
            }
            this.Buffer.Text += "\t Time:" + timeStamp + "\n";
        }
    }
}
