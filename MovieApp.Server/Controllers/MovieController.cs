using Microsoft.AspNetCore.DataProtection.Repositories;
using MovieApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Services;
using Microsoft.EntityFrameworkCore;
using MovieApp.Server.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using MovieApp.Server.Repositories.Seeding;

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
                        Actors = movie.ActorMovies.Select(am => new ActorMovieDTO
                        {
                            ActorId = am.Actor.ActorId,
                            Name = am.Actor.Name

                        }).ToList(),
                    }
                );
            }

            return base.Ok(moviesMapped);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id) 
        {
            var movie = await movieService.FindMovie(id);

            if (movie == null)
            {
                return base.NotFound();
            }

         
            MovieDTO movieMapped = new MovieDTO
            {
                MovieId = movie.MovieId,
                Name = movie.Name,
                ReleaseDate = movie.ReleaseDate,
                Description = movie.Description,
                Director = movie.Director,
                Picture = movie.Picture,
                Genres = movie.Genres.Select(g => g.ToString()).ToList(),
                Actors = movie.ActorMovies.Select(am => new ActorMovieDTO
                {
                    ActorId = am.Actor.ActorId,
                    Name = am.Actor.Name,
                    Picture = am.Actor.Picture
                    
                }).ToList(),

            };

            return base.Ok(movieMapped);
        }  

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] JsonElement movieJson)
        {
            DateTime dateTime = DateTime.Parse(movieJson.GetProperty("releaseDate").GetString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
            var picture = movieJson.GetProperty("picture");
            var base64 = picture.GetProperty("image").ToString();
            Movie newMovie = new Movie()
            {
                Name = movieJson.GetProperty("name").GetString(),
                ReleaseDate = DateOnly.FromDateTime(dateTime),
                Picture = picture.GetProperty("name").ToString(),
                DirectorId = movieJson.GetProperty("directorId").GetInt32(),
                Description = movieJson.GetProperty("description").GetString(),
                ActorMovies = new List<ActorMovie>(),
                Genres = new List<Genre>()
            };

            if (!string.IsNullOrEmpty(newMovie.Picture))
            {
                savePicture(base64, newMovie.Picture);
            }

            addActors(movieJson, newMovie);
            addGenres(movieJson, newMovie);

            if (ModelState.IsValid)
            {
                try
                {
                    await movieService.Create(newMovie);
                    return base.Ok();
                }
                catch (DbUpdateException)
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

        [HttpGet("genres")]
        public async Task<ActionResult> getGenres()
        {
            return base.Ok(Enum.GetNames(typeof(Genre)));
        }


        private async void saveImage(string base64, string fileName)
        {
            byte[] pictureBytes = Convert.FromBase64String(base64);

            string imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }
            string filePath = Path.Combine(imagesPath, fileName);
            await System.IO.File.WriteAllBytesAsync(filePath, pictureBytes);
        }

        private void savePicture(string base64, string fileName)
        {
            string[] splitString = base64.Split(new string[] { "base64," }, StringSplitOptions.None);

            saveImage(splitString[1], fileName);
        }

        private void addActors(JsonElement movieJson, Movie newMovie)
        {
            if (movieJson.TryGetProperty("actors", out JsonElement actorsJson))
            {
                foreach (var actorJson in actorsJson.EnumerateArray())
                {
                    var actorMovie = new ActorMovie
                    {
                        ActorId = actorJson.GetProperty("id").GetInt32(),
                        Movie = newMovie
                    };
                    newMovie.ActorMovies.Add(actorMovie);
                }
            }
        }

        private void addGenres(JsonElement movieJson, Movie newMovie)
        {
            if (movieJson.TryGetProperty("genres", out JsonElement genresJson))
            {
                foreach (var genreJson in genresJson.EnumerateArray())
                {
                    var genre = (Genre)Enum.Parse(typeof(Genre), genreJson.GetProperty("name").GetString(), true);

                    newMovie.Genres.Add(genre);
                }
            }
        }
    }
}
