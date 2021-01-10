//-----------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using DataMapper;
    using DomainModel;
    using Interfaces.DataAccess;

    /// <summary>
    /// Employee Repository class
    /// </summary>
    /// <seealso cref="LibraryAdministration.DataAccessLayer.BaseRepository{LibraryAdministration.DomainModel.Employee}" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IEmployeeRepository" />
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EmployeeRepository(LibraryContext context)
            : base(context)
        {
        }
    }
}
