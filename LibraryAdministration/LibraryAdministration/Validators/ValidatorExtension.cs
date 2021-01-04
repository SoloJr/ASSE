using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Validators
{
    public static class ValidatorExtension
    {
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

            return pi.Email != "" || pi.PhoneNumber != "";
        }
    }
}
