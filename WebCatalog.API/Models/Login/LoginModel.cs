using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebCatalog.API.Models.Login
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Input is required")]
        [JsonPropertyName("inputvalue")]
        public string? InputValue { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
