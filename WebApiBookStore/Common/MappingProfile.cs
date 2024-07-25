using AutoMapper;
using WebApiBookStore.Entities;
using static WebApiBookStore.Application.BookOperations.Commands.CreateBooks.CreateBooksQuery;
using static WebApiBookStore.Application.BookOperations.Quaries.GetBooks.GetBooksQuery;
using static WebApiBookStore.Application.BookOperations.Commands.UpdateBooks.UpdateBooksQuery;
using WebApiBookStore.Application.BookOperations.Quaries.GetBookDetails;
using static WebApiBookStore.Application.GenreOperations.Quaries.GetGenre.GetGenresQuery;
using WebApiBookStore.Application.GenreOperations.Commands.CreateGenres;
using static WebApiBookStore.Application.GenreOperations.Commands.CreateGenres.CreateGenresCommand;
using static WebApiBookStore.Application.GenreOperations.Quaries.GetGenreDetails.GetGenreDetailQuery;
using WebApiBookStore.Application.AuthorOperations.Quaries.GetAuthors;
using static WebApiBookStore.Application.AuthorOperations.Quaries.GetAuthors.GetAuthorsQuery;
using static WebApiBookStore.Application.AuthorOperations.Quaries.GetAuthorDetails.GetAuthorDetailQuery;
using static WebApiBookStore.Application.AuthorOperations.Commands.CreateAuthors.CreateAuthorCommand;
using System.Linq;
using static WebApiBookStore.Application.UserOperations.Commands.CreateUsers.CreateUsersCommand;

namespace WebApiBookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Book
            CreateMap<CreateBookModel, Book>();
                
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName));
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Name));
               
            
            
            //Genre
            CreateMap<Genre, GenreViewModel>();
            CreateMap<Genre, GenreDetailModel>();
            CreateMap<CreateGenreModel, Genre>();



            //Author
            CreateMap<Author, GetAuthorModel>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book.Title));
                
                
                
            CreateMap<Author, GetAuthorDetailModel>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<CreateAuthorModel, Author>();


            //User
            CreateMap<CreateUserModel, User>();

        }
    }
}
