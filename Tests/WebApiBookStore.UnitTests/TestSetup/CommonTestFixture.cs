using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.Common;
using WebApiBookStore.DbOperations;

namespace WebApiBookStore.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            Context = new BookContext(options);

            Context.Database.EnsureCreated();
            Context.AddBook();
            Context.AddGenre();
            Context.AddAuthor();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
