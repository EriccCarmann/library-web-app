using Library.Domain.Interfaces;

namespace Library.Application.UseCases.AccountUseCases
{
    public class LogoutUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public LogoutUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Logout()
        {
            await _unitOfWork.Account.Logout();
        }
    }
}
