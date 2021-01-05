using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAdministration.DomainModel
{
    public class Publisher
    {
        public Publisher()
        {
            this.Books = new List<BookPublisher>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public DateTime FoundingDate { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string Headquarter { get; set; }

        public List<BookPublisher> Books { get; set; }
    }
}
