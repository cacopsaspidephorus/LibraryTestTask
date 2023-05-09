using LibraryTestTask.Models;
using LibraryTestTask.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTestTask.Controllers
{
    [ApiController]
    [Route("api/readers")]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderService _readerService;

        public ReaderController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReaders()
        {
            List<Reader> readers = await _readerService.GetAllReadersAsync();

            return Ok(readers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReaderById(int id)
        {
            Reader reader = await _readerService.GetReaderByIdAsync(id);

            if (reader == null)
                return NotFound();

            return Ok(reader);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchReadersByName(string name)
        {
            List<Reader> readers = await _readerService.SearchReadersByNameAsync(name);

            return Ok(readers);
        }

        [HttpPost]
        public async Task<IActionResult> AddReader(Reader reader)
        {
            try
            {
                await _readerService.AddReaderAsync(reader);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReader(int id, Reader updatedReader)
        {
            try
            {
                await _readerService.UpdateReaderAsync(id, updatedReader);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReader(int id)
        {
            try
            {
                await _readerService.DeleteReaderAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{readerId}/books/{bookId}/issue")]
        public async Task<IActionResult> IssueBookToReader(int readerId, int bookId)
        {
            try
            {
                await _readerService.IssueBookToReaderAsync(readerId, bookId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{readerId}/books/{bookId}/return")]
        public async Task<IActionResult> ReturnBook(int readerId, int bookId)
        {
            try
            {
                await _readerService.ReturnBookAsync(readerId, bookId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{readerId}/history")]
        public async Task<IActionResult> GetReaderHistory(int readerId)
        {
            List<History> history = await _readerService.GetReaderHistoryAsync(readerId);

            return Ok(history);
        }
    }
}
