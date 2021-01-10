//-----------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration
{
    using System.Windows;
    using Startup;

    /// <summary>
    /// Interaction logic for App
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            Injector.Inject(new Bindings());
            base.OnStartup(e);
        }
    }
}
