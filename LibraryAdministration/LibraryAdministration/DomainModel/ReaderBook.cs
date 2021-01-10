using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAdministration.DomainModel
{
    public class ReaderBook
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime LoanDate { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public DateTime? LoanReturnDate { get; set; }

        public int? ExtensionDays { get; set; }

        public int BookPublisherId { get; set; }

        [ForeignKey("BookPublisherId")]
        public BookPublisher BookPublisher { get; set; }

        public int ReaderId { get; set; }

        [ForeignKey("ReaderId")]
        public Reader Reader { get; set; }
    }
}
