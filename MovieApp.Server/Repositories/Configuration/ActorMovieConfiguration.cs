using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;

namespace MovieApp.Server.Repositories.Configuration
{
    public class ActorMovieConfiguration : IEntityTypeConfiguration<ActorMovie>
    {
        public void Configure(EntityTypeBuilder<ActorMovie> builder)
        {
            builder.HasKey(am => new { am.ActorId, am.MovieId });

            builder
                .HasOne(am => am.Actor)
                .WithMany(a => a.ActorMovies)
                .HasForeignKey(am => am.ActorId);

            builder
                .HasOne(am => am.Movie)
                .WithMany(m => m.ActorMovies)
                .HasForeignKey(am => am.MovieId);

            builder.HasData(
                new ActorMovie { ActorId = 1, MovieId = 1 }, 
                new ActorMovie { ActorId = 2, MovieId = 1 }, 
                new ActorMovie { ActorId = 3, MovieId = 1 }, 
                new ActorMovie { ActorId = 4, MovieId = 2 }, 
                new ActorMovie { ActorId = 5, MovieId = 2 },
                new ActorMovie { ActorId = 6, MovieId = 2 },
                new ActorMovie { ActorId = 7, MovieId = 3 },
                new ActorMovie { ActorId = 8, MovieId = 3 },
                new ActorMovie { ActorId = 9, MovieId = 3 }
            );
        }
    }
}
