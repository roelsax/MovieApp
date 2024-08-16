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
            var actor = await context.Actors.Where(a => a.ActorId == actorId)
                            .FirstOrDefaultAsync();

            if (actor != null)
            {
                await DeleteActorImage(actor.Picture);
                context.Actors.Remove(actor);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteActorImage(Image? image)
        {
            if (image == null) 
            {
                return;
            }
            string imagePath = image.ImagePath;
            var filePath = Path.Combine(env.WebRootPath, "images", imagePath);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);

                context.Images.Remove(image);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Actor?> Get(int actorId) {
            var actor = await context.Actors.Where(a => a.ActorId == actorId)
                .Include(a => a.Picture)
                .Include(a => a.ActorMovies)
                .ThenInclude(am => am.Movie)
                .ThenInclude(am => am.Picture)
                .FirstOrDefaultAsync();

            if (actor == null)
            {
                return null;
            }

            return actor;
        }
       
        public async Task<IEnumerable<Actor>> GetActors() {
            var actors = await context.Actors
            .Include(a => a.Picture)
            .Include(a => a.ActorMovies)
            .ThenInclude(am => am.Movie)
            .ThenInclude(am => am.Picture)
            .AsNoTracking()
            .ToListAsync();

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
    }
}
