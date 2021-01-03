using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAdministration.Test
{
    public class Test : ITest
    {
        public void Tester()
        {
            Debug.Write("Bagi le pule");
        }
    }

    public interface ITest
    {
        void Tester();
    }
}
