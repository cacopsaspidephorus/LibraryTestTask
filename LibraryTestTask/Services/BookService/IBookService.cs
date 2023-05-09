using LibraryTestTask.Models;

namespace LibraryTestTask.Services
{
    public interface IBookService
    {
        public Task<List<Book>> GetAllBooksAsync();
        public Task<Book> GetBookByIdAsync(int bookId);
        public Task<List<Book>> SearchBooksByNameAsync(string name);
        public Task AddBookAsync(Book book);
        public Task UpdateBookAsync(int bookId, Book updatedBook);
        public Task DeleteBookAsync(int bookId);
        public Task<List<Book>> GetIssuedBooksAsync();
        public Task<List<Book>> GetAvailableBooksAsync();
        public Task<List<History>> GetBookHistoryAsync(int bookId);
    }
}
