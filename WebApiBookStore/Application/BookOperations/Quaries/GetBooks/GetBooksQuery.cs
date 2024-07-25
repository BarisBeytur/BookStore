using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiBookStore.Common;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;

namespace WebApiBookStore.Application.BookOperations.Quaries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookContext _bookContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(IBookContext bookContext, IMapper mapper)
        {
            _bookContext = bookContext;
            _mapper = mapper;
        }
        public List<BookViewModel> Handle()
        {
            var booklist = _bookContext.Books.Include(x => x.Author).Include(x => x.Genre).OrderBy(x => x.Id).ToList();
            List<BookViewModel> bvm = _mapper.Map<List<BookViewModel>>(booklist);
            //List<BookViewModel> bvm = new List<BookViewModel>();
            //foreach (var book in booklist)
            //{
            //    bvm.Add(new BookViewModel()
            //    {
            //        Title = book.Title,
            //        Genre = ((GenreEnum)book.GenreId).ToString(),
            //        PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"),
            //        PageCount = book.PageCount
            //    });
            //}
            return bvm;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }
        }
    }
}
