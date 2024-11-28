using AutoMapper;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.BookUseCases
{
    public class DeleteBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task DeleteBook([FromRoute] int id)
        {
            await _unitOfWork.Book.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
