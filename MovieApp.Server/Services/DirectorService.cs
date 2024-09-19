using MovieApp.Server.Repositories;
using MovieApp.Server.Models;

namespace MovieApp.Server.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository repository;

        public DirectorService(IDirectorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Director>> GetDirectors() => await repository.GetAll();

        public async Task<Director?> FindDirector(int directorId) => await repository.Get(directorId);

        public async Task Create(Director director) => await repository.Add(director);

        public async Task Update(Director director) => await repository.Update(director);

        public async Task Remove(int directorId) => await repository.Delete(directorId);
    }
}
