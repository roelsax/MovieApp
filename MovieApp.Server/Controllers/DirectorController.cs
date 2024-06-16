using Microsoft.AspNetCore.Mvc;
using MovieApp.Server.Services;
using MovieApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MovieApp.Server.Controllers
{
    [Route("directors")]
    [ApiController]
    public class DirectorController(DirectorService directorService) : Controller
    {
        [HttpGet]
        public async Task<ActionResult> FindAll() => base.Ok(await directorService.GetDirectors());

        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id) => await directorService.FindDirector(id) is Director director ? base.Ok(director) : base.NotFound();

        [HttpPost]
        public async Task<ActionResult> Create(Director director)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await directorService.Create(director);
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

        [HttpDelete("{id}")]
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
    }
}
