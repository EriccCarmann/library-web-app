using AutoMapper;
using Library.Application.DTOs.BookDTOs;
using Library.Application.Validators;
using Library.Domain.Entities;
using Library.Application.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.BookUseCases
{
    public class CreateBookUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookValidator _bookValidator;

        public CreateBookUseCase(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            BookValidator bookValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _bookValidator = bookValidator;
        }

        public async Task<BookReadDto> CreateBook(BookCreateDto data)
        {
            var _book = _mapper.Map<Book>(data);

            if (!_bookValidator.Validate(_book).IsValid)
            {
                throw new DataValidationException("Input data is invalid. Make sure that ISBN has 13 characters.");
            }

            if (await _unitOfWork.Book.GetByISBN(data.ISBN) != null)
            {
                throw new BookDataException("ISBN already exists");
            }

            await _unitOfWork.Book.CreateAsync(_book);

            await _unitOfWork.SaveChangesAsync();

            var _newBook = _mapper.Map<BookReadDto>(_book);

            return _newBook;
        }
    }
}
