using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

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

        public virtual DbSet<BookRental> BookRentals { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }

        public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }

        public virtual DbSet<Reader> Readers { get; set; }
    }
}
