using Microsoft.Identity.Client;
using MovieApp.Server.Models;

namespace MovieApp.Server.Repositories
{
    public interface IMovieRepository
    {
        public Task<IEnumerable<Movie>> GetAll(string? search, int? genre);
        public Task<Movie?> Get(int movieId);
        public Task Add(Movie movie);
        public Task Update(Movie movie);
        public Task Delete(int movieId);
    }
}
