﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryAdministration.DataMapper;
using LibraryAdministration.DomainModel;
using LibraryAdministration.Interfaces.DataAccess;

namespace LibraryAdministration.DataAccessLayer
{
    public class PublisherRepository : BaseRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(LibraryContext context)
            : base(context)
        {

        }

        public ICollection<Publisher> GetAllBookPublishersOfABook(int bookId)
        {
            var bookPublishers = _context.BookPublisher.Where(x => x.BookId == bookId).ToList();
            var list = new List<Publisher>();

            bookPublishers.ForEach(x => list.Add(x.Publisher));

            return list;
        }
    }
}
