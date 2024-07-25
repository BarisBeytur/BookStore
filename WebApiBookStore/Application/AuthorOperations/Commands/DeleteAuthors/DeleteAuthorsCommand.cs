using System.Linq;
using System;
using WebApiBookStore.DbOperations;

namespace WebApiBookStore.Application.AuthorOperations.Commands.DeleteAuthors
{
    public class DeleteAuthorsCommand
    {
        private readonly IBookContext _context;
        public int ID { get; set; }

        public DeleteAuthorsCommand(IBookContext context)
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

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

    }
}
