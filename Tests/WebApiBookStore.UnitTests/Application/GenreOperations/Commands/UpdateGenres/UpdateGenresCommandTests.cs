using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.GenreOperations.Commands.UpdateGenres;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.GenreOperations.Commands.UpdateGenres.UpdateGenresCommand;

namespace WebApiBookStore.UnitTests.Application.GenreOperations.Commands.UpdateGenres
{
    public class UpdateGenresCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookContext _context;

        public UpdateGenresCommandTests(CommonTestFixture textFixture)
        {
            _context = textFixture.Context;
        }

        [Fact]
        public void WhenInvalidGenreIdIsGiven_InvalidOperationException_ShouldBeReturnError()
        {

            //arrange (hazırlık)
            UpdateGenresCommand command = new UpdateGenresCommand(_context);
            command.ID = 099;

            //act && assert(çalıştırma && doğrulama) 
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Bu id ile bir kategori yok");
        }
        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturnError()
        {
            //arrange (hazırlık)
            var genre = new Genre() { GenreName = "TestUpdateGenre" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            UpdateGenresCommand command = new UpdateGenresCommand(_context);
            command.ID = 1;
            command.Model = new UpdateGenreModel() { GenreName = genre.GenreName };

            //act && assert (çalıştırma && doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message
                .Should().Be("Aynı isimde kategori var");
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldNotBeReturnError()
        {
            //arrange (hazırlık)
            UpdateGenresCommand command = new UpdateGenresCommand(_context);
            UpdateGenreModel model = new UpdateGenreModel() { GenreName = "TestUpdateGenreTrue"};
            command.Model = model;
            command.ID = 1;
            

            //act
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert
            var genre = _context.Genres.SingleOrDefault(x => x.GenreID == command.ID);
            genre.Should().NotBeNull();
            genre.GenreName.Should().Be(model.GenreName);
        }
    }
}
