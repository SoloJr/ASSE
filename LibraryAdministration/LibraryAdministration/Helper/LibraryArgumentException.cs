using System;

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
