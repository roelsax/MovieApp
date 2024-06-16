using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;

namespace MovieApp.Server.Repositories
{
    public class SQLMovieRepository : IMovieRepository
    {
        private readonly MovieAppContext context;

        public SQLMovieRepository(MovieAppContext context) 
        { 
            this.context = context;
        }

        public async Task Add(Movie movie)
        {
            try
            {
                context.Add(movie);
                await context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task Delete(int movieId)
        {
            var movie = Get(movieId);
            context.Remove(movie);
            await context.SaveChangesAsync();
        }

        public async Task<Movie?> Get(int movieId) =>
            await context.Movies.Where(m => m.MovieId == movieId)
            .Include(m => m.Director)
            .Include(m => m.ActorMovies)
            .ThenInclude(am => am.Actor)
            .FirstOrDefaultAsync();

        public async Task<IEnumerable<Movie>> GetAll() => 
            await context.Movies
            .Include(m => m.Director)
            .Include(m => m.ActorMovies)
            .ThenInclude(am => am.Actor)
            .AsNoTracking()
            .ToListAsync();
        

        public async Task Update(Movie movie)
        {
            try
            {
                context.Movies.Update(movie);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
