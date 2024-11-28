using AutoMapper;
using Library.Application.DTOs.BookDTOs;
using Library.Application.Validators;
using Library.Domain.Entities;
using Library.Application.Exceptions;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.BookUseCases
{
    public class UpdateBookUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BookValidator _bookValidator;

        public UpdateBookUseCase(IMapper mapper,
            IUnitOfWork unitOfWork,
            BookValidator bookValidator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _bookValidator = bookValidator;
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
    }
}
