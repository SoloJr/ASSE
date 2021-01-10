//----------------------------------------------------------------------
// <copyright file="LoanExtensionException.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Helper
{
    using System;

    /// <summary>
    /// Custom Exception class
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class LoanExtensionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoanExtensionException"/> class.
        /// </summary>
        public LoanExtensionException()
            : base("Cannot extend loan")
        {
        }
    }
}
