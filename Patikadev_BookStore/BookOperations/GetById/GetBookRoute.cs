using AutoMapper;
using Patikadev_BookStore.Common;
using Patikadev_BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patikadev_BookStore.BookOperations.GetById
{
    public class GetBookRoute
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public int BookId { get; set; }
        public GetBookRoute(BookStoreDbContext dbcontext,IMapper mapper)
        {
            _dbContext = dbcontext;
        }
        public BookViewIdModel Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if(book is null)
            {
                throw new InvalidOperationException("Kitap bulunamadı");
            }
            BookViewIdModel model = _mapper.Map<BookViewIdModel>(book);                 //new BookViewIdModel();
            //model.Title = book.Title;
            //model.Genre = ((GenreEnum)book.GenreId).ToString(); // Genre enum casting
            //model.PageCount = book.PageCount;
            //model.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
            return model;




        }
      
    }
    public class BookViewIdModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}
