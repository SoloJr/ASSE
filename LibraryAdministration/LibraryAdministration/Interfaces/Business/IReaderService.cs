﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.Business
{
    public interface IReaderService : IService<Reader>
    {
        bool CheckEmployeeStatus(int readerId, int employeeId);
    }
}
