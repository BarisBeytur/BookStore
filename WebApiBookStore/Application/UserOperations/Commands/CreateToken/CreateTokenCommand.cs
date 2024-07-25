using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using WebApiBookStore.DbOperations;
using WebApiBookStore.TokenOperations;
using WebApiBookStore.TokenOperations.Models;

namespace WebApiBookStore.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _coonfiguration;
        public CreateTokenCommand(IBookContext context, IMapper mapper, IConfiguration coonfiguration)
        {
            _context = context;
            _mapper = mapper;
            _coonfiguration = coonfiguration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);
            if (user is not null)
            {
                TokenHandler tokenHandler = new TokenHandler(_coonfiguration);
                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();

                return token;
            }
            else
            {
                throw new InvalidOperationException("Kullanıcı Adı - Şifre Hatalı!");
            }

        }

        public class CreateTokenModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
