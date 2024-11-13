using AutoMapper;
using Library.Application.DTOs.AuthorDTOs;
using Library.Domain.Helpers;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.AuthorUseCases
{
    public class GetAllAuthorsUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllAuthorsUseCase(IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AuthorReadDto>> GetAll([FromQuery] QueryObject queryObject)
        {
            var authors = await _unitOfWork.Author.GetAllAsync(queryObject);

            var _authors = _mapper.Map<IEnumerable<AuthorReadDto>>(authors);

            return _authors;
        }
    }
}
