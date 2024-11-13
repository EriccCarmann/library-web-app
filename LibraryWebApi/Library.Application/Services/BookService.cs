using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Application.DTOs.BookDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Library.Application.UseCases.BookUseCases;

namespace Library.Application.Services
{
    public class BookService
    {
        private readonly GetAllBooksUseCase _getAllBooksUseCase;
        private readonly GetBookByIdUseCase _getBookByIdUseCase;
        private readonly GetBookByISBNUseCase _getBookByISBNUseCase;
        private readonly CreateBookUseCase _createBookUseCase;
        private readonly UpdateBookUseCase _updateBookUseCase;
        private readonly DeleteBookUseCase _deleteBookUseCase;
        private readonly TakeBookUseCase _takeBookUseCase;
        private readonly GetTakenBooksUseCase _getTakenBooksUseCase;
        private readonly ReturnBookUseCase _returnBookUseCase;
        private readonly AddCoverUseCase _addCoverUseCase;

        public BookService(
            GetAllBooksUseCase getAllBooksUseCase,
            GetBookByIdUseCase getBookByIdUseCase,
            GetBookByISBNUseCase getBookByISBNUseCase,
            CreateBookUseCase createBookUseCase,
            UpdateBookUseCase updateBookUseCase,
            DeleteBookUseCase deleteBookUseCase,
            TakeBookUseCase takeBookUseCase,
            GetTakenBooksUseCase getTakenBooksUseCase,
            ReturnBookUseCase returnBookUseCase,
            AddCoverUseCase addCoverUseCase)
        {
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _getBookByISBNUseCase = getBookByISBNUseCase;
            _createBookUseCase = createBookUseCase;
            _updateBookUseCase = updateBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
            _takeBookUseCase = takeBookUseCase;
            _getTakenBooksUseCase = getTakenBooksUseCase;
            _returnBookUseCase = returnBookUseCase;
            _addCoverUseCase = addCoverUseCase;
        }

        public async Task<IEnumerable<Book>> GetAll([FromQuery] QueryObject queryObject)
        {
            return await _getAllBooksUseCase.GetAll(queryObject);
        }

        public async Task<Book> GetById([FromRoute] int id)
        {
            return await _getBookByIdUseCase.GetById(id);
        }

        public async Task<Book> GetByISBN(string ISBN)
        {
            return await _getBookByISBNUseCase.GetByISBN(ISBN);
        }

        public async Task<BookReadDto> CreateBook(BookCreateDto data)
        {
            return await _createBookUseCase.CreateBook(data);
        }

        public async Task<BookReadDto> UpdateBook([FromRoute] int id, [FromBody] BookUpdateDto bookUpdatingDto)
        {
            return await _updateBookUseCase.UpdateBook(id, bookUpdatingDto);
        }

        public async Task DeleteBook([FromRoute] int id)
        {
            await _deleteBookUseCase.DeleteBook(id);
        }

        public async Task<Book> TakeBook(string bookTitle, string userId)
        {
            return await _takeBookUseCase.TakeBook(bookTitle, userId);
        }

        public async Task<IEnumerable<Book>> GetTakenBooks([FromQuery] QueryObject query, string userId)
        {
            return await _getTakenBooksUseCase.GetTakenBooks(query, userId);
        }

        public async Task<Book> ReturnBook(string userId, string bookTitle)
        {
            return await _returnBookUseCase.ReturnBook(userId, bookTitle);
        }

        public async Task<IEnumerable<Book>> AddCover(string bookTitle, IFormFile file, [FromQuery] QueryObject queryObject)
        {
            return await _addCoverUseCase.AddCover(bookTitle, file, queryObject);
        }
    }
}
