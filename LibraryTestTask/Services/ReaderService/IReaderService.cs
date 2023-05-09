using LibraryTestTask.Models;

namespace LibraryTestTask.Services
{
    public interface IReaderService
    {
        Task<List<Reader>> GetAllReadersAsync();
        Task<Reader> GetReaderByIdAsync(int readerId);
        Task<List<Reader>> SearchReadersByNameAsync(string name);
        Task AddReaderAsync(Reader reader);
        Task UpdateReaderAsync(int readerId, Reader updatedReader);
        Task DeleteReaderAsync(int readerId);
        Task IssueBookToReaderAsync(int readerId, int bookId);
        Task ReturnBookAsync(int readerId, int bookId);
        Task<List<History>> GetReaderHistoryAsync(int readerId);
       
    }
}
