using AutoMapper;
using Library.Application.Exceptions;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.AuthorUseCases
{
    public class DeleteAuthorUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAuthorUseCase(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteAuthor([FromRoute] int id)
        {
            var existingAuthor = await _unitOfWork.Author.GetByIdAsync(id);

            if (existingAuthor is null)
            {
                throw new EntityNotFoundException($"{existingAuthor} is not found in database.");
            }

            await _unitOfWork.Author.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
