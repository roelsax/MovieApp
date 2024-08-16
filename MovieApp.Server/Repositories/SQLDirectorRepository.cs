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
                await DeleteDirectorImage(director.Picture);
                context.Directors.Remove(director);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteDirectorImage(Image? image)
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

        public async Task<Director?> Get(int directorId) 
        {
            var director = await context.Directors
                .Where(d => d.DirectorId == directorId)
                .Include(d => d.Movies)
                .ThenInclude(d => d.Picture)
                .Include(d => d.Picture)
                .FirstOrDefaultAsync();

            if (director == null)
            {
                return null;
            }

            return director;
        }
       
        public async Task<IEnumerable<Director>> GetAll()
        {
            var directors =  await context.Directors
                .Include(d => d.Movies)
                .Include(d => d.Picture)
                .AsNoTracking()
                .ToListAsync();
            
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
    }
}
