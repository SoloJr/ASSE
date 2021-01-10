//----------------------------------------------------------------------
// <copyright file="ReaderBook.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Reader Book Class
    /// </summary>
    public class ReaderBook
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
        /// Gets or sets the loan date.
        /// </summary>
        /// <value>
        /// The loan date.
        /// </value>
        [Required]
        public DateTime LoanDate { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        [Required]
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the loan return date.
        /// </summary>
        /// <value>
        /// The loan return date.
        /// </value>
        public DateTime? LoanReturnDate { get; set; }

        /// <summary>
        /// Gets or sets the extension days.
        /// </summary>
        /// <value>
        /// The extension days.
        /// </value>
        public int? ExtensionDays { get; set; }

        /// <summary>
        /// Gets or sets the book publisher identifier.
        /// </summary>
        /// <value>
        /// The book publisher identifier.
        /// </value>
        public int BookPublisherId { get; set; }

        /// <summary>
        /// Gets or sets the book publisher.
        /// </summary>
        /// <value>
        /// The book publisher.
        /// </value>
        [ForeignKey("BookPublisherId")]
        public BookPublisher BookPublisher { get; set; }

        /// <summary>
        /// Gets or sets the reader identifier.
        /// </summary>
        /// <value>
        /// The reader identifier.
        /// </value>
        public int ReaderId { get; set; }

        /// <summary>
        /// Gets or sets the reader.
        /// </summary>
        /// <value>
        /// The reader.
        /// </value>
        [ForeignKey("ReaderId")]
        public Reader Reader { get; set; }
    }
}
