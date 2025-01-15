using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService(ApplicationDbContext dbContext) : IBookService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Book> GetBook()
        {
            return await _dbContext.Books
                .AsNoTracking()
                .OrderByDescending(b => b.QuantityPublished * b.Price)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Book>> GetBooks()
        {
            var albumReleaseDate = new DateTime(2015, 5, 12, 12, 0, 0);

            return await _dbContext.Books
                .AsNoTracking()
                .Where(
                    b => b.Title.Contains("Red") &&
                    b.PublishDate > albumReleaseDate)
                .ToListAsync();
        }
    }
}
