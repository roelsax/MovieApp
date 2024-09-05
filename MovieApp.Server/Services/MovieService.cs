using MovieApp.Server.Repositories;
using MovieApp.Server.Models;

namespace MovieApp.Server.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository repository;

        public MovieService(IMovieRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Movie>> GetMovies(string? search, int? genre) => await repository.GetAll(search, genre);
        public async Task<Movie?> FindMovie(int movieId) => await repository.Get(movieId);
        public async Task Create(Movie movie) => await repository.Add(movie);
        public async Task Update(Movie movie) => await repository.Update(movie);
        public async Task Remove(int movieId) => await repository.Delete(movieId);
    }
}
