﻿using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Services;
using MovieApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using MovieApp.Server.DTOs;
using System.IO;
using System.Text;

namespace MovieApp.Server.Controllers
{
    [Route("actors")]
    [ApiController]
    public class ActorController(ActorService actorService, IWebHostEnvironment env) : Controller
    {
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
                            Base64 = getBase64(actor.Picture?.ImagePath)
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
                    ImagePath = actor.Picture.ImagePath,
                    Base64 = getBase64(actor.Picture.ImagePath)
                },
                ActorMovies = actor.ActorMovies.Select(am => new ActorMovieDTO
                {
                    MovieId = am.MovieId,
                    Name = am.Movie.Name,
                    Picture = new ImageDTO
                    {
                        ImagePath = am.Movie.Picture.ImagePath,
                        Base64 = getBase64(am.Movie.Picture.ImagePath)
                    }

                }).ToList(),
            };

            return base.Ok(actorMapped);
    }

        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] ActorCreateDTO actor)
        {
            if (!DateTimeOffset.TryParse(actor.DateOfBirth, out var dateTimeOffset))
            {
                return BadRequest("Invalid date format.");
            }

            Actor newActor = new Actor()
            {
                Name = actor.Name,
                DateOfBirth = DateOnly.FromDateTime(dateTimeOffset.DateTime),
                Bio = actor.Bio,
                Location = actor.Location,
                Nationality = actor.Nationality,
            };

            if (actor.Picture != null)
            {
                newActor.Picture = new Image
                {
                    ImagePath = actor.Picture.FileName,
                    
                };
                savePicture(actor.Picture);
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
        public async Task<ActionResult> Put(int id, Actor actor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    actor.ActorId = id;
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
