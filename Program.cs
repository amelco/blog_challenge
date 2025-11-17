
using blog.Entities;
using blog.Interfaces;
using blog.Repositories;
using Microsoft.EntityFrameworkCore;

namespace blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? "Data Source=blog.db";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            builder.Services.AddScoped<IBasicRepository<Comment>, BasicRepository<Comment>>();
            builder.Services.AddScoped<IBasicRepository<BlogPost>, BasicRepository<BlogPost>>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.CreateTables();

            app.Run();
        }
    }
}
