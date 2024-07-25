using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApiBookStore.Application.GenreOperations.Commands.CreateGenres;
using WebApiBookStore.Application.GenreOperations.Commands.DeleteGenres;
using WebApiBookStore.Application.GenreOperations.Commands.UpdateGenres;
using WebApiBookStore.Application.GenreOperations.Quaries.GetGenre;
using WebApiBookStore.Application.GenreOperations.Quaries.GetGenreDetails;
using WebApiBookStore.DbOperations;
using static WebApiBookStore.Application.GenreOperations.Commands.CreateGenres.CreateGenresCommand;
using static WebApiBookStore.Application.GenreOperations.Commands.UpdateGenres.UpdateGenresCommand;
using static WebApiBookStore.Application.GenreOperations.Quaries.GetGenreDetails.GetGenreDetailQuery;

namespace WebApiBookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : Controller
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult GetIdGenres(int id)
        {
            GenreDetailModel result;

            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.ID = id;

            GetGenresDetailValidator validator = new GetGenresDetailValidator();
            validator.ValidateAndThrow(query);

            result = query.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel createGenres)
        {
            CreateGenresCommand command = new CreateGenresCommand(_context, _mapper);
            command.Model = createGenres;

            CreateGenresValidator validator = new CreateGenresValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenresCommand command = new UpdateGenresCommand(_context);
            command.ID = id;
            command.Model = updateGenre;

            UpdateGenresCommandValidator validator = new UpdateGenresCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenresCommand command = new DeleteGenresCommand(_context);
            command.ID = id;

            DeleteGenresCommandValidator validator = new DeleteGenresCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();

            return Ok();
        }

    }
}
