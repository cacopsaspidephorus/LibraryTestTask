using LibraryTestTask.Data;
using LibraryTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryTestTask.Services
{
    public class ReaderService : IReaderService
    {
        private readonly ILogger _logger;
        private readonly LibraryContext _libraryContext;

        public ReaderService(LibraryContext libraryContext, ILogger<ReaderService> logger)
        {
            _libraryContext = libraryContext;
            _logger = logger;
        }

        public async Task<List<Reader>> GetAllReadersAsync()
        {
            return await _libraryContext.Readers.ToListAsync();
        }

        public async Task<Reader> GetReaderByIdAsync(int readerId)
        {
            return await _libraryContext.Readers
                .Include(r => r.IssuedBooks)
                .FirstOrDefaultAsync(r => r.Id == readerId);
        }

        public async Task<List<Reader>> SearchReadersByNameAsync(string name)
        {
            return await _libraryContext.Readers
                .Include(b => b.IssuedBooks)
                .Where(b => b.FullName.Contains(name))
                .ToListAsync();
        }

        public async Task AddReaderAsync(Reader reader)
        {
            if (await _libraryContext.Readers.AnyAsync(r => r.FullName == reader.FullName && r.DateOfBirth == reader.DateOfBirth))
            {
                _logger.LogError(LogMessages.ReaderAlreadyExists);

                throw new ArgumentException(LogMessages.ReaderAlreadyExists);
            }

            reader.DateOfAddition = DateTime.Now;
            reader.DateOfChange = DateTime.Now;

            _libraryContext.Readers.Add(reader);

            await _libraryContext.SaveChangesAsync();

            _logger.LogInformation($"{reader.FullName} added");
        }

        public async Task UpdateReaderAsync(int readerId, Reader updatedReader)
        {
            Reader reader = await _libraryContext.Readers.FirstOrDefaultAsync(r => r.Id == readerId);

            if (reader == null)
            {
                _logger.LogError($"{LogMessages.ReaderNotFound} (Reader id: {readerId})");

                throw new ArgumentException(LogMessages.ReaderNotFound);
            }

            if (reader.IssuedBooks.Any())
            {
                _logger.LogError($"{LogMessages.CannotChangeReader} (Reader id: {readerId})");

                throw new InvalidOperationException(LogMessages.CannotChangeReader);
            }

            reader.FullName = updatedReader.FullName;
            reader.DateOfBirth = updatedReader.DateOfBirth;
            reader.DateOfChange = DateTime.Now;

            await _libraryContext.SaveChangesAsync();

            _logger.LogInformation($"{reader.FullName} updated");
        }

        public async Task DeleteReaderAsync(int readerId)
        {
            Reader reader = await _libraryContext.Readers.FirstOrDefaultAsync(r => r.Id == readerId);

            if (reader == null)
            {
                _logger.LogError($"{LogMessages.ReaderNotFound} (Reader id: {readerId})");

                throw new ArgumentException(LogMessages.ReaderNotFound);
            }

            if (reader.IssuedBooks.Any())
            {
                _logger.LogError($"{LogMessages.CannotDeleteReader} (Reader id: {readerId})");

                throw new InvalidOperationException(LogMessages.CannotDeleteReader);
            }

            _libraryContext.Readers.Remove(reader);

            await _libraryContext.SaveChangesAsync();

            _logger.LogInformation($"{reader.FullName} deleted");
        }

        public async Task IssueBookToReaderAsync(int readerId, int bookId)
        {
            Reader reader = await _libraryContext.Readers.FirstOrDefaultAsync(r => r.Id == readerId);
            Book book = await _libraryContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (reader == null)
                throw new ArgumentException(LogMessages.ReaderNotFound);

            if (book == null)
                throw new ArgumentException(LogMessages.BookNotFound);

            if (book.Readers.Any(r => r.Id == readerId))
                throw new Exception(LogMessages.BookAlreadyIssued);

            book.Readers.Add(reader);

            _libraryContext.Histories.Add(new History
            {
                BookId = bookId,
                ReaderId = readerId,
                DateTaken = DateTime.Now,
            });

            await _libraryContext.SaveChangesAsync();

            _logger.LogInformation($"Book {book.Name} issued to reader {reader.FullName}");
        }

        public async Task ReturnBookAsync(int readerId, int bookId)
        {
            Reader reader = await _libraryContext.Readers.FirstOrDefaultAsync(r => r.Id == readerId);
            Book book = await _libraryContext.Books.FirstOrDefaultAsync(b => b.Id == bookId);

            if (reader == null)
                throw new ArgumentException(LogMessages.ReaderNotFound);

            if (book == null)
                throw new ArgumentException(LogMessages.BookNotFound);

            if (!book.Readers.Any(r => r.Id == readerId))
                throw new InvalidOperationException(LogMessages.BookIsNotIssued);

            book.Readers.Remove(reader);

            History bookHistory = await _libraryContext.Histories.FirstOrDefaultAsync(b => b.ReaderId == readerId && b.BookId == bookId);

            if (bookHistory != null)
                bookHistory.DateReturned = DateTime.Now;

            await _libraryContext.SaveChangesAsync();

            _logger.LogInformation($"Reader {reader.FullName} returned book {book.Name}");
        }

        public async Task<List<History>> GetReaderHistoryAsync(int readerId)
        {
            return await _libraryContext.Histories
                .Where(h => h.ReaderId == readerId)
                .ToListAsync();
        }
    }
}
