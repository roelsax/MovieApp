using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Server.Repositories
{
    public class SQLDirectorRepository : IDirectorRepository
    {
        private readonly MovieAppContext context;
        private readonly IWebHostEnvironment env;

        public SQLDirectorRepository(MovieAppContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public async Task Add(Director director)
        {
            try
            {
                context.Add(director);
                await context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task Delete(int directorId)
        {
            var director = await context.Directors
                .Where(d => d.DirectorId == directorId)
                .FirstOrDefaultAsync();

            if (director != null)
            {
                context.Directors.Remove(director);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Director?> Get(int directorId) 
        {
            var director = await context.Directors
                .Where(d => d.DirectorId == directorId)
                .Include(d => d.Movies)
                .FirstOrDefaultAsync();

            if (director == null)
            {
                return null;
            }


            addBase64ToDirector(director);
            foreach(Movie movie in director.Movies)
            {
                addBase64ToMovie(movie);
            }
            return director;
        }
       
        public async Task<IEnumerable<Director>> GetAll()
        {
            var directors =  await context.Directors
                .Include(d => d.Movies)
                .AsNoTracking()
                .ToListAsync();

            foreach (Director director in directors) 
            {
                addBase64ToDirector(director);
            }
            
            return directors;
        }
        public async Task Update(Director director)
        {
            try
            {
                context.Update(director);
                await context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        private Director addBase64ToDirector(Director director)
        {
            var filePath = Path.Combine(env.WebRootPath, "images", director.Picture);

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(env.WebRootPath, "images", "dummy-person.jpg");
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

            string base64String = Convert.ToBase64String(imageBytes);

            director.Picture = base64String;
            return director;
        }

        private Movie addBase64ToMovie(Movie movie)
        {
            var filePath = Path.Combine(env.WebRootPath, "images", movie.Picture);

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(env.WebRootPath, "images", "dummy-person.jpg");
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);

            string base64String = Convert.ToBase64String(imageBytes);

            movie.Picture = base64String;
            return movie;
        }
    }
}
