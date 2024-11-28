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
    }
}
