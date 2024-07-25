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
    /// <summary>
    /// Bu controller kitap türleri ile ilgili işlemleri yapmaktadır.
    /// </summary>
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

        /// <summary>
        /// Bu metot tüm kitap türlerini listeler.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        /// <summary>
        /// Bu metot id parametresi ile gönderilen kitap türünün detaylarını getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Bu metot yeni bir kitap türü ekler.
        /// </summary>
        /// <param name="createGenres"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Bu metot id parametresi ile gönderilen kitap türünü günceller.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateGenre"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Bu metot id parametresi ile gönderilen kitap türünü siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
