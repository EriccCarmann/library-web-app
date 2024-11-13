using AutoMapper;
using Library.Application.DTOs.AuthorDTOs;
using Library.Application.Validators;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.AuthorUseCases
{
    public class CreateAuthorUseCase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthorValidator _authorValidator;

        public CreateAuthorUseCase(
            IMapper mapper,
            IUnitOfWork unitOfWork,
            AuthorValidator authorValidato)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _authorValidator = authorValidato;
        }

        public async Task<AuthorReadDto> CreateAuthor(AuthorCreateDto author)
        {
            var _author = _mapper.Map<Author>(author);

            if (!_authorValidator.Validate(_author).IsValid)
            {
                throw new DataValidationException("Input data is invalid");
            }

            await _unitOfWork.Author.CreateAsync(_author);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AuthorReadDto>(_author);
        }
    }
}
