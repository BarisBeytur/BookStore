using AutoMapper;
using System.Linq;
using System;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors
{
    public class CreateAuthorCommand
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorModel Model { get; set; }
        public CreateAuthorCommand(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Name == Model.Name);
            if (_context.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower()))
            {
                throw new InvalidOperationException("Bu yazar zaten ekli!");
            }
            if (_context.Authors.Any(x => x.BookID == Model.BookID))
            {
                throw new InvalidOperationException("Bu kitabın zaten yazarı var");
            }

            author = _mapper.Map<Author>(Model);
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public class CreateAuthorModel
        {
            public string Name { get; set; }
            public string SurName { get; set; }
            public int DateOfBirth { get; set; }
            public int BookID { get; set; } = 0;
        }
    }
}
