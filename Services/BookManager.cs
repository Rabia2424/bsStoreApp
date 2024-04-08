using AutoMapper;
using Entities.DTOs;
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
        private readonly IMapper _mapper;

		public BookManager(IRepositoryManager manager,
			ILoggerService logger,
			IMapper mapper)
		{
			_manager = manager;
			_logger = logger;
			_mapper = mapper;
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

        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
            var books = _manager.Book.GetAllBooks(trackChanges);
			var bookDto = _mapper.Map<IEnumerable<BookDto>>(books);
            return bookDto;
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book =  _manager.Book.GetOneBookById(id, trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);

            return book;
        }

        public void UpdateOneBook(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            var model = _manager.Book.GetOneBookById(id, trackChanges);

            if (model == null)
                throw new BookNotFoundException(id);

            //Mapping
            //model.Title = book.Title;   
            //model.Price = book.Price;

            model = _mapper.Map<Book>(bookDto);
            //_mapper.Map(bookDto,model);

            _manager.Book.Update(model);
            _manager.Save();
        }
    }
}
