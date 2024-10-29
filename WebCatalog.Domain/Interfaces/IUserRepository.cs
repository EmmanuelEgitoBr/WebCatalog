using WebCatalog.Domain.Entities;

namespace WebCatalog.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetUserByCpfAsync(string cpf);
    }
}
