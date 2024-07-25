using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System;
using WebApiBookStore.DbOperations;
using WebApiBookStore.Entities;

namespace WebApiBookStore.Application.GenreOperations.Commands.UpdateGenres
{
    public class UpdateGenresCommand
    {
        private readonly IBookContext _context;
        public int ID { get; set; }
        public UpdateGenreModel Model { get; set; }

        public UpdateGenresCommand(IBookContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var updategenre = _context.Genres.SingleOrDefault(x => x.GenreID == ID);
            if (updategenre is null)
            {
                throw new InvalidOperationException("Bu id ile bir kategori yok");
            }
            if (_context.Genres.Any(x => x.GenreName.ToLower() == Model.GenreName.ToLower() && x.GenreID != ID))
            {
                throw new InvalidOperationException("Aynı isimde kategori var");
            }

            updategenre.GenreName = Model.GenreName.Trim() != default ? Model.GenreName : updategenre.GenreName;
            updategenre.IsActive = Model.IsActive;

            _context.SaveChanges();
        }

        public class UpdateGenreModel
        {
            public string GenreName { get; set; }
            public bool IsActive { get; set; } = true;
        }
    }
}
