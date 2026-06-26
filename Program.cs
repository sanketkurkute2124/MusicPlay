
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;

namespace MusicApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //  builder.Services.AddControllers();
            //builder.Services.AddDbContext<MusicDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddDbContext<MusicDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReact",
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:3000")
                              .AllowAnyMethod()
                              .AllowAnyHeader();
                    });
            });


            var app = builder.Build();

            app.UseCors("AllowReact");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
