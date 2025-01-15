using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService(ApplicationDbContext dbContext) : IAuthorService
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task<Author> GetAuthor()
        {
            return await _dbContext.Books
                .AsNoTracking()
                .OrderByDescending(b => b.Title.Length)
                .ThenBy(b => b.AuthorId)
                .Select(b => b.Author)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _dbContext.Authors
                .AsNoTracking()
                .Where(
                    a => (a.Books.Count & 1) == 0 &&
                    a.Books.Count != 0 &&
                    !a.Books.Any(b => b.PublishDate.Year <= 2015))
                .ToListAsync();
        }
    }
}
