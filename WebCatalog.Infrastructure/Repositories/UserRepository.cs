using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Interfaces;

namespace WebCatalog.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByCpfAsync(string cpf)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Cpf == cpf); //FirstOrDefaultAsync(u => u.Cpf == cpf);

            return user;
        }
    }
}
