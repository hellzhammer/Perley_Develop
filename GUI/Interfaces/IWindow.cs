using Gtk;
namespace Perley_Develop_IDE.GUI
{
    public interface IWindow
    {
         void BuildApp();

         void Window_DeleteEvent(object sender, DeleteEventArgs a);
    }
}