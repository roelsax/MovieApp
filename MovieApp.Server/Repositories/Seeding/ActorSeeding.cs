using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApp.Server.Models;

namespace MovieApp.Server.Repositories.Seeding
{
    public class ActorSeeding : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder
                .Property(a => a.Aangepast).HasColumnType("timestamp");
            
            builder
                .Property(a => a.Aangepast)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();

            builder.HasData(
                ActorData.GetActors()
                );
        }
    }
}
