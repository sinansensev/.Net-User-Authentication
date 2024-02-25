using Microsoft.EntityFrameworkCore;
using PayosferIdentity.Models;

namespace PayosferIdentity.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ProjectRequest> ProjectRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(255);

            modelBuilder.Entity<ProjectRequest>()
                .HasKey(pr => pr.Id);

            
            modelBuilder.Entity<ProjectRequest>()
                .Property(pr => pr.CreateTime)
                .IsRequired();

            modelBuilder.Entity<ProjectRequest>()
                .Property(pr => pr.PdfContent)
                .IsRequired(false); 
        }
    }
}
