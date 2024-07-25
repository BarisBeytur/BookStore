using AutoMapper;
using System;
using System.Linq;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;

namespace WebApiBookStore.Application.UserOperations.Commands.CreateUsers
{
    public class CreateUsersCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IBookContext _context;
        private readonly IMapper _mapper;

        public CreateUsersCommand(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == Model.Email);
            if (user is not null)
            {
                throw new InvalidOperationException("Kullanıcı Zaten Mevcut");
            }
            user = _mapper.Map<User>(Model);

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public class CreateUserModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
