using Microsoft.EntityFrameworkCore;

namespace blog
{
    public static class AppExtensions
    {
        public static void CreateTables(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated();

                dbContext.Database.ExecuteSqlRaw(@"
                    CREATE TABLE IF NOT EXISTS BlogPosts (
                        Id      INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title   TEXT    NOT NULL,
                        Content TEXT    NOT NULL
                    );
                ");

                dbContext.Database.ExecuteSqlRaw(@"
                    CREATE TABLE IF NOT EXISTS Comments (
                        Id          INTEGER PRIMARY KEY AUTOINCREMENT,
                        BlogPostId  INTEGER NOT NULL,
                        Content     TEXT    NOT NULL,
                        FOREIGN KEY (BlogPostId) REFERENCES BlogPosts(Id)
                    );
                ");
            }
        }
    }
}
