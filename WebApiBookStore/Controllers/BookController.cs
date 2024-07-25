using System;
using System.Collections.Generic;

using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiBookStore.Application.BookOperations.Commands.CreateBooks;
using WebApiBookStore.Application.BookOperations.Commands.DeleteBooks;
using WebApiBookStore.Application.BookOperations.Commands.UpdateBooks;
using WebApiBookStore.Application.BookOperations.Quaries.GetBookDetails;
using WebApiBookStore.Application.BookOperations.Quaries.GetBooks;
using WebApiBookStore.DbOperations;
using static WebApiBookStore.Application.BookOperations.Commands.CreateBooks.CreateBooksQuery;
using static WebApiBookStore.Application.BookOperations.Commands.UpdateBooks.UpdateBooksQuery;

namespace WebApi.AddController
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult GetIdBook(int id)
        {
            BookDetailViewModel result;
    
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;

                GetBooksDetalValidator validator = new GetBooksDetalValidator();
                validator.ValidateAndThrow(query);

                result = query.Handle();
            

            return Ok(result);
        }
        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //    return (book);
        //}


        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel NewBook)
        {
            CreateBooksQuery command = new CreateBooksQuery(_context, _mapper);
 
            command.Model = NewBook;

            CreateBooksValidator validator = new CreateBooksValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

                //if (!result.IsValid)
                //{
                //    foreach ( var item in result.Errors)
                //    {
                //        Console.WriteLine("Property : " + item.PropertyName + " - Error Message : " + item.ErrorMessage);
                //    }
                //}

      

            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookViewModel UpdateBook)
        {
            
      
                UpdateBooksQuery query = new UpdateBooksQuery(_context);
                query.BookId = id;
                query.Model = UpdateBook;

                UpdateBooksValidator validator = new UpdateBooksValidator();
                validator.ValidateAndThrow(query);

                query.Handle();
   

            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

                DeleteBooksQuery query = new DeleteBooksQuery(_context);
                query.BookId = id;

                DeleteBooksValidator validator = new DeleteBooksValidator();
                validator.ValidateAndThrow(query);

                query.Handle();
        

            return Ok();
        }
    }
}