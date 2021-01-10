//----------------------------------------------------------------------
// <copyright file="LibraryArgumentException.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Helper
{
    using System;

    /// <summary>
    /// Custom exception type
    /// </summary>
    /// <seealso cref="System.ArgumentException" />
    public class LibraryArgumentException : ArgumentException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryArgumentException"/> class.
        /// </summary>
        public LibraryArgumentException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryArgumentException"/> class.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        public LibraryArgumentException(string propName)
            : base($"This parameter can't be null: {propName}")
        {
        }
    }
}
