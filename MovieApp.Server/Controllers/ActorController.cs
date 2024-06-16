using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Services;
using MovieApp.Server.Models;
using Microsoft.EntityFrameworkCore;

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

        [HttpPost]
        public async Task<ActionResult> Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await actorService.Create(actor);
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
    }
}
