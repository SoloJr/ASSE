using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.Test;
using Ninject;
using Ninject.Extensions.Logging;
using Ninject.Modules;

namespace LibraryAdministration.Startup
{
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ITest>().To<Test.Test>();
        }
    }
}
