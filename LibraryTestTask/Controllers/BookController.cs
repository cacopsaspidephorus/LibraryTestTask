using LibraryTestTask.Models;
using LibraryTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTestTask.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            List<Book> books = await _bookService.GetAllBooksAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            Book book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooksByName(string name)
        {
            List<Book> books = await _bookService.SearchBooksByNameAsync(name);

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            try
            {
                await _bookService.AddBookAsync(book);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            try
            {
                await _bookService.UpdateBookAsync(id, updatedBook);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                await _bookService.DeleteBookAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("issued")]
        public async Task<IActionResult> GetIssuedBooks()
        {
            List<Book> issuedBooks = await _bookService.GetIssuedBooksAsync();

            return Ok(issuedBooks);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBooks()
        {
            List<Book> availableBooks = await _bookService.GetAvailableBooksAsync();

            return Ok(availableBooks);
        }

        [HttpGet("{bookId}/history")]
        public async Task<IActionResult> GetBookHistory(int bookId)
        {
            List<History> history = await _bookService.GetBookHistoryAsync(bookId);

            return Ok(history);
        }
    }
}
