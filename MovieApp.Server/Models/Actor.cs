using System.ComponentModel.DataAnnotations;

namespace MovieApp.Server.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Location { get; set; }
        public string? Nationality { get; set; }
        public string? Bio { get; set; }
        public Image? Picture { get; set; }
        public int? PictureId { get; set; }

        [Timestamp]
        public byte[]? Aangepast { get; set; }
        public ICollection<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();

    }
}
