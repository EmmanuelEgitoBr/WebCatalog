using Microsoft.AspNetCore.Identity;
using WebCatalog.Application.Interfaces;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Interfaces;

namespace WebCatalog.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> FindByCpfAsync(string cpf)
        {
            var user = await _userRepository.GetUserByCpfAsync(cpf);

            return user;
        }
    }
}
