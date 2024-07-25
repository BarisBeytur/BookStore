using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection.Metadata;
using WebApi;
using WebApiBookStore.Common;
using WebApiBookStore.DbOperations;

namespace WebApiBookStore.Application.BookOperations.Quaries.GetBookDetails
{
    public class GetBookDetailQuery
    {
        private readonly IBookContext _bookContext;
        public int BookId { get; set; }
        private readonly IMapper _mapper;

        public GetBookDetailQuery(IBookContext bookContext, IMapper mapper)
        {
            _bookContext = bookContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var GetBook = _bookContext.Books.Include(x => x.Genre).Where(x => x.Id == BookId).SingleOrDefault();
            if (GetBook is null)
            {
                throw new InvalidOperationException("Bulunamadı");
            }
            BookDetailViewModel vm = _mapper.Map<BookDetailViewModel>(GetBook);
            //vm.Title = GetBook.Title;
            //vm.PageCount = GetBook.PageCount;
            //vm.PublishDate = GetBook.PublishDate.Date.ToString("dd/mm/yyy");
            //vm.Genre = ((GenreEnum)GetBook.GenreId).ToString();
            return vm;
        }

    }
    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
