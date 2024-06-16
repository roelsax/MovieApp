using MovieApp.Server.Models;
namespace MovieApp.Server.Repositories
{
    public interface IActorRepository
    {
        public Task<IEnumerable<Actor>> GetActors();
        public Task<Actor?> Get(int actorId);
        public Task Add(Actor actor);
        public Task Update(Actor actor);
        public Task Delete(int actorId);
    }
}
