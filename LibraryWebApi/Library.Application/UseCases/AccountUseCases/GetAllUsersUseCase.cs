using Library.Domain.Entities;
using Library.Domain.Helpers;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Application.UseCases.AccountUseCases
{
    public class GetAllUsersUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<LibraryUser>> GetAll([FromQuery] QueryObject queryObject)
        {
            return await _unitOfWork.Account.GetAllAsync(queryObject);
        }
    }
}
