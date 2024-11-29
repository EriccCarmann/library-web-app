using AutoMapper;
using Library.Domain.Entities;
using Library.Application.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.BookUseCases
{
    public class ReturnBookUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ReturnBookUseCase(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> ReturnBook(string userId, string bookTitle)
        {
            if (userId == null)
            {
                throw new EntityNotFoundException($"User was not found");
            }

            var existingBook = await _unitOfWork.Book.GetByTitle(bookTitle);

            if (existingBook is null)
            {
                throw new EntityNotFoundException($"{existingBook} is not found in database.");
            }

            if (userId != existingBook.UserId)
            {
                throw new DataValidationException($"Current user did not take {bookTitle} book");
            }

            var book = await _unitOfWork.Book.ReturnBook(existingBook, userId);

            await _unitOfWork.SaveChangesAsync();

            return book;
        }
    }
}
