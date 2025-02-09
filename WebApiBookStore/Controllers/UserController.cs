﻿using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApiBookStore.Application.UserOperations.Commands.CreateToken;
using WebApiBookStore.Application.UserOperations.Commands.CreateUsers;
using WebApiBookStore.DbOperations;
using WebApiBookStore.TokenOperations.Models;
using static WebApiBookStore.Application.UserOperations.Commands.CreateToken.CreateTokenCommand;
using static WebApiBookStore.Application.UserOperations.Commands.CreateUsers.CreateUsersCommand;

namespace WebApiBookStore.Controllers
{
    /// <summary>
    /// Bu controller kullanıcı işlemlerini gerçekleştirir.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;

        public UserController(IBookContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        /// <summary>
        /// Bu metot kullanıcı oluşturur.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateUser([FromBody] CreateUserModel newUser)
        {
            CreateUsersCommand command = new CreateUsersCommand(_context, _mapper);
            command.Model= newUser;
            command.Handle();

            return Ok();

        }

        /// <summary>
        /// Bu metot token oluşturur.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model= login;
            var token = command.Handle();
            return token;
        }
    }
}
