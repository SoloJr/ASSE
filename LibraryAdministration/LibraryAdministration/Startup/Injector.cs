//----------------------------------------------------------------------
// <copyright file="Injector.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Startup
{
    using System;
    using System.Reflection;
    using Ninject;
    using Ninject.Extensions.Logging.Log4net;
    using Ninject.Modules;

    /// <summary>
    /// Injector class
    /// </summary>
    public class Injector
    {
        /// <summary>
        /// The kernel
        /// </summary>
        private static IKernel kernel;

        /// <summary>
        /// Gets the kernel.
        /// </summary>
        /// <value>
        /// The kernel.
        /// </value>
        /// <exception cref="ArgumentNullException">Injection method should be called first!</exception>
        public static IKernel Kernel
        {
            get
            {
                if (kernel == null)
                {
                    throw new ArgumentNullException("Injection method should be called first!");
                }

                return kernel;
            }
        }

        /// <summary>
        /// Injects the specified bindings.
        /// </summary>
        /// <param name="bindings">The bindings.</param>
        public static void Inject(NinjectModule bindings)
        {
            log4net.Config.XmlConfigurator.Configure();
            var settings = new NinjectSettings { LoadExtensions = false };
            kernel = new StandardKernel(settings, new INinjectModule[] { new Log4NetModule(), bindings });
            kernel.Load(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <typeparam name="T">anything injected</typeparam>
        /// <returns>The kernel</returns>
        public static T Get<T>()
        {
            return kernel.Get<T>();
        }
    }
}
