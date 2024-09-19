using MovieApp.Server.Models;

namespace MovieApp.Server.Services
{
    public interface IActorService
    {
        public Task<IEnumerable<Actor>> GetActors();
        public Task<Actor?> FindActor(int actorId);
        public Task Create(Actor actor);
        public Task Update(Actor actor);
        public Task Remove(int actorId);
    }
}
