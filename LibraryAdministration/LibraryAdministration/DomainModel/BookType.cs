//----------------------------------------------------------------------
// <copyright file="BookType.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DomainModel
{
    /// <summary>
    /// Book Types
    /// </summary>
    public enum BookType
    {
        /// <summary>
        /// The paperback
        /// </summary>
        Paperback = 0,

        /// <summary>
        /// The hardback
        /// </summary>
        Hardback = 1,

        /// <summary>
        /// The audiobook
        /// </summary>
        Audiobook = 2,

        /// <summary>
        /// The e-book
        /// </summary>
        Ebook = 3
    }
}
