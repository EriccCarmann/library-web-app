using AutoMapper;
using FluentValidation;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Helpers;
using Library.Infrastructure.UnitOfWork;
using LibraryWebApi.DTOs.BookDTOs;
using LibraryWebApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Services
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

            var book = _mapper.Map<BookReadDto>(updatedBook);

            return book;
        }

        public async Task DeleteBook([FromRoute] int id)
        {
            await _unitOfWork.Book.DeleteAsync(id);
        }

        public async Task<Book> TakeBook(string bookName, string userId)
        {
            var takeBook = await _unitOfWork.Book.TakeBook(bookName, userId);

            return takeBook;
        }

        public async Task<IEnumerable<Book>> GetTakenBooks([FromQuery] QueryObject query, string userId)
        {
            var takenBooks = await _unitOfWork.Book.GetTakenBooks(userId, query);

            return takenBooks;
        }

        public async Task<Book> ReturnBook(string userId, string bookName)
        {
            var book = await _unitOfWork.Book.ReturnBook(bookName, userId);

            return book;
        }

        public async Task<IEnumerable<Book>> AddCover(string bookTitle, IFormFile file, [FromQuery] QueryObject queryObject)
        {
            byte[] imageData = new byte[file.Length];

            using (var stream = file.OpenReadStream())
            {
                await stream.ReadAsync(imageData, 0, imageData.Length);
            }

            await _unitOfWork.Book.AddCover(bookTitle, imageData);

            var all = await GetAll(queryObject);

            return all;
        }
    }
}
