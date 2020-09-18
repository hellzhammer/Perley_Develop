using System.Collections.Generic;
using Perley_Develop_IDE.GUI;
namespace Perley_Develop_IDE.Global 
{
    public class SystemComponents
    {
        public static ConsoleWidget IDE_Console { get; set;}
        public static List<TextEditorView> Open_Views { get; set; }
        public static TextEditorView ActiveView { get; set; }
    }
}