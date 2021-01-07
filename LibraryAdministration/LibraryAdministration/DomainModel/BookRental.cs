using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAdministration.DomainModel
{
    public class BookRental
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public int ForRent { get; set; }

        public int RentBookPublisherId { get; set; }

        [ForeignKey("RentBookPublisherId")]
        public BookPublisher BookPublisher { get; set; }
    }
}
