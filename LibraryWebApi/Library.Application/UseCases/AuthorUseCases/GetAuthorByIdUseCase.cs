using AutoMapper;
using Library.Application.Exceptions;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.AuthorUseCases
{
    public class GetAuthorByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorByIdUseCase(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Author> GetById([FromRoute] int id)
        {
            var author = await _unitOfWork.Author.GetByIdAsync(id);

            if (author is null)
            {
                throw new EntityNotFoundException($"{author} with ID {id} not found.");
            }

            return author;
        }
    }
}
