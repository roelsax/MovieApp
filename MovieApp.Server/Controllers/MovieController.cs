using Microsoft.AspNetCore.DataProtection.Repositories;
using MovieApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Services;
using Microsoft.EntityFrameworkCore;
using MovieApp.Server.DTOs;

namespace MovieApp.Server.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MovieController(MovieService movieService) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> FindAll(){

            var movies = await movieService.GetMovies();

            List<MovieDTO> moviesMapped = new List<MovieDTO>();

            foreach (var movie in movies)
            {
                moviesMapped.Add(
                    new MovieDTO
                    {
                        MovieId = movie.MovieId,
                        Name = movie.Name,
                        ReleaseDate = movie.ReleaseDate,
                        Description = movie.Description,
                        Director = movie.Director,
                        Picture = movie.Picture,
                        Genres = movie.Genres.Select(g => g.ToString()).ToList(),
                        Actors = movie.ActorMovies.Select(am => am.Actor).ToList(),
                    }
                );
            }

            return base.Ok(moviesMapped);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id) => await movieService.FindMovie(id) is Movie movie ? base.Ok(movie) : base.NotFound();

        [HttpPost]
        public async Task<ActionResult> Create(Movie movie)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await movieService.Create(movie);
                    return base.Ok();
                } catch (DbUpdateException)
                {
                    return base.NotFound();
                }
                catch
                {
                    return base.Problem();
                }
                
            }
            return base.BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Movie movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    movie.MovieId = id;
                    await movieService.Update(movie);
                    return base.Ok();
                } catch(DbUpdateConcurrencyException)
                {
                    return base.NotFound();
                } catch
                {
                    return base.Problem();
                }
            }
            return base.BadRequest(ModelState);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await movieService.FindMovie(id);

            if (movie == null)
            {
                return base.NotFound();
            }

            await movieService.Remove(id);
            return base.Ok();
        }
    }
}
