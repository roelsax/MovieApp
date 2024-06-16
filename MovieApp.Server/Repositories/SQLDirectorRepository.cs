using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;

namespace MovieApp.Server.Repositories
{
    public class SQLDirectorRepository : IDirectorRepository
    {
        private readonly MovieAppContext context;
        public SQLDirectorRepository(MovieAppContext context)
        {
            this.context = context;
        }

        public async Task Add(Director director)
        {
            try
            {
                context.Add(director);
                await context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task Delete(int directorId)
        {
            var director = Get(directorId);
            context.Remove(director);
            await context.SaveChangesAsync();
        }

        public async Task<Director?> Get(int directorId) =>
            await context.Directors.FindAsync(directorId);
        

        public async Task<IEnumerable<Director>> GetAll() => await context.Directors.AsNoTracking().ToListAsync();
        

        public async Task Update(Director director)
        {
            try
            {
                context.Update(director);
                await context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
