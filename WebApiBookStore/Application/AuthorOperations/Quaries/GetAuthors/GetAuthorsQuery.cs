using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiBookStore.DbOperations;

namespace WebApiBookStore.Application.AuthorOperations.Quaries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetAuthorModel> Handle()
        {
            var author = _context.Authors.OrderBy(x => x.AuthorID).Include(x => x.Book).ToList();
            List<GetAuthorModel> vm = _mapper.Map<List<GetAuthorModel>>(author);
            
            
            return vm;
        }

        public class GetAuthorModel
        {
            public string Name { get; set; }
            public string SurName { get; set; }
            public string Book { get; set; }
            public int DateOfBirth { get; set; }
            
        }
    }
}
