using AutoMapper;
using System.Linq;
using System;
using WebApiBookStore.DbOperations;

namespace WebApiBookStore.Application.AuthorOperations.Commands.UpdateAuthors
{
    public class UpdateAuthorsCommand
    {
        private readonly IBookContext _context;
        
        public int ID { get; set; }
        public UpdateAuthorsModel Model { get; set; }
        public UpdateAuthorsCommand(IBookContext context)
        {
            _context = context;
            
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.AuthorID == ID);
            if (author is null)
            {
                throw new InvalidOperationException("Böyle bir yazar yok");
            }
            if (_context.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.AuthorID != ID))
            {
                throw new InvalidOperationException("Aynı isimde yazar var");
            }
            author.Name = Model.Name.Trim() != default ? Model.Name.Trim() : author.Name;
            author.SurName = Model.SurName.Trim() != default ? Model.SurName.Trim() : author.SurName;
            author.DateOfBirth = Model.DateOfBirth != default ? Model.DateOfBirth : author.DateOfBirth;

            _context.SaveChanges();
        }

        public class UpdateAuthorsModel
        {
            public string Name { get; set; }
            public string SurName { get; set; }
            public int DateOfBirth { get; set; }
        }
    }
}
