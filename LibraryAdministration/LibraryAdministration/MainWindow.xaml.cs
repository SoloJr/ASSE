using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Startup;
using Ninject;
using Ninject.Extensions.Logging;
using System.Windows;

namespace LibraryAdministration
{
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var kernel = Injector.Kernel;

            var loggerFactory = kernel.Get<ILoggerFactory>();
            var logger = loggerFactory.GetCurrentClassLogger();
            logger.Info("Entered in Main Window - new version - log4net with ninject");

            var bookService = kernel.Get<IBookService>();

            //var result = bookService.GetAllDomainsOfBook(2);

            var domainService = kernel.Get<IDomainService>();

            var _ = domainService.GetAllParentDomains(13);

            _ = domainService.GetAllParentDomains(7);

            InitializeComponent();
        }
    }
}
