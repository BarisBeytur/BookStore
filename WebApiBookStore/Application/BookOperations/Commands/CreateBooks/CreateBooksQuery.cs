using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;

namespace WebApiBookStore.Application.BookOperations.Commands.CreateBooks
{
    public class CreateBooksQuery
    {
        public CreateBookModel Model { get; set; }
        private readonly IBookContext _bookContext;
        private readonly IMapper _mapper;
        public CreateBooksQuery(IBookContext bookContext, IMapper mapper)
        {
            _bookContext = bookContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _bookContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
            {
                throw new InvalidOperationException("Bu Kitap Zaten Mevcut.");
            }
            book = _mapper.Map<Book>(Model);   //new Book();
            //book.Title = Model.Title;
            //book.PublishDate = Model.PublishDate;
            //book.PageCount = Model.PageCount;
            //book.GenreId = Model.GenreId;

            _bookContext.Books.Add(book);
            _bookContext.SaveChanges();
        }
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}
