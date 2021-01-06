using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.Business;
using LibraryAdministration.Startup;
using Ninject;
using Ninject.Extensions.Logging;

namespace LibraryAdministration
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
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
