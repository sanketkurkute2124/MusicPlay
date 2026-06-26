using Microsoft.EntityFrameworkCore;
using MusicApi.Data;

namespace MusicApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register Controllers
            builder.Services.AddControllers();

            // PostgreSQL
            builder.Services.AddDbContext<MusicDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReact", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseCors("AllowReact");

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            // Uncomment these only if you add authentication later
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}