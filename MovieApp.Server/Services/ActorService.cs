using MovieApp.Server.Repositories;
using MovieApp.Server.Models;

namespace MovieApp.Server.Services
{
    public class ActorService
    {
        private readonly IActorRepository repository;

        public ActorService(IActorRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Actor>> GetActors() => await repository.GetActors();

        public async Task<Actor?> FindActor(int actorId) => await repository.Get(actorId);
        public async Task Create(Actor actor) => await repository.Add(actor);
        public async Task Update(Actor actor) => await repository.Update(actor);
        public async Task Remove(int actorId) => await repository.Delete(actorId);
    }
}
