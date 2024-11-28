using AutoMapper;
using Library.Application.Exceptions;
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
            var existingBook = await _unitOfWork.Book.GetByIdAsync(id);

            if (existingBook is null)
            {
                throw new EntityNotFoundException($"{existingBook} is not found in database.");
            }

            await _unitOfWork.Book.DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
