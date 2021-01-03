using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAdministration.DomainModel
{
    public class Book
    {
        public Book()
        {
            this.Authors = new HashSet<Author>();
            this.Readers = new HashSet<Reader>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Language { get; set; }

        public List<BookPublisher> Publishers { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Reader> Readers { get; set; }
    }
}
