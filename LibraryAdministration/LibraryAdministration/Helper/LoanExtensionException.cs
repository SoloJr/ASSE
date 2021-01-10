using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAdministration.Helper
{
    public class LoanExtensionException : Exception
    {
        public LoanExtensionException()
            : base("Cannot extend loan")
        {

        }
    }
}
