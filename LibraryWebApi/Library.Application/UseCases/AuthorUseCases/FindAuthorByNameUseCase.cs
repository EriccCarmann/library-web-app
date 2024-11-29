using AutoMapper;
using Library.Domain.Entities;
using Library.Application.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.AuthorUseCases
{
    public class FindAuthorByNameUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FindAuthorByNameUseCase(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Author?> FindAuthorByName(string name)
        {
            var author = await _unitOfWork.Author.FindAuthorByName(name);

            if (author is null)
            {
                throw new EntityNotFoundException($"{name} is not found in database.");
            }

            return author;
        }
    }
}
