using WebCatalog.Domain.Entities;

namespace WebCatalog.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> FindByCpfAsync(string cpf);
    }
}
