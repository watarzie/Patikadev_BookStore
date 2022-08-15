using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patikadev_BookStore.BookOperations.CreateBook;
using Patikadev_BookStore.BookOperations.GetBooks;
using Patikadev_BookStore.BookOperations.GetById;
using Patikadev_BookStore.BookOperations.UpdateBook;
using Patikadev_BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Patikadev_BookStore.BookOperations.CreateBook.CreateBookCommand;
using static Patikadev_BookStore.BookOperations.UpdateBook.UpdateBookBody;

namespace Patikadev_BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        //private static List<Book> BookList = new List<Book>()
        //{
        //    new Book
        //    {
        //        Id=1,
        //        Title="Lean Startup",
        //        GenreId=1,//Personel Growth
        //        PageCount=200,
        //        PublishDate=new DateTime(2001,6,12)
        //    },
        //    new Book
        //    {
        //        Id=2,
        //        Title="Herland",
        //        GenreId=2,//Science Fiction
        //        PageCount=250,
        //        PublishDate=new DateTime(2010,5,23)
        //    },
        //    new Book
        //    {
        //        Id=3,
        //        Title="Dune",
        //        GenreId=1,//Science Fiction
        //        PageCount=540,
        //        PublishDate=new DateTime(2001,12,21)
        //    }
        //};
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result=query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]//From Route
        public IActionResult GetById(int id)
        {
            GetBookRoute route = new GetBookRoute(_context);
            var result = route.Handle(id);
            return Ok(result);

        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                
                command.Model = newBook;
                command.Handle();
                
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok();

        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookBody command = new UpdateBookBody(_context);
            try
            {
                
                command.Model = updatedBook;
                command.Handle(id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if(book is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

    }
}
