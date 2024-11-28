using AutoMapper;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.BookUseCases
{
    public class GetBookByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetBookByIdUseCase(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Book> GetById([FromRoute] int id)
        {
            var book = await _unitOfWork.Book.GetByIdAsync(id);

            if (book is null)
            {
                throw new EntityNotFoundException($"{book} with ID {id} not found.");
            }

            return book;
        }
    }
}
