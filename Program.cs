
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
            
            builder.AddCustomServices();

            var app = builder.Build();

            app.AddCustomMiddleware();

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
