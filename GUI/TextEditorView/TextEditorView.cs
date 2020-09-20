using System;
using System.IO;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

using Perley_Develop_Core_lib.FileSystem;
using Perley_Develop_Core_lib.PerleyDevelop_Core.FileSystem.Models;
using Perley_Develop_IDE.Global;
namespace Perley_Develop_IDE.GUI
{
    public class TextEditorView : TextView
    {
        public string ViewID { get; set; }
        public TextEditorView() : this(new Builder("TextEditorView.glade")) { }

        private TextEditorView(Builder builder) : base(builder.GetObject("TextEditorView").Handle)
        {
            builder.Autoconnect(this);
            this.ViewID = Guid.NewGuid().ToString();
            this.Buffer.Changed += (sender, args)=>{
                //if we start editing a view we want to set it as the active view. 
                
                //while typing we want to get when the spacebar is hit and then highlight text if needed. 
            };
        }

        public void LoadData(PerleyDev_File file){
            string[] data = File.ReadAllLines(file.Path);
            foreach (var line in data)
            {
                this.Buffer.Text += "\n" + line;              
            }
        }
    }
}
