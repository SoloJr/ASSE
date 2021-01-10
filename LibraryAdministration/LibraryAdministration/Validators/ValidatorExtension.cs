//----------------------------------------------------------------------
// <copyright file="ValidatorExtension.cs" company="Transilvania University of Brasov">
//     Mircea Solovastru
// </copyright>
//-----------------------------------------------------------------------

namespace LibraryAdministration.Validators
{
    using DomainModel;

    /// <summary>
    /// Extensions for validators
    /// </summary>
    public static class ValidatorExtension
    {
        /// <summary>
        /// Checks the personal information null or empty.
        /// </summary>
        /// <param name="pi">The pi.</param>
        /// <returns>boolean value</returns>
        public static bool CheckPersonalInfoNullOrEmpty(PersonalInfo pi)
        {
            if (pi == null)
            {
                return false;
            }

            if (pi.Email == null && pi.PhoneNumber == null)
            {
                return false;
            }

            return pi.Email != string.Empty || pi.PhoneNumber != string.Empty;
        }
    }
}
