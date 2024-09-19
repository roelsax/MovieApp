using MovieApp.Server.Models;

namespace MovieApp.Server.Services
{
    public interface IDirectorService
    {
        public Task<IEnumerable<Director>> GetDirectors();
        public Task<Director?> FindDirector(int directorId);
        public Task Create(Director director);
        public Task Update(Director director);
        public Task Remove(int directorId);
    }
}
