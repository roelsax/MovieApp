using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieApp.Server.Repositories;
using MovieApp.Server.Repositories.Configuration;
using MovieApp.Server.Repositories.Seeding;
using MovieApp.Server.Services;

namespace MovieApp.Server.Models
{
    public class MovieAppContext : DbContext
    {
        // ------------------
        // Instance Variables
        // ------------------
        public static IConfigurationRoot configuration = null!;
        bool testMode = false;
        
        const string ConnectionString = "movieapp";
        public DbSet<Movie> Movies { get; set;}
        public DbSet<Director> Directors { get; set;}
        public DbSet<Actor> Actors { get; set;}
        public DbSet<ActorMovie> ActorMovies { get; set;}
        public DbSet<Image> Images { get; set;}
        
        public MovieAppContext() { }
        public MovieAppContext(DbContextOptions<MovieAppContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Zoek de naam in de connectionStrings section - appsettings.json
                configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
                var connectionString = configuration.GetConnectionString(ConnectionString);
                if (connectionString != null) // Indien de naam is gevonden
                {
                    optionsBuilder
                        .UseLazyLoadingProxies()
                        .UseSqlServer(
                        connectionString
                        , options => options.MaxBatchSize(150));
                }
            }
            else
            {
                testMode = true;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DirectorSeeding());
            modelBuilder.ApplyConfiguration(new ActorSeeding());
            
            modelBuilder.ApplyConfiguration(new MovieSeeding());
            modelBuilder.ApplyConfiguration(new ActorMovieConfiguration());
            modelBuilder.ApplyConfiguration(new ImageSeeding());

            base.OnModelCreating(modelBuilder);
        }
    }
}
