using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.BookUseCases
{
    public class GetTakenBooksUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetTakenBooksUseCase(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Book>> GetTakenBooks([FromQuery] QueryObject query, string userId)
        {
            var takenBooks = await _unitOfWork.Book.GetTakenBooks(userId, query);

            return takenBooks;
        }
    }
}
