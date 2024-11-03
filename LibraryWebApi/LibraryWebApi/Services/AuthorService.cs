using AutoMapper;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Helpers;
using Library.Infrastructure.UnitOfWork;
using LibraryWebApi.DTOs.AuthorDTOs;
using LibraryWebApi.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Services
{
    public class AuthorService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthorValidator _authorValidator;

        public AuthorService(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            AuthorValidator authorValidato)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _authorValidator = authorValidato;
        }

        public async Task<IEnumerable<AuthorReadDto>> GetAll([FromQuery] QueryObject queryObject)
        {
            var authors = await _unitOfWork.Author.GetAllAsync(queryObject);

            var _authors = _mapper.Map<IEnumerable<AuthorReadDto>>(authors);

            return _authors;
        }

        public async Task<Author> GetById([FromRoute] int id)
        {
            var author = await _unitOfWork.Author.GetByIdAsync(id);

            return author;
        }

        public async Task<AuthorReadDto> CreateAuthor(AuthorCreateDto author)
        {
            var _author = _mapper.Map<Author>(author);

            if (!_authorValidator.Validate(_author).IsValid)
            {
                throw new DataValidationException("Input data is invalid");
            }

            await _unitOfWork.Author.CreateAsync(_author);

            return _mapper.Map<AuthorReadDto>(_author);
        }

        public async Task<AuthorReadDto> UpdateAuthor(string name, [FromBody] AuthorUpdateDto authorUpdateDto)
        {
            var existingAuthor = await _unitOfWork.Author.FindAuthorByName(name);

            var updateAuthor = await _unitOfWork.Author.UpdateAsync(
                existingAuthor.Id,
                new Author
                {
                    Id = existingAuthor.Id,
                    FirstName = authorUpdateDto.FirstName,
                    LastName = authorUpdateDto.LastName,
                    DateOfBirth = authorUpdateDto.DateOfBirth,
                    Country = authorUpdateDto.Country
                });

            return _mapper.Map<AuthorReadDto>(updateAuthor);
        }

        public async Task DeleteAuthor([FromRoute] int id)
        {
            await _unitOfWork.Author.DeleteAsync(id);
        }

        public async Task<Author?> FindAuthorByName(string name)
        {
            return await _unitOfWork.Author.FindAuthorByName(name);
        }
    }
}
