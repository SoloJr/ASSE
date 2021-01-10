using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryAdministration.DomainModel
{
    public class Reader
    {
        public Reader()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 20)]
        public string Address { get; set; }

        public int ReaderPersonalInfoId { get; set; }

        [ForeignKey("ReaderPersonalInfoId")]
        public PersonalInfo Info { get; set; }
    }
}
