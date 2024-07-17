using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;


namespace MovieApp.Server.Repositories
{
    public class SQLMovieRepository : IMovieRepository
    {
        private readonly MovieAppContext context;
        private readonly IWebHostEnvironment env;

        public SQLMovieRepository(MovieAppContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }

        public async Task Add(Movie movie)
        {
            try
            {
                context.Add(movie);
                await context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task Delete(int movieId)
        {
            var movie = Get(movieId);
            context.Remove(movie);
            await context.SaveChangesAsync();
        }

        public async Task<Movie?> Get(int movieId) {
            var movie = await context.Movies.Where(m => m.MovieId == movieId)
            .Include(m => m.Director)
            .Include(m => m.ActorMovies)
            .ThenInclude(am => am.Actor)
            .FirstOrDefaultAsync();

            addBase64ToMovie(movie);

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            var movies = await context.Movies
            .Include(m => m.Director)
            .Include(m => m.ActorMovies)
            .ThenInclude(am => am.Actor)
            .AsNoTracking()
            .ToListAsync();

            foreach(Movie movie in movies)
            {
                addBase64ToMovie(movie);
            }

            return movies;
        }

        public async Task Update(Movie movie)
        {
            try
            {
                context.Movies.Update(movie);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        private Movie addBase64ToMovie(Movie movie)
        {

            var filePath = Path.Combine(env.WebRootPath, "images", movie.Picture);

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(env.WebRootPath, "images", "dummy-image-square.jpg");
            }
            
            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(imageBytes);

            movie.Picture = base64String;

            return movie;
        }
    }
}
