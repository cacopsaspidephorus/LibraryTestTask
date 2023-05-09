using LibraryTestTask.Models;

namespace LibraryTestTask.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int bookId);
        Task<List<Book>> SearchBooksByNameAsync(string name);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(int bookId, Book updatedBook);
        Task DeleteBookAsync(int bookId);
        Task<List<Book>> GetIssuedBooksAsync();
        Task<List<Book>> GetAvailableBooksAsync();
        Task<List<History>> GetBookHistoryAsync(int bookId);
    }
}
