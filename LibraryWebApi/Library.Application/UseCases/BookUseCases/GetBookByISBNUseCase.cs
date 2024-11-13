using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;
namespace Library.Application.UseCases.BookUseCases
{
    public class GetBookByISBNUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetBookByISBNUseCase(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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
    }
}
