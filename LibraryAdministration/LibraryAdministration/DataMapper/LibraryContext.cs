//----------------------------------------------------------------------
// <copyright file="LibraryContext.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataMapper
{
    using System.Data.Entity;
    using DomainModel;

    /// <summary>
    /// Library Context
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class LibraryContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryContext"/> class.
        /// </summary>
        public LibraryContext()
            : base("libraryConnectionString")
        {
        }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        public virtual DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the book publisher.
        /// </summary>
        /// <value>
        /// The book publisher.
        /// </value>
        public virtual DbSet<BookPublisher> BookPublisher { get; set; }

        /// <summary>
        /// Gets or sets the domains.
        /// </summary>
        /// <value>
        /// The domains.
        /// </value>
        public virtual DbSet<Domain> Domains { get; set; }

        /// <summary>
        /// Gets or sets the publishers.
        /// </summary>
        /// <value>
        /// The publishers.
        /// </value>
        public virtual DbSet<Publisher> Publishers { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public virtual DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// Gets or sets the personal info.
        /// </summary>
        /// <value>
        /// The personal info.
        /// </value>
        public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }

        /// <summary>
        /// Gets or sets the readers.
        /// </summary>
        /// <value>
        /// The readers.
        /// </value>
        public virtual DbSet<Reader> Readers { get; set; }

        /// <summary>
        /// Gets or sets the reader books.
        /// </summary>
        /// <value>
        /// The reader books.
        /// </value>
        public virtual DbSet<ReaderBook> ReaderBooks { get; set; }
    }
}
