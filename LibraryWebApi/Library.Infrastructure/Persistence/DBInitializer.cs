using Library.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Library.Infrastructure.Persistence
{
    public static class DBInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider) 
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = new LibraryUser
            {
                UserName = "Admin"
            };

            var result = userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();

            if (result.Succeeded) 
            {
                userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin")).GetAwaiter().GetResult();
            }
        }
    }
}
