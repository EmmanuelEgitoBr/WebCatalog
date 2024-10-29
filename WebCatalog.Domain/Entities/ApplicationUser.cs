using Microsoft.AspNetCore.Identity;

namespace WebCatalog.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Cpf { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
