using LibraryTestTask.Data;
using LibraryTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryTestTask.Services
{
    public class BookService : IBookService
    {
        private readonly ILogger _logger;
        private readonly LibraryContext _libraryContext;

        public BookService(LibraryContext libraryContext, ILogger<BookService> logger)
        {
            _libraryContext = libraryContext;
            _logger = logger;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _libraryContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            return await _libraryContext.Books
                .Include(b => b.Readers)
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<List<Book>> SearchBooksByNameAsync(string name)
        {
            return await _libraryContext.Books
                .Include(b => b.Readers)
                .Where(b => b.Name.Contains(name))
                .ToListAsync();
        }

        public async Task AddBookAsync(Book book)
        {
            if (await _libraryContext.Books.AnyAsync(b => b.Sku == book.Sku))
            {
                _logger.LogError(LogMessages.BookAlreadyExists);

                throw new ArgumentException(LogMessages.BookAlreadyExists);
            }

            book.DateOfAddition = DateTime.Now;
            book.DateOfChange = DateTime.Now;

            _libraryContext.Books.Add(book);

            await _libraryContext.SaveChangesAsync();

            _logger.LogInformation($"{book.Name} added");
        }

        public async Task UpdateBookAsync(int bookId, Book updatedBook)
        {
            Book book = await _libraryContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                _logger.LogError($"{LogMessages.BookNotFound} (Book id: {bookId})");

                throw new ArgumentException(LogMessages.BookNotFound);
            }

            if (book.Readers.Any())
            {
                _logger.LogError($"{LogMessages.CannotChangeBook} (Book id: {bookId})");

                throw new InvalidOperationException(LogMessages.CannotChangeBook);
            }

            book.Name = updatedBook.Name;
            book.Author = updatedBook.Author;
            book.YearOfPublication = updatedBook.YearOfPublication;
            book.NumberOfCopies = updatedBook.NumberOfCopies;
            book.DateOfChange = DateTime.Now;

            await _libraryContext.SaveChangesAsync();

            _logger.LogInformation($"{book.Name} updated");
        }

        public async Task DeleteBookAsync(int bookId)
        {
            Book book = await _libraryContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                _logger.LogError($"{LogMessages.BookNotFound} (Book id: {bookId})");

                throw new ArgumentException(LogMessages.BookNotFound);
            }

            if (book.Readers.Any())
            {
                _logger.LogError($"{LogMessages.CannotDeleteBook} (Book id: {bookId})");

                throw new InvalidOperationException(LogMessages.CannotDeleteBook);
            }

            _libraryContext.Books.Remove(book);

            await _libraryContext.SaveChangesAsync();

            _logger.LogInformation($"{book.Name} deleted");
        }

        public async Task<List<Book>> GetIssuedBooksAsync()
        {
            return await _libraryContext.Books
                .Include(b => b.Readers)
                .Where(b => b.Readers.Any())
                .ToListAsync();
        }

        public async Task<List<Book>> GetAvailableBooksAsync()
        {
            return await _libraryContext.Books
                .Include(b => b.Readers)
                .Where(b => !b.Readers.Any())
                .ToListAsync();
        }

        public async Task<List<History>> GetBookHistoryAsync(int bookId)
        {
            return await _libraryContext.Histories
                .Where(h => h.BookId == bookId)
                .ToListAsync();
        }
    }
}
