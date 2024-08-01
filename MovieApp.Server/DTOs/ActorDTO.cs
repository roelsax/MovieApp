using MovieApp.Server.Models;

namespace MovieApp.Server.DTOs
{
    public class ActorDTO
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Location { get; set; }
        public string? Nationality { get; set; }
        public string? Bio { get; set; }
        public ImageDTO? Picture { get; set; }
        public List<ActorMovieDTO> ActorMovies { get; set; } = new List<ActorMovieDTO>();

    }
}
