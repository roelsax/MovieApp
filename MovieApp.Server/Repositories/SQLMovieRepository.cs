using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Hosting;


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
            Movie movie = await context.Movies.Where(m => m.MovieId == movieId).FirstOrDefaultAsync();

            if (movie != null)
            {
                await DeleteMovieImage(movie.Picture);
                context.Movies.Remove(movie);
                await context.SaveChangesAsync();
            }
            
        }

        public async Task DeleteMovieImage(Image? image)
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

        public async Task<Movie?> Get(int movieId) {
            var movie = await context.Movies.Where(m => m.MovieId == movieId)
            .Include(m => m.Director)
            .Include(m => m.Picture)
            .Include(m => m.ActorMovies)
            .ThenInclude(am => am.Actor)
            .ThenInclude(am => am.Picture)
            .FirstOrDefaultAsync();

            if (movie == null)
            {
                return null;
            }

            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAll(string? search, int? genre)
        {
            var query = context.Movies
                .Include(m => m.Director)
                .Include(m => m.Picture)
                .Include(m => m.ActorMovies)
                .ThenInclude(am => am.Actor)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(m => m.Name.StartsWith(search));
            }

            if (genre.HasValue && genre != -1)
            {
                query = query.Where(m => m.Genres.Contains((Genre)genre));
            }

            var movies = query.ToListAsync();

            return await movies;
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

    }
}
