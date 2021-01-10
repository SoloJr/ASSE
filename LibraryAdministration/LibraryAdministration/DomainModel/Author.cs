using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAdministration.DomainModel
{
    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public DateTime DeathDate { get; set; }

        [Required]
        public string Country { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
