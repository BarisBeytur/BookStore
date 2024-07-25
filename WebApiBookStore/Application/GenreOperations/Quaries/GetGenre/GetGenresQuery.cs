using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using WebApiBookStore.DbOperations;

namespace WebApiBookStore.Application.GenreOperations.Quaries.GetGenre
{
    public class GetGenresQuery
    {
        public readonly IBookContext _context;
        public readonly IMapper _mapper;
        public GetGenresQuery(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GenreViewModel> Handle()
        {
            var genrelist = _context.Genres.Where(x => x.IsActive).OrderBy(x => x.GenreID);
            List<GenreViewModel> vm = _mapper.Map<List<GenreViewModel>>(genrelist);
            return vm;
        }
        public class GenreViewModel
        {
            public int GenreID { get; set; }
            public string GenreName { get; set; }
            
        }

    }
}
