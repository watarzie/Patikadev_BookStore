using AutoMapper;
using Patikadev_BookStore.BookOperations.GetBooks;
using Patikadev_BookStore.BookOperations.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Patikadev_BookStore.BookOperations.CreateBook.CreateBookCommand;

namespace Patikadev_BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookViewIdModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
        }
    }
}
