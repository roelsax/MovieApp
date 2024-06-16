using MovieApp.Server.Models;
namespace MovieApp.Server.Repositories
{
    public interface IDirectorRepository
    {
        public Task<IEnumerable<Director>> GetAll();
        public Task<Director?> Get(int directorId);
        public Task Add(Director director);
        public Task Update(Director director);
        public Task Delete(int directorId);
    }
}
