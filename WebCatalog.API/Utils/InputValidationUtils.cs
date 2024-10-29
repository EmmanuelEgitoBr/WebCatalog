using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Text.RegularExpressions;

namespace WebCatalog.API.Utils
{
    public static class InputValidationUtils
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidCpf(string cpf)
        {
            if (cpf.Length != 11) { return false; }

            return cpf.All(char.IsDigit);
        }
    }
}
