using System;
using System.Security.Cryptography;
using System.Text;

namespace SESCAP.Ecommerce.Libraries.CriptoSenha
{
    public static class CriptografiaSenha
    {
        public static string HashSenha(string senha)
        {
            var sha = SHA384.Create();
            var arrayByte = Encoding.Default.GetBytes(senha);
            var hashedSenha = sha.ComputeHash(arrayByte);
            return Convert.ToBase64String(hashedSenha);
        }
        
    }
}
