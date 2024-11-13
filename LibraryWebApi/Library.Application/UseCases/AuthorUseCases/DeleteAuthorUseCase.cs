using AutoMapper;
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
            await _unitOfWork.Author.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
