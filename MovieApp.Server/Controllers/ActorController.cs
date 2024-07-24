using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Services;
using MovieApp.Server.Models;
using Microsoft.EntityFrameworkCore;
using MovieApp.Server.DTOs;
using System.IO;

namespace MovieApp.Server.Controllers
{
    [Route("actors")]
    [ApiController]
    public class ActorController(ActorService actorService) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> FindAll() => base.Ok(await actorService.GetActors());

        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id) => await actorService.FindActor(id) is Actor actor ? base.Ok(actor) : base.NotFound();

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
                newActor.Picture = actor.Picture.FileName;
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

        [HttpDelete("{id}")]
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
    }
}
