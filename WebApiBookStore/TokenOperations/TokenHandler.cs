﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApiBookStore.Entities;
using WebApiBookStore.TokenOperations.Models;

namespace WebApiBookStore.TokenOperations
{
    /// <summary>
    /// Bu sınıf, token oluşturma işlemlerini gerçekleştirir.
    /// </summary>
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            tokenModel.Expiration = DateTime.Now.AddMinutes(30);

            JwtSecurityToken securityToken = new JwtSecurityToken
                (
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials
                );


            // Bu aşamada token üretilir
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel; 

        }
        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
