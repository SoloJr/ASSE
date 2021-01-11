//----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration
{
    using System.Windows;
    using Interfaces.Business;
    using Ninject;
    using Ninject.Extensions.Logging;
    using Startup;

    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            var kernel = Injector.Kernel;

            var loggerFactory = kernel.Get<ILoggerFactory>();
            var logger = loggerFactory.GetCurrentClassLogger();
            logger.Info("Entered in Main Window - new version - log4net with ninject");

            this.InitializeComponent();
        }
    }
}
