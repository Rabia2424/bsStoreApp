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

        public BookManager(IRepositoryManager manager)
        {
            _manager = manager;
        }

        public Book CreateOneBook(Book book)
        {
            if(book is null)
                throw new ArgumentNullException(nameof(book));

            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;    
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            Book book = _manager.Book.GetOneBookById(id, trackChanges);
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
            return _manager.Book.GetOneBookById(id, trackChanges);
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var model = _manager.Book.GetOneBookById(id, trackChanges);
            model.Title = book.Title;   
            model.Price = book.Price;
            _manager.Book.Update(model);
            _manager.Save();
        }
    }
}
