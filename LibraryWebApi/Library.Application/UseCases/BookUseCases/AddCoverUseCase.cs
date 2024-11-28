using AutoMapper;
using Library.Domain.Entities;
using Library.Application.Exceptions;
using Library.Domain.Helpers;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.BookUseCases
{
    public class AddCoverUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AddCoverUseCase(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> AddCover(string bookTitle, IFormFile file, [FromQuery] QueryObject queryObject)
        {
            if (file == null && file.Length <= 0)
            {
                throw new InvalidCoverImageException("Invalid Image File");
            }

            byte[] imageData = new byte[file.Length];

            using (var stream = file.OpenReadStream())
            {
                await stream.ReadAsync(imageData, 0, imageData.Length);
            }

            var result = await _unitOfWork.Book.AddCover(bookTitle, imageData);

            if (result == null)
            {
                throw new EntityNotFoundException($"Book name {bookTitle} was not found");
            }

            await _unitOfWork.SaveChangesAsync();

            var all = _mapper.Map<IEnumerable<Book>>(await _unitOfWork.Book.GetAllAsync(queryObject)); ;

            return all;
        }
    }
}
