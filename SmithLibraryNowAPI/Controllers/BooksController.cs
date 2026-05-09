using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SmithLibraryNowAPI.Models;

namespace SmithLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Clean Code",
                Author = "Robert Martin",
                Genre = "Programming",
                Available = true,
                PublishedYear = 2008
            },
            new Book
            {
                Id = 2,
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt",
                Genre = "Programming",
                Available = true,
                PublishedYear = 1999
            }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new
            {
                status = "success",
                data = books,
                message = "Books Retrieved."
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book retrieved."
            });
        }

        [HttpPost]
        public IActionResult Creaet([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById),
                new { id = newBook.Id},
                new { status = "success",
                data = newBook,
                message = "Book created."});
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,
            [FromBody] Book updateBook)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });
            
            book.Title = updateBook.Title;
            book.Author = updateBook.Author;
            book.Genre = updateBook.Genre;
            book.Available = updateBook.Available;
            book.PublishedYear = updateBook.PublishedYear;

            return Ok(new
            {
                status = "success",
                data = book,
                message = "Book updated."
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book == null)
                return NotFound(new
                {
                    status = "error",
                    data = (object?)null,
                    message = "Book not found."
                });

            books.Remove(book);
            return Ok(new
            {
                status = "success",
                data = (object?)null,
                message = "Book deleted."
            });

        }
    }
}
