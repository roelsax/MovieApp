using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;

namespace MovieApp.Server.Repositories
{
    public class SQLActorRepository : IActorRepository
    {
        private readonly MovieAppContext context;

        public SQLActorRepository(MovieAppContext context)
        {
            this.context = context;
        }
        public async Task Add(Actor actor)
        {
            try
            {
                context.Add(actor);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task Delete(int actorId)
        {
            var actor = Get(actorId);
            context.Remove(actor);
            await context.SaveChangesAsync();
        }

        public async Task<Actor?> Get(int actorId) =>
            await context.Actors.Where(a => a.ActorId == actorId)
                .Include(a => a.ActorMovies)
                .ThenInclude(am => am.Movie)
                .FirstOrDefaultAsync();
        

        public async Task<IEnumerable<Actor>> GetActors() =>
            await context.Actors
            .Include(a => a.ActorMovies)
            .ThenInclude(am => am.Movie)
            .AsNoTracking()
            .ToListAsync();
        

        public async Task Update(Actor actor)
        {
            try
            {
                context.Update(actor);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
