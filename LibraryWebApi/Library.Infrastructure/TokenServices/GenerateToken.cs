using Library.Domain.Entities;
using Library.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.Infrastructure.TokenServices
{
    public class GenerateToken : IGenerateToken
    {
        private readonly UserManager<LibraryUser> _userManager;

        public GenerateToken(UserManager<LibraryUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> CreateToken(LibraryUser libraryUser)
        {
            var roles = await _userManager.GetClaimsAsync(libraryUser);

            var roleClaims = roles.Where(c => c.Type == ClaimTypes.Role);

            var roleNames = roleClaims.Select(c => c.Value);

            var role = roleNames.FirstOrDefault();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, libraryUser.UserName),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("vaWE8WuA19cleeg2RhHLB7qp8wsSpUTVGgbjq6AhIcPELx42jm8feBUH5c7m5oc7"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
