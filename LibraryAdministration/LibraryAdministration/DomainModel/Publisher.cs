//----------------------------------------------------------------------
// <copyright file="Publisher.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Publisher Class
    /// </summary>
    public class Publisher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Publisher"/> class.
        /// </summary>
        public Publisher()
        {
            this.Books = new List<BookPublisher>();
        }

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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the founding date.
        /// </summary>
        /// <value>
        /// The founding date.
        /// </value>
        [Required]
        public DateTime FoundingDate { get; set; }

        /// <summary>
        /// Gets or sets the headquarter.
        /// </summary>
        /// <value>
        /// The headquarter.
        /// </value>
        [StringLength(30, MinimumLength = 3)]
        public string Headquarter { get; set; }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public List<BookPublisher> Books { get; set; }
    }
}
