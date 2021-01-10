using System;

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
