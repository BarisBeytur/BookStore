using System;

namespace WebApiBookStore.TokenOperations.Models
{
    /// <summary>
    /// Bu sınıf, token bilgilerini içerir.
    /// </summary>
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
