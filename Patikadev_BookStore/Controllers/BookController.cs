using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patikadev_BookStore.BookOperations.CreateBook;
using Patikadev_BookStore.BookOperations.DeleteBook;
using Patikadev_BookStore.BookOperations.GetBooks;
using Patikadev_BookStore.BookOperations.GetById;
using Patikadev_BookStore.BookOperations.UpdateBook;
using Patikadev_BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Patikadev_BookStore.BookOperations.CreateBook.CreateBookCommand;
using static Patikadev_BookStore.BookOperations.UpdateBook.UpdateBookCommand;

namespace Patikadev_BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result=query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]//From Route
        public IActionResult GetById(int id)
        {
            BookViewIdModel result;
            try
            {
                GetBookRoute route = new GetBookRoute(_context,_mapper);
                route.BookId = id;
                result=route.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
            return Ok(result);

        }
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
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
           
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();
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
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
         
            return Ok();
          
        }

    }
}
