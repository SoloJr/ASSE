//----------------------------------------------------------------------
// <copyright file="BookPublisher.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// BookPublisher class
    /// </summary>
    public class BookPublisher
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the pages.
        /// </summary>
        /// <value>
        /// The pages.
        /// </value>
        [Required]
        public int Pages { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [Required]
        public BookType Type { get; set; }

        /// <summary>
        /// Gets or sets the release date.
        /// </summary>
        /// <value>
        /// The release date.
        /// </value>
        [Required]
        public DateTime ReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the rent count.
        /// </summary>
        /// <value>
        /// The rent count.
        /// </value>
        [Required]
        public int RentCount { get; set; }

        /// <summary>
        /// Gets or sets for rent.
        /// </summary>
        /// <value>
        /// For rent.
        /// </value>
        [Required]
        public int ForRent { get; set; }

        /// <summary>
        /// Gets or sets for lecture.
        /// </summary>
        /// <value>
        /// For lecture.
        /// </value>
        [Required]
        public int ForLecture { get; set; }

        /// <summary>
        /// Gets or sets the book identifier.
        /// </summary>
        /// <value>
        /// The book identifier.
        /// </value>
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets the book.
        /// </summary>
        /// <value>
        /// The book.
        /// </value>
        [ForeignKey("BookId")]
        public Book Book { get; set; }

        /// <summary>
        /// Gets or sets the publisher identifier.
        /// </summary>
        /// <value>
        /// The publisher identifier.
        /// </value>
        public int PublisherId { get; set; }

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        /// <value>
        /// The publisher.
        /// </value>
        [ForeignKey("PublisherId")]
        public Publisher Publisher { get; set; }
    }
}
