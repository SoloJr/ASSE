﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DomainModel;

namespace LibraryAdministration.Interfaces.DataAccess
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        ICollection<Publisher> GetAllBookPublishersOfABook(int bookId);
    }
}
