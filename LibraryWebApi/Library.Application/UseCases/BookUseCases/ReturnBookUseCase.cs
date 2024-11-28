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
            var book = await _unitOfWork.Book.ReturnBook(bookTitle, userId);

            if (book == null)
            {
                throw new EntityNotFoundException($"Book name {bookTitle} was not found");
            }

            await _unitOfWork.SaveChangesAsync();

            return book;
        }
    }
}
