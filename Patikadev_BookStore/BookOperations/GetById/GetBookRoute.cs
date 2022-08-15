﻿using Patikadev_BookStore.Common;
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
        public GetBookRoute(BookStoreDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public BookViewIdModel Handle(int id)
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Id == id);
            BookViewIdModel model = new BookViewIdModel();
            model.Title = book.Title;
            model.Genre = ((GenreEnum)book.GenreId).ToString();
            model.PageCount = book.PageCount;
            model.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy");
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