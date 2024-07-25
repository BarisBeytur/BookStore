using System.Linq;
using System;
using System.Reflection.Metadata;
using WebApiBookStore.DbOperations;
using static WebApiBookStore.Application.BookOperations.Commands.CreateBooks.CreateBooksQuery;
using WebApi;
using Microsoft.EntityFrameworkCore;

namespace WebApiBookStore.Application.BookOperations.Commands.UpdateBooks
{
    public class UpdateBooksQuery
    {
        private readonly IBookContext _bookContext;
        public int BookId { get; set; }
        public UpdateBookViewModel Model { get; set; }
        public UpdateBooksQuery(IBookContext bookContext)
        {
            _bookContext = bookContext;
        }
        public void Handle()
        {
            var book = _bookContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("bu id ile bir kitap yok");
            }
            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;

            _bookContext.SaveChanges();

        }
        public class UpdateBookViewModel
        {
            public string Title { get; set; }

            public int GenreId { get; set; }
        }
    }
}


