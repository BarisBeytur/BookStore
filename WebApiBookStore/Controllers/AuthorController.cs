using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors;
using WebApiBookStore.Application.AuthorOperations.Commands.DeleteAuthors;
using WebApiBookStore.Application.AuthorOperations.Commands.UpdateAuthors;
using WebApiBookStore.Application.AuthorOperations.Quaries.GetAuthorDetails;
using WebApiBookStore.Application.AuthorOperations.Quaries.GetAuthors;
using WebApiBookStore.DbOperations;
using static WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors.CreateAuthorCommand;
using static WebApiBookStore.Application.AuthorOperations.Commands.UpdateAuthors.UpdateAuthorsCommand;
using static WebApiBookStore.Application.AuthorOperations.Quaries.GetAuthorDetails.GetAuthorDetailQuery;

namespace WebApiBookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : Controller
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthor()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult GetIdAuthor(int id)
        {
            GetAuthorDetailModel result;

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.ID = id;

            GetAuthorDetailValidator validator = new GetAuthorDetailValidator();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);

        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel createAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = createAuthor;

            CreateAuthorValidator validator = new CreateAuthorValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateAuthor(int id,[FromBody] UpdateAuthorsModel model)
        {
            UpdateAuthorsCommand command = new UpdateAuthorsCommand(_context);
            command.ID = id;
            command.Model = model;

            UpdateAuthorsValidator validator = new UpdateAuthorsValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorsCommand command = new DeleteAuthorsCommand(_context);
            command.ID = id;

            DeleteAuthorValidator validator = new DeleteAuthorValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }
    }
}
