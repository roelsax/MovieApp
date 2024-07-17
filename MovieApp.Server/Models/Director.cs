using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Server.Models
{
    public class Director
    {
        public int DirectorId { get; set; }
        public string Name { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Location { get; set; }
        public string? Nationality { get; set; }
        public string? Bio { get; set; }
        public string? Picture { get; set; }

        [Timestamp]
        public byte[]? Aangepast { get; set; }
        public List<Movie> Movies { get; set; } = null!;
    }
}
