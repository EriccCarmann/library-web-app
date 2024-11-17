using Library.Domain.Entities;

namespace Library.Domain.Interfaces
{
    public interface IGenerateRefreshToken
    {
        RefreshToken CreateRefreshToken();
    }
}
