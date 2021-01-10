using Ninject;
using Ninject.Extensions.Logging.Log4net;
using Ninject.Modules;
using System;
using System.Reflection;

namespace LibraryAdministration.Startup
{
    public class Injector
    {
        private static IKernel _kernel;
        public static IKernel Kernel
        {
            get
            {
                if (_kernel == null)
                {
                    throw new ArgumentNullException("Injection method should be called first!");
                }

                return _kernel;
            }
        }

        public static void Inject(NinjectModule bindings)
        {
            log4net.Config.XmlConfigurator.Configure();
            var settings = new NinjectSettings { LoadExtensions = false };
            _kernel = new StandardKernel(settings, new INinjectModule[] { new Log4NetModule(), bindings });
            _kernel.Load(Assembly.GetExecutingAssembly());
        }

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }
    }
}
