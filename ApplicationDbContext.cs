using blog.Entities;
using Microsoft.EntityFrameworkCore;

namespace blog
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) {}

        public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
        public DbSet<Comment> Comments => Set<Comment>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BlogPost>(entity => {
                entity.HasKey(bp => bp.Id);
                entity.Property(bp => bp.Id)
                    .ValueGeneratedOnAdd();

                entity.Property(bp => bp.Title)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(bp => bp.Content)
                    .IsRequired();

                entity.HasMany(bp => bp.Comments)
                      .WithOne()
                      .HasForeignKey(c => c.BlogPostId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Comment>(entity => {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(c => c.Content)
                    .IsRequired()
                    .HasMaxLength(2048);
            });
        }
    }
}
