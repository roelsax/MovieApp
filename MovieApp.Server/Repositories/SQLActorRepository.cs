using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MovieApp.Server.Repositories
{
    public class SQLActorRepository : IActorRepository
    {
        private readonly MovieAppContext context;
        private readonly IWebHostEnvironment env;

        public SQLActorRepository(MovieAppContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env; 
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

        public async Task<Actor?> Get(int actorId) {
            var actor = await context.Actors.Where(a => a.ActorId == actorId)
                .Include(a => a.ActorMovies)
                .ThenInclude(am => am.Movie)
                .FirstOrDefaultAsync();

            if (actor == null)
            {
                return null;
            }


            addBase64ToActor(actor);

            foreach(ActorMovie actorMovie in actor.ActorMovies)
            {
                addBase64ToActorMovies(actorMovie);
            }

            return actor;
        }
       
        public async Task<IEnumerable<Actor>> GetActors() {
            var actors = await context.Actors
            .Include(a => a.ActorMovies)
            .ThenInclude(am => am.Movie)
            .AsNoTracking()
            .ToListAsync();

            foreach(Actor actor in actors) 
            {
                addBase64ToActor(actor);
            }

            return actors;
        }
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

        private Actor addBase64ToActor(Actor actor)
        {
            var filePath = Path.Combine(env.WebRootPath, "images", actor.Picture);

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(env.WebRootPath, "images", "dummy-person.jpg");
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

            string base64String = Convert.ToBase64String(imageBytes);

            actor.Picture = base64String;
            return actor;
        }

        private ActorMovie addBase64ToActorMovies(ActorMovie actorMovie)
        {
            var filePath = Path.Combine(env.WebRootPath, "images", actorMovie.Movie.Picture);

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(env.WebRootPath, "images", "dummy-person.jpg");
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

            string base64String = Convert.ToBase64String(imageBytes);

            actorMovie.Movie.Picture = base64String;
            return actorMovie;
        }
    }
}
