using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Server.Models;
using MovieApp.Server.Services;
namespace MovieApp.Server.Repositories.Seeding
{
    public class MovieSeeding : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder
                .Property(a => a.Aangepast).HasColumnType("timestamp");

            builder
                .Property(a => a.Aangepast)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();

            var americanbeauty = new Movie
            {
                MovieId = 1,
                Name = "American Beauty",
                ReleaseDate = new DateOnly(2000, 2, 2),
                Description = "A sexually frustrated suburban father has a mid-life crisis after becoming infatuated with his daughter's best friend.\r\n\r\n",
                Picture = "american_beauty.jpg",
                DirectorId = 1,
                Genres = new List<Genre>() { Genre.Drama, Genre.Romance, Genre.Comedy },
                
            };

            var bladerunner = new Movie
            {
                MovieId = 2,
                Name = "Blade Runner 2049",
                ReleaseDate = new DateOnly(2017, 10, 4),
                Description = "Young Blade Runner K's discovery of a long-buried secret leads him to track down former Blade Runner Rick Deckard, who's been missing for thirty years.\r\n\r\n",
                Picture = "blade_runner.jpg",
                DirectorId = 2,
                Genres = new List<Genre>() { Genre.Scifi, Genre.Action},
                
            };

            var hereditary = new Movie
            {
                MovieId = 3,
                Name = "Hereditary",
                ReleaseDate = new DateOnly(2018, 6, 8),
                Description = "A grieving family is haunted by tragic and disturbing occurrences.\r\n\r\n",
                Picture = "hereditary.jpg",
                DirectorId = 3,
                Genres = new List<Genre>() { Genre.Horror },
               
            };


            builder.HasData(americanbeauty, bladerunner, hereditary);
        }
    }
}
