using MovieApp.Server.Models;

namespace MovieApp.Server.DTOs
{
    public class ActorMovieDTO
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public ImageDTO Picture { get; set; }
    }
}
