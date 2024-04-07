using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public BookManager(IRepositoryManager manager,
            ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreateOneBook(Book book)
        {
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;    
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            Book book = _manager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);

            _manager.Book.Delete(book);
            _manager.Save();

        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            var books = _manager.Book.GetAllBooks(trackChanges);
            return books;   
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book =  _manager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);

            return book;
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var model = _manager.Book.GetOneBookById(id, trackChanges);

            if (model == null)
                throw new BookNotFoundException(id);       

            model.Title = book.Title;   
            model.Price = book.Price;
            _manager.Book.Update(model);
            _manager.Save();
        }
    }
}
