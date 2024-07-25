using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;

namespace WebApiBookStore.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBook(this BookContext context)
        {
            context.Books.AddRange(
                new Book { Title = "Bukre", GenreId = 1, PageCount = 250, PublishDate = new DateTime(2000, 05, 25) },
                new Book { Title = "Bukre 2", GenreId = 2, PageCount = 350, PublishDate = new DateTime(2004, 06, 25) },
                new Book { Title = "Bukre 3", GenreId = 3, PageCount = 450, PublishDate =new DateTime(2006,07,29)},
                new Book { Title = "Bukre 4", GenreId = 2, PageCount = 550, PublishDate =new DateTime(2009,07,29)}
            );
        }
    }
}
