using AutoMapper;
using Library.Application.Validators;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Helpers;
using Library.Infrastructure.UnitOfWork;
using Library.Application.DTOs.BookDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.Services
{
    public class BookService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookValidator _bookValidator;

        public BookService(IMapper mapper,
            IUnitOfWork unitOfWork,
            BookValidator bookValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _bookValidator = bookValidator;
        }

        public async Task<IEnumerable<Book>> GetAll([FromQuery] QueryObject queryObject)
        {
            var books = await _unitOfWork.Book.GetAllAsync(queryObject);

            var all = _mapper.Map<IEnumerable<Book>>(books);

            return all;
        }

        public async Task<Book> GetById([FromRoute] int id)
        {
            var book = await _unitOfWork.Book.GetByIdAsync(id);

            return book;
        }

        public async Task<Book> GetByISBN(string ISBN)
        {
            var book = await _unitOfWork.Book.GetByIdISBN(ISBN);

            if (book == null)
            {
                throw new EntityNotFoundException($"Book with ISBN {ISBN} was not found");
            }

            return book;
        }
        public async Task<BookReadDto> CreateBook(BookCreateDto data)
        {
            var _book = _mapper.Map<Book>(data);

            if (!_bookValidator.Validate(_book).IsValid)
            {
                throw new DataValidationException("Input data is invalid. Make sure that ISBN has 13 characters.");
            }

            await _unitOfWork.Book.CreateAsync(_book);

            await _unitOfWork.SaveChangesAsync();

            var _newBook = _mapper.Map<BookReadDto>(_book);

            return _newBook;
        }

        public async Task<BookReadDto> UpdateBook([FromRoute] int id, [FromBody] BookUpdateDto bookUpdatingDto)
        {
            Book newBook = new Book
            {
                Id = id,
                Title = bookUpdatingDto.Title,
                Genre = bookUpdatingDto.Genre,
                Description = bookUpdatingDto.Description,
                ISBN = bookUpdatingDto.ISBN,
                IsTaken = bookUpdatingDto.IsTaken,
                AuthorId = bookUpdatingDto.AuthorId
            };

            if (!_bookValidator.Validate(newBook).IsValid)
            {
                throw new DataValidationException("Input data is invalid. Make sure that ISBN has 13 characters.");
            }

            var updatedBook = await _unitOfWork.Book.UpdateAsync(id, newBook);

            await _unitOfWork.SaveChangesAsync();

            var book = _mapper.Map<BookReadDto>(updatedBook);

            return book;
        }

        public async Task DeleteBook([FromRoute] int id)
        {
            await _unitOfWork.Book.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Book> TakeBook(string bookTitle, string userId)
        {
            var takeBook = await _unitOfWork.Book.TakeBook(bookTitle, userId);

            if (takeBook == null)
            {
                throw new EntityNotFoundException($"Book name {bookTitle} was not found");
            }

            if (takeBook.IsTaken == true)
            {
                throw new BookTakenException($"Book {bookTitle} was already taken");
            }

            await _unitOfWork.SaveChangesAsync();

            return takeBook;
        }

        public async Task<IEnumerable<Book>> GetTakenBooks([FromQuery] QueryObject query, string userId)
        {
            var takenBooks = await _unitOfWork.Book.GetTakenBooks(userId, query);

            return takenBooks;
        }

        public async Task<Book> ReturnBook(string userId, string bookTitle)
        {
            var book = await _unitOfWork.Book.ReturnBook(bookTitle, userId);

            if (book == null)
            {
                throw new EntityNotFoundException($"Book name {bookTitle} was not found");
            }

            await _unitOfWork.SaveChangesAsync();

            return book;
        }

        public async Task<IEnumerable<Book>> AddCover(string bookTitle, IFormFile file, [FromQuery] QueryObject queryObject)
        {
            if (file == null && file.Length <= 0)
            {
                throw new InvalidCoverImageException("Invalid Image File");
            }

            byte[] imageData = new byte[file.Length];

            using (var stream = file.OpenReadStream())
            {
                await stream.ReadAsync(imageData, 0, imageData.Length);
            }

            var result = await _unitOfWork.Book.AddCover(bookTitle, imageData);

            if (result == null)
            {
                throw new EntityNotFoundException($"Book name {bookTitle} was not found");
            }

            await _unitOfWork.SaveChangesAsync();

            var all = await GetAll(queryObject);

            return all;
        }
    }
}
