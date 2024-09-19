using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Services;
using MovieApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using MovieApp.Server.DTOs;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;

namespace MovieApp.Server.Controllers
{
    [Route("actors")]
    [ApiController]
    public class ActorController(IActorService actorService, IWebHostEnvironment env) : Controller
    {
        private static readonly object _fileLock = new object();

        [HttpGet]
        public async Task<ActionResult> FindAll() {
            var actors = await actorService.GetActors();

            List<ActorDTO> actorsMapped = new List<ActorDTO>();

            foreach (var actor in actors)
            {
                actorsMapped.Add(
                    new ActorDTO 
                    { 
                        ActorId = actor.ActorId,
                        Name = actor.Name,
                        DateOfBirth = actor.DateOfBirth,
                        Location = actor.Location,
                        Nationality = actor.Nationality,
                        Bio = actor.Bio,
                        Picture = new ImageDTO
                        {
                            Base64 = getBase64(actor.Picture?.ImagePath ?? null)
                        },
                    });
            }
            return base.Ok(actorsMapped);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            var actor = await actorService.FindActor(id);

            if (actor == null)
            {
                return base.NotFound();
            }

            ActorDTO actorMapped = new ActorDTO
            {
                ActorId = actor.ActorId,
                Name = actor.Name,
                DateOfBirth = actor.DateOfBirth,
                Location = actor.Location,
                Nationality = actor.Nationality,
                Bio = actor.Bio,
                Picture = new ImageDTO
                {
                    ImagePath = actor.Picture?.ImagePath ?? null,
                    Base64 = getBase64(actor.Picture?.ImagePath ?? null)
                },
                ActorMovies = actor.ActorMovies.Select(am => new ActorMovieDTO
                {
                    MovieId = am.MovieId,
                    Name = am.Movie.Name,
                    Picture = new ImageDTO
                    {
                        ImagePath = am.Movie.Picture?.ImagePath ?? null,
                        Base64 = getBase64(am.Movie.Picture?.ImagePath ?? null)
                    }

                }).ToList(),
            };

            return base.Ok(actorMapped);
    }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] ActorFormDTO actor)
        {
            if (!DateTimeOffset.TryParse(actor.DateOfBirth, out var dateTimeOffset))
            {
                return BadRequest("Invalid date format.");
            }

            Actor newActor = new Actor()
            {
                Name = actor.Name != null ? HtmlEncoder.Default.Encode(actor.Name) : null,
                DateOfBirth = DateOnly.FromDateTime(dateTimeOffset.DateTime),
                Bio = HtmlEncoder.Default.Encode(actor.Bio),
                Location = HtmlEncoder.Default.Encode(actor.Location),
                Nationality = HtmlEncoder.Default.Encode(actor.Nationality),
            };

            if (actor.Picture != null)
            {
                var fileName = generateFileName(actor.Picture);
                newActor.Picture = new Image
                {
                    ImagePath = fileName,
                    
                };
                
                savePicture(actor.Picture, fileName);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await actorService.Create(newActor);
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
        public async Task<ActionResult> Put(int id, [FromForm] ActorFormDTO actorFormData)
        {
            if (!DateTimeOffset.TryParse(actorFormData.DateOfBirth, out var dateTimeOffset))
            {
                return BadRequest("Invalid date format.");
            }
            var actor = await actorService.FindActor(id);

            if (actor != null) 
            {
                actor.Name = HtmlEncoder.Default.Encode(actorFormData.Name);
                actor.DateOfBirth = DateOnly.FromDateTime(dateTimeOffset.DateTime);
                actor.Bio = HtmlEncoder.Default.Encode(actorFormData.Bio);
                actor.Location = HtmlEncoder.Default.Encode(actorFormData.Location);
                actor.Nationality = HtmlEncoder.Default.Encode(actorFormData.Nationality);
                actor.ActorId = id;

                if (
                    actorFormData.Picture != null &&
                    actor.Picture?.ImagePath != actorFormData.Picture?.FileName
                    )
                {
                    var fileName = generateFileName(actorFormData.Picture);
                    actor.Picture = new Image
                    {
                        ImagePath = fileName,

                    };
                    savePicture(actorFormData.Picture, fileName);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await actorService.Update(actor);
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
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var actor = await actorService.FindActor(id);

            if (actor == null)
            {
                return base.NotFound();
            }

            await actorService.Remove(id);
            return base.Ok();
        }

        private void savePicture(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            if (!Directory.Exists("images"))
            {
                Directory.CreateDirectory("images");
            }

            lock (_fileLock)
            {
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyToAsync(stream).GetAwaiter().GetResult();
                    }
                }
            }
        }

        private string generateFileName(IFormFile file)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var fileName = $"{timestamp}_{file.FileName}";

            return fileName;
        }

        private string getBase64(string? path)
        {
            string filePath = "";
            if (path != null)
            {
                filePath = Path.Combine(env.WebRootPath, "images", path);
            }

            if (!System.IO.File.Exists(filePath) || string.IsNullOrEmpty(filePath))
            {
                filePath = Path.Combine(env.WebRootPath, "images", "dummy-person.jpg");
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }
    }
}
