using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAdministration.DomainModel
{
    public class Domain
    {
        public Domain()
        {
            this.Books = new HashSet<Book>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int? EntireDomainId { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
