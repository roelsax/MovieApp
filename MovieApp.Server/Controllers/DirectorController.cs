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
                            Base64 = getBase64(director.Picture?.ImagePath ?? null)
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
                    ImagePath = director.Picture?.ImagePath ?? null,
                    Base64 = getBase64(director.Picture?.ImagePath ?? null)
                },
                Movies = director.Movies.Select(m => new MovieDTO
                {
                    MovieId = m.MovieId,
                    Name = m.Name,
                    Picture = new ImageDTO
                    {
                        ImagePath = m.Picture?.ImagePath ?? null,
                        Base64 = getBase64(m.Picture?.ImagePath ?? null)
                    }
                }).ToList()
            };

            return base.Ok(directorMapped);
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] DirectorFormDTO director)
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
                var fileName = generateFileName(director.Picture);

                newDirector.Picture = new Image
                {
                    ImagePath = fileName,
                };
                savePicture(director.Picture, fileName);
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
        public async Task<ActionResult> Put(int id, [FromForm] DirectorFormDTO directorFormData)
        {
            if (!DateTimeOffset.TryParse(directorFormData.DateOfBirth, out var dateTimeOffset))
            {
                return BadRequest("Invalid date format.");
            }

            var director = await directorService.FindDirector(id);

            if (director != null)
            {
                director.Name = directorFormData.Name;
                director.DateOfBirth = DateOnly.FromDateTime(dateTimeOffset.DateTime);
                director.Bio = directorFormData.Bio;
                director.Location = directorFormData.Location;
                director.Nationality = directorFormData.Nationality;
                director.DirectorId = id;

                if (
                    directorFormData.Picture != null &&
                    director.Picture?.ImagePath != directorFormData.Picture?.FileName
                    )
                {
                    var fileName = generateFileName(directorFormData.Picture);
                    director.Picture = new Image
                    {
                        ImagePath = fileName,

                    };
                    savePicture(directorFormData.Picture, fileName);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
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

        private async void savePicture(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            if (!Directory.Exists("images"))
            {
                Directory.CreateDirectory("images");
            }

            if (!System.IO.File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
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
