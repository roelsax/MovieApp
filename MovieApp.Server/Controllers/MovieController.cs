using MovieApp.Server.Models;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Services;
using Microsoft.EntityFrameworkCore;
using MovieApp.Server.DTOs;
using System.Text.Json;
using System.IO;
using System;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;


namespace MovieApp.Server.Controllers
{
    [Route("movies")]
    [ApiController]
    public class MovieController(IMovieService movieService, IWebHostEnvironment env) : Controller
    {
        private static readonly object _fileLock = new object();

        [HttpGet]
        public async Task<IActionResult> FindAll(string? search, string? genre){

            int? genreInt;
            if (genre != null)
            {
                genreInt = GetGenreValue(genre);
            } else
            {
                genreInt = null;
            }
            
            var movies = await movieService.GetMovies(search, genreInt);

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
                        Picture = new ImageDTO {
                            ImagePath = movie.Picture?.ImagePath ?? null,
                            Base64 = getBase64(movie.Picture?.ImagePath ?? null)
                        },
                        Genres = movie.Genres.Select(g => new GenreDTO
                        {
                            EnumId = (int)g,
                            Name = g.ToString()
                        }).ToList(),
                        Actors = movie.ActorMovies?.Select(am => new ActorMovieDTO
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
                Picture = new ImageDTO
                {
                    ImagePath = movie.Picture?.ImagePath ?? null,
                    Base64 = getBase64(movie.Picture?.ImagePath ?? null)
                },
                Genres = movie.Genres.Select(g => new GenreDTO 
                {
                    EnumId = (int) g,
                    Name = g.ToString()
                }).ToList(),
                Actors = movie.ActorMovies.Select(am => new ActorMovieDTO
                {
                    ActorId = am.Actor.ActorId,
                    Name = am.Actor.Name,
                    Picture = new ImageDTO {
                        ImagePath = am.Actor.Picture?.ImagePath ?? null,
                        Base64 = getBase64(am.Actor.Picture?.ImagePath ?? null)
                    }
                }).ToList(),

            };

            return base.Ok(movieMapped);
        }  

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] JsonElement movieJson)
        {
            if (!movieJson.TryGetProperty("releaseDate", out var releaseDate) || string.IsNullOrEmpty(releaseDate.GetString()))
            {
                ModelState.AddModelError("releaseDate", "Release date is required.");
            }

            Movie newMovie = new Movie()
            {
                Name = HtmlEncoder.Default.Encode(movieJson.GetProperty("name").GetString()),
                Picture = processPicture(movieJson),
                Description = HtmlEncoder.Default.Encode(movieJson.GetProperty("description").GetString()),
            };

            if (ModelState.IsValid)
            {
                try
                {
                   DateTime dateTime = DateTime.Parse(releaseDate.GetString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
                    newMovie.ReleaseDate = DateOnly.FromDateTime(dateTime);
                }
                catch (FormatException)
                {
                    ModelState.AddModelError("releaseDate", "Invalid release date format.");
                }
            }

            movieJson.TryGetProperty("directorId", out var directorId);

            if (directorId.ValueKind != JsonValueKind.Undefined && directorId.ValueKind != JsonValueKind.Null)
            {
                newMovie.DirectorId = directorId.GetInt32();
            } else
            {
                ModelState.AddModelError("Director", "Director is required.");
            }

            addActors(movieJson, newMovie);
            addGenres(movieJson, newMovie);

            var validationResultList = new List<ValidationResult>();

            if (newMovie.Genres == null)
            {
                ModelState.AddModelError("Genres", "Genres is required.");
            }

            if (!Validator.TryValidateObject(newMovie, new ValidationContext(newMovie), validationResultList))
            {
                return base.BadRequest(ModelState);
            }

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
        public async Task<ActionResult> Put(int id, [FromBody] JsonElement movieJson)
        {
            if (!movieJson.TryGetProperty("releaseDate", out var releaseDate) || string.IsNullOrEmpty(releaseDate.GetString()))
            {
                ModelState.AddModelError("releaseDate", "Release date is required.");
            }

            //DateTime dateTime = DateTime.Parse(movieJson.GetProperty("releaseDate").GetString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
            
            var movie = await movieService.FindMovie(id);

            if (movie != null)
            {
                movie.Name = HtmlEncoder.Default.Encode(movieJson.GetProperty("name").GetString());
                //movie.ReleaseDate = DateOnly.FromDateTime(dateTime);
                //movie.DirectorId = movieJson.GetProperty("directorId").GetInt32();
                movie.Description = HtmlEncoder.Default.Encode(movieJson.GetProperty("description").GetString());

                if (ModelState.IsValid)
                {
                    try
                    {
                        DateTime dateTime = DateTime.Parse(releaseDate.GetString(), null, System.Globalization.DateTimeStyles.RoundtripKind);
                        movie.ReleaseDate = DateOnly.FromDateTime(dateTime);
                    }
                    catch (FormatException)
                    {
                        ModelState.AddModelError("releaseDate", "Invalid release date format.");
                    }
                }

                movieJson.TryGetProperty("directorId", out var directorId);

                if (directorId.ValueKind != JsonValueKind.Undefined && directorId.ValueKind != JsonValueKind.Null)
                {
                    movie.DirectorId = directorId.GetInt32();
                }
                else
                {
                    ModelState.AddModelError("Director", "Director is required.");
                }
                

                if (movieJson.TryGetProperty("picture", out var newPicture) && !string.IsNullOrEmpty(newPicture.GetProperty("image").ToString()))
                {
                    var base64 = newPicture.GetProperty("image").ToString();
                    var newFileName = generateFileName(newPicture.GetProperty("name").ToString());
                    if (newPicture.GetProperty("name").ToString() != null &&
                        movie.Picture?.ImagePath != newPicture.GetProperty("name").ToString())
                    {
                        movie.Picture = new Image
                        {
                            ImagePath = newFileName,
                        };
                        savePicture(base64, movie.Picture.ImagePath);
                    }
                }

                addActors(movieJson, movie);
                addGenres(movieJson, movie);

                var validationResultList = new List<ValidationResult>();

                if (movie.Genres == null)
                {
                    ModelState.AddModelError("Genres", "Genres is required.");
                }

                if (!Validator.TryValidateObject(movie, new ValidationContext(movie), validationResultList))
                {
                    return base.BadRequest(ModelState);
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        movie.MovieId = id;
                        await movieService.Update(movie);
                        return base.Ok();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return base.NotFound();
                    }
                    catch
                    {
                        return base.Problem();
                    }
                }
                return base.BadRequest(ModelState);
            } else
            {
                return base.NotFound();
            }
        }
        [HttpDelete("delete/{id}")]
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


        private void saveImage(string base64, string fileName)
        {
            byte[] pictureBytes = Convert.FromBase64String(base64);

            string imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }
            
            string filePath = Path.Combine(imagesPath, fileName);

            lock (_fileLock)
            {
                if (!System.IO.File.Exists(filePath))
                {
                    
                    System.IO.File.WriteAllBytesAsync(filePath, pictureBytes).GetAwaiter().GetResult();
                    
                }
            }
        }

        private Image? processPicture(JsonElement movieJson)
        {
            
            if (movieJson.TryGetProperty("picture", out var picture) && !string.IsNullOrEmpty(picture.GetString()))
            {
                var base64 = picture.GetProperty("image").ToString();
                var newFileName = generateFileName(picture.GetProperty("name").ToString());
                if (!string.IsNullOrEmpty(newFileName))
                {
                    savePicture(base64, newFileName);
                }

                return new Image
                {
                    ImagePath = newFileName
                };
            }
            return null;
        }

        private void savePicture(string base64, string fileName)
        {
            string[] splitString = base64.Split(new string[] { "base64," }, StringSplitOptions.None);
            
            saveImage(splitString[1], fileName);
        }

        private string generateFileName(string originalFileName)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = $"{timestamp}_{originalFileName}";

            return fileName;
        }

        private void addActors(JsonElement movieJson, Movie newMovie)
        {
            if (movieJson.TryGetProperty("actors", out JsonElement actorsJson))
            {
                newMovie.ActorMovies = actorsJson.EnumerateArray().Any() ? new List<ActorMovie>() : null;
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
                newMovie.Genres = genresJson.EnumerateArray().Any() ? new List<Genre>() : null;
                foreach (var genreJson in genresJson.EnumerateArray())
                {
                    var genre = (Genre)Enum.Parse(typeof(Genre), genreJson.GetProperty("name").GetString(), true);

                    newMovie.Genres.Add(genre);
                }
            }
        }

        private int GetGenreValue(string genreName)
        {
            if (Enum.TryParse(typeof(Genre), genreName, true, out var genre))
            {
                return (int)genre;
            }
            else
            {
                throw new ArgumentException($"Invalid genre name: {genreName}");
            }
        }

        private string? getBase64(string? path)
        {
            var filePath = !string.IsNullOrEmpty(path) ? Path.Combine(env.WebRootPath, "images", path) : null;

            if (!System.IO.File.Exists(filePath))
            {
                filePath = Path.Combine(env.WebRootPath, "images", "dummy-image-square.jpg");
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }
    }
}
