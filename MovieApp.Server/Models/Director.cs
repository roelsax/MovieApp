using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Server.Models
{
    public class Director
    {
        public int DirectorId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateOnly DateOfBirth { get; set; }
        public string? Location { get; set; }
        public string? Nationality { get; set; }
        public string? Bio { get; set; }
        public Image? Picture { get; set; }
        public int? PictureId { get; set; }

        [Timestamp]
        public byte[]? Aangepast { get; set; }
        public List<Movie> Movies { get; set; } = null!;
    }
}
