using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;

namespace WebApiBookStore.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthor(this BookContext context)
        {
            context.Authors.AddRange
                    (
                    new Author
                    {
                        Name = "Hüseyin",
                        SurName = "Öztürk",
                        DateOfBirth = 2001,
                        BookID = 1
                    },
                    new Author
                    {
                        Name = "Hasan",
                        SurName = "Seyrek",
                        DateOfBirth = 2001,
                        BookID = 2
                    },
                    new Author
                    {
                        Name = "Recep",
                        SurName = "Tayyip",
                        DateOfBirth = 2001,
                        BookID = 3
                    }
                    );
        }
    }
}
