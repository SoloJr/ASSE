using LibraryAdministration.Startup;
using System.Windows;

namespace LibraryAdministration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            Injector.Inject(new Bindings());
            base.OnStartup(e);
        }
    }
}
