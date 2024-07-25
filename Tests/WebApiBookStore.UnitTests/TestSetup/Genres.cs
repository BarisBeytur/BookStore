using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;

namespace WebApiBookStore.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenre(this BookContext context)
        {
            context.Genres.AddRange(
                new Genre { GenreName = "Personel Growth"},
                new Genre { GenreName = "Science Fiction"},
                new Genre { GenreName = "Romance"}
                );
        }
    }
}
