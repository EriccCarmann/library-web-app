using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Application.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.Mvc;
using Library.Application.UseCases.AuthorUseCases;

namespace LibraryWebApi.Services
{
    public class AuthorService
    {
        private readonly GetAllAuthorsUseCase _getAllAuthorsUseCase;
        private readonly GetAuthorByIdUseCase _getAuthorByIdUseCase;
        private readonly CreateAuthorUseCase _createAuthorUseCase;
        private readonly UpdateAuthorUseCase _updateAuthorUseCase;
        private readonly DeleteAuthorUseCase _deleteAuthorUseCase;
        private readonly FindAuthorByNameUseCase _findAuthorByNameUseCase;

        public AuthorService(
            GetAllAuthorsUseCase getAllAuthorsUseCase,
            GetAuthorByIdUseCase getAuthorByIdUseCase,
            CreateAuthorUseCase createAuthorUseCase,
            UpdateAuthorUseCase updateAuthorUseCase,
            DeleteAuthorUseCase deleteAuthorUseCase,
            FindAuthorByNameUseCase findAuthorByNameUseCase)
        {
            _getAllAuthorsUseCase = getAllAuthorsUseCase;
            _getAuthorByIdUseCase = getAuthorByIdUseCase;
            _createAuthorUseCase = createAuthorUseCase;
            _updateAuthorUseCase = updateAuthorUseCase;
            _deleteAuthorUseCase = deleteAuthorUseCase;
            _findAuthorByNameUseCase = findAuthorByNameUseCase;
        }

        public async Task<IEnumerable<AuthorReadDto>> GetAll([FromQuery] QueryObject queryObject)
        {
            return await _getAllAuthorsUseCase.GetAll(queryObject);
        }

        public async Task<Author> GetById([FromRoute] int id)
        {
            return await _getAuthorByIdUseCase.GetById(id);
        }

        public async Task<AuthorReadDto> CreateAuthor(AuthorCreateDto author)
        {
            return await _createAuthorUseCase.CreateAuthor(author);
        }

        public async Task<AuthorReadDto> UpdateAuthor(string name, [FromBody] AuthorUpdateDto authorUpdateDto)
        {
            return await _updateAuthorUseCase.UpdateAuthor(name, authorUpdateDto);
        }

        public async Task DeleteAuthor([FromRoute] int id)
        {
            await _deleteAuthorUseCase.DeleteAuthor(id);
        }

        public async Task<Author?> FindAuthorByName(string name)
        {
            return await _findAuthorByNameUseCase.FindAuthorByName(name);
        }
    }
}
