//-----------------------------------------------------------------------
// <copyright file="ReaderService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using DataAccessLayer;
    using DataMapper;
    using DomainModel;
    using Helper;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Validators;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="LibraryAdministration.BusinessLayer.BaseService{LibraryAdministration.DomainModel.Reader, LibraryAdministration.Interfaces.DataAccess.IReaderRepository}" />
    /// <seealso cref="LibraryAdministration.Interfaces.Business.IReaderService" />
    public class ReaderService : BaseService<Reader, IReaderRepository>, IReaderService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderService"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ReaderService(LibraryContext context)
            : base(new ReaderRepository(context), new ReaderValidator())
        {

        }

        /// <summary>
        /// Checks the employee status.
        /// </summary>
        /// <param name="readerId">The reader identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        /// <exception cref="LibraryArgumentException">
        /// readerId
        /// or
        /// employeeId
        /// </exception>
        public bool CheckEmployeeStatus(int readerId, int employeeId)
        {
            if (readerId <= 0)
            {
                throw new LibraryArgumentException(nameof(readerId));
            }

            if (employeeId <= 0)
            {
                throw new LibraryArgumentException(nameof(employeeId));
            }

            return _repository.CheckEmployeeStatus(readerId, employeeId);
        }
    }
}
