using blog.Entities;
using blog.Interfaces;
using blog.Repositories;
using Microsoft.EntityFrameworkCore;

namespace blog
{
    public static class BuilderExtensions
    {
        public static void AddCustomServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? "Data Source=blog.db";
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));


            builder.Services.AddScoped<IBasicRepository<Comment>, BasicRepository<Comment>>();
            builder.Services.AddScoped<IBasicRepository<BlogPost>, BasicRepository<BlogPost>>();
        }
    }
}
