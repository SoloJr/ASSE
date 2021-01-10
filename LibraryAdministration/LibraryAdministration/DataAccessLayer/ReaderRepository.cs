//-----------------------------------------------------------------------
// <copyright file="ReaderRepository.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.DataAccessLayer
{
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Interfaces.DataAccess;

    /// <summary>
    /// Reader Repository class
    /// </summary>
    /// <seealso cref="LibraryAdministration.DataAccessLayer.BaseRepository{LibraryAdministration.DomainModel.Reader}" />
    /// <seealso cref="LibraryAdministration.Interfaces.DataAccess.IReaderRepository" />
    public class ReaderRepository : BaseRepository<Reader>, IReaderRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ReaderRepository(LibraryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Checks the employee status.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns>boolean value</returns>
        public bool CheckEmployeeStatus(int readerId, int employeeId)
        {
            var reader = Context.Readers.FirstOrDefault(x => x.Id == readerId);
            var employee = Context.Employees.FirstOrDefault(x => x.Id == employeeId);

            if (reader == null || employee == null)
            {
                return false;
            }

            return reader.ReaderPersonalInfoId == employee.EmployeePersonalInfoId;
        }
    }
}
