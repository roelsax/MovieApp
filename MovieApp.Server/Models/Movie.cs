﻿using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MovieApp.Server.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateOnly ReleaseDate { get; set; }
        public string? Description { get; set; }
        public Image? Picture { get; set; }
        public int? PictureId { get; set; }
        public int? DirectorId { get; set; }

        [Timestamp]
        public byte[]? Aangepast { get; set; }
        public Director? Director { get; set; }
        public ICollection<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();
        [Required]
        public List<Genre> Genres { get; set; } = null!;
    }
}
