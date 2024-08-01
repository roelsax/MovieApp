using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Services;
using MovieApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using MovieApp.Server.DTOs;

namespace MovieApp.Server.Controllers
{
    [Route("directors")]
    [ApiController]
    public class DirectorController(DirectorService directorService, IWebHostEnvironment env) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> FindAll() 
        {
            var directors = await directorService.GetDirectors();

            List<DirectorDTO> directorsMapped = new List<DirectorDTO>();

            foreach (var director in directors) {
                directorsMapped.Add(
                    new DirectorDTO {
                        DirectorId = director.DirectorId,
                        Name = director.Name,
                        DateOfBirth = director.DateOfBirth,
                        Location = director.Location,
                        Nationality = director.Nationality,
                        Bio = director.Bio,
                        Picture = new ImageDTO 
                        { 
                            Base64 = getBase64(director.Picture?.ImagePath)
                        }
                    });
            }
            return base.Ok(directorsMapped);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id) 
        {
            var director = await directorService.FindDirector(id);

            if (director == null) 
            {
                return base.NotFound();
            }

            DirectorDTO directorMapped = new DirectorDTO 
            {
                DirectorId = director.DirectorId,
                Name = director.Name,
                DateOfBirth = director.DateOfBirth,
                Location = director.Location,
                Nationality = director.Nationality,
                Bio = director.Bio,
                Picture = new ImageDTO
                {
                    ImagePath = director.Picture.ImagePath,
                    Base64 = getBase64(director.Picture.ImagePath)
                },
                Movies = director.Movies.Select(m => new MovieDTO
                {
                    MovieId = m.MovieId,
                    Name = m.Name,
                    Picture = new ImageDTO
                    {
                        ImagePath = m.Picture.ImagePath,
                        Base64 = getBase64(m.Picture.ImagePath)
                    }
                }).ToList()
            };

            return base.Ok(directorMapped);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] DirectorCreateDto director)
        {
            if (!DateTimeOffset.TryParse(director.DateOfBirth, out var dateTimeOffset))
            {
                return BadRequest("Invalid date format.");
            }

            Director newDirector = new Director()
            {
                Name = director.Name,
                DateOfBirth = DateOnly.FromDateTime(dateTimeOffset.DateTime),
                Bio = director.Bio,
                Location = director.Location,
                Nationality = director.Nationality,
            };

            if (director.Picture != null)
            {
                newDirector.Picture = new Image
                {
                    ImagePath = director.Picture.FileName,
                };
                savePicture(director.Picture);
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    await directorService.Create(newDirector);
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
        public async Task<ActionResult> Put(int id, Director director)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    director.DirectorId = id;
                    await directorService.Update(director);
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
            var director = await directorService.FindDirector(id);

            if (director == null)
            {
                return base.NotFound();
            }

            await directorService.Remove(id);
            return base.Ok();
        }

        private async void savePicture(IFormFile file)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);

            if (!Directory.Exists("images"))
            {
                Directory.CreateDirectory("images");
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        private string getBase64(string? path)
        {
            string filePath = "";
            if (path != null)
            {
                filePath = Path.Combine(env.WebRootPath, "images", path);
            }

            if (!System.IO.File.Exists(filePath) || string.IsNullOrEmpty(filePath) )
            {
                filePath = Path.Combine(env.WebRootPath, "images", "dummy-person.jpg");
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(imageBytes);

            return base64String;
        }
    }
}
