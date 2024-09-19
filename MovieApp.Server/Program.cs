using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Models;
using MovieApp.Server.Repositories;
using MovieApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});
    

builder.Services.AddDbContext<MovieAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("movieapp")));
builder.Services.AddTransient<IActorService, ActorService>();
builder.Services.AddScoped<IActorRepository, SQLActorRepository>();
builder.Services.AddTransient<IDirectorService, DirectorService>();
builder.Services.AddScoped<IDirectorRepository, SQLDirectorRepository>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, SQLMovieRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseRouting();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToFile("/index.html");

app.Run();


