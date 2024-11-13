using AutoMapper;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.BookUseCases
{
    public class DeleteBookUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookUseCase(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteBook([FromRoute] int id)
        {
            await _unitOfWork.Book.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
