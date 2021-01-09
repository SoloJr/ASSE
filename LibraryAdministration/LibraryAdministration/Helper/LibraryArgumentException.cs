using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAdministration.Helper
{
    public class LibraryArgumentException : ArgumentException
    {
        public LibraryArgumentException()
        {

        }

        public LibraryArgumentException(string propName)
            : base($"This parameter can't be null: {propName}")
        {

        }
    }
}
