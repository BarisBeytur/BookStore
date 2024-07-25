using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiBookStore.Entities
{
    public class User
    {
        [Key]
        public int UserID{ get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}
