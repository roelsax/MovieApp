using MovieApp.Server.Repositories.Seeding;
using MovieApp.Server.Models;
using System.Text.Json.Serialization;
namespace MovieApp.Server.DTOs
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public Director Director { get; set; }
        public List<string> Genres { get; set; }
        public List<ActorMovieDTO> Actors { get; set; }
        
    }
}
