using AutoMapper;
using Library.Domain.Entities;
using Library.Application.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.BookUseCases
{
    public class TakeBookUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TakeBookUseCase(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> TakeBook(string bookTitle, string userId)
        {
            if (userId == null)
            {
                throw new EntityNotFoundException($"User was not found");
            }

            var existingBook = await _unitOfWork.Book.GetByTitle(bookTitle);

            if (existingBook is null)
            {
                throw new EntityNotFoundException($"{bookTitle} is not found in database.");
            }

            if (existingBook.IsTaken == true)
            {
                throw new BookTakenException($"Book {bookTitle} was already taken");
            }

            var takeBook = await _unitOfWork.Book.TakeBook(existingBook, userId);

            await _unitOfWork.SaveChangesAsync();

            return takeBook;
        }
    }
}
