using System;

namespace LibraryAdministrationTest.Mocks
{
    public class DeleteItemException : Exception
    {
        public DeleteItemException()
        {
            
        }

        public DeleteItemException(string name)
            : base($"Just a test mock for this: {name}")
        {

        }
    }
}