using Library.Application.DTOs.LibraryUserDTOs;
using Library.Domain.Entities;
using Library.Domain.Exceptions;
using Library.Domain.Interfaces;
using Library.Infrastructure.UnitOfWork;
using System.Security.Cryptography;

namespace Library.Infrastructure.TokenServices
{
    public class GenerateRefreshToken : IGenerateRefreshToken
    {
        public RefreshToken CreateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7) 
            };
            return refreshToken;
        }
    }
}
