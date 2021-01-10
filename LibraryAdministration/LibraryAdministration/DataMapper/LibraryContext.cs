using LibraryAdministration.DomainModel;
using System.Data.Entity;

namespace LibraryAdministration.DataMapper
{
    public class LibraryContext : DbContext
    {
        public LibraryContext()
            : base("libraryConnectionString")
        {

        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<BookPublisher> BookPublisher { get; set; }

        public virtual DbSet<Domain> Domains { get; set; }

        public virtual DbSet<Publisher> Publishers { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }

        public virtual DbSet<Reader> Readers { get; set; }

        public virtual DbSet<ReaderBook> ReaderBooks { get; set; }
    }
}
