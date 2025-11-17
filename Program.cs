
using blog.Entities;
using blog.Interfaces;
using blog.Repositories;
using Microsoft.EntityFrameworkCore;

namespace blog
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? "Data Source=blog.db";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            builder.Services.AddScoped<IBasicRepository<Comment>, BasicRepository<Comment>>();
            builder.Services.AddScoped<IBasicRepository<BlogPost>, BasicRepository<BlogPost>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();

                await dbContext.Database.ExecuteSqlRawAsync(@"
                    CREATE TABLE IF NOT EXISTS BlogPosts (
                        Id      INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title   TEXT    NOT NULL,
                        Content TEXT    NOT NULL
                    );
                ");

                await dbContext.Database.ExecuteSqlRawAsync(@"
                    CREATE TABLE IF NOT EXISTS Comments (
                        Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                        BlogPostId  INTEGER NOT NULL,
                        Content     TEXT    NOT NULL,
                        FOREIGN KEY (BlogPostId) REFERENCES BlogPosts(Id)
                    );
                ");
            }

            app.Run();
        }
    }
}
