using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.BookUseCases
{
    public class GetAllBooksUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBooksUseCase(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> GetAll([FromQuery] QueryObject queryObject)
        {
            var books = await _unitOfWork.Book.GetAllAsync(queryObject);

            var all = _mapper.Map<IEnumerable<Book>>(books);

            return all;
        }
    }
}
