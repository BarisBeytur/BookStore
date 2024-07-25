using AutoMapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Application.GenreOperations.Commands.CreateGenres;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;
using WebApiBookStore.UnitTests.TestSetup;
using Xunit;
using static WebApiBookStore.Application.GenreOperations.Commands.CreateGenres.CreateGenresCommand;

namespace WebApiBookStore.UnitTests.Application.GenreOperations.Commands.CreateGenres
{
    public class CreateGenresCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookContext _context;
        private readonly IMapper _mapper;

        public CreateGenresCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreTitleGiven_Genre_ShouldBeReturnError()
        {
            //arrange (hazırlık)
            var genre = new Genre() { GenreName = "GenreTest" };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenresCommand command = new CreateGenresCommand(_context, _mapper);
            command.Model = new CreateGenreModel() { GenreName = genre.GenreName};

            //act && assert (çalıştırma && doğrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().Be("Kategori zaten mevcut");

        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeCreated()
        {
            //arrange (hzırlık)
            CreateGenresCommand command = new CreateGenresCommand(_context, _mapper);
            CreateGenreModel model = new CreateGenreModel() { GenreName = "GenreTest1" };

            command.Model = model;

            //act (çalıştırma)
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            //assert (doğrulama)
            var genre = _context.Genres.SingleOrDefault(x => x.GenreName == model.GenreName);

            genre.Should().NotBeNull();
            genre.GenreName.Should().Be(model.GenreName);
        }
    }
}
