using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.GenreOperations.Commands.DeleteGenres;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;

namespace WebApiBookStore.UnitTests.Application.GenreOperations.Commands.DeleteGenres
{
    public class DeleteGenresCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookContext _context;

        public DeleteGenresCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {

            //arrange (hazırlık)
            DeleteGenresCommand command = new DeleteGenresCommand(_context);
            command.ID = 999;

            //act && assert (çalıştırma && doğrulama)
            FluentActions
                .Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Böyle bir kategori yok");
        }

        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
        {

            //arrange (hazırlık)
            var genre = new Genre { GenreName= "TestssGenress" };
            _context.Genres.Add(genre);
            _context.SaveChanges();



            DeleteGenresCommand command = new DeleteGenresCommand( _context);
            command.ID = genre.GenreID;

            //act (çalıştırma)
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert (doğrulama)
            genre = _context.Genres.SingleOrDefault(x => x.GenreID== genre.GenreID);
            genre.Should().BeNull();
        }
    }
}
