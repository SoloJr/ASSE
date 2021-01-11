//-----------------------------------------------------------------------
// <copyright file="ReaderService.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.BusinessLayer
{
    using System.Collections.Generic;
    using DataAccessLayer;
    using DataMapper;
    using DomainModel;
    using Helper;
    using Interfaces.Business;
    using Interfaces.DataAccess;
    using Validators;

    /// <summary>
    /// ReaderService class
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
        /// <returns>boolean value</returns>
        /// <exception cref="LibraryArgumentException">
        /// readerId
        /// or
        /// employeeId
        /// </exception>
        public bool CheckEmployeeStatus(int readerId, int employeeId)
        {
            if (readerId <= 0)
            {
                logger.Error($"{this.GetType()}: CheckEmployeeStatus, param error: {readerId}");
                throw new LibraryArgumentException(nameof(readerId));
            }

            if (employeeId <= 0)
            {
                logger.Error($"{this.GetType()}: CheckEmployeeStatus, param error: {employeeId}");
                throw new LibraryArgumentException(nameof(employeeId));
            }

            return Repository.CheckEmployeeStatus(readerId, employeeId);
        }

        /// <summary>
        /// Gets all readers that have phone numbers.
        /// </summary>
        /// <returns>Employee list</returns>
        public List<Reader> GetAllEmployeesThatHavePhoneNumbers()
        {
            return Repository.GetAllEmployeesThatHavePhoneNumbers();
        }

        /// <summary>
        /// Gets all readers that have emails.
        /// </summary>
        /// <returns>Employee list</returns>
        public List<Reader> GetAllEmployeesThatHaveEmails()
        {
            return Repository.GetAllEmployeesThatHaveEmails();
        }

        /// <summary>
        /// Gets the readers that have email and phone numbers set.
        /// </summary>
        /// <returns>Employee list</returns>
        public List<Reader> GetEmployeesThatHaveEmailAndPhoneNumbersSet()
        {
            return Repository.GetEmployeesThatHaveEmailAndPhoneNumbersSet();
        }
    }
}
