using AutoMapper;
using Library.Application.DTOs.AuthorDTOs;
using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.AuthorUseCases
{
    public class UpdateAuthorUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorUseCase(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AuthorReadDto>(updateAuthor);
        }
    }
}
