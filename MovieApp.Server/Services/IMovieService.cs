using MovieApp.Server.Models;

namespace MovieApp.Server.Services
{
    public interface IMovieService
    {
        public Task<IEnumerable<Movie>> GetMovies(string? search, int? genre);
        public Task<Movie?> FindMovie(int movieId);
        public Task Create(Movie movie);
        public Task Update(Movie movie);
        public Task Remove(int movieId);
    }
}
