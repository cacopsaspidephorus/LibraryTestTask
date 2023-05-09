using LibraryTestTask.Models;

namespace LibraryTestTask.Services
{
    public interface IReaderService
    {
        public Task<List<Reader>> GetAllReadersAsync();
        public Task<Reader> GetReaderByIdAsync(int readerId);
        public Task<List<Reader>> SearchReadersByNameAsync(string name);
        public Task AddReaderAsync(Reader reader);
        public Task UpdateReaderAsync(int readerId, Reader updatedReader);
        public Task DeleteReaderAsync(int readerId);
        public Task IssueBookToReaderAsync(int readerId, int bookId);
        public Task ReturnBookAsync(int readerId, int bookId);
        public Task<List<History>> GetReaderHistoryAsync(int readerId);
       
    }
}
